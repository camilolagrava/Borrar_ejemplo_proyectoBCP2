using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

using Borrar_BCP_CL_3.Service.Iterfaces;

namespace Borrar_BCP_CL_3.Service
{
    public class PdfService : IPdfService
    {
        private readonly IConfiguration _config;
        private readonly IContratoService _contratoService;
        private readonly IClienteService _clienteService;

        public PdfService(IConfiguration config, IContratoService contratoService, IClienteService clienteService)
        {
            _config = config;
            _contratoService = contratoService;
            _clienteService = clienteService;
        }

        public async Task<bool> createPdf(int id)
        {

            var contrato = await _contratoService.GetContratoById(id);
            if (contrato == null)
            {
                return false;
            }

            var cliente = await _clienteService.GetClienteById(contrato.ClienteId);

            string filePath = "Utility/inicial/brave.html";
            string html = System.IO.File.ReadAllText(filePath);

            var valoresVariables = new Dictionary<string, string>
        {
            { "REGIO", "RegioEjemplo" },
            { "REPRESENTANTELEGAL", cliente.Nombres+" "+cliente.Paterno+" "+cliente.Materno },
            { "TESTIMONIO", contrato.Testimonio },
            { "FECHAÍNICIAL", contrato.FechaInicial.ToString() },
            { "FECHAFINAL", contrato.FechaFinal.ToString() },
            { "FECHATESTIMONIO", contrato.FechaTestimonio.ToString() },
            { "NUMERONOTARIA", contrato.NumeroNotaria.ToString() },
            { "BANCO", "Banco Generico" },
            { "ARRENDADOR", contrato.NombresProvedor},
            { "DIRECCIÓN1", contrato.DireccionAmbiente },
            { "CIUDAD", contrato.Ciudad },
            { "SUPERFICIE", contrato.Superficie.ToString() },
            { "NUMERODIRECCION", contrato.NumeroDireccion },
            { "IMPORTE LITERAL", contrato.Importe.ToString() },
            { "FEOHAFINALARRENDAMIENTO", contrato.FechaFinalArrendamiento.ToString() },
            { "CUENTA", contrato.Cuenta },
            { "NUMEROMESES", contrato.Meses.ToString() },
            { "FECHAINCIALARRENDAMIENTO", contrato.FechaInicialArrendamiento.ToString() },
            { "FECHAFINALARRENDAMIENTO", contrato.FechaFinalArrendamiento.ToString() },
            { "FECHATENOR", contrato.FechaTenor.ToString() },
            { "MES", contrato.Mes },
            { "ANIO", contrato.Anno }
        };

            foreach (var kvp in valoresVariables)
            {
                html = html.Replace("{" + kvp.Key + "}", kvp.Value);
            }

            // Guardar el HTML en un archivo
            File.WriteAllText("Utility/html/brave.html", html);


            return true;
           
        }
    }
}
