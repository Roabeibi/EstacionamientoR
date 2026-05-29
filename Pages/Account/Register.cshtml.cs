using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Models;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UsuarioService _usuarioService;

        public RegisterModel(UsuarioService usuarioService)
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
            var existe = await _usuarioService
                .ObtenerPorCorreo(Usuario.Correo);

            if (existe != null)
            {
                ModelState.AddModelError(
                    "",
                    "El correo ya existe");

                return Page();
            }

            // Seguridad extra:
            // aunque intenten modificar el HTML,
            // aquí forzamos Cliente
            Usuario.Rol = "Cliente";

            await _usuarioService
                .CrearUsuario(Usuario);

            return RedirectToPage("/Account/Login");
        }
    }
}