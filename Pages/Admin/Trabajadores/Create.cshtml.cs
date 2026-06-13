using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Admin.Trabajadores
{
    public class CreateModel : PageModel
    {
        private readonly UsuarioService _usuarioService;
        private readonly MongoDbContext _context;

        public CreateModel(
            UsuarioService usuarioService,
            MongoDbContext context)
        {
            _usuarioService = usuarioService;
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; }
            = new();

        public List<Estacionamiento> Estacionamientos { get; set; }
            = new();

        public async Task OnGetAsync()
        {
            Usuario.Rol = "Trabajador";

            Estacionamientos =
                await _context.Estacionamientos
                    .Find(_ => true)
                    .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Usuario.Rol = "Trabajador";

            var estacionamiento =
                await _context.Estacionamientos
                    .Find(x =>
                        x.Id == Usuario.EstacionamientoId)
                    .FirstOrDefaultAsync();

            if (estacionamiento != null)
            {
                Usuario.NombreEstacionamiento =
                    estacionamiento.Nombre;
            }

            await _usuarioService.CrearUsuario(Usuario);

            return RedirectToPage(
                "/Admin/Trabajadores/Index");
        }
    }
}