using ApplicationCore.DTOs;
using ApplicationCore.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IEstudianteService
    {
        Task<Response<object>> CrearEstudiante(EstudianteDTO estudiante);
        Task<Response<object>> GetEstudiantes(int edad);
        Task<Response<object>> DeleteEstudiante(int id);
        Task<Response<object>> UpdateEstudiante(EstudianteDTO estudiante);
        Task<byte[]> GetEstudiantePDF();
    }
}
