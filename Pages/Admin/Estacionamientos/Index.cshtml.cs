using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Admin.Estacionamientos
{
    public class IndexModel : PageModel
    {
        private readonly MongoDbContext _context;

        public IndexModel(MongoDbContext context)
        {
            _context = context;
        }

        public List<Estacionamiento> Estacionamientos
            = new();

        public async Task OnGetAsync()
        {
            Estacionamientos =
                await _context.Estacionamientos
                    .Find(_ => true)
                    .ToListAsync();
        }
    }
}