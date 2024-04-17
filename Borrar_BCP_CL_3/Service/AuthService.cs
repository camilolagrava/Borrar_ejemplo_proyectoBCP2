using Borrar_BCP_CL_3.Models;
using Borrar_BCP_CL_3.Models.Dto;
using Borrar_BCP_CL_3.Service.Iterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Borrar_BCP_CL_3.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly BcpCl2Context _context;
        public AuthService(IConfiguration configuration, BcpCl2Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string GenerateToken(string id)
        {
            var key = _configuration.GetValue<string>("JwtSetting:key");
            var keyBites = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(
                new Claim(ClaimTypes.NameIdentifier, id)
                );

            var tokenCredential = new SigningCredentials(
                new SymmetricSecurityKey(keyBites),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = tokenCredential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;

        }

        public async Task<AuthorizationResponseDto> GiveToken(Usuario user)
        {
            if (user == null)
            {
                return await Task.FromResult<AuthorizationResponseDto>(new AuthorizationResponseDto()
                {
                    Token = "",
                    Result = false,
                    Msg = "Ese usuario no existe"
                }
                );
            }

            string token = GenerateToken(user.UsuarioId.ToString());

            return new AuthorizationResponseDto()
            {
                Token = token,
                Result = true,
                Msg = "OK"
            };
        }

        private async Task<Usuario> UserPassLogin(string email, string pass)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Contrasenna == pass );
        }

        public async Task<AuthorizationResponseDto> Login(LoginDto formulario)
        {
            var user = await UserPassLogin(formulario.email, formulario.pass);
            return await GiveToken(user);
   
        }


    }
}
