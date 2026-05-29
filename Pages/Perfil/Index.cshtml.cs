using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using System.Security.Claims;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Perfil
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MongoDbContext _context;

        public IndexModel(MongoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario UsuarioActual { get; set; }
            = new();

        public async Task OnGetAsync()
        {
            var correo =
                User.FindFirstValue(
                    ClaimTypes.Email);

            UsuarioActual =
                await _context.Usuarios
                    .Find(x => x.Correo == correo)
                    .FirstOrDefaultAsync()
                ?? new Usuario();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _context.Usuarios
                .ReplaceOneAsync(
                    x => x.Id == UsuarioActual.Id,
                    UsuarioActual);

            TempData["Mensaje"] =
                "Perfil actualizado";

            return RedirectToPage();
        }
    }
}