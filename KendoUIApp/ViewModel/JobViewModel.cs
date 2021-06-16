using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoUIApp.ViewModel
{
    public class JobViewModel
    {
        public int JobId { get; set; }
        public string JobName { get; set; }
        public System.DateTime NotificationDate { get; set; }
        public System.DateTime SolutionDate { get; set; }
        public string JobOwner { get; set; }
    }
}