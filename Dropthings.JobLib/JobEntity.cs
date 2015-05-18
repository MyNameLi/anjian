using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dropthings.JobLib
{
    public class JobEntity
    {
        public string JobName
        {
            set;
            get;
        }


        public string GroupName
        {
            set;
            get;
        }

        public string TriggerName
        {
            set;
            get;
        }

        public int JobType
        {
            set;
            get;
        }

        public Type JobRunType
        {
            set;
            get;
        }

        public string ConTriggerRgx
        {
            set;
            get;
        }

        public Dictionary<string, object> ParamsList
        {
            set;
            get;
        }
    }
}
