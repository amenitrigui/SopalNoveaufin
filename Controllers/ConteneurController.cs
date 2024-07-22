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



        //[HttpPost]
        //public IActionResult Add([FromBody] Conteneur model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Ajouter le nouveau conteneur à votre base de données
        //        _dataContext.Conteneur.Add(model);
        //        _dataContext.SaveChanges();

        //        // Rediriger vers l'Index après l'ajout
        //        return RedirectToAction("Index");
        //    }

        //    // Si le modèle n'est pas valide, retourner à la vue du formulaire avec des erreurs
        //    return View(model);
        //}




        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<Conteneur> objCONTENEURList = await _dataContext.Conteneur.ToListAsync();
        //    return View(objCONTENEURList);
        //}


        //public async Task<IActionResult> Index()
        //{
        //    List<Conteneur> liste = await _dataContext.Conteneur.ToListAsync();
        //    return View(liste);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //  List<Conteneur> conteneurs = await _dataContext.Conteneur.ToListAsync();
        //   return View(conteneurs);
        //}

        //[httppost]
        //public async task<iactionresult> index(string value)
        //{
        //    iqueryable<conteneur> query = _datacontext.conteneur;

        //    if (!string.isnullorempty(value))
        //    {
        //        query = query.where(c => c.usercreate.toupper().contains(value.toupper()));
        //    }

        //    list<conteneur> customers = await query.tolistasync();

        //    return view(customers);
        //}
        //[HttpGet]

        //public async Task<IActionResult> Index()
        //{
        //    var customers = await _dataContext.Conteneur.ToListAsync();

        //    return View(customers);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Index(string value)
        //{

        //    var customers = await _dataContext.Conteneur.ToListAsync();
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        customers = customers.Where(c => c.CodeBarres.ToUpper().Contains(value.ToUpper())).ToList();
        //    }

        //    return View(customers);
        //}
        public IActionResult Index()
        {
            IEnumerable<Conteneur> objUtilisateurList = _dataContext.Conteneur.ToList();
            return View(objUtilisateurList);
        }
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] Utilisateur utilisateur)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Ajouter le préfixe "SOPAL\" au nom d'utilisateur
        //            utilisateur.Username = "SOPAL\\" + utilisateur.Username;

        //            _db.Utilisateurs.Add(utilisateur);
        //            await _db.SaveChangesAsync();
        //            return Ok();
        //        }
        //        catch (DbUpdateException ex)
        //        {
        //            // Log the error (uncomment ex variable name and write a log.)
        //            return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
        //        }
        //    }

        //    return BadRequest(ModelState);
        //}
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
                    // Log the error
                    Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                    return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
                }
            }
            else
            {
                // Log the model state errors
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

















        //[HttpGet]


        //public async Task<IActionResult> Index()
        //{
        //    var conteneurs = await _dataContext.Conteneur.ToListAsync();
        //    return View(conteneurs);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Index(string value)
        //{
        //    var customers = await _dataContext.Conteneur.ToListAsync();

        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        customers = customers.Where(c => c.CodeBarres.ToUpper().Contains(value.ToUpper())).ToList();
        //    }

        //    return View(customers);
        //}


        // GET: ConteneurController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ConteneurController/Create
      
        public IActionResult ConfirmDelete(int? CodUsr)
        {
            if (CodUsr == null || CodUsr == 0)
            {
                return NotFound();
            }

            var utilisateurFromDB = _dataContext.Conteneur.Find(CodUsr);
            if (utilisateurFromDB == null)
            {
                return NotFound();
            }

            return View(utilisateurFromDB);
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Logique pour supprimer l'utilisateur avec l'ID spécifié (id)
            try
            {
                // Exemple de logique pour supprimer l'utilisateur
                var utilisateur = _dataContext.Conteneur.Find(id);
                if (utilisateur == null)
                {
                    return NotFound();
                }

                _dataContext.Conteneur.Remove(utilisateur);
                _dataContext.SaveChanges();

                return Ok(); // Retourner un statut HTTP 200 OK si la suppression réussit
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Gérer les erreurs si la suppression échoue
            }
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






        //[HttpPost]
        //public async Task<IActionResult> Add([FromBody] Conteneur conteneur)
        //{
        //    if (conteneur.DateCreate < SqlDateTime.MinValue.Value || conteneur.DateCreate > SqlDateTime.MaxValue.Value)
        //    {
        //        conteneur.DateCreate = DateTime.Now;
        //    }

        //    // Faites de même pour toutes les autres dates
        //    // conteneur.DateDernierEtalonnage, conteneur.DateMiseEnService, conteneur.DateUpdate

        //    _dataContext.Conteneur.Add(conteneur);
        //    await _dataContext.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}





        // POST: ConteneurController/Create
        //[HttpPost]
        //public async Task<IActionResult> Add(Conteneur addCutomerViewModel)
        //{


        //    var customer = new Conteneur()
        //    {
        //        //Ref = nextId,
        //        //Ref = Guid.NewGuid(),
        //        Ref = addCutomerViewModel.Ref,
        //        CodeBarres = addCutomerViewModel.CodeBarres,
        //        Emplacement = addCutomerViewModel.Emplacement,
        //        DateMiseEnService = addCutomerViewModel.DateMiseEnService,
        //        PeriodiciteEtalonnage = addCutomerViewModel.PeriodiciteEtalonnage,
        //        DateDernierEtalonnage = addCutomerViewModel.DateDernierEtalonnage,
        //        DernierPoids = addCutomerViewModel.DernierPoids,
        //        Unite = addCutomerViewModel.Unite,
        //        UserCreate = addCutomerViewModel.UserCreate,
        //        UserUpdate = addCutomerViewModel.UserUpdate,
        //        DateCreate = addCutomerViewModel.DateCreate,
        //        DateUpdate = addCutomerViewModel.DateUpdate,
        //    };
        //    await _dataContext.Conteneur.AddAsync(customer);
        //    await _dataContext.SaveChangesAsync();
        //    // return View();
        //    return RedirectToAction(nameof(Index));
        //    //return RedirectToAction("Index");
        //}

        //[HttpGet]
        //public async Task<IActionResult> Update(int id)
        //{
        //    var customer = await _dataContext.Conteneurs.FirstOrDefaultAsync(x => x.Ref == id);
        //    if (customer != null)
        //    {
        //        var viewModel = new EditCustomerViewModel
        //        {
        //            Ref = customer.Ref,
        //            CodeBarres = customer.CodeBarres,
        //            Emplacement = customer.Emplacement,
        //            DateMiseEnService = customer.DateMiseEnService,
        //            PeriodiciteEtalonnage = customer.PeriodiciteEtalonnage,
        //            DateDernierEtalonnage = customer.DateDernierEtalonnage,
        //            DernierPoids = customer.DernierPoids,
        //            Unite = customer.Unite,
        //            UserCreate = customer.UserCreate,
        //            UserUpdate = customer.UserUpdate,
        //            DateCreate = customer.DateCreate,
        //            DateUpdate = customer.DateUpdate
        //        };
        //        return View("Update", viewModel);
        //    }

        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(EditCustomerViewModel model)
        //{
        //    var customer = await _dataContext.Conteneurs.FindAsync(model.Ref);
        //    if (customer != null)
        //    {
        //        customer.CodeBarres = model.CodeBarres;
        //        customer.Emplacement = model.Emplacement;
        //        customer.DateMiseEnService = model.DateMiseEnService;
        //        customer.PeriodiciteEtalonnage = model.PeriodiciteEtalonnage;
        //        customer.DateDernierEtalonnage = model.DateDernierEtalonnage;
        //        customer.DernierPoids = model.DernierPoids;
        //        customer.Unite = model.Unite;
        //        customer.UserCreate = model.UserCreate;
        //        customer.UserUpdate = model.UserUpdate;
        //        customer.DateCreate = model.DateCreate;
        //        customer.DateUpdate = model.DateUpdate;

        //        await _dataContext.SaveChangesAsync();

        //        return RedirectToAction("Index");
        //    }

        //    return RedirectToAction("Index");
        //}


        // GET: ConteneurController/Delete/5
        //public async Task <ActionResult> Delete (int id)
        //    {
        //        var customer = await _dataContext.Conteneurs.FirstOrDefaultAsync(x => x.Ref == id);
        //        if (customer != null)
        //        {
        //            var viewModel = new DeleteCustomerViewModel()
        //            {
        //                CodeBarres = customer.CodeBarres,
        //                Emplacement = customer.Emplacement,
        //                DateMiseEnService = customer.DateMiseEnService,
        //                PeriodiciteEtalonnage = customer.PeriodiciteEtalonnage,
        //                DateDernierEtalonnage = customer.DateDernierEtalonnage,
        //                DernierPoids = customer.DernierPoids,
        //                Unite = customer.Unite,
        //                UserCreate = customer.UserCreate,
        //                UserUpdate = customer.UserUpdate,
        //                DateCreate = customer.DateCreate,
        //                DateUpdate = customer.DateUpdate,
        //            };
        //            return await Task.Run(() => View("Delete", viewModel));

        //        }
        //        return RedirectToAction("Index");
        //    }

        //    // POST: ConteneurController/Delete/5
        //    [HttpPost]
        //    public async Task<IActionResult> Delete(DeleteCustomerViewModel model)
        //    {
        //        var customer = await _dataContext.Conteneurs.FindAsync(model.Ref);
        //        if (customer != null)
        //        {
        //            _dataContext.Conteneurs.Remove(customer);
        //            await _dataContext.SaveChangesAsync();

        //            return RedirectToAction("Index");

        //        }
        //        return RedirectToAction("Index");
        //    }


        //[HttpGet]
        //public IActionResult delete(int? id)


        //{
        //    if (id != null)
        //    {
        //        NotFound();
        //    }

        //    var product = _dataContext.Conteneur.Find(id);
        //    return View(product);
        //}

        //[HttpPost]
        //public async Task<IActionResult> delete(Conteneur model)
        //{
        //    var employee = await _dataContext.Conteneur.FindAsync(model.Ref);
        //    if (employee != null)
        //    {
        //        _dataContext.Conteneur.Remove(employee);

        //        await _dataContext.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction("Index");

        //}
        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var customer = await _dataContext.Conteneur.FirstOrDefaultAsync(x => x.Ref== id);
        //    if (customer != null)
        //    {
        //        var viewModel = new Conteneur()
        //        {

        //            CodeBarres = customer.CodeBarres,
        //            Emplacement = customer.Emplacement,
        //            DateMiseEnService = customer.DateMiseEnService,
        //            PeriodiciteEtalonnage = customer.PeriodiciteEtalonnage,
        //            DateDernierEtalonnage = customer.DateDernierEtalonnage,
        //            DernierPoids = customer.DernierPoids,
        //            Unite = customer.Unite,
        //            UserCreate = customer.UserCreate,
        //            UserUpdate = customer.UserUpdate,
        //            DateCreate = customer.DateCreate,
        //            DateUpdate = customer.DateUpdate,

        //        };
        //        return await Task.Run(() => View("Delete", viewModel));

        //    }
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Delete(Conteneur model)
        //{
        //    var customer = await _dataContext.Conteneur.FindAsync(model.Ref);
        //    if (customer != null)
        //    {
        //        _dataContext.Conteneur.Remove(customer);
        //        await _dataContext.SaveChangesAsync();

        //        return RedirectToAction("Index");

        //    }
        //    return RedirectToAction("Index");
        //}
        // HttpGet method to display the delete confirmation view
        //[HttpPost]
        //public async Task<IActionResult> Delete(Conteneur model)
        //{
        //    var employee = await _dataContext.Conteneur.FindAsync(model.Ref);
        //    if (employee != null)
        //    {
        //        _dataContext.Conteneur.Remove(employee);

        //        await _dataContext.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction("Index");

        //}
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken] // Helps in preventing cross site request forgery attacks
        //public IActionResult DeletePOST(int? id)
        //{
        //    var obj = _dataContext.Conteneur.Find(id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }


        //    //Update the category object 
        //    _dataContext.Conteneur.Remove(obj);
        //    _dataContext.SaveChanges(); // Saved to database
        //                       //Using TempData for alerts
        //    TempData["success"] = "Category Deleted Successfully!";

        //    //After saving data redirect to index action of category
        //    return RedirectToAction("Index");



























        //    [HttpGet]
        //    public async Task<IActionResult> Edit(int id)
        //    {
        //        var conteneur = await _dataContext.Conteneur.FindAsync(id);
        //        if (conteneur == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(conteneur);
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> Edit(Conteneur conteneur)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(conteneur);
        //        }

        //        var existingConteneur = await _dataContext.Conteneur.FindAsync(conteneur.Ref);
        //        if (existingConteneur == null)
        //        {
        //            return NotFound();
        //        }

        //        existingConteneur.CodeBarres = conteneur.CodeBarres;
        //        existingConteneur.Emplacement = conteneur.Emplacement;
        //        existingConteneur.DateMiseEnService = conteneur.DateMiseEnService;
        //        existingConteneur.PeriodiciteEtalonnage = conteneur.PeriodiciteEtalonnage;
        //        existingConteneur.DateDernierEtalonnage = conteneur.DateDernierEtalonnage;
        //        existingConteneur.DernierPoids = conteneur.DernierPoids;
        //        existingConteneur.Unite = conteneur.Unite;
        //        existingConteneur.UserCreate = conteneur.UserCreate;
        //        existingConteneur.UserUpdate = conteneur.UserUpdate;
        //        existingConteneur.DateCreate = conteneur.DateCreate;
        //        existingConteneur.DateUpdate = conteneur.DateUpdate;

        //        try
        //        {
        //            await _dataContext.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ConteneurExists(conteneur.Ref))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool ConteneurExists(int id)
        //    {
        //        return _dataContext.Conteneur.Any(e => e.Ref == id);
        //    }



        //}







        //[HttpGet]
        //    public async Task<IActionResult> Edit(int id)
        //    {
        //        var conteneur = await _dataContext.Conteneur.FindAsync(id);
        //        if (conteneur == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(conteneur);
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> Edit(Conteneur conteneur)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(conteneur);
        //        }

        //        var existingConteneur = await _dataContext.Conteneur.FindAsync(conteneur.Ref);
        //        if (existingConteneur == null)
        //        {
        //            return NotFound();
        //        }

        //        existingConteneur.CodeBarres = conteneur.CodeBarres;
        //        existingConteneur.Emplacement = conteneur.Emplacement;
        //        existingConteneur.DateMiseEnService = conteneur.DateMiseEnService;
        //        existingConteneur.PeriodiciteEtalonnage = conteneur.PeriodiciteEtalonnage;
        //        existingConteneur.DateDernierEtalonnage = conteneur.DateDernierEtalonnage;
        //        existingConteneur.DernierPoids = conteneur.DernierPoids;
        //        existingConteneur.Unite = conteneur.Unite;
        //        existingConteneur.UserCreate = conteneur.UserCreate;
        //        existingConteneur.UserUpdate = conteneur.UserUpdate;
        //        existingConteneur.DateCreate = conteneur.DateCreate;
        //        existingConteneur.DateUpdate = conteneur.DateUpdate;

        //        try
        //        {
        //            await _dataContext.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ConteneurExists(conteneur.Ref))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool ConteneurExists(int id)
        //    {
        //        return _dataContext.Conteneur.Any(e => e.Ref == id);
        //    }
        //}







        //[HttpGet]
        //    public async Task<IActionResult> Edit(int id)
        //    {
        //        var Product = await _dataContext.Conteneur.FindAsync(id);

        //        return View(Product);
        //    }
        //    [HttpPost]
        //    public async Task<IActionResult> Edit(Conteneur viewModel)
        //    {
        //        var customer = await _dataContext.Conteneur.FindAsync(viewModel.Ref);
        //        if (customer != null)
        //        {
        //            customer.CodeBarres = viewModel.CodeBarres;
        //            customer.Emplacement = viewModel.Emplacement;
        //            customer.DateMiseEnService = viewModel.DateMiseEnService;
        //            customer.PeriodiciteEtalonnage = viewModel.PeriodiciteEtalonnage;
        //            customer.DateDernierEtalonnage = viewModel.DateDernierEtalonnage;
        //            customer.DernierPoids = viewModel.DernierPoids;
        //            customer.Unite = viewModel.Unite;
        //            customer.UserCreate = viewModel.UserCreate;
        //            customer.UserUpdate = viewModel.UserUpdate;
        //            customer.DateCreate = viewModel.DateCreate;
        //            customer.DateUpdate = viewModel.DateUpdate;
        //            //product.categorie = viewModel.categorie;
        //            await _dataContext.SaveChangesAsync();

        //        }
        //        return RedirectToAction("List");
        //    }








        //public IActionResult ConfirmDelete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    var conteneurFromDB = _dataContext.Conteneurs.Find(id);
        //    if (conteneurFromDB == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(conteneurFromDB);
        //}

        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var conteneur = _dataContext.Conteneurs.Find(id);
        //        if (conteneur == null)
        //        {
        //            return NotFound();
        //        }

        //        _dataContext.Conteneurs.Remove(conteneur);
        //        _dataContext.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (ex) if needed
        //        return BadRequest(ex.Message);
        //    }
        //}
    }

}