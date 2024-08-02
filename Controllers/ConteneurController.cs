using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SopalS.Models.ConteneurModel;
using SopalS.Models;
using Microsoft.EntityFrameworkCore;
using SopalS.Data;
//using System.Data.Entity;
using Microsoft.AspNetCore.Http.HttpResults;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Data.Entity;
using System.Data.SqlTypes;

namespace SopalS.Controllers
{
    public class ConteneurController : Controller

    {

        private readonly DataContext _dataContext;

        public ConteneurController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddConteneurviewmodel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Ajouter le nouveau conteneur à votre base de données
                    //entite.dde =null
                    var conteneurtoadd = new Conteneur()
                    {
                        Ref = model.Ref,
                        CodeBarres = model.CodeBarres,
                        EmplacementId = model.EmplacementId,
                        DateMiseEnService = model.DateMiseEnService,
                        PeriodiciteEtalonnage = model.PeriodiciteEtalonnage,
                        DateDernierEtalonnage = model.DateDernierEtalonnage,
                        DernierPoids = model.DernierPoids,
                        Unite = model.Unite,
                        UserCreate = model.UserCreate,
                        UserUpdate = model.UserUpdate,
                        DateCreate = model.DateCreate,
                        DateUpdate = model.DateUpdate
                    };

                    _dataContext.Conteneur.Add(conteneurtoadd);
                    // add function that adds mds+durre to dde 
                    await _dataContext.SaveChangesAsync();

                    // Rediriger vers l'Index après l'ajout
                    return Ok();
                }
                catch (DbUpdateException ex)
                {
                    // Capturer les détails de l'exception interne
                    var errorMessage = ex.InnerException?.Message ?? ex.Message;
                    // Logger l'erreur (vous pouvez utiliser un système de logging approprié)
                    Console.WriteLine(errorMessage);
                    // Retourner une réponse avec un message d'erreur
                    return BadRequest(new { message = errorMessage });
                }
            }

            // Si le modèle n'est pas valide, retourner une réponse BadRequest avec les erreurs du modèle
            return BadRequest(ModelState);
        }
        public async Task<IActionResult> GetEmplacements()
        {
            var emplacements = await _dataContext.Emplacement
                .Select(e => new { e.Codeemp, e.libele })
                .ToListAsync();
            return Json(emplacements);
        }



        public IActionResult Index()
        {
            IEnumerable<Conteneur> objUtilisateurList = _dataContext.Conteneur.ToList();
            return View(objUtilisateurList);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var conteneur = await _dataContext.Conteneur.FindAsync(id);
            if (conteneur == null)
            {
                return NotFound();
            }

            _dataContext.Conteneur.Remove(conteneur);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Conteneur conteneur)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Conteneur.Add(conteneur);
                    await _dataContext.SaveChangesAsync();
                    return Ok();
                }
                catch (DbUpdateException ex)
                {
                
                    Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                    return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
                }
            }
            else
            {
               
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return BadRequest(ModelState);
        }




        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var customer = await _dataContext.Conteneur.FirstOrDefaultAsync(x => x.Ref == id);

            if (customer != null)
            {
                var viewModel = new Conteneur()
                {
                    Ref = customer.Ref,
                    CodeBarres = customer.CodeBarres,
                    EmplacementId = customer.EmplacementId,
                    DateMiseEnService = customer.DateMiseEnService,
                    PeriodiciteEtalonnage = customer.PeriodiciteEtalonnage,
                    DateDernierEtalonnage = customer.DateDernierEtalonnage,
                    DernierPoids = customer.DernierPoids,
                    Unite = customer.Unite,
                    UserCreate = customer.UserCreate,
                    UserUpdate = customer.UserUpdate,
                    DateCreate = customer.DateCreate,
                    DateUpdate = customer.DateUpdate
                };
                return await Task.Run(() => View("Update", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Conteneur model)
        {
            var customer = await _dataContext.Conteneur.FindAsync(model.Ref);
            
            if (customer != null)
            {
                
                customer.CodeBarres = model.CodeBarres;
                customer.EmplacementId = model.EmplacementId;
                customer.DateMiseEnService = model.DateMiseEnService;
                customer.PeriodiciteEtalonnage = model.PeriodiciteEtalonnage;
                customer.DateDernierEtalonnage = model.DateDernierEtalonnage;
                customer.DernierPoids = model.DernierPoids;
                customer.Unite = model.Unite;
                customer.UserCreate = model.UserCreate;
                customer.UserUpdate = model.UserUpdate;
                customer.DateCreate = model.DateCreate;
                customer.DateUpdate = model.DateUpdate;



                await _dataContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }









        // GET: ConteneurController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)


        {
            if (id != null)
            {
                NotFound();
            }

            var product = _dataContext.Conteneur.Find(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Conteneur model)
        {
            var employee = await _dataContext.Conteneur.FindAsync(model.Ref);
            if (employee != null)
            {
                _dataContext.Conteneur.Remove(employee);

                await _dataContext.SaveChangesAsync();
                //return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(Index));
        }

    }

}