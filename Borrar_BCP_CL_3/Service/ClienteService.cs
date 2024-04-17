using Borrar_BCP_CL_3.Models;
using Borrar_BCP_CL_3.Models.Dto;
using Borrar_BCP_CL_3.Service.Iterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Borrar_BCP_CL_3.Service
{
    public class ClienteService : IClienteService
    {
        private readonly BcpCl2Context _context;

        public ClienteService(BcpCl2Context context)
        {
            _context = context;
        }

        public async Task<Cliente> CreateCliente(ClienteDto formulario)
        {
            var a = DtoToModel(formulario);
            _context.Clientes.Add(a);
            await _context.SaveChangesAsync();
            return a;
        }

        public async Task<bool> DeleteCliente(int id)
        {
            if (ClienteExists(id))
            {
                var a = await _context.Clientes.FindAsync(id);
                _context.Clientes.Remove(a);
                await _context.SaveChangesAsync();

                return true;

            }

            return false;
        }

        public async Task<Cliente> EditClienteById(int id, ClienteDto formulario)
        {
            if (ClienteExists(id))
            {
                var a = DtoToModel(formulario);
                a.ClienteId = id;
                _context.Entry(a).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return null;
        }

        public async  Task<ActionResult<IEnumerable<Cliente>>> GetAllCliente()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }

        private Cliente DtoToModel(ClienteDto formulario)
        {
            var a = new Cliente
            {
                Paterno = formulario.Paterno,
                Materno = formulario.Materno,
                Nombres = formulario.Nombres
            };

            return a;
        }

    }
}
