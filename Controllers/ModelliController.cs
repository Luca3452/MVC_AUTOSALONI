using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_AUTOSALONI.Data;
using MVC_AUTOSALONI.Models;
using MVC_AUTOSALONI.Models.Entities;

namespace MVC_AUTOSALONI.Controllers
{
    public class ModelliController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ModelliController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //------------------------------------------------------------//
        //--------------------------ADD-------------------------------//
        //------------------------------------------------------------//

        [HttpGet]
        public IActionResult Add()
        {
            PopolaDDL();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddModelloViewModel viewModel)
        {
            var modello = new Modello
            {
                modello = viewModel.modello,
                ID_marca = viewModel.ID_marca
            };
            await dbContext.Modello.AddAsync(modello);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Modelli");
        }

        //------------------------------------------------------------//
        //----------------------POPOLA DDL MARCA----------------------//
        //------------------------------------------------------------//

        public void PopolaDDL()
        {
            IEnumerable<SelectListItem> ListaMarche = dbContext.Marca.Select(i => new SelectListItem
            {
                Text = i.marca,
                Value = i.ID_Marca.ToString()
            });
            ViewBag.MarcaDDL = ListaMarche;
        }

        //------------------------------------------------------------//
        //-------------------------LISTA------------------------------//
        //------------------------------------------------------------//


        [HttpGet]
        public async Task<ActionResult> List()
        {
            var modelli = await dbContext.Modello.ToListAsync();
            foreach (var riga in modelli)
            {
                riga.Marca = dbContext.Marca.FirstOrDefault(u => u.ID_Marca == riga.ID_marca);
            }
            return View(modelli);
        }


        //------------------------------------------------------------//
        //--------------------------EDIT------------------------------//
        //------------------------------------------------------------//

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var modello = await dbContext.Modello.FindAsync(id);
            PopolaDDL();
            return View(modello);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Modello viewModel)
        {
            var model = await dbContext.Modello.FindAsync(viewModel.ID_modello);
            if (model is not null)
            {
                model.ID_modello = viewModel.ID_modello;
                model.modello = viewModel.modello;
                model.ID_marca = viewModel.ID_marca;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Modelli");
        }

        //------------------------------------------------------------//
        //--------------------------DELETE----------------------------//
        //------------------------------------------------------------//

        [HttpPost]
        public async Task<ActionResult> Delete(Modello viewModel)
        {
            var model = await dbContext.Modello
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID_modello == viewModel.ID_modello);
            if (model is not null)
            {
                dbContext.Modello.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Modelli");
        }
    }
}
