using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Admin.Espacios
{
    public class CreateModel : PageModel
    {
        private readonly MongoDbContext _context;

        public CreateModel(MongoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Espacio Espacio { get; set; }
            = new();

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _context.Espacios
                .InsertOneAsync(Espacio);

            return RedirectToPage("Index");
        }
    }
}