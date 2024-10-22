using System;
using ApplicationCore.Wrappers;
using Domain.Entities;

namespace ApplicationCore.Interfaces
{
	public interface IEstudianteService
	{
        Task<Response<object>> GetEstudiantes();
        Task<Response<object>> PostEstudiante(Estudiante estudiante);
        Task<Response<object>> DeleteEstudiante(int id);
    }
}

