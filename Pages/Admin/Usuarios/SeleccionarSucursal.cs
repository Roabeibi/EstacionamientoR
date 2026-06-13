using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using System.Security.Claims;
using EstacionamientoR.Data;

namespace EstacionamientoR.Pages.Client
{
    public class SeleccionarSucursalModel : PageModel
    {
        private readonly MongoDbContext _context;

        public SeleccionarSucursalModel(MongoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string EstacionamientoId { get; set; }
            = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            var correo =
                User.FindFirst(ClaimTypes.Email)?.Value;

            var usuario =
                await _context.Usuarios
                    .Find(x => x.Correo == correo)
                    .FirstOrDefaultAsync();

            var estacionamiento =
                await _context.Estacionamientos
                    .Find(x => x.Id == EstacionamientoId)
                    .FirstOrDefaultAsync();

            if (usuario == null || estacionamiento == null)
                return Page();

            usuario.EstacionamientoFavoritoId =
                estacionamiento.Id;

            usuario.EstacionamientoFavoritoNombre =
                estacionamiento.Nombre;

            await _context.Usuarios
                .ReplaceOneAsync(
                    x => x.Id == usuario.Id,
                    usuario);

            return RedirectToPage("/Client/Dashboard");
        }
    }
}