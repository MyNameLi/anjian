using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using log4net;

namespace Dropthings.JobLib
{
    public class JTrendData : IJob
    {
        ILog logger = LogManager.GetLogger("JTrendData");
        
        public JTrendData(){}

        public void Execute(JobExecutionContext context)
        {
            logger.Error("The JTrendData is Started");
            DBJob.DBTrendData job = new DBJob.DBTrendData();
            job.Do();
        }
    }
}
