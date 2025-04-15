using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_AUTOSALONI.Data;
using MVC_AUTOSALONI.Models;
using MVC_AUTOSALONI.Models.Entities;

namespace MVC_AUTOSALONI.Controllers
{
    public class AutoController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AutoController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //------------------------------------------------------------//
        //--------------------------ADD-------------------------------//
        //------------------------------------------------------------//

        [HttpGet]
        public IActionResult Add()
        {
            PopolaDDLMarca();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAutoViewModel viewModel)
        {
            var auto = new Auto
            {
                ID_Modello = viewModel.ID_Modello,
                data_acquisto = viewModel.data_acquisto,
                targa = viewModel.targa,
                prezzo_offerto = viewModel.prezzo_offerto
            };
            await dbContext.Auto.AddAsync(auto);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Auto");
        }

        //------------------------------------------------------------//
        //----------------------POPOLA DDL MARCA----------------------//
        //------------------------------------------------------------//

        public void PopolaDDLMarca()
        {
            IEnumerable<SelectListItem> ListaMarche = dbContext.Marca.Select(i => new SelectListItem
            {
                Text = i.marca,
                Value = i.ID_Marca.ToString()
            });
           
            ViewBag.MarcaDDL = ListaMarche;
        }

        [HttpGet]
        public JsonResult PopolaDDLModelli(Guid ID_marca)
        {
            var modelli = dbContext.Modello
                .Where(m => m.ID_marca == ID_marca)
                .Select(m => new
                {
                    value = m.ID_modello,
                    text = m.modello
                })
                .ToList();

            return Json(modelli);
        }


        //------------------------------------------------------------//
        //-------------------------LISTA------------------------------//
        //------------------------------------------------------------//


        //[HttpGet]
        //public async Task<ActionResult> List()
        //{
        //    var auto = await dbContext.Auto.ToListAsync();
        //    foreach (var riga in auto)
        //    {
        //        riga = dbContext.Marca.FirstOrDefault(u => u.ID_Marca == riga.ID_marca);
                
        //    }
        //    return View(modelli);
        //}


        //------------------------------------------------------------//
        //--------------------------EDIT------------------------------//
        //------------------------------------------------------------//

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var modello = await dbContext.Modello.FindAsync(id);
            PopolaDDLMarca();
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
