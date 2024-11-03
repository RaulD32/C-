using System;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Services
{
	public class EstudianteServices : IEstudianteService
	{
		private readonly ApplicationDbContext _context;

        public EstudianteServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<object>> GetEstudiantes()
        {
            Response<object> response = new();
            try
            {
                var results = _context.estudiantes.ToList();

                response.Succeeded = true;
                response.Message = "Estudiantes en lista, correcto";
                response.Result = results;

                return response;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = $"Hubo un error + {ex}";

                return response;
            }
        }

        public async Task<Response<object>> PostEstudiante(Estudiante estudiante)
        {
            Response<object> response = new();
            try
            {
                var results = await _context.estudiantes.AddAsync(estudiante);

                await _context.SaveChangesAsync();

                response.Succeeded = true;
                response.Message = "Se ha insertado al estudiante de forma exitosa";
                response.Result = estudiante;

                return response;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = $"Ha ocurrido un error: {ex}";
                response.Result = ex;

                return response;
            }
        }


        public async Task<Response<object>> DeleteEstudiante(int id)
        {
            Response<object> response = new();
            try
            {
                
                var estudiante = await _context.estudiantes.FindAsync(id);

                
                if (estudiante == null)
                {
                    response.Succeeded = false;
                    response.Message = "Estudiante no encontrado";
                    return response;
                }

                
                _context.estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();

                response.Succeeded = true;
                response.Message = "Estudiante eliminado con éxito";
                return response;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = $"Error al eliminar el estudiante: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<int>> UpdateEstudiante(EstudianteDto request)
        {
            var estudianteExistente = await _context.estudiantes.FindAsync(request.id);
            if (estudianteExistente == null)
            {
                return new Response<int>(0,"jugador no encontrado");
            }

            estudianteExistente.nombre = request.nombre;
            estudianteExistente.edad=request.edad;
            estudianteExistente.correo=request.correo;
            await _context.SaveChangesAsync();

            return new Response<int>(estudianteExistente.id, "estudiante actualizado");
        }

        public async Task<byte[]> GetPDF()
        {
            DevExpress.DataAccess.ObjectBinding.ObjectDataSource source = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();

            var report = new ApplicationCore.PDF.EstudiantesPDF();
            var estudiantes = await (from e in _context.estudiantes
                                     select new EstudianteDto
                                     {
                                         id= e.id,
                                         edad =e.edad,
                                         nombre = e.nombre,
                                         correo = e.correo
                                     }).ToListAsync();
            EstudiantesPDFDTO reportePdf = new EstudiantesPDFDTO();
            reportePdf.Fecha = DateTime.Now.ToString("dd/mm/yyyy");
            reportePdf.Hora = DateTime.Now.ToString("hh:mm");
            reportePdf.estudiantes = estudiantes;

            source.DataSource = reportePdf;
            report.DataSource = source;
            using (var memory = new MemoryStream())
            {
                await report.ExportToPdfAsync(memory);
                memory.Position = 0;
                return memory.ToArray();
            }
        }

    }
}

