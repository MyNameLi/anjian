using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using log4net;

namespace Dropthings.JobLib
{
    public class JSubject : IJob
    {
        ILog logger = LogManager.GetLogger("JSubject");

        public JSubject()
        {
        }

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {
            ILog log = LogManager.GetLogger("JSubject");
            if (context.JobDetail.JobDataMap["keyword"] != null)
            {
                string keywords = context.JobDetail.JobDataMap["keyword"].ToString();
                if (keywords != string.Empty)
                {
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        #endregion
    }
}
