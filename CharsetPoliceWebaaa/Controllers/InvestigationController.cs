using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharsetPoliceWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharsetPoliceWeb.Controllers
{
    public class InvestigationController : Controller
    {
        private InvestigationRepository investigationRepository;

        public InvestigationController(InvestigationRepository investigationRepository)
        {
            this.investigationRepository = investigationRepository;
        }

        // GET: Investigation
        public ActionResult Index()
        {
            return View();
        }

        // GET: Investigation/Details/5
        public ActionResult View(int id)
        {
            return View();
        }

        // GET: Investigation/Create
        public ActionResult Form()
        {
            return View();
        }

        // POST: Investigation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Form(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}