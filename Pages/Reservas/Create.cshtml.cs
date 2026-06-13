using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Reservas
{
    public class CreateModel : PageModel
    {
        private readonly MongoDbContext _context;

        public CreateModel(MongoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reserva Reserva { get; set; }
            = new();

        public List<Espacio> Espacios { get; set; }
            = new();

        public List<Estacionamiento> Estacionamientos { get; set; }
            = new();

        public async Task OnGetAsync()
        {
            Estacionamientos =
                await _context.Estacionamientos
                    .Find(_ => true)
                    .ToListAsync();

            Espacios =
                await _context.Espacios
                    .Find(x => x.Estado == "Disponible")
                    .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Reserva.FechaEntrada =
                DateTime.Now;

            Reserva.Estado =
                "Activa";

            var estacionamiento =
                await _context.Estacionamientos
                    .Find(x =>
                        x.Id ==
                        Reserva.EstacionamientoId)
                    .FirstOrDefaultAsync();

            if (estacionamiento != null)
            {
                Reserva.NombreEstacionamiento =
                    estacionamiento.Nombre;
            }

            await _context.Reservas
                .InsertOneAsync(Reserva);

            var espacio =
                await _context.Espacios
                    .Find(x =>
                        x.NumeroEspacio ==
                        Reserva.NumeroEspacio)
                    .FirstOrDefaultAsync();

            if (espacio != null)
            {
                espacio.Estado =
                    "Reservado";

                await _context.Espacios
                    .ReplaceOneAsync(
                        x => x.Id == espacio.Id,
                        espacio);
            }

            return RedirectToPage("/Reservas/Index");
        }
    }
}