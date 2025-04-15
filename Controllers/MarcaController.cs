using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_AUTOSALONI.Data;
using MVC_AUTOSALONI.Models;
using MVC_AUTOSALONI.Models.Entities;

namespace MVC_AUTOSALONI.Controllers
{
    public class MarcaController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public MarcaController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //------------------------------------------------------------//
        //-------------------------LISTA------------------------------//
        //------------------------------------------------------------//

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var marca = await dbContext.Marca.ToListAsync();
            return View(marca);
        }

        //------------------------------------------------------------//
        //--------------------------ADD-------------------------------//
        //------------------------------------------------------------//

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMarcaViewModel viewModel)
        {
            var inputmarca = viewModel.Marc.ToString().ToLower();
            var marcaverifica = await dbContext.Marca.AnyAsync(m => m.marca.ToLower() == inputmarca);
            if (marcaverifica == true)
            {
                TempData["Errore"] = "Marca gia presente";
                return View(viewModel);
            }
            else
            {
                TempData["Errore"] = "";
            }

            var nmarca = new Marca
            {
                marca = viewModel.Marc.ToString()
            };

            await dbContext.Marca.AddAsync(nmarca);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Marca"); //da cambiare
        }


        //------------------------------------------------------------//
        //--------------------------EDIT------------------------------//
        //------------------------------------------------------------//

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var marca = await dbContext.Marca.FindAsync(id);
            return View(marca);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Marca viewModel)
        {
            var nmarca = await dbContext.Marca.FindAsync(viewModel.ID_Marca);
            if (nmarca != null)
            {
                nmarca.marca = viewModel.marca;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Marca");
        }

        //------------------------------------------------------------//
        //--------------------------DELETE----------------------------//
        //------------------------------------------------------------//

        [HttpPost]
        public async Task<ActionResult> Delete(Marca viewModel)
        {
            var marca = await dbContext.Marca
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID_Marca == viewModel.ID_Marca);
            if (marca is not null)
            {
                dbContext.Marca.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Marca");
        }
    }
}
