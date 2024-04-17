using Borrar_BCP_CL_3.Service.Iterfaces;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace Borrar_BCP_CL_3.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IUsuarioService _usuarioService;

        public EmailService(IConfiguration config, IUsuarioService usuarioService)
        {
            _config = config;
            _usuarioService = usuarioService;
        }

        private void _SendEmail(string para, string asunto, string contenido)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse(para));
            email.Subject = asunto;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = contenido
            };

            using var smtp = new SmtpClient();
            smtp.Connect(
                _config.GetSection("Email:Host").Value,
                Convert.ToInt32(_config.GetSection("Email:Port").Value),
                SecureSocketOptions.StartTls
            );

            smtp.Authenticate(_config.GetSection("Email:UserName").Value, _config.GetSection("Email:PassWord").Value);

            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public async Task<int> SendEmail(int id, string asunto, string contenido)
        {
            var user = await  _usuarioService.GetUsuarioById(id);
            _SendEmail(user.Email, asunto, contenido);
            return 0;
        }

    }
}
