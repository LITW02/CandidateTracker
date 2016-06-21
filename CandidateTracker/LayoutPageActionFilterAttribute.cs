using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CandidateTracker.Data;

namespace CandidateTracker
{
    public class LayoutPageActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var manager = new CandidateManager(Properties.Settings.Default.ConStr);
            filterContext.Controller.ViewBag.CandidateCounts = manager.GetCounts();
            base.OnActionExecuting(filterContext);
        }
    }
}