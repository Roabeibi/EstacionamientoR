using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstacionamientoR.Models;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Comentarios
{
    public class PublicoModel : PageModel
    {
        private readonly ComentarioService _comentarioService;

        public PublicoModel(ComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        [BindProperty]
        public Comentario Comentario { get; set; }

        public List<Comentario> Comentarios { get; set; }

        public async Task OnGetAsync()
        {
            Comentarios =
                await _comentarioService.ObtenerTodos();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _comentarioService.CrearComentario(Comentario);

            return RedirectToPage();
        }
    }
}