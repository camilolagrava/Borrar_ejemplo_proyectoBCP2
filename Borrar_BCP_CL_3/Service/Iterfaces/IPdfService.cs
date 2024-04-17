namespace Borrar_BCP_CL_3.Service.Iterfaces
{
    public interface IPdfService
    {
        Task<Boolean> createPdf(int id);
    }
}
