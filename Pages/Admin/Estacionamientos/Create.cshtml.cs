using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Admin.Estacionamientos
{
    public class CreateModel : PageModel
    {
        private readonly MongoDbContext _context;

        public CreateModel(MongoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Estacionamiento Estacionamiento { get; set; }
            = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _context.Estacionamientos
                .InsertOneAsync(Estacionamiento);

            return RedirectToPage("Index");
        }
    }
}