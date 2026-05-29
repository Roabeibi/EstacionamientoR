using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EstacionamientoR.Models
{
    public class Comentario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string NombreUsuario { get; set; }

        public string Mensaje { get; set; }

        public int Calificacion { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}