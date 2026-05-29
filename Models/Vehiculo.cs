using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EstacionamientoR.Models
{
    public class Vehiculo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Placa { get; set; }

        public string? TipoVehiculo { get; set; }

        public string? Modelo { get; set; }

        public string? Color { get; set; }

        public DateTime HoraEntrada { get; set; }

        public DateTime? HoraSalida { get; set; }

        // NUEVO
        public double TiempoEstancia { get; set; }

        public string? EspacioAsignado { get; set; }

        public bool Activo { get; set; } = true;
    }
}