using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EstacionamientoR.Models
{
    public class Configuracion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; } = "";

        public string NombreEstacionamiento { get; set; } = "";

        public decimal TarifaHora { get; set; }

        public decimal TarifaMoto { get; set; }

        public decimal TarifaDia { get; set; }

        public string HorarioApertura { get; set; } = "";

        public string HorarioCierre { get; set; } = "";

        public string Direccion { get; set; } = "";

        public string Telefono { get; set; } = "";

        public string Correo { get; set; } = "";
    }
}