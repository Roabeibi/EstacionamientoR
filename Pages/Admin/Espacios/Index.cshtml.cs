using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Admin.Espacios
{
    public class IndexModel : PageModel
    {
        private readonly MongoDbContext _context;

        public IndexModel(MongoDbContext context)
        {
            _context = context;
        }

        public List<Espacio> Espacios { get; set; }
            = new();

        public async Task OnGetAsync()
        {
            Espacios =
                await _context.Espacios
                    .Find(_ => true)
                    .ToListAsync();
        }
    }
}