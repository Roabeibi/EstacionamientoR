using MongoDB.Driver;
using EstacionamientoR.Models;

namespace EstacionamientoR.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

public IMongoCollection<Estacionamiento> Estacionamientos =>
    _database.GetCollection<Estacionamiento>("Estacionamientos");
        public IMongoCollection<Usuario> Usuarios =>
            _database.GetCollection<Usuario>("usuarios");

        public IMongoCollection<Vehiculo> Vehiculos =>
            _database.GetCollection<Vehiculo>("vehiculos");

        public IMongoCollection<Espacio> Espacios =>
            _database.GetCollection<Espacio>("espacios");

        public IMongoCollection<Pago> Pagos =>
            _database.GetCollection<Pago>("pagos");

        public IMongoCollection<Reserva> Reservas =>
            _database.GetCollection<Reserva>("reservas");

        public IMongoCollection<Comentario> Comentarios =>
            _database.GetCollection<Comentario>("comentarios");

        public IMongoCollection<Configuracion> Configuracion =>
            _database.GetCollection<Configuracion>("configuracion");

        public IMongoCollection<Reporte> Reportes =>
            _database.GetCollection<Reporte>("reportes");

        public IMongoCollection<Auditoria> Auditoria =>
            _database.GetCollection<Auditoria>("auditoria");
    }
}