using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Vehiculos
{
    public class IndexModel : PageModel
    {
        private readonly MongoDbContext _context;

        public IndexModel(MongoDbContext context)
        {
            _context = context;
        }

        public List<Vehiculo> Vehiculos { get; set; }
            = new();

        [BindProperty(SupportsGet = true)]
        public string BuscarPlaca { get; set; }
            = string.Empty;

        public async Task OnGetAsync()
        {
            var filter =
                Builders<Vehiculo>.Filter.Empty;

            if (!string.IsNullOrEmpty(BuscarPlaca))
            {
                filter =
                    Builders<Vehiculo>.Filter.Regex(
                        x => x.Placa,
                        new MongoDB.Bson.BsonRegularExpression(
                            BuscarPlaca,
                            "i"));
            }

            Vehiculos =
                await _context.Vehiculos
                    .Find(filter)
                    .SortByDescending(x => x.HoraEntrada)
                    .ToListAsync();
        }
    }
}