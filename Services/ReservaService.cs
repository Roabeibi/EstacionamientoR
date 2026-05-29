using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Services
{
    public class ReservaService
    {
        private readonly IMongoCollection<Reserva> _reservas;
        private readonly IMongoCollection<Espacio> _espacios;

        public ReservaService(MongoDbContext context)
        {
            _reservas = context.Reservas;
            _espacios = context.Espacios;
        }

        public async Task<List<Reserva>> ObtenerTodas()
        {
            return await _reservas
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task CrearReserva(Reserva reserva)
        {
            var espacioDisponible =
                await _espacios
                    .Find(x => x.Estado == "Disponible")
                    .FirstOrDefaultAsync();

            if (espacioDisponible != null)
            {
                reserva.NumeroEspacio =
                    espacioDisponible.NumeroEspacio;

                reserva.FechaEntrada =
                    DateTime.Now;

                reserva.Estado =
                    "Activa";

                await _reservas
                    .InsertOneAsync(reserva);

                var update =
                    Builders<Espacio>.Update
                        .Set(x => x.Estado, "Reservado");

                await _espacios.UpdateOneAsync(
                    x => x.Id == espacioDisponible.Id,
                    update);
            }
        }

        public async Task EliminarReserva(string id)
        {
            var reserva =
                await _reservas
                    .Find(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (reserva == null)
                return;

            var update =
                Builders<Espacio>.Update
                    .Set(x => x.Estado, "Disponible");

            await _espacios.UpdateOneAsync(
                x => x.NumeroEspacio ==
                     reserva.NumeroEspacio,
                update);

            await _reservas.DeleteOneAsync(
                x => x.Id == id);
        }
    }
}