using MongoDB.Driver;
using EstacionamientoR.Data;
using EstacionamientoR.Models;

namespace EstacionamientoR.Services
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        public UsuarioService(MongoDbContext context)
        {
            _usuarios = context.Usuarios;
        }

        public async Task<List<Usuario>> ObtenerTodos()
        {
            return await _usuarios
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task<Usuario> ObtenerPorCorreo(string correo)
        {
            return await _usuarios
                .Find(x => x.Correo == correo)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Usuario>> ObtenerTrabajadores()
        {
            return await _usuarios
                .Find(x => x.Rol == "Trabajador")
                .ToListAsync();
        }

        public async Task CrearUsuario(Usuario usuario)
        {
            usuario.FechaRegistro = DateTime.Now;

            await _usuarios.InsertOneAsync(usuario);
        }

        public async Task EliminarUsuario(string id)
        {
            await _usuarios.DeleteOneAsync(x => x.Id == id);
        }
    }
}