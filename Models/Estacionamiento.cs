using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EstacionamientoR.Models
{
    public class Estacionamiento
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        public string? Ciudad { get; set; }

        public string? Telefono { get; set; }

        public bool Activo { get; set; } = true;
    }
}