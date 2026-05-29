using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Models;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Admin.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly UsuarioService _usuarioService;

        public IndexModel(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public List<Usuario> Usuarios { get; set; }

        public async Task OnGetAsync()
        {
            Usuarios = await _usuarioService.ObtenerTodos();
        }
    }
}