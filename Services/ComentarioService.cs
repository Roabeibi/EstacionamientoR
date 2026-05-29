using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Services
{
    public class ComentarioService
    {
        private readonly IMongoCollection<Comentario> _comentarios;

        public ComentarioService(MongoDbContext context)
        {
            _comentarios = context.Comentarios;
        }

        public async Task<List<Comentario>> ObtenerTodos()
        {
            return await _comentarios
                .Find(_ => true)
                .SortByDescending(x => x.Fecha)
                .ToListAsync();
        }

        public async Task CrearComentario(Comentario comentario)
        {
            comentario.Fecha = DateTime.Now;

            await _comentarios.InsertOneAsync(comentario);
        }
    }
}