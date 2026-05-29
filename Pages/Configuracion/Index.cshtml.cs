using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Configuracion
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly MongoDbContext _context;

        public IndexModel(MongoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EstacionamientoR.Models.Configuracion ConfigSistema { get; set; }
            = new();

        public async Task OnGetAsync()
        {
            ConfigSistema =
                await _context.Configuracion
                    .Find(_ => true)
                    .FirstOrDefaultAsync();

            if (ConfigSistema == null)
            {
                ConfigSistema =
                    new EstacionamientoR.Models.Configuracion();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existente =
                await _context.Configuracion
                    .Find(_ => true)
                    .FirstOrDefaultAsync();

            if (existente == null)
            {
                await _context.Configuracion
                    .InsertOneAsync(ConfigSistema);
            }
            else
            {
                ConfigSistema.Id = existente.Id;

                await _context.Configuracion
                    .ReplaceOneAsync(
                        x => x.Id == existente.Id,
                        ConfigSistema);
            }

            TempData["Mensaje"] =
                "Configuración actualizada correctamente";

            return RedirectToPage();
        }
    }
}