using Borrar_BCP_CL_3.Models.Dto;
using Borrar_BCP_CL_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Borrar_BCP_CL_3.Service.Iterfaces
{
    public interface IUsuarioService
    {
        Task<ActionResult<IEnumerable<Usuario>>> GetAllUsuarios();
        Task<Usuario> GetUsuarioById(int id);
        Task<Usuario> EditUsuarioById(int id, UsuarioDto contrato);
        Task<Usuario> CreateUsuario(UsuarioDto contrato);
        Task<Boolean> DeleteUsuario(int id);
    }
}
