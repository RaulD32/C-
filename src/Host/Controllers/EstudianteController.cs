using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : Controller
    {
        private readonly IEstudianteService _service;
        public EstudianteController(IEstudianteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstudiantes()
        {
            var results = await _service.GetEstudiantes();

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> PostEstudiante([FromBody] Estudiante estudiante)
        {
            // Asegúrate de que el objeto estudiante no sea null
            if (estudiante == null)
            {
                return BadRequest("Estudiante no válido.");
            }

            var results = await _service.PostEstudiante(estudiante);

            if (results == null)
            {
                return StatusCode(500, "Error al guardar el estudiante."); // Maneja el caso en que no se pudo guardar
            }

            return Ok(results); // Retorna el estudiante creado
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiantes(int id)
        {
            var result = await _service.DeleteEstudiante(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }
        [HttpPost("update-estudiante")]
        public async Task<ActionResult<Response<int>>> UpdateEstudiante([FromBody] EstudianteDto request)
        {
            var result= await _service.UpdateEstudiante(request);
            return Ok(result);
        }
    }
}

