using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ApplicationCore.Wrappers;
using ApplicationCore.Commands;
using ApplicationCore.DTOs;


namespace Host.Controllers
{
    [Route("api/estudiante")]
    [ApiController]
    public class EstudianteController: ControllerBase
    {

        private readonly IEstudianteService _estudianteService;
        public EstudianteController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;

        }

        [HttpGet("pdf")]
        public async Task<IActionResult> generarPDF()
        {
            var result = await _estudianteService.GetEstudiantePDF();
            return File(result, "application/pdf", "pdf.pdf");
        }

    }
}
