using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Dropthings.Business.Facade
{
	public static class TransPic
	{
        //定义图片横轴空余
        private static int GapX = 10;
        //定义图片纵轴空余
        private static int GapY = 10;
        //定义图片标题的高度
        private static float TitleHeight = 25f;
        //定义图片的宽度
        private static int ImgWidth = 554;
        //定义图片的高度，根据数据的个数改变
        private static int ImgHeight = 200;
        //定义每个长方体的高度
        private static int ItemHeight = 100;
        //定义每个长方体的宽度
        private static int ItemWidth = 150;
        //定义每行之间的垂直间距
        private static int RowVerticalGap = 25;
        //定义每行之间的横向间距
        private static int RowCrossGap = 35;

        private static Random rd = new Random();


        public static string DrawTransPic(string title, Dictionary<string, IList<string>> datadict)
        {
            string OutPutPath = "~/Admin/dataImg/";
            string fileName = "TransPic_" + rd.Next(0, 50) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".gif";
            string strAbsolutePath = System.Web.HttpContext.Current.Server.MapPath(OutPutPath) + fileName;            
            //初始化图片高度
            InnitImgHeight(datadict.Keys.Count);
            Bitmap bm = new Bitmap(ImgWidth, ImgHeight);
            Graphics g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            DrawTitle(g, title);
            //DrawGrapBack(g);
            DrawItemRect(datadict, g);
            bm.Save(strAbsolutePath);
            bm.Dispose();
            g.Dispose();
            return fileName;
        }

        /*私有方法*/
        private static void DrawTitle(Graphics g, string title)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(title, new Font("Arial", 16));
            float x = ImgWidth / 2 - stringSize.Width / 2;
            g.DrawString(title, new Font("Arial", 16), Brushes.Black, x, 10f);
        }


        private static void DrawGrapBack(Graphics g)
        {
            string path = System.Environment.CurrentDirectory.ToString().Replace("bin\\Debug", "");
            path = path + "top2_02.jpg";
            Image img = Image.FromFile(path);
            g.DrawImage(img, 0f, 0f);
        }

        //画出每个方块体
        private static void DrawItemRect(Dictionary<string, IList<string>> datadict, Graphics g)
        {
            int count = 0;
            IList<PointF> PointList = new List<PointF>();
            float CoordX = 0f;
            float CoordY = 0f;
            foreach (string key in datadict.Keys)
            {
                int RowNum = count / 3;
                CoordY = GapY + RowNum * (RowVerticalGap + ItemHeight) * 1f + TitleHeight;
                int Remainder = count % 6;
                switch (Remainder)
                {
                    case 0:
                        CoordX = GapX;
                        break;
                    case 1:
                        CoordX = GapX + ItemWidth + RowCrossGap * 1f;
                        break;
                    case 2:
                        CoordX = GapX + (ItemWidth + RowCrossGap) * 2f;
                        break;
                    case 3:
                        CoordX = GapX + (ItemWidth + RowCrossGap) * 2f;
                        break;
                    case 4:
                        CoordX = GapX + ItemWidth + RowCrossGap * 1f;
                        break;
                    case 5:
                        CoordX = GapX;
                        break;
                    default:
                        break;
                }
                PointF point = new PointF(CoordX, CoordY);
                PointList.Add(point);
                //画所有箭头
                DrawAllArrows(PointList, g);
                g.DrawRectangle(Pens.Black, CoordX, CoordY, ItemWidth * 1f, ItemHeight * 1f);
                g.DrawLine(Pens.Black, CoordX, CoordY + ItemHeight * 1f - 20f, CoordX + ItemWidth * 1f, CoordY + ItemHeight * 1f - 20f);
                float KeyWidth = GetStrWidth(key, 10, g) / 2f;
                g.DrawString(key, new Font("Arial", 10), Brushes.Black, CoordX + ItemWidth / 2f - KeyWidth, CoordY + ItemHeight * 1f - 18f);
                IList<string> MarkStrList = datadict[key];
                int MarkCount = 0;
                foreach (string MarkStr in MarkStrList)
                {
                    float MarkStrHeight = GetStrHeight(MarkStr, 10, g);
                    float MarkStrWidth = GetStrWidth(MarkStr, 10, g) / 2f;
                    g.DrawString(MarkStr, new Font("Arial", 10), Brushes.Black, CoordX + ItemWidth / 2f - MarkStrWidth, CoordY + 2f + MarkStrHeight * MarkCount);
                    MarkCount++;
                }
                count++;
            }
        }

        private static void DrawAllArrows(IList<PointF> list, Graphics g)
        {
            if (list.Count > 0)
            {
                int totalcount = list.Count;
                for (int i = 0, j = totalcount; i < j; i++)
                {
                    if (i < (totalcount - 1))
                    {
                        PointF startPoint = list[i];
                        PointF endPoint = list[i + 1];
                        if (i > 0 && i % 3 == 2)
                        {
                            DrawArrows(startPoint.X + ItemWidth / 2f, startPoint.Y + ItemHeight, endPoint.X + ItemWidth / 2f, endPoint.Y, 2, g);
                        }
                        else
                        {
                            if (i % 6 == 0 || (i - 1) % 6 == 0)
                            {
                                DrawArrows(startPoint.X + ItemWidth, startPoint.Y + (ItemHeight - 20f) / 2, endPoint.X, endPoint.Y + (ItemHeight - 20f) / 2, 1, g);
                            }
                            else
                            {
                                DrawArrows(startPoint.X, startPoint.Y + (ItemHeight - 20f) / 2, endPoint.X + ItemWidth, endPoint.Y + (ItemHeight - 20f) / 2, 3, g);
                            }
                        }
                    }
                }
            }
        }

        private static void DrawArrows(float startX, float startY, float endX, float endY, int type, Graphics g)
        {
            g.DrawLine(Pens.Black, startX, startY, endX, endY);
            if (type == 1)
            {
                g.DrawLine(Pens.Black, endX, endY, endX - 5f, endY - 3f);
                g.DrawLine(Pens.Black, endX, endY, endX - 5f, endY + 3f);
            }
            else if (type == 2)
            {
                g.DrawLine(Pens.Black, endX, endY, endX - 3f, endY - 5f);
                g.DrawLine(Pens.Black, endX, endY, endX + 3f, endY - 5f);
            }
            else
            {
                g.DrawLine(Pens.Black, endX, endY, endX + 5f, endY - 3f);
                g.DrawLine(Pens.Black, endX, endY, endX + 5f, endY + 3f);
            }
        }

        private static void InnitImgHeight(int count)
        {
            int RowCount = count % 3 == 0 ? count / 3 : count / 3 + 1;
            ImgHeight = RowCount * ItemHeight + (RowCount - 1) * RowVerticalGap + 2 * GapY + Convert.ToInt32(TitleHeight);
        }
        //根据FONTSIZE来计算字符的宽度
        private static float GetStrWidth(string str, int fontsize, Graphics g)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(str, new Font("Arial", fontsize));
            return stringSize.Width;
        }

        //根据FONTSIZE来计算字符的高度
        private static float GetStrHeight(string str, int fontsize, Graphics g)
        {
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(str, new Font("Arial", fontsize));
            return stringSize.Height;
        }
	}
}
