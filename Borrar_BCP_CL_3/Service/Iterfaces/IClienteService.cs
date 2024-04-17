using Borrar_BCP_CL_3.Models.Dto;
using Borrar_BCP_CL_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Borrar_BCP_CL_3.Service.Iterfaces
{
    public interface IClienteService
    {
        Task<ActionResult<IEnumerable<Cliente>>> GetAllCliente();
        Task<Cliente> GetClienteById(int id);
        Task<Cliente> EditClienteById(int id, ClienteDto contrato);
        Task<Cliente> CreateCliente(ClienteDto contrato);
        Task<Boolean> DeleteCliente(int id);
    }
}
