using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoUIApp.Models;

using KendoUIApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;

namespace KendoUIApp.Controllers
{

    public class HomeController : Controller
    {
        private KendoUIDbEntities objKendoUIDbEntities;
        JobService jobService;
        public HomeController()
        {
            objKendoUIDbEntities = new KendoUIDbEntities();
            this.jobService = new JobService(objKendoUIDbEntities);
        }

        // GET: Kendo
        public ActionResult Index()
        {

    

            return View();
        }

        public PartialViewResult MenuBar()
        {

            KendoUIDbEntities entities = new KendoUIDbEntities();

            return PartialView(entities.Menu);
        }

        public ActionResult GridView()
        {
            return View();
        }
      
       

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<JobViewModel> listOfJobViewModel =
               (from objJob in objKendoUIDbEntities.Jobs
                select new JobViewModel()
                {
                    JobId = objJob.JobId,
                    JobName = objJob.JobName,
                    NotificationDate = objJob.NotificationDate,
                    SolutionDate = objJob.SolutionDate,
                    JobOwner = objJob.JobOwner



                }).ToList();

            return Json(listOfJobViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, JobViewModel job)
        {
            if (job != null && ModelState.IsValid)
            {
                jobService.Create(job);
            }

            return Json(new[] { job }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, JobViewModel job)
        {
            if (job != null && ModelState.IsValid)
            {
                jobService.Update(job);
            }

            return Json(new[] { job }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, JobViewModel job)
        {
            if (job != null)
            {
                jobService.Destroy(job);
            }

            return Json(new[] { job }.ToDataSourceResult(request, ModelState));
        }
    }
}


