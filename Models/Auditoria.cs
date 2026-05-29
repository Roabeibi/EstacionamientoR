using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EstacionamientoR.Models
{
    public class Auditoria
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Usuario { get; set; }

        public string Accion { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}