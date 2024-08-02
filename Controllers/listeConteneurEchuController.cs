using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SopalS.Data;
using SopalS.Models.ConteneurModel;
//using System.Data.Entity;

using Microsoft.EntityFrameworkCore;


using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
//using System.Data.Entity;

namespace SopalS.Controllers
{
    public class listeConteneurEchuController : Controller
    {
        private readonly DataContext _dataContext;

        public listeConteneurEchuController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        // GET: listeConteneurEchuController
        public async Task<IActionResult> Index()
        {
            List<ConteneurViewModel> conteneurs = await _dataContext.Conteneurs.ToListAsync();
            return View(conteneurs);
        }
        [HttpGet]
        public async Task<IActionResult> List(int id)
        {
            var histoEtalonnages = await _dataContext.HistoEtalonnages
                .Where(h => h.Ref == id)
                .ToListAsync();

            return View(histoEtalonnages);
        }
        //public IActionResult Add()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Details(int id)
        {
            var histoEtalonnages = await _dataContext.HistoEtalonnages
                .Where(h => h.Ref == id)
                .ToListAsync();

            if (histoEtalonnages == null)
            {
                return NotFound();
            }

            return View(histoEtalonnages);
        }
        public IActionResult Add(int id)
        {
            var histoEtalonnage = new HistoEtalonnage
            {
                Ref = id,
                Date = DateTime.Now // Initialisation de la date à la date actuelle par exemple
            };

            return View(histoEtalonnage);
        }
        [HttpPost]
        public IActionResult Add(HistoEtalonnage histoEtalonnage)
        {
            if (ModelState.IsValid)
            {
                _dataContext.HistoEtalonnages.Add(histoEtalonnage);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(histoEtalonnage);
        }
        [HttpPost]
        public async Task<IActionResult> GetList()
        {
            var conteneurs = await _dataContext.Conteneurs
                .Select(c => new
                {
                    c.Ref,
                    c.CodeBarres,
                    c.DateMiseEnService,
                    c.PeriodiciteEtalonnage,
                    c.DateDernierEtalonnage,
                    c.DernierPoids,
                    c.Unite,
                    c.UserCreate,
                    c.DateCreate,
                    c.UserUpdate,
                    c.DateUpdate,
                    c.EmplacementId,
                    c.DateProchainEtalonnage,
                    Action = ""
                }).ToListAsync();

            return Json(new { data = conteneurs });
        }


        //[HttpPost]
        //public async Task<IActionResult> Add(HistoEtalonnage histoEtalonnage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _dataContext.HistoEtalonnages.Add(histoEtalonnage);
        //        await _dataContext.SaveChangesAsync();
        //        return RedirectToAction("Details", new { id = histoEtalonnage.Ref });
        //    }

        //    // Si le modèle n'est pas valide, renvoyer la vue avec les erreurs
        //    return View(histoEtalonnage);
        //}
        // POST: HistoEtalonnage/Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add(HistoEtalonnage histoEtalonnage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Optionally set the Date property to current date if not provided
        //        if (histoEtalonnage.Date == default(DateTime))
        //        {
        //            histoEtalonnage.Date = DateTime.Now;
        //        }

        //        _dataContext.HistoEtalonnages.Add(histoEtalonnage);
        //        await _dataContext.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(histoEtalonnage);
        //}

        //[HttpPost]
        //public async Task<IActionResult> List(HistoEtalonnage histoEtalonnage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _dataContext.Update(histoEtalonnage);
        //        await _dataContext.SaveChangesAsync();
        //        return RedirectToAction(nameof(List), new { id = histoEtalonnage.Ref });
        //    }
        //    return View(histoEtalonnage);
        //}

        // GET: listeConteneurEchuController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: listeConteneurEchuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: listeConteneurEchuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: listeConteneurEchuController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: listeConteneurEchuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: listeConteneurEchuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: listeConteneurEchuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
