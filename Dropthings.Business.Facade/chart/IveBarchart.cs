using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Dropthings.Business.Facade
{
    public static class IveBarchart
    {
        private static float LeftgapX = 0f;
        private static float RightgapX = 50f;
        private static float gapY = 2f;
        private static float titleHeight = 35f;
        private static float legendX = 35f;
        private static float legendY = 20f;
        private static int ImgWidth = 800;
        private static int ImgHeight = 200;
        private static float XSapn = 0f;
        private static Random rd = new Random();
        private static int Barcolortype = 1;
        //private static float YMarkSpan = 25f;       

        public static string DrawChart(string title, string subTitle, int l_imgWidth, Dictionary<string, int> dict, int colortype)
        {
            Barcolortype = colortype;
            string OutPutPath = "~/Admin/dataImg/";
            string fileName = "IveBar_" + rd.Next(0, 50) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".gif";
            string strAbsolutePath = System.Web.HttpContext.Current.Server.MapPath(OutPutPath) + fileName;            
            IList<int> data = new List<int>();
            IList<string> YData = new List<string>();
            int count = 0;
            foreach (string key in dict.Keys)
            {
                data.Add(dict[key]);
                YData.Add(key);
                count++;
            }
            ImgWidth = l_imgWidth;
            ImgHeight = Convert.ToInt32(gapY * 2 + titleHeight + legendY + GetAxisXHeight(data));
            Bitmap bm = new Bitmap(ImgWidth, ImgHeight);
            Graphics g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            InnitLegendX(YData, g);
            DrawTitle(g, title);
            DrawSubTitle(g, subTitle);
            DrawAxisX(g, data);
            DrawAxisY(g, data, YData);
            bm.Save(strAbsolutePath);
            bm.Dispose();
            g.Dispose();
            return fileName;
        }

        private static void DrawSubTitle(Graphics g, string subTitle)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(subTitle, new Font("Arial", 9));
            RightgapX = stringSize.Width + 10f;
            g.DrawString(subTitle, new Font("Arial", 9), Brushes.Black, ImgWidth - RightgapX, ImgHeight - gapY - legendY - stringSize.Height / 2f);
        }

        private static void DrawTitle(Graphics g, string title)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(title, new Font("Arial", 16));
            float x = ImgWidth / 2 - stringSize.Width / 2;
            g.DrawString(title, new Font("Arial", 16), Brushes.Black, x, stringSize.Height / 2f);
            //titleHeight = stringSize.Height + 10f;
        }

        private static float GetCueWidth(string cue, int fontsize, Graphics g)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(cue, new Font("Arial", fontsize));
            return stringSize.Width;
        }

        private static void InnitLegendX(IList<string> data, Graphics g)
        {
            float size = 0f;
            foreach (string key in data)
            {
                SizeF stringSize = new SizeF();
                stringSize = g.MeasureString(key, new Font("Arial", 12));
                if (stringSize.Width > size)
                {
                    size = stringSize.Width;
                }
            }
            legendX = size;
        }

        private static float GetAxisXHeight(IList<int> data)
        {
            int count = data.Count;
            float XHeight = count * 30f;
            return XHeight;
        }

        private static void DrawAxisY(Graphics g, IList<int> data, IList<string> Ydata)
        {
            g.DrawLine(Pens.Black, LeftgapX + legendX, gapY + titleHeight, LeftgapX + legendX, gapY + titleHeight + GetAxisXHeight(data));
            int cuecount = 0;
            foreach (string key in Ydata)
            {
                g.DrawString(key, new Font("Arial", 9), Brushes.Black, LeftgapX + legendX - GetCueWidth(key, 9, g), titleHeight + gapY + cuecount * 30f + 5f);
                cuecount++;
            }
            int maxNum = GetAxisMaxX(data);
            int markSpan = maxNum / 6;
            DrawBar(g, data, markSpan, 30f);
        }

        private static void DrawBar(Graphics g, IList<int> data, int markSpan, float markHeight)
        {
            int dataNum = 0;
            foreach (int num in data)
            {
                float CueHeight = 20f;
                float CueWidth = num * 1f / markSpan * XSapn;
                g.FillRectangle(GetBarBrush(Barcolortype), legendX + LeftgapX + 0.5f, titleHeight + gapY + dataNum * 30f + 5f, CueWidth, CueHeight);
                g.DrawString(num.ToString(), new Font("Arial", 8), Brushes.Black, legendX + LeftgapX + CueWidth, titleHeight + gapY + dataNum * 30f + 5f);
                dataNum++;
            }
        }

        private static void DrawAxisX(Graphics g, IList<int> data)
        {
            g.DrawLine(Pens.Black, LeftgapX + legendX, ImgHeight - gapY - legendY, ImgWidth - RightgapX, ImgHeight - gapY - legendY);
            int maxNum = GetAxisMaxX(data);
            int markSpan = maxNum / 6;
            float markWidth = (ImgWidth - LeftgapX - RightgapX - legendX) / 6;
            int marknum = 0;
            XSapn = markWidth;
            for (int i = 0; i <= maxNum; i = i + markSpan)
            {
                g.DrawString(i.ToString(), new Font("Arial", 8), Brushes.Black, LeftgapX + legendX + marknum * markWidth - GetStrWidth(i.ToString(), 8, g) / 2f, ImgHeight - gapY - legendY + 8f);
                g.DrawLine(Pens.Black, LeftgapX + legendX + marknum * markWidth, ImgHeight - gapY - legendY - 2f, LeftgapX + legendX + marknum * markWidth, ImgHeight - gapY - legendY);
                marknum++;
            }
        }

        private static float GetStrWidth(string str, int fontsize, Graphics g)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(str, new Font("Arial", fontsize));
            return stringSize.Width;
        }

        private static int GetAxisMaxX(IList<int> data)
        {
            int maxnum = 0;
            foreach (int num in data)
            {
                if (maxnum < num)
                {
                    maxnum = num;
                }
            }
            if (maxnum < 6)
            {
                maxnum = 6;
            }
            else
            {
                int remNum = maxnum % 6;
                int basespannum = Convert.ToInt32(maxnum * 0.05);
                int remspanmum = basespannum % 6;
                int spannum = remspanmum == 0 ? basespannum : basespannum - remspanmum;
                maxnum = remNum == 0 ? maxnum + 6 : maxnum - remNum + 6 + spannum;
            }
            return maxnum;
        }

        private static Brush GetBarBrush(int type)
        {
            switch (type)
            {
                case 0:
                    return Brushes.SkyBlue;
                case 1:
                    return Brushes.Red;
                case 2:
                    return Brushes.Orange;
                default:
                    return Brushes.Black;
            }
        }
    }
}
