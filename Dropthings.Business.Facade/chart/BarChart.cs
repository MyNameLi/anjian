using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Dropthings.Business.Facade
{
	public static class BarChart
	{
        private static float gapX = 0f;
        private static float gapY = 0f;
        private static float titleHeight = 20f;
        private static float legendX = 35f;
        private static float legendY = 20f;
        private static int ImgWidth = 800;
        private static int ImgHeight = 200;
        private static float YSapn = 0f;
        private static int BarCount = 0;
        private static float barspan = 1f;
        private static float YMarkSpan = 25f;
        private static Random rd = new Random();

        public static string DrawChart(string title, string subTitle, int l_imgWidth, int l_imgHeight, Dictionary<string, IList<int>> dict, Dictionary<string, int> sublist)
        {
            string OutPutPath = "~/Admin/dataImg/";
            string fileName = "Bar_" + rd.Next(0, 50) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".gif";
            string strAbsolutePath = System.Web.HttpContext.Current.Server.MapPath(OutPutPath) + fileName;            
            IList<IList<int>> data = new List<IList<int>>();
            IList<string> YData = new List<string>();
            int count = 0;
            foreach (string key in dict.Keys)
            {
                if (BarCount == 0)
                {
                    BarCount = dict[key].Count;
                }
                data.Add(dict[key]);
                YData.Add(key);
                count++;
            }
            gapX = 25f;
            gapY = ImgHeight * 0.1f + 5f;
            ImgWidth = l_imgWidth;
            ImgHeight = l_imgHeight;
            Bitmap bm = new Bitmap(ImgWidth, ImgHeight);
            Graphics g = Graphics.FromImage(bm);
            InnitLegendX(data, g);
            g.Clear(Color.White);
            DrawTitle(g, title);
            DrawSubList(g, sublist);
            DrawSubTitle(g, subTitle);
            DrawAxisY(g, YData, data);
            DrawAxisX(g, data);
            bm.Save(strAbsolutePath);
            bm.Dispose();
            g.Dispose();
            return fileName;
        }
        private static void InnitLegendX(IList<IList<int>> data, Graphics g)
        {
            int maxnum = GetAxisMaxX(data);
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(maxnum.ToString(), new Font("Arial", 8));
            legendX = stringSize.Width + 2f;
        }

        private static void DrawTitle(Graphics g, string title)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(title, new Font("Arial", 16));
            float x = ImgWidth / 2 - stringSize.Width / 2;
            g.DrawString(title, new Font("Arial", 16), Brushes.Black, x, 10f);
        }

        private static void DrawSubList(Graphics g, Dictionary<string, int> sublist)
        {
            int subcount = 0;
            foreach (string key in sublist.Keys)
            {
                int type = sublist[key];
                SizeF stringSize = new SizeF();
                stringSize = g.MeasureString(key, new Font("Arial", 8));
                g.FillRectangle(GetBarBrush(type), ImgWidth - 60f, subcount * stringSize.Height + 2f + stringSize.Height / 2 - 2.5f, 5f, 5f);
                g.DrawString(key, new Font("Arial", 8), Brushes.Black, ImgWidth - 50f, subcount * stringSize.Height + 2f);
                subcount++;
            }
        }

        private static void DrawSubTitle(Graphics g, string subTitle)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(subTitle, new Font("Arial", 8));
            g.DrawString(subTitle, new Font("Arial", 8), Brushes.Black, gapX + legendX - stringSize.Width + 5f, 20f);
        }

        private static void DrawAxisX(Graphics g, IList<IList<int>> data)
        {
            g.DrawLine(Pens.Black, gapX + legendX, gapY + titleHeight, gapX + legendX, ImgHeight - gapY - legendY);
            int maxNum = GetAxisMaxX(data);
            int markSpan = maxNum / 6;
            float markHeight = (ImgHeight - gapY * 2 - titleHeight - legendY) / 6;
            int marknum = 0;
            for (int i = 0; i <= maxNum; i = i + markSpan)
            {
                g.DrawString(i.ToString(), new Font("Arial", 8), Brushes.Black, gapX + legendX - GetStrWidth(i.ToString(), 8, g) - 2f, ImgHeight - gapY - legendY - marknum * markHeight - 5f);
                g.DrawLine(Pens.Gray, gapX + legendX, ImgHeight - gapY - legendY - marknum * markHeight, ImgWidth - gapX, ImgHeight - gapY - legendY - marknum * markHeight);
                marknum++;
            }
            
            DrawBar(g, data, markSpan, markHeight);
        }

        private static void DrawBar(Graphics g, IList<IList<int>> data, int markSpan, float markHeight)
        {
            int dataNum = 1;
            int len = data.Count;
            float markWidth = (ImgWidth - gapX * 2 - legendX - 15f) / (len - 1);
            foreach (IList<int> numList in data)
            {
                float regWidth = markWidth < 25 ? markWidth * 0.45f : 12.5f;
                float CueWidth = gapX + legendX + (dataNum - 1) * YSapn + 15f + regWidth * 2;
                int numcount = -1;
                foreach (int num in numList)
                {
                    float CueHeight = num * 1f / markSpan * markHeight;

                    g.FillRectangle(GetBarBrush(numcount), CueWidth + numcount * (regWidth + barspan) * 2, ImgHeight - gapY - legendY - CueHeight, regWidth * 2, CueHeight);

                    g.DrawString(num.ToString(), new Font("Arial", 8, FontStyle.Bold), GetFontBrush(numcount), CueWidth + numcount * (regWidth + barspan) * 2, ImgHeight - gapY - legendY - CueHeight - 15f);
                    numcount++;
                }
                dataNum++;
            }
        }

        private static void DrawAxisY(Graphics g, IList<string> data, IList<IList<int>> Ydata)
        {
            g.DrawLine(Pens.Black, gapX + legendX, ImgHeight - gapY - legendY, ImgWidth - gapX, ImgHeight - gapY - legendY);
            int len = data.Count + 1;
            int avglen = len / 6 + 1;
            int marknum = 0;
            float markWidth = (ImgWidth - gapX * 2 - legendX - 15f) / (len - 1);
            YSapn = markWidth;
            int maxNum = GetAxisMaxX(Ydata);
            int markSpan = maxNum / 6;
            float regWidth = markWidth < 25 ? markWidth * 0.3f : 12.5f;
            foreach (string mark in data)
            {
                if (len < 12)
                {
                    g.DrawString(mark, new Font("Arial", 8), Brushes.Black, gapX + legendX + marknum * markWidth + (BarCount * regWidth * 2 + (BarCount - 1) * barspan) / 2 - GetStrWidth(mark, 8, g) / 2f + 15f, ImgHeight - gapY - legendY + 5f);

                }
                else
                {
                    if ((marknum - 1) % avglen == 0)
                    {
                        g.DrawString(mark, new Font("Arial", 8), Brushes.Black, gapX + legendX + marknum * markWidth + (BarCount * regWidth * 2 + (BarCount - 1) * barspan) / 2 - GetStrWidth(mark, 8, g) / 2f + 15f, ImgHeight - gapY - legendY + 5f);
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

        private static int GetAxisMaxX(IList<IList<int>> data)
        {
            int maxnum = 0;
            foreach (IList<int> numList in data)
            {
                foreach (int num in numList)
                {
                    if (maxnum < num)
                    {
                        maxnum = num;
                    }
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

        #region
        //private static Color GetColor(int i)
        //{
        //    switch (i)
        //    {
        //        case 1:
        //            return Color.Red;
        //        case 2:
        //            return Color.Gray;
        //        case 3:
        //            return Color.Green;
        //        case 4:
        //            return Color.GreenYellow;
        //        case 5:
        //            return Color.Yellow;
        //        case 6:
        //            return Color.Blue;
        //        case 7:
        //            return Color.BlueViolet;
        //        case 8:
        //            return Color.Cyan;
        //        case 9:
        //            return Color.DarkBlue;
        //        case 10:
        //            return Color.Cyan;
        //        default:
        //            return Color.White;
        //    }
        //}

        private static Brush GetBarBrush(int type)
        {
            switch (type)
            {
                case -1:
                    return Brushes.SkyBlue;
                case 0:
                    return Brushes.Red;
                case 1:
                    return Brushes.Orange;
                default:
                    return Brushes.Black;
            }
        }

        private static Brush GetFontBrush(int type)
        {
            switch (type)
            {
                case -1:
                    return Brushes.Blue;
                case 0:
                    return Brushes.Red;
                case 1:
                    return Brushes.Orange;
                default:
                    return Brushes.Black;
            }
        }
        #endregion
    }
}
