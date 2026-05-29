using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Models;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Admin.Usuarios
{
    public class CreateModel : PageModel
    {
        private readonly UsuarioService _usuarioService;

        public CreateModel(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [BindProperty]
        public Usuario Usuario { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _usuarioService.CrearUsuario(Usuario);

            return RedirectToPage("/Admin/Usuarios/Index");
        }
    }
}