using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Admin.Trabajadores
{
    public class DeleteModel : PageModel
    {
        private readonly UsuarioService _usuarioService;

        public DeleteModel(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _usuarioService.EliminarUsuario(id);

            return RedirectToPage("/Admin/Trabajadores/Index");
        }
    }
}