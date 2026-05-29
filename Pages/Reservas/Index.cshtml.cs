using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Models;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Reservas
{
    public class IndexModel : PageModel
    {
        private readonly ReservaService _reservaService;

        public IndexModel(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        public List<Reserva> Reservas { get; set; }

        public async Task OnGetAsync()
        {
            Reservas = await _reservaService.ObtenerTodas();
        }
    }
}