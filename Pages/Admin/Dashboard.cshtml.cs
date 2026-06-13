using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly MongoDbContext _context;

        public DashboardModel(MongoDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? EstacionamientoId { get; set; }

        public string NombreSucursalSeleccionada { get; set; }
            = "Todas las sucursales";

        public List<Estacionamiento> Estacionamientos { get; set; }
            = new();

        public long VehiculosActivos { get; set; }

        public long EspaciosDisponibles { get; set; }

        public long TotalPagosHoy { get; set; }

        public decimal IngresosHoy { get; set; }

        public List<Vehiculo> VehiculosRecientes { get; set; }
            = new();

        public List<Pago> PagosRecientes { get; set; }
            = new();

        public List<Espacio> Espacios { get; set; }
            = new();

        public async Task OnGetAsync()
        {
            Estacionamientos =
                await _context.Estacionamientos
                    .Find(_ => true)
                    .ToListAsync();

            if (!string.IsNullOrEmpty(EstacionamientoId))
            {
                var sucursal =
                    await _context.Estacionamientos
                        .Find(x => x.Id == EstacionamientoId)
                        .FirstOrDefaultAsync();

                if (sucursal != null)
                {
                    NombreSucursalSeleccionada =
                        sucursal.Nombre;
                }

                VehiculosActivos =
                    await _context.Vehiculos
                        .CountDocumentsAsync(x =>
                            x.HoraSalida == null &&
                            x.EstacionamientoId ==
                            EstacionamientoId);

                EspaciosDisponibles =
                    await _context.Espacios
                        .CountDocumentsAsync(x =>
                            x.EstacionamientoId ==
                            EstacionamientoId &&
                            x.Estado == "Disponible");

                var pagosHoy =
                    await _context.Pagos
                        .Find(x =>
                            x.EstacionamientoId ==
                            EstacionamientoId &&
                            x.FechaPago.Date ==
                            DateTime.Today)
                        .ToListAsync();

                TotalPagosHoy =
                    pagosHoy.Count;

                IngresosHoy =
                    pagosHoy.Sum(x => x.Total);

                VehiculosRecientes =
                    await _context.Vehiculos
                        .Find(x =>
                            x.EstacionamientoId ==
                            EstacionamientoId)
                        .SortByDescending(x => x.HoraEntrada)
                        .Limit(5)
                        .ToListAsync();

                PagosRecientes =
                    await _context.Pagos
                        .Find(x =>
                            x.EstacionamientoId ==
                            EstacionamientoId)
                        .SortByDescending(x => x.FechaPago)
                        .Limit(5)
                        .ToListAsync();

                Espacios =
                    await _context.Espacios
                        .Find(x =>
                            x.EstacionamientoId ==
                            EstacionamientoId)
                        .Limit(8)
                        .ToListAsync();
            }
            else
            {
                VehiculosActivos =
                    await _context.Vehiculos
                        .CountDocumentsAsync(
                            x => x.HoraSalida == null);

                EspaciosDisponibles =
                    await _context.Espacios
                        .CountDocumentsAsync(
                            x => x.Estado == "Disponible");

                TotalPagosHoy =
                    await _context.Pagos
                        .CountDocumentsAsync(
                            x => x.FechaPago.Date ==
                            DateTime.Today);

                var pagosHoy =
                    await _context.Pagos
                        .Find(x =>
                            x.FechaPago.Date ==
                            DateTime.Today)
                        .ToListAsync();

                IngresosHoy =
                    pagosHoy.Sum(x => x.Total);

                VehiculosRecientes =
                    await _context.Vehiculos
                        .Find(_ => true)
                        .SortByDescending(x => x.HoraEntrada)
                        .Limit(5)
                        .ToListAsync();

                PagosRecientes =
                    await _context.Pagos
                        .Find(_ => true)
                        .SortByDescending(x => x.FechaPago)
                        .Limit(5)
                        .ToListAsync();

                Espacios =
                    await _context.Espacios
                        .Find(_ => true)
                        .Limit(8)
                        .ToListAsync();
            }
        }
    }
}