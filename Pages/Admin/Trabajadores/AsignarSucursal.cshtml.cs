using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Admin.Trabajadores
{
    public class AsignarSucursalModel : PageModel
    {
        private readonly MongoDbContext _context;

        public AsignarSucursalModel(MongoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; }
            = new();

        public List<Estacionamiento> Estacionamientos
            = new();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Usuario =
                await _context.Usuarios
                    .Find(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (Usuario == null)
                return RedirectToPage("Index");

            Estacionamientos =
                await _context.Estacionamientos
                    .Find(_ => true)
                    .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var sucursal =
                await _context.Estacionamientos
                    .Find(x => x.Id == Usuario.EstacionamientoId)
                    .FirstOrDefaultAsync();

            if (sucursal != null)
            {
                Usuario.NombreEstacionamiento =
                    sucursal.Nombre;
            }

            await _context.Usuarios
                .ReplaceOneAsync(
                    x => x.Id == Usuario.Id,
                    Usuario);

            return RedirectToPage("Index");
        }
    }
}