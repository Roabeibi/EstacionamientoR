using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Models;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Admin.Trabajadores
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
            Usuario = new Usuario();
            Usuario.Rol = "Trabajador";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Usuario.Rol = "Trabajador";

            await _usuarioService.CrearUsuario(Usuario);

            return RedirectToPage("/Admin/Trabajadores/Index");
        }
    }
}