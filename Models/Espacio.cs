using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EstacionamientoR.Models
{
    public class Espacio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
            = string.Empty;

        public string NumeroEspacio { get; set; }
            = string.Empty;

        public string Tipo { get; set; }
            = string.Empty;

        public string Estado { get; set; }
            = string.Empty;

    public string? EstacionamientoId { get; set; }
    }
}