using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using System.Security.Claims;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Client
{
    [Authorize(Roles = "Cliente")]
    public class DashboardModel : PageModel
    {
        private readonly MongoDbContext _context;

        public DashboardModel(MongoDbContext context)
        {
            _context = context;
        }

        public int TotalReservas { get; set; }

        public int EspaciosDisponibles { get; set; }

        public List<Reserva> Reservas { get; set; }
            = new();

        public async Task OnGetAsync()
        {
            var nombreUsuario =
                User.Identity?.Name;

            if (!string.IsNullOrEmpty(nombreUsuario))
            {
                Reservas =
                    await _context.Reservas
                        .Find(x =>
                            x.UsuarioCliente ==
                            nombreUsuario)
                        .ToListAsync();

                TotalReservas =
                    Reservas.Count;
            }

            EspaciosDisponibles =
                (int)await _context.Espacios
                    .CountDocumentsAsync(x =>
                        x.Estado == "Disponible");
        }
    }
}