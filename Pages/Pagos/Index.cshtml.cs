using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Pagos
{
    public class IndexModel : PageModel
    {
        private readonly MongoDbContext _context;

        public IndexModel(MongoDbContext context)
        {
            _context = context;
        }

        public List<Pago> Pagos { get; set; }
            = new();

        public async Task OnGetAsync()
        {
            Pagos =
                await _context.Pagos
                    .Find(_ => true)
                    .SortByDescending(x => x.FechaPago)
                    .ToListAsync();
        }
    }
}