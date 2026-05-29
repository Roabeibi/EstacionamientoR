using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Models;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Admin.Trabajadores
{
    public class IndexModel : PageModel
    {
        private readonly UsuarioService _usuarioService;

        public IndexModel(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public List<Usuario> Trabajadores { get; set; }

        public async Task OnGetAsync()
        {
            Trabajadores =
                await _usuarioService.ObtenerTrabajadores();
        }
    }
}