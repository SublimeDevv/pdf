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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EstudianteDTO request)
        {
            var result = await _estudianteService.CrearEstudiante(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEstudiante([FromQuery] int edad)
        {
            var result = await _estudianteService.GetEstudiantes(edad);
            return Ok(result);
        }

        [HttpDelete("deleteEstudiante/{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            var result = await _estudianteService.DeleteEstudiante(id);
            return Ok(result);
        }

        [HttpPut("updateEstudiante")]
        public async Task<IActionResult> UpdateEstudiante([FromBody] EstudianteDTO request)
        {
            var result = await _estudianteService.UpdateEstudiante(request);
            return Ok(result);
        }

        [HttpGet("generarPDFEstudiantes")]
        public async Task<IActionResult> GetEstudiantePDF()
        {
            var result = await _estudianteService.GetEstudiantePDF();
            return File(result, "application/pdf", "reporte.pdf");
        }

    }
}
