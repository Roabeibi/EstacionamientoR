using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Reservas
{
    public class DeleteModel : PageModel
    {
        private readonly ReservaService _reservaService;

        public DeleteModel(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _reservaService.EliminarReserva(id);

            return RedirectToPage("/Reservas/Index");
        }
    }
}