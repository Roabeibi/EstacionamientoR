using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Pages.Pagos
{
    public class TicketModel : PageModel
    {
        private readonly MongoDbContext _context;

        public TicketModel(MongoDbContext context)
        {
            _context = context;
        }

        public Pago Pago { get; set; }
            = new();

        public Vehiculo Vehiculo { get; set; }
            = new();

        public async Task OnGetAsync(string id)
        {
            Pago =
                await _context.Pagos
                    .Find(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (Pago != null)
            {
                Vehiculo =
                    await _context.Vehiculos
                        .Find(x => x.Id == Pago.VehiculoId)
                        .FirstOrDefaultAsync();
            }
        }
    }
}