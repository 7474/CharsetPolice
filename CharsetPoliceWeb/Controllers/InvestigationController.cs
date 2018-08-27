using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharsetPolice.Police;
using CharsetPoliceWeb.Models;
using CharsetPoliceWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharsetPoliceWeb.Controllers
{
    public class InvestigationController : Controller
    {
        private IInvestigationRepository investigationRepository;

        public InvestigationController(IInvestigationRepository investigationRepository)
        {
            this.investigationRepository = investigationRepository;
        }

        // GET: Investigation
        public ActionResult Index()
        {
            // TODO Pagenate.
            var results = investigationRepository.GetRecents(0, 10);
            return View(results);
        }

        // GET: Investigation
        public ActionResult Ranking()
        {
            // TODO Pagenate.
            var results = investigationRepository.GetRanking(0, 10);
            return View(results);
        }

        // GET: Investigation/Details/5
        public ActionResult Details(string uri)
        {
            var result = investigationRepository.Get(new Uri(uri));
            return View(result);
        }

        // GET: Investigation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Investigation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InvestigationRequest request)
        {
            try
            {
                var policeMan = new CharsetPoliceMan();
                var result = await policeMan.SearchAsync(request.Uri);
                investigationRepository.Put(result);

                return RedirectToAction(nameof(Details), new { uri = request.Uri.ToString() });
            }
            catch
            {
                return View("Views/Home/Index.cshtml");
            }
        }
    }
}