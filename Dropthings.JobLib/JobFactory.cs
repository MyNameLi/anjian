using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dropthings.JobLib
{
    public static class JobFactory
    {
        public static Type GetJobType(int tag){
            switch(tag)
            {
                case 1:
                    return typeof(JBaiduNews);                   
                case 2:
                    return typeof(JSubject);
                case 3:
                    return typeof(JTrendData);
                default:
                    return null;
            }
        }
    }
}
