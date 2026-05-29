using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Espacios
{
    public class DeleteModel : PageModel
    {
        private readonly MongoDbContext _context;

        public DeleteModel(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _context.Espacios.DeleteOneAsync(
                x => x.Id == id);

            return RedirectToPage("/Espacios/Index");
        }
    }
}