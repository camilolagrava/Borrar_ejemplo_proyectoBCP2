using Borrar_BCP_CL_3.Models;
using Borrar_BCP_CL_3.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Borrar_BCP_CL_3.Service.Iterfaces
{
    public interface IContratoService
    {
        Task<ActionResult< IEnumerable<Contrato>>> GetAllContrato();
        Task<Contrato> GetContratoById(int id);
        Task<Contrato> EditContratoById(int id, ContratoDto contrato);
        Task<Contrato> CreateContrato(ContratoDto contrato);
        Task<Boolean> DeleteContrato(int id);
        Task<ICollection<ReporteDto>> getReport(DateOnly date);

    }
}
