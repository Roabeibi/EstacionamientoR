using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Services
{
    public class VehiculoService
    {
        private readonly IMongoCollection<Vehiculo> _vehiculos;
        private readonly IMongoCollection<Espacio> _espacios;

        public VehiculoService(MongoDbContext context)
        {
            _vehiculos = context.Vehiculos;
            _espacios = context.Espacios;
        }

        public async Task<List<Vehiculo>> ObtenerActivos()
        {
            return await _vehiculos
                .Find(x => x.HoraSalida == null)
                .ToListAsync();
        }

        public async Task RegistrarEntrada(Vehiculo vehiculo)
        {
            var espacioDisponible = await _espacios
                .Find(x =>
                    x.Estado == "Disponible" &&
                    x.Tipo == vehiculo.TipoVehiculo)
                .FirstOrDefaultAsync();

            if (espacioDisponible != null)
            {
                vehiculo.HoraEntrada = DateTime.Now;
                vehiculo.EspacioAsignado =
                    espacioDisponible.NumeroEspacio;

                await _vehiculos.InsertOneAsync(vehiculo);

                var update = Builders<Espacio>.Update
                    .Set(x => x.Estado, "Ocupado");

                await _espacios.UpdateOneAsync(
                    x => x.Id == espacioDisponible.Id,
                    update);
            }
        }

        public async Task RegistrarSalida(string id)
        {
            var vehiculo = await _vehiculos
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (vehiculo == null)
                return;

            var updateVehiculo = Builders<Vehiculo>.Update
                .Set(x => x.HoraSalida, DateTime.Now);

            await _vehiculos.UpdateOneAsync(
                x => x.Id == id,
                updateVehiculo);

            var updateEspacio = Builders<Espacio>.Update
                .Set(x => x.Estado, "Disponible");

            await _espacios.UpdateOneAsync(
                x => x.NumeroEspacio ==
                     vehiculo.EspacioAsignado,
                updateEspacio);
        }
    }
}