using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Borrar_BCP_CL_3.Models;
using Borrar_BCP_CL_3.Service.Iterfaces;
using Borrar_BCP_CL_3.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using static Azure.Core.HttpHeader;


namespace Borrar_BCP_CL_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService _contratoService;
        private readonly IEmailService _emailService;
        private readonly IPdfService _pdfService;

        public ContratoController(IContratoService contratoService, IEmailService emailService, IPdfService pdfService)
        {
            _contratoService = contratoService;
            _emailService = emailService;
            _pdfService = pdfService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetAllContratos()
        {
            return await _contratoService.GetAllContrato();
        }

        [Authorize]
        [HttpPost("GetById")]
        public async Task<ActionResult<Contrato>> GetContrato(IdDto idDto)
        {
            var contrato = await _contratoService.GetContratoById(idDto.Id);

            if (contrato == null)
            {
                return NotFound();
            }

            return contrato;
        }

        [Authorize]
        [HttpPost("EditById")]
        public async Task<ActionResult<Contrato>> EditContrato(int id, ContratoDto contrato)
        {
            var a = await _contratoService.EditContratoById(id, contrato);

            if (a == null)
            {
                return NotFound();
            }

            return a;
        }

        [Authorize]
        [HttpPost("CreatePost")]
        public async Task<ActionResult<Contrato>> CreateContrato(ContratoDto contrato)
        {
            return await _contratoService.CreateContrato(contrato);
            //return Ok(a);
        }

        [Authorize]
        [HttpPost("DeleteContratoById")]
        public async Task<IActionResult> DeleteContrato(int id)
        {
            var b = await _contratoService.DeleteContrato(id);
            if (!b)
            {
                return NotFound();
            }

            return Ok("se borro con exito");
        }

        [Authorize]
        [HttpPost("GetRepost")]
        public async Task<ActionResult<AnswerResportDto>> GetRepost(DateDto date)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

            string nameId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;

            var asunto = "reporte";
            var contenido = "se hizo este repote amido uwu";
            var a = await _emailService.SendEmail(int.Parse(nameId), asunto, contenido);

            //_emailService.Develop(authorizationHeader);

            var list = await _contratoService.getReport(date.FechaInicial);

            return new AnswerResportDto { 
                Reports = list,
                cuantity = list.Count()
            };
        }

        [Authorize]
        [HttpPost("PdfCreate")]
        public async  Task<ActionResult> PdfCreate(IdDto id)
        {

            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

            string nameId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;

            var document = new PdfDocument();

            //string html = "<h1> Que se jodan, porfavor que funcione </h1>";

            var b = await _pdfService.createPdf(id.Id);

            if (!b)
            {
                return NotFound();
            }

            string filePath = "Utility/html/brave.html";
            string html = System.IO.File.ReadAllText(filePath);

            

            PdfGenerator.AddPdfPages(document, html, PageSize.A4);
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }

            string filename = "file.pdf";


            var asunto = "pdf";
            var contenido = "se imprimio un pdf uwu";
            var a = await _emailService.SendEmail(int.Parse(nameId), asunto, contenido);

            return File(response, "application/pdf", filename);

        }

    }
}
