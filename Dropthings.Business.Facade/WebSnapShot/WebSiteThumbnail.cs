using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Dropthings.Business.Facade
{
    public class WebSiteThumbnail
    {
        Bitmap m_Bitmap;
        string m_Url;
        //string m_ImgFilePath;
        //int m_BrowserWidth, m_BrowserHeight, m_ThumbnailWidth, m_ThumbnailHeight;
        public WebSiteThumbnail(string Url)
        {
            m_Url = Url;            
            //m_ImgFilePath = ImgFilePath;
        }
        public static Bitmap GetWebSiteThumbnail(string Url)
        {
            WebSiteThumbnail thumbnailGenerator = new WebSiteThumbnail(Url);
            return thumbnailGenerator.GenerateWebSiteThumbnailImage();
        }
        public Bitmap GenerateWebSiteThumbnailImage()
        {
            Thread m_thread = new Thread(new ThreadStart(_GenerateWebSiteThumbnailImage));
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
            m_thread.Join();
            return m_Bitmap;
        }
        private void _GenerateWebSiteThumbnailImage()
        {
            WebBrowser m_WebBrowser = new WebBrowser();
            m_WebBrowser.ScrollBarsEnabled = false;
            m_WebBrowser.Navigate(m_Url);
            m_WebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
            while (m_WebBrowser.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();
            m_WebBrowser.Dispose();
        }
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser m_WebBrowser = (WebBrowser)sender;
            int m_ThumbnailWidth = m_WebBrowser.Document.Body.ScrollRectangle.Width + 20;
            int m_ThumbnailHeight = m_WebBrowser.Document.Body.ScrollRectangle.Height;
            m_WebBrowser.ClientSize = new Size(m_ThumbnailWidth, m_ThumbnailHeight);
            m_WebBrowser.ScrollBarsEnabled = false;
            m_Bitmap = new Bitmap(m_WebBrowser.Bounds.Width, m_WebBrowser.Bounds.Height);
            m_WebBrowser.BringToFront();
            m_WebBrowser.DrawToBitmap(m_Bitmap, m_WebBrowser.Bounds);
            m_Bitmap = (Bitmap)m_Bitmap.GetThumbnailImage(m_ThumbnailWidth, m_ThumbnailHeight, null, IntPtr.Zero);
        }
    }
}
