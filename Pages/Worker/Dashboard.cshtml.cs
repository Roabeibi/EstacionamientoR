using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

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

        public async Task OnGetAsync()
        {
            VehiculosActivos =
                await _context.Vehiculos
                    .Find(x => x.HoraSalida == null)
                    .SortByDescending(x => x.HoraEntrada)
                    .Limit(10)
                    .ToListAsync();

            Espacios =
                await _context.Espacios
                    .Find(_ => true)
                    .Limit(8)
                    .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Vehiculo.HoraEntrada =
                DateTime.Now;

            var espacioDisponible =
                await _context.Espacios
                    .Find(x =>
                        x.Estado == "Disponible")
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