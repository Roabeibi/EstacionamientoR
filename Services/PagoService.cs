using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Services
{
    public class PagoService
    {
        private readonly IMongoCollection<Pago> _pagos;

        private readonly IMongoCollection<Vehiculo> _vehiculos;

        private readonly IMongoCollection<Configuracion> _configuracion;

        public PagoService(MongoDbContext context)
        {
            _pagos = context.Pagos;

            _vehiculos = context.Vehiculos;

            _configuracion = context.Configuracion;
        }

        public async Task<Pago> GenerarPago(string vehiculoId)
        {
            var vehiculo =
                await _vehiculos
                    .Find(x => x.Id == vehiculoId)
                    .FirstOrDefaultAsync();

            if (vehiculo == null)
                return null;

            // =========================
            // OBTENER CONFIGURACION
            // =========================

            var config =
                await _configuracion
                    .Find(_ => true)
                    .FirstOrDefaultAsync();

            // =========================
            // CALCULAR HORAS
            // =========================

            var horaSalida =
                DateTime.Now;

            var horas =
                Math.Ceiling(
                    (horaSalida -
                     vehiculo.HoraEntrada)
                    .TotalHours);

            if (horas < 1)
                horas = 1;

            // =========================
            // TARIFAS DINAMICAS
            // =========================

            decimal tarifa = 20;

            if (config != null)
            {
                tarifa = config.TarifaHora;
            }

            // Tarifa especial para motos

            if (vehiculo.TipoVehiculo == "Moto")
            {
                tarifa =
                    config?.TarifaMoto ?? 10;
            }

            // =========================
            // TOTAL
            // =========================

            var total =
                (decimal)horas * tarifa;

            // =========================
            // CREAR PAGO
            // =========================

            var pago = new Pago
            {
                VehiculoId =
                    vehiculo.Id,

                Total =
                    total,

                MetodoPago =
                    "Pendiente",

                FechaPago =
                    DateTime.Now
            };

            // =========================
            // GUARDAR PAGO
            // =========================

            await _pagos
                .InsertOneAsync(pago);

            return pago;
        }

        // =========================
        // OBTENER PAGOS
        // =========================

        public async Task<List<Pago>> ObtenerPagos()
        {
            return await _pagos
                .Find(_ => true)
                .ToListAsync();
        }
    }
}