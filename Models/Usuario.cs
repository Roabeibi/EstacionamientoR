using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EstacionamientoR.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Password { get; set; }

        public string Rol { get; set; } // Admin, Trabajador, Cliente

        public string Telefono { get; set; } = "";

public string FotoPerfil { get; set; } = "";

public DateTime FechaRegistro { get; set; }
    = DateTime.Now;

public string Descripcion { get; set; } = "";
    }
}