using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Espacios
{
    public class EditModel : PageModel
    {
        private readonly MongoDbContext _context;

        public EditModel(MongoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Espacio Espacio { get; set; }
            = new();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Espacio =
                await _context.Espacios
                    .Find(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (Espacio == null)
            {
                return RedirectToPage("/Espacios/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _context.Espacios.ReplaceOneAsync(
                x => x.Id == Espacio.Id,
                Espacio);

            return RedirectToPage("/Espacios/Index");
        }
    }
}