using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Vehiculos
{
    public class SalidaModel : PageModel
    {
        private readonly MongoDbContext _context;

        public SalidaModel(MongoDbContext context)
        {
            _context = context;
        }

        public Vehiculo Vehiculo { get; set; }
            = new();

        public decimal Total { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Vehiculo =
                await _context.Vehiculos
                    .Find(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (Vehiculo == null)
            {
                return RedirectToPage("/Worker/Dashboard");
            }

            Vehiculo.HoraSalida = DateTime.Now;

            // CALCULAR TIEMPO
            TimeSpan tiempo =
                Vehiculo.HoraSalida.Value
                - Vehiculo.HoraEntrada;

            // GUARDAR HORAS
            Vehiculo.TiempoEstancia =
                Math.Ceiling(tiempo.TotalHours);

            Vehiculo.Activo = false;

            decimal precioHora =
                Vehiculo.TipoVehiculo == "Moto"
                ? 10
                : 20;

            Total =
                (decimal)Vehiculo.TiempoEstancia
                * precioHora;

            await _context.Vehiculos.ReplaceOneAsync(
                x => x.Id == Vehiculo.Id,
                Vehiculo);

            // LIBERAR ESPACIO
            var espacio =
                await _context.Espacios
                    .Find(x =>
                        x.NumeroEspacio ==
                        Vehiculo.EspacioAsignado)
                    .FirstOrDefaultAsync();

            if (espacio != null)
            {
                espacio.Estado = "Disponible";

                await _context.Espacios.ReplaceOneAsync(
                    x => x.Id == espacio.Id,
                    espacio);
            }

            // GENERAR PAGO
            var pago = new Pago
            {
                VehiculoId = Vehiculo.Id,
                Total = Total,
                MetodoPago = "Efectivo",
                FechaPago = DateTime.Now
            };

            await _context.Pagos.InsertOneAsync(pago);

            return RedirectToPage(
                "/Pagos/Ticket",
                new
                {
                    id = pago.Id
                });
        }
    }
}