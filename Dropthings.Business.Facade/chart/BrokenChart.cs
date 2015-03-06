using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Dropthings.Business.Facade
{
	public static class BrokenChart
	{
        private static float LeftgapX = 0f;
        private static float RightgapX = 0f;
        private static float gapY = 0f;
        private static float titleHeight = 20f;
        private static float legendX = 35f;
        private static float legendY = 20f;
        private static int ImgWidth = 800;
        private static int ImgHeight = 200;
        private static float YSapn = 0f;
        private static Random rd = new Random();

        public static string DrawChart(string title, string XSubTitle, string YSubTitle, int l_imgWidth, int l_imgHeight, Dictionary<string, int> dict)
        {
            string OutPutPath = "~/Admin/dataImg/";
            string fileName = "Broken_" + rd.Next(0, 50) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".gif";
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
            LeftgapX = 25f;
            gapY = ImgHeight * 0.1f;
            ImgWidth = l_imgWidth;
            ImgHeight = l_imgHeight;
            Bitmap bm = new Bitmap(ImgWidth, ImgHeight);
            Graphics g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            InnitLegendX(data, g);
            DrawTitle(g, title);
            DrawXSubTitle(g, XSubTitle);
            DrawYSubTitle(g, YSubTitle);
            DrawAxisY(g, YData);
            DrawAxisX(g, data);
            bm.Save(strAbsolutePath, ImageFormat.Gif);            
            bm.Dispose();
            g.Dispose();
            return fileName;
        }



        private static void InnitLegendX(IList<int> data, Graphics g)
        {
            int maxnum = GetAxisMaxX(data);
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(maxnum.ToString(), new Font("Arial", 8));
            legendX = stringSize.Width + 2f;
        }
        private static void DrawXSubTitle(Graphics g, string XSubTitle)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(XSubTitle, new Font("Arial", 9));
            g.DrawString(XSubTitle, new Font("Arial", 9), Brushes.Black, LeftgapX + legendX - stringSize.Width / 2, gapY + titleHeight - stringSize.Height - 3f);
        }

        private static void DrawYSubTitle(Graphics g, string YSubTitle)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(YSubTitle, new Font("Arial", 9));
            RightgapX = stringSize.Width + 10f;
            g.DrawString(YSubTitle, new Font("Arial", 9), Brushes.Black, ImgWidth - RightgapX, ImgHeight - gapY - legendY);
        }

        private static void DrawTitle(Graphics g, string title)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(title, new Font("Arial", 16));
            float x = ImgWidth / 2 - stringSize.Width / 2;
            g.DrawString(title, new Font("Arial", 16), Brushes.Black, x, 10f);
        }

        private static void DrawAxisX(Graphics g, IList<int> data)
        {
            g.DrawLine(Pens.Black, LeftgapX + legendX, gapY + titleHeight, LeftgapX + legendX, ImgHeight - gapY - legendY);
            int maxNum = GetAxisMaxX(data);
            int markSpan = maxNum / 6;
            float markHeight = (ImgHeight - gapY * 2 - titleHeight - legendY) / 6;
            int marknum = 0;
            for (int i = 0; i <= maxNum; i = i + markSpan)
            {
                g.DrawString(i.ToString(), new Font("Arial", 8), Brushes.Black, LeftgapX + legendX - GetStrWidth(i.ToString(), 8, g) - 2f, ImgHeight - gapY - legendY - marknum * markHeight - 5f);
                g.DrawLine(Pens.Black, LeftgapX + legendX, ImgHeight - gapY - legendY - marknum * markHeight, ImgWidth - RightgapX, ImgHeight - gapY - legendY - marknum * markHeight);
                marknum++;
            }
            DrawBroken(g, data, markSpan, markHeight);
        }

        private static void DrawBroken(Graphics g, IList<int> data, int markSpan, float markHeight)
        {
            IList<PointF> cuePointList = new List<PointF>();
            int dataNum = 1;
            foreach (int num in data)
            {
                float CueWidth = LeftgapX + legendX + dataNum * YSapn - 2f;
                float CueHeight = ImgHeight - gapY - legendY - num * 1f / markSpan * markHeight - 1f;
                PointF point = new PointF(CueWidth + 1.5f, CueHeight + 1.5f);
                cuePointList.Add(point);
                g.FillPie(Brushes.Green, CueWidth, CueHeight, 3f, 3f, 0f, 361f);
                dataNum++;
            }
            int cueNum = 0;
            int cueCount = cuePointList.Count;
            foreach (PointF point in cuePointList)
            {
                if (cueNum > 0 && cueNum < cueCount)
                {
                    PointF startPoint = cuePointList[cueNum - 1];
                    PointF endPoint = cuePointList[cueNum];
                    g.DrawLine(Pens.OrangeRed, startPoint, endPoint);
                }
                cueNum++;
            }
        }

        private static void DrawAxisY(Graphics g, IList<string> data)
        {
            g.DrawLine(Pens.Black, LeftgapX + legendX, ImgHeight - gapY - legendY, ImgWidth - RightgapX, ImgHeight - gapY - legendY);
            int len = data.Count + 1;
            int avglen = len / 6 + 1;
            int marknum = 1;
            float markWidth = (ImgWidth - LeftgapX - RightgapX - legendX - 4f) / len;
            YSapn = markWidth;
            foreach (string mark in data)
            {
                if (len < 12)
                {
                    g.DrawString(mark, new Font("Arial", 8), Brushes.Black, LeftgapX + legendX + marknum * markWidth - GetStrWidth(mark, 8, g) / 2f, ImgHeight - gapY - legendY + 5f);
                    g.DrawLine(Pens.Black, LeftgapX + legendX + marknum * markWidth, ImgHeight - gapY - legendY, LeftgapX + legendX + marknum * markWidth, ImgHeight - gapY - legendY - 1f);
                }
                else
                {
                    if ((marknum - 1) % avglen == 0)
                    {
                        g.DrawString(mark, new Font("Arial", 8), Brushes.Black, LeftgapX + legendX + marknum * markWidth - GetStrWidth(mark, 8, g) / 2f, ImgHeight - gapY - legendY + 5f);
                        g.DrawLine(Pens.Black, LeftgapX + legendX + marknum * markWidth, ImgHeight - gapY - legendY, LeftgapX + legendX + marknum * markWidth, ImgHeight - gapY - legendY - 4f);
                    }
                    else
                    {
                        g.DrawLine(Pens.Black, LeftgapX + legendX + marknum * markWidth, ImgHeight - gapY - legendY, LeftgapX + legendX + marknum * markWidth, ImgHeight - gapY - legendY - 1f);
                    }
                }
                marknum++;
            }
        }

        private static float GetStrWidth(string str, int fontsize, Graphics g)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(str, new Font("Arial", fontsize));
            float width = stringSize.Width;
            return width;
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
	}
}
