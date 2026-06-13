using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EstacionamientoR.Models
{
    public class Pago
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string VehiculoId { get; set; }
            = string.Empty;

        public decimal Total { get; set; }

        public string MetodoPago { get; set; }
            = "Efectivo";

        public DateTime FechaPago { get; set; }
            = DateTime.Now;

        public string Folio { get; set; }
            = Guid.NewGuid()
                .ToString()
                .Substring(0, 8)
                .ToUpper();

        public string? EstacionamientoId { get; set; }
    }
}