using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EstacionamientoR.Models
{
    [BsonIgnoreExtraElements]
    public class Reserva
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UsuarioCliente { get; set; }
            = string.Empty;

        public string NumeroEspacio { get; set; }
            = string.Empty;

        public DateTime FechaEntrada { get; set; }

        public DateTime FechaSalida { get; set; }

        public string Estado { get; set; }
            = "Activa";
        
        public string? EstacionamientoId { get; set; }

        public string? NombreEstacionamiento { get; set; }
    }
}