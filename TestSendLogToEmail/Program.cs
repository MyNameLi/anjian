using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Data;
using Dropthings.Business.Facade;

namespace TestSendLogToEmail
{
    public class Program
    {
        private static FileStream fileStream = null;
        private static StreamWriter sw;

        public static void Main(string[] args)
        {
            Console.WriteLine("程序运行中...");
            string paht = "";
            string writeMsg = "";
            try
            {
                paht = GenerateLog();
                writeMsg = string.Format("生成成功\n文件位置：{0}!", paht);
            }
            catch (Exception e)
            {
                CloseWrite();
                string errorMsg = e.ToString();
                paht = WriteErrorLog(errorMsg);
                writeMsg = string.Format("生成异常\n错误日志路径：{0}", paht);
            }
            //生成日志
            CloseWrite();
            Console.WriteLine(writeMsg);
            Console.Read();
            //Console.WriteLine(System.Environment.CurrentDirectory);
            //发生日志邮件
            //sendmail("520li-xiaolin@163.com", "李晓林", "185435798@qq.com", "185435798", "日志", "日志内容", @"C:\20131030-日报.txt", "smtp.163.com", "520li-xiaolin@163.com", "53514098");
        }
        public static void CloseWrite()
        {
            if (sw != null && fileStream != null)
            {

                sw.Close();
                fileStream.Close();
                sw.Dispose();
                fileStream.Dispose();
            }
            sw = null;
            fileStream = null;
        }
        public static string WriteErrorLog(string msg)
        {
            string fileName = string.Format("C:\\{0}", DateTime.Now.ToString("yyyyMMdd") + "-日报.txt");
            fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);//创建写入文件
            sw = new StreamWriter(fileStream);
            sw.Write(msg);
            return fileName;
        }
        public static string GenerateLog()
        {
            string fileName = string.Format("C:\\{0}", DateTime.Now.ToString("yyyyMMdd") + "-日报.txt");
            fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);//创建写入文件
            sw = new StreamWriter(fileStream);
            DataTable resultTable;
            resultTable = GetDataLog();
            int i = 1;//表示当前栏目下第几条新闻
            int type = -1;//表示当前记录是什么类型的新闻
            bool j = true;//表示是不是换栏目了
            foreach (DataRow item in resultTable.Rows)
            {
                int newsType = int.Parse(item["NEWSTYPE"].ToString());
                string title = item["NEWSTITLE"].ToString();
                string summary = item["SUMMARY"].ToString();
                string link = item["LINK"].ToString();
                string content = item["CONTENT"].ToString();
                string createtime = item["CREATETIME"].ToString();
                string count = item["COUNT"].ToString();
                string mcount = item["MCOUNT"].ToString();
                if (type != newsType)
                {
                    sw.WriteLine("");
                    i = 1; type = newsType; j = false;
                }
                else
                {
                    i++; type++; j = true;
                }


                if (j)
                {
                    WriteDataInfo(i, title, content);
                }
                else
                {
                    WriteNewsType(type);
                    WriteDataInfo(i, title, content);
                }
            }
            return fileName;

        }
        public static void WriteNewsType(int type)
        {
            switch (type)
            {
                case 0:
                    sw.WriteLine("一、安全生产热点");
                    break;
                case 1:
                    sw.WriteLine("二、时政新闻");
                    break;
                case 2:
                    sw.WriteLine("三、经济新闻");
                    break;
                case 3:
                    sw.WriteLine("四、社会新闻");
                    break;
                case 4:
                    sw.WriteLine("文化、社会新闻");
                    break;
                default:
                    break;
            }
        }
        public static void WriteDataInfo(int i, string title, string content)
        {
            sw.WriteLine(string.Format("{0}.{1}", i, title));
            sw.WriteLine(string.Format("{0}", content));
        }
        public static DataTable GetDataLog()
        {
            string sTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string eTime = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            return DailyRankFacade.GetTodayLog(sTime, eTime);
        }

        /// <summary>
        /// C#发送邮件函数
        /// </summary>
        /// <param name="from">发送者邮箱</param>
        /// <param name="fromer">发送人</param>
        /// <param name="to">接受者邮箱</param>
        /// <param name="toer">收件人</param>
        /// <param name="Subject">主题</param>
        /// <param name="Body">内容</param>
        /// <param name="file">附件</param>
        /// <param name="SMTPHost">smtp服务器</param>
        /// <param name="SMTPuser">邮箱</param>
        /// <param name="SMTPpass">密码</param>

        /// <returns></returns>
        public static bool sendmail(string sfrom, string sfromer, string sto, string stoer, string sSubject, string sBody, string sfile, string sSMTPHost, string sSMTPuser, string sSMTPpass)
        {
            ////设置from和to地址
            MailAddress from = new MailAddress(sfrom, sfromer);
            MailAddress to = new MailAddress(sto, stoer);
            ////创建一个MailMessage对象
            System.Net.Mail.MailMessage oMail = new System.Net.Mail.MailMessage(from, to);
            //// 添加附件
            if (sfile != "")
            {
                oMail.Attachments.Add(new Attachment(sfile));
            }

            ////邮件标题
            oMail.Subject = sSubject;
            ////邮件内容
            oMail.Body = sBody;
            ////邮件格式
            oMail.IsBodyHtml = false;
            ////邮件采用的编码
            oMail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");
            ////设置邮件的优先级为高
            oMail.Priority = MailPriority.High;
            ////发送邮件
            SmtpClient client = new SmtpClient();
            ////client.UseDefaultCredentials = false; 
            client.Host = sSMTPHost;
            client.Credentials = new NetworkCredential(sSMTPuser, sSMTPpass);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                client.Send(oMail);
                return true;
            }
            catch (Exception err)
            {
                // Response.Write(err.Message.ToString());
                Console.WriteLine(err.Message.ToString());
                return false;
            }
            finally
            {
                ////释放资源
                oMail.Dispose();
            }

        }

    }

}
