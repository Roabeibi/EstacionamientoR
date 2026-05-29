using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Espacios
{
    public class ObtenerEspaciosModel : PageModel
    {
        private readonly MongoDbContext _context;

        public ObtenerEspaciosModel(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var espacios =
                await _context.Espacios
                    .Find(_ => true)
                    .ToListAsync();

            return new JsonResult(espacios);
        }
    }
}