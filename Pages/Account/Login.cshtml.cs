using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UsuarioService _usuarioService;

        public LoginModel(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [BindProperty]
        public string Correo { get; set; }
            = string.Empty;

        [BindProperty]
        public string Password { get; set; }
            = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var usuario =
                await _usuarioService
                    .ObtenerPorCorreo(Correo);

            if (usuario == null ||
                usuario.Password != Password)
            {
                ModelState.AddModelError(
                    "",
                    "Credenciales incorrectas");

                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.Name,
                    usuario.Nombre),

                new Claim(
                    ClaimTypes.Email,
                    usuario.Correo),

                new Claim(
                    ClaimTypes.Role,
                    usuario.Rol)
            };

            var identity =
                new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

            var principal =
                new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            if (usuario.Rol == "Admin")
            {
                return RedirectToPage(
                    "/Admin/Dashboard");
            }

            if (usuario.Rol == "Trabajador")
            {
                return RedirectToPage(
                    "/Worker/Dashboard");
            }

            if (usuario.Rol == "Cliente")
            {
                return RedirectToPage(
                    "/Client/Dashboard");
            }

            return RedirectToPage("/Index");
        }
    }
}