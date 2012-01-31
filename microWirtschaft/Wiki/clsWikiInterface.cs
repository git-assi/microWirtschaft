using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace piratesWirtschaft.Wiki
{
    public class clsWikiInterface
    {
        private WebBrowser web;

        private List<string> m_strErrorList = new List<string>();

        public delegate void statusHandler(string strStatus);
        public event statusHandler onStatus;
        public void RaiseOnStatus(string strStatus)
        {
            if (onStatus != null)
                onStatus(strStatus);
        }

        public clsWikiInterface(bool blnBrowserForm)
        {
            if (blnBrowserForm)
            {
                frmBrowser browser = new frmBrowser();
                web = browser.webBrowser1;
                browser.Show();
            }
                else
                web = new WebBrowser();
            
                web.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(web_DocumentCompleted);
        }

        private bool m_blnWaitForLogin = false;

        private string m_strAktUrl
        {
            get { return web.Url.ToString().ToLower(); }
        }
        private class clsWikiInfo
        {
            public clsWikiInfo(string strSeite, string strText)
            {
                m_strSeite = strSeite;
                m_strText = strText;
            }

            public string m_strSeite;
            public string m_strText;
        }
        private List<clsWikiInfo> m_lisToDo = new List<clsWikiInfo>();

        private clsWikiInfo m_aktInfo
        {
            get
            {
                
                if (m_lisToDo.Count > 0)
                    return m_lisToDo[0];
                else
                    return null;
            }
        }

        private void web_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if (m_strAktUrl.Contains("login"))
                {
                    if (!m_blnWaitForLogin)
                    {
                        RaiseOnStatus("login");

                        //User und PW hier setzen:
                        web.Document.GetElementById("login").SetAttribute("Value", "");
                        web.Document.GetElementById("password").SetAttribute("Value", "");

                        clickFormCommitButton("Log in");
                        m_blnWaitForLogin = true;
                    }
                    else
                    {
                        RaiseOnStatus("WaitForLogin!");
                    }
                }
                else
                {


                    if (m_strAktUrl.EndsWith(m_aktInfo.m_strSeite.ToLower() + "/edit"))
                    {
                        RaiseOnStatus("Wiki Seite Edit");
                        HtmlElement element = web.Document.GetElementById("wiki_page_body");
                        element.InnerHtml = m_aktInfo.m_strText;
                        clickFormCommitButton("Save");
                    }
                    else if (m_strAktUrl.EndsWith("????"))
                    {
                        RaiseOnStatus("Wiki Seite new");
                        HtmlElement element = web.Document.GetElementById("wiki_page_body");
                        element.InnerHtml = m_aktInfo.m_strText;
                        clickFormCommitButton("Save");
                    }
                    else if (m_strAktUrl.EndsWith(m_aktInfo.m_strSeite.ToLower()))
                    {
                        RaiseOnStatus("Wiki Seite done");
                        m_lisToDo.RemoveAt(0);
                        m_blnBusy = false;
                        this.updateWikiPages();
                    }
                    else 
                    {
                        throw new Exception("Wiki Seite Status - unbekannt");
                        
                    }
                }

                
                
            }
            catch (Exception ex)
            {
                onStatus("Error DocComp:" + ex.ToString());
                m_blnBusy = false;
            }
        }

        private void clickFormCommitButton(string strValue)
        {
            //Log in
            HtmlElementCollection elements = web.Document.All;
            foreach (HtmlElement currentElement in elements)
            {
                string str = currentElement.Name.ToString();
                if (str == "commit")
                {

                    if (currentElement.GetAttribute("value") == strValue)
                    {
                        currentElement.InvokeMember("click");
                        break;
                    }
                }
            }
        }

        private bool m_blnBusy = false;
        public void setWikiSeitenText(string strSeite, string strText)
        {
            try
            {
                strSeite = strSeite.Replace(" ", "-");
                strText = strText.Replace(Environment.NewLine, "\n\r");
                strText += " - Autoupdate - " + DateTime.Now.ToString();

                m_lisToDo.Add(new clsWikiInfo(strSeite, strText));

               
                this.updateWikiPages();
                
                    
            }
            catch (Exception ex)
            {
                m_strErrorList.Add(ex.ToString());
            }
        }

        private void updateWikiPages()
        {
            try
            {
                if (!m_blnBusy && this.m_aktInfo != null)
                {
                   m_blnBusy = true;
                   onStatus("Navigate " + m_aktInfo.m_strSeite);
                   web.Navigate("http://www.obsidianportal.com/campaign/lego-legends-of-the-high-seas/wikis/" + m_aktInfo.m_strSeite + "/edit");
                }
            }
            catch (Exception ex)
            {
                onStatus("Error updateWikiPages: " + ex.ToString());
            }
        }

   


    }
}
