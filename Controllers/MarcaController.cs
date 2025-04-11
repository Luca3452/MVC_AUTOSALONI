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


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var marca = await dbContext.Marche.ToListAsync();
            return View(marca);
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMarcaViewModel viewModel)
        {
            var nmarca = new Marca
            {
               marca = viewModel.Marc.ToString()
            };
            await dbContext.Marche.AddAsync(nmarca);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Marca"); //da cambiare
        }
    }
}
