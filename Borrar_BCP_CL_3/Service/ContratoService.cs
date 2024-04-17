using Borrar_BCP_CL_3.Models;
using Borrar_BCP_CL_3.Models.Dto;
using Borrar_BCP_CL_3.Service.Iterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Borrar_BCP_CL_3.Service
{
    public class ContratoService : IContratoService
    {
        private readonly BcpCl2Context _context;
        private readonly IClienteService _clienteService;

        public ContratoService(BcpCl2Context context , IClienteService clienteService)
        {
            _context = context;
            _clienteService = clienteService;
        }

        public async Task<Contrato> CreateContrato(ContratoDto contrato)
        {
            var a = DtoToModel(contrato);
            _context.Contratos.Add(a);
            await _context.SaveChangesAsync();
            return a;



        }

        public async Task<bool> DeleteContrato(int id)
        {
            if (ContratoExists(id))
            {
                var a = await _context.Contratos.FindAsync(id);
                _context.Contratos.Remove(a);
                await _context.SaveChangesAsync();

                return true;
                
            }

            return false;


        }

        public async Task<Contrato> EditContratoById(int id, ContratoDto formulario)
        {
            if (ContratoExists(id))
            {
                var a = DtoToModel(formulario);
                a.ContratoId = id;
                _context.Entry(a).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return null;
        }

        public async Task< ActionResult< IEnumerable<Contrato>>> GetAllContrato()
        {
            return await _context.Contratos.ToListAsync();
        }

        public async Task<Contrato> GetContratoById(int id)
        {
            return  await _context.Contratos.FindAsync(id);

        }

        public async Task< ICollection <ReporteDto>> getReport(DateOnly date)
        {
            var contratos = await _context.Contratos.Where(c => c.FechaInicial == date).ToListAsync();

            List<ReporteDto> preList = new List<ReporteDto>();

            foreach (var c in contratos) {

                preList.Add(await contratoToReporteDto (c));

            }

            return preList;
        }

        private bool ContratoExists(int id)
        {
            return _context.Contratos.Any(e => e.ContratoId == id);
        }

        private Contrato DtoToModel(ContratoDto formulario)
        {
            var a = new Contrato
            {
                CodigoContrato = formulario.CodigoContrato,
                ClienteId = formulario.ClienteId,
                Testimonio = formulario.Testimonio,
                FechaFinal = formulario.FechaFinal,
                FechaTestimonio = formulario.FechaTestimonio,
                NumeroNotaria = formulario.NumeroNotaria,
                PaternoProvedor = formulario.PaternoProvedor,
                MaternoProvedor = formulario.MaternoProvedor,
                NombresProvedor = formulario.NombresProvedor,
                DocumentoProvedor = formulario.DocumentoProvedor,
                Domicilio = formulario.Domicilio,
                DireccionAmbiente = formulario.DireccionAmbiente,
                Ciudad = formulario.Ciudad,
                Superficie = formulario.Superficie,
                NumeroDireccion = formulario.NumeroDireccion,
                Importe = formulario.Importe,
                Literal = formulario.Literal,
                Cuenta = formulario.Cuenta,
                Meses = formulario.Meses,
                FechaInicialArrendamiento = formulario.FechaInicialArrendamiento,
                FechaFinalArrendamiento = formulario.FechaFinalArrendamiento,
                FechaTenor = formulario.FechaTenor,
                Mes = formulario.Mes,
                Anno = formulario.Anno,
            };

            return a;
        }

        private async Task<ReporteDto> contratoToReporteDto(Contrato contrato)
        {
            ReporteDto respuesta = new ReporteDto();
            var cliente = await _clienteService.GetClienteById(contrato.ClienteId);

            respuesta.ContratoID = contrato.ContratoId;
            respuesta.Fecha = contrato.FechaFinal;
            respuesta.RepresentanteLegal = cliente.Nombres + " " + cliente.Paterno + " " + cliente.Materno;
            respuesta.NombreProvedor = contrato.NombresProvedor + " " + contrato.PaternoProvedor;
            respuesta.DocumentoProvedor = contrato.DocumentoProvedor;
            respuesta.Domicilio = contrato.Domicilio;
            respuesta.Direccion = contrato.DireccionAmbiente;
            respuesta.Ciudad = contrato.Ciudad;
            respuesta.Superficie = contrato.Superficie;
            respuesta.Importe = contrato.Importe;
            respuesta.Cuenta = contrato.Cuenta;

            return respuesta;
        }


    }
}
