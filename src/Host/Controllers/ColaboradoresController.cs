using ApplicationCore.DTOs;
using Domain.Entities;
using Infraestructure.Persistence;
using Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradoresController : ControllerBase
    {
        private readonly ColaboradorService _colaboradorService;

        public ColaboradoresController(ColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }


        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] bool? esProfesor, [FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFinal)
        {
            var colaboradores = await _colaboradorService.ListColaboradoresAsync(esProfesor, fechaInicio, fechaFinal);
            return Ok(colaboradores);
        }

        

        
    }
}
