using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Services
{
    public class ReporteService
    {
        private readonly IMongoCollection<Pago> _pagos;

        public ReporteService(MongoDbContext context)
        {
            _pagos = context.Pagos;
        }

        public async Task<decimal> ObtenerTotalIngresos()
        {
            var pagos = await _pagos
                .Find(_ => true)
                .ToListAsync();

            return pagos.Sum(x => x.Total);
        }

        public async Task<List<Pago>> ObtenerPagos()
        {
            return await _pagos
                .Find(_ => true)
                .ToListAsync();
        }
    }
}