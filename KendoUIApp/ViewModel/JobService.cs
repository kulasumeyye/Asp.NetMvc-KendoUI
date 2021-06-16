
using KendoUIApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KendoUIApp.ViewModel
{

    public class JobService 
    {
        private static bool UpdateDatabase = false;
        private KendoUIDbEntities entities;

        public JobService(KendoUIDbEntities entities)
        {
            this.entities = entities;
        }

        public IList<JobViewModel> GetAll()
        {
            var result = HttpContext.Current.Session["Jobs"] as IList<JobViewModel>;

            if (result == null || UpdateDatabase)
            {
                result = entities.Jobs.Select(job => new JobViewModel
                {
                    JobId = job.JobId,
                    JobName = job.JobName,
                    NotificationDate = job.NotificationDate,
                    SolutionDate = job.SolutionDate,
                    JobOwner = job.JobOwner
                }).ToList();

                HttpContext.Current.Session["Jobs"] = result;
            }

            return result;
        }

        public IEnumerable<JobViewModel> Read()
        {
            return GetAll();
        }

        public void Create(JobViewModel job)
        {



            var entity = new Jobs();

            entity.JobName = job.JobName;
            entity.NotificationDate = job.NotificationDate;
            entity.SolutionDate = job.SolutionDate;
            entity.JobOwner = job.JobOwner;


            entities.Jobs.Add(entity);
            entities.SaveChanges();

            job.JobId = entity.JobId;

        }

        public void Update(JobViewModel job)
        {

            var target = One(e => e.JobId == job.JobId);


            if (target != null)
            {
                var entity = entities.Jobs.Where(j => j.JobId == job.JobId).FirstOrDefault();
                entity.JobName = job.JobName;
                entity.NotificationDate = job.NotificationDate;
                entity.SolutionDate = job.SolutionDate;
                entity.JobOwner = job.JobOwner;


                entities.Jobs.Attach(entity);
                entities.Entry(entity).State = EntityState.Modified;


            }
            else
            {
                var entity = new Jobs();

                entity.JobId = job.JobId;
                entity.JobName = job.JobName;
                entity.NotificationDate = job.NotificationDate;
                entity.SolutionDate = job.SolutionDate;
                entity.JobOwner = job.JobOwner;



                entities.Jobs.Attach(entity);
                entities.Entry(entity).State = EntityState.Modified;


            }
            entities.SaveChanges();

        }

        public void Destroy(JobViewModel Job)
        {

            var entity = new Jobs();

            entity.JobId = Job.JobId;

            entities.Jobs.Attach(entity);

            entities.Jobs.Remove(entity);



            entities.SaveChanges();

        }

        public JobViewModel One(Func<JobViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}

    
