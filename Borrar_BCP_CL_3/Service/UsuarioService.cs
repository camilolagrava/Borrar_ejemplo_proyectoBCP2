using Borrar_BCP_CL_3.Models;
using Borrar_BCP_CL_3.Models.Dto;
using Borrar_BCP_CL_3.Service.Iterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Borrar_BCP_CL_3.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly BcpCl2Context _context;

        public UsuarioService(BcpCl2Context context)
        {
            _context = context;
        }


        public async Task<Usuario> CreateUsuario(UsuarioDto contrato)
        {
            var a = DtoToModel(contrato);
            _context.Usuarios.Add(a);
            await _context.SaveChangesAsync();
            return a;
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            if (UsuarioExists(id))
            {
                var a = await _context.Usuarios.FindAsync(id);
                _context.Usuarios.Remove(a);
                await _context.SaveChangesAsync();

                return true;

            }

            return false;


        }

        public async Task<Usuario> EditUsuarioById(int id, UsuarioDto formulario)
        {
            if (UsuarioExists(id))
            {
                var a = DtoToModel(formulario);
                a.UsuarioId = id;
                _context.Entry(a).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return null;
        }

        public async Task<ActionResult<IEnumerable<Usuario>>> GetAllUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _context.Usuarios.FindAsync(id);

        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }

        private Usuario DtoToModel(UsuarioDto formulario)
        {
            var a = new Usuario
            {
               Nombre = formulario.Nombre,
               Email = formulario.Email,
               Contrasenna = formulario.Contrasenna
            };

            return a;
        }
    }
}
