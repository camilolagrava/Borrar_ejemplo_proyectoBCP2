namespace Borrar_BCP_CL_3.Service.Iterfaces
{
    public interface IEmailService
    {
        Task<int> SendEmail(int id, string asunto, string contenido);
    }
}
