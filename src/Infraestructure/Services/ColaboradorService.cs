using ApplicationCore.DTOs;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Services;


namespace Infraestructure.Services
{
    public class ColaboradorService
    {
        private readonly ApplicationDbContext _context;

        public ColaboradorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ColaboradorDTO> CreateColaboradorAsync(ColaboradorCreateDTO colaboradorDTO)
        {
            var colaborador = new Colaborador
            {
                Nombre = colaboradorDTO.Nombre,
                Edad = colaboradorDTO.Edad,
                Birthday = colaboradorDTO.Birthday,
                EsProfesor = colaboradorDTO.EsProfesor,
                FechaCreacion = DateTime.Now
            };

            _context.Colaboradores.Add(colaborador);
            await _context.SaveChangesAsync();

            if (colaboradorDTO.EsProfesor)
            {
                var profesor = new Profesor
                {
                    FkColaborador = colaborador.Id,
                    Correo = colaboradorDTO.Correo,
                    Departamento = colaboradorDTO.Departamento
                };
                _context.Profesores.Add(profesor);
            }
            else
            {
                var administrativo = new Admin
                {
                    FkColaborador = colaborador.Id,
                    Correo = colaboradorDTO.Correo,
                    Puesto = colaboradorDTO.Puesto,
                    Nomina = colaboradorDTO.Nomina ?? 0
                };
                _context.Administrativos.Add(administrativo);
            }

            await _context.SaveChangesAsync();

            return new ColaboradorDTO
            {
                Id = colaborador.Id,
                Nombre = colaborador.Nombre,
                Edad = colaborador.Edad,
                Birthday = colaborador.Birthday,
                EsProfesor = colaborador.EsProfesor,
                FechaCreacion = colaborador.FechaCreacion,
                Correo = colaboradorDTO.Correo,
                Departamento = colaboradorDTO.Departamento,
                Puesto = colaboradorDTO.Puesto,
                Nomina = colaboradorDTO.Nomina
            };
        }

        public async Task<List<ColaboradorDTO>> ListColaboradoresAsync(bool? esProfesor, DateTime? fechaInicio, DateTime? fechaFinal)
        {
            var query = _context.Colaboradores.AsQueryable();

            if (esProfesor.HasValue)
                query = query.Where(c => c.EsProfesor == esProfesor.Value);

            if (fechaInicio.HasValue && fechaFinal.HasValue)
                query = query.Where(c => c.FechaCreacion >= fechaInicio.Value && c.FechaCreacion <= fechaFinal.Value);
            else if (fechaInicio.HasValue)
                query = query.Where(c => c.FechaCreacion >= fechaInicio.Value);
            else if (fechaFinal.HasValue)
                query = query.Where(c => c.FechaCreacion <= fechaFinal.Value);

            var colaboradores = await query
                .Select(c => new ColaboradorDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Edad = c.Edad,
                    Birthday = c.Birthday,
                    EsProfesor = c.EsProfesor,
                    FechaCreacion = c.FechaCreacion,
                    Correo = c.EsProfesor ? c.Profesor.Correo : c.Admin.Correo,
                    Departamento = c.EsProfesor ? c.Profesor.Departamento : null,
                    Puesto = c.EsProfesor ? null : c.Admin.Puesto,
                    Nomina = c.EsProfesor ? null : c.Admin.Nomina
                })
                .ToListAsync();

            return colaboradores;
        }
    }
}
