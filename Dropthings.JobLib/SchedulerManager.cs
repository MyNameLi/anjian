using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Xml;
using Quartz.Impl;

namespace Dropthings.JobLib
{
    public static class SchedulerManager
    {
        private static IScheduler sched;

        /// <summary>
        /// 启动Scheduler
        /// </summary>
        private static void SchedulerStart()
        {
            try
            {
                ISchedulerFactory sf = new StdSchedulerFactory();
                sched = sf.GetScheduler();
                sched.Start();
            }
            catch (Exception e)
            {
                throw;
            }

            
        }

        public static Boolean AddJob(JobEntity entity)
        {
            if (entity != null)
            {
                if (sched == null)
                {
                    SchedulerStart();
                }
                else
                {
                    if (!sched.IsStarted)
                    {
                        SchedulerStart();
                    }
                }
                if (GetJob(entity) == null)
                {
                    if (entity.JobType == 1)
                    {
                        return AddOnceJob(entity);
                    }
                    else
                    {
                        return AddPeriodJob(entity);
                    }
                }
                else
                {
                    return ResumeJob(entity);
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加一个一次性任务
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        private static Boolean AddOnceJob(JobEntity entity)
        {
            try
            {             
                JobDetail job = new JobDetail(entity.JobName, entity.GroupName, entity.JobRunType);
                if (entity.ParamsList.Count > 0)
                {
                    job.JobDataMap = new JobDataMap();
                    Dictionary<string, object> dict = entity.ParamsList;
                    foreach (string key in dict.Keys)
                    {
                        job.JobDataMap.Add(key, dict[key]);
                    }
                }
                DateTime runTime = TriggerUtils.GetEvenMinuteDate(DateTime.UtcNow);
                SimpleTrigger STrigger = new SimpleTrigger(entity.TriggerName, entity.GroupName, runTime);
                sched.ScheduleJob(job, STrigger);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 添加一个周期性任务
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <param name="ConTriggerRgx"></param>
        /// <returns></returns>
        private static Boolean AddPeriodJob(JobEntity entity)
        {
            try
            {                
                JobDetail job = new JobDetail(entity.JobName, entity.GroupName, entity.JobRunType);
                if (entity.ParamsList.Count > 0)
                {
                    job.JobDataMap = new JobDataMap();
                    Dictionary<string, object> dict = entity.ParamsList;
                    foreach (string key in dict.Keys)
                    {
                        job.JobDataMap.Add(key, dict[key]);
                    }
                }
                CronTrigger CTrigger = new CronTrigger(entity.TriggerName, entity.GroupName, entity.JobName, entity.GroupName, entity.ConTriggerRgx);
                sched.AddJob(job, true);
                DateTime ft = sched.ScheduleJob(CTrigger);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 暂停一个Job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static Boolean PauseJob(JobEntity entity)
        {
            try
            {
                sched.PauseJob(entity.JobName, entity.GroupName);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 恢复一个暂停的JOB
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static Boolean ResumeJob(JobEntity entity)
        {
            try
            {
                sched.ResumeJob(entity.JobName, entity.GroupName);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一个JOB，此操作不可逆，请慎用
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static Boolean DeleteJob(JobEntity entity)
        {
            try
            {
                sched.DeleteJob(entity.JobName, entity.GroupName);
                return true;
                
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static JobDetail GetJob(JobEntity entity){
            return sched.GetJobDetail(entity.JobName, entity.GroupName);
        }

        /// <summary>
        /// 停止Scheduler
        /// </summary>
        public static void SchedulerStop()
        {
            try
            {
                if (!sched.IsShutdown)
                {
                    sched.Shutdown(true);
                    sched = null;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
