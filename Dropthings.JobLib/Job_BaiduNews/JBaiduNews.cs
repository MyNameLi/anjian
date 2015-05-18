using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Dropthings.Util;
using log4net;

namespace Dropthings.JobLib
{
    public class JBaiduNews : IJob
    {        
        ILog logger = LogManager.GetLogger("JSubject");
        public JBaiduNews(){ }
        #region IJob Members        
        public void Execute(JobExecutionContext context)
        {
            if (context.JobDetail.JobDataMap["keyword"] != null)
            {
                string keywords = context.JobDetail.JobDataMap["keyword"].ToString();
                if (keywords != string.Empty)
                {
                    logger.Error("this is JBaiduNews." + "the keyword is" +keywords);
                    IdxSource.IDXJob s = new IdxSource.IDXJob();
                    s.Keywords = keywords;
                    s.save();                  
                }
                else
                {
                    logger.Error("keywords is null");
                }
            }
            else
            {

                logger.Error("subject is null");
            }          
        }

        #endregion
    }
}
