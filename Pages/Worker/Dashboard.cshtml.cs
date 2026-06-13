using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;
using System.Security.Claims;

namespace EstacionamientoR.Pages.Worker
{
    public class DashboardModel : PageModel
    {
        private readonly MongoDbContext _context;

        public DashboardModel(MongoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vehiculo Vehiculo { get; set; }
            = new();

        public List<Vehiculo> VehiculosActivos { get; set; }
            = new();

        public List<Espacio> Espacios { get; set; }
            = new();

        public Usuario? UsuarioActual { get; set; }

        public async Task OnGetAsync()
        {
            var correo =
                User.FindFirst(ClaimTypes.Email)?.Value;

            UsuarioActual =
                await _context.Usuarios
                    .Find(x => x.Correo == correo)
                    .FirstOrDefaultAsync();

            if (UsuarioActual != null)
            {
                VehiculosActivos =
                    await _context.Vehiculos
                        .Find(x =>
                            x.HoraSalida == null &&
                            x.EstacionamientoId ==
                            UsuarioActual.EstacionamientoId)
                        .SortByDescending(x => x.HoraEntrada)
                        .Limit(10)
                        .ToListAsync();

                Espacios =
                    await _context.Espacios
                        .Find(x =>
                            x.EstacionamientoId ==
                            UsuarioActual.EstacionamientoId)
                        .Limit(8)
                        .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var correo =
                User.FindFirst(ClaimTypes.Email)?.Value;

            var usuario =
                await _context.Usuarios
                    .Find(x => x.Correo == correo)
                    .FirstOrDefaultAsync();

            if (usuario == null)
                return RedirectToPage();

            Vehiculo.HoraEntrada =
                DateTime.Now;

            Vehiculo.EstacionamientoId =
                usuario.EstacionamientoId;

            Vehiculo.NombreEstacionamiento =
                usuario.NombreEstacionamiento;

            var espacioDisponible =
                await _context.Espacios
                    .Find(x =>
                        x.Estado == "Disponible" &&
                        x.EstacionamientoId ==
                        usuario.EstacionamientoId)
                    .FirstOrDefaultAsync();

            if (espacioDisponible != null)
            {
                Vehiculo.EspacioAsignado =
                    espacioDisponible.NumeroEspacio;

                espacioDisponible.Estado =
                    "Ocupado";

                await _context.Espacios
                    .ReplaceOneAsync(
                        x => x.Id == espacioDisponible.Id,
                        espacioDisponible);
            }

            await _context.Vehiculos
                .InsertOneAsync(Vehiculo);

            return RedirectToPage();
        }
    }
}