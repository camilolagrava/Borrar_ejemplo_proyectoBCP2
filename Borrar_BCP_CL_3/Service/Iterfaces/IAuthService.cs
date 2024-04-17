using Borrar_BCP_CL_3.Models;
using Borrar_BCP_CL_3.Models.Dto;

namespace Borrar_BCP_CL_3.Service.Iterfaces
{
    public interface IAuthService
    {
        //Task<AuthorizationResponseDto> GiveToken(Usuario usuario);
        Task<AuthorizationResponseDto> Login(LoginDto formulario);
    }
}
