using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Models;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Vehiculos
{
    public class CreateModel : PageModel
    {
        private readonly VehiculoService _vehiculoService;

        public CreateModel(VehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        [BindProperty]
        public Vehiculo Vehiculo { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _vehiculoService.RegistrarEntrada(Vehiculo);

            return RedirectToPage("/Vehiculos/Index");
        }
    }
}