using Domain.Entities;

namespace ApplicationCore.DTOs
{
    public class EstudiantesDTO
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public List<Estudiante> Estudiante { get; set; }
    }
}
