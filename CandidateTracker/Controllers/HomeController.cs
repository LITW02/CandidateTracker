using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CandidateTracker.Data;
using CandidateTracker.Models;

namespace CandidateTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCandidate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCandidate(Candidate candidate)
        {
            var manager = new CandidateManager(Properties.Settings.Default.ConStr);
            manager.AddCandidate(candidate);
            return Redirect("/home/pending");
        }

        public ActionResult Pending()
        {
            var manager = new CandidateManager(Properties.Settings.Default.ConStr);
            return View(new CandidatesViewModel { Candidates = manager.GetCandidates(Status.Pending) });
        }

        public ActionResult Details(int id)
        {
            var manager = new CandidateManager(Properties.Settings.Default.ConStr);
            return View(new CandidateViewModel { Candidate = manager.GetCandidate(id) });
        }

        [HttpPost]
        public void UpdateStatus(int id, Status status)
        {
            var manager = new CandidateManager(Properties.Settings.Default.ConStr);
            manager.UpdateStatus(id, status);
        }

        public ActionResult GetCounts()
        {
            var manager = new CandidateManager(Properties.Settings.Default.ConStr);
            return Json(manager.GetCounts(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Confirmed()
        {
            var manager = new CandidateManager(Properties.Settings.Default.ConStr);
            ViewBag.Type = "Confirmed";
            return View("Completed", new CandidatesViewModel { Candidates = manager.GetCandidates(Status.Confirmed) });
        }

        public ActionResult Refused()
        {
            var manager = new CandidateManager(Properties.Settings.Default.ConStr);
            ViewBag.Type = "Refused";
            return View("Completed", new CandidatesViewModel { Candidates = manager.GetCandidates(Status.Refused) });
        }

    }
}
