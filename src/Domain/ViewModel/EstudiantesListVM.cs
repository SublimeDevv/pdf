
using Domain.Entities;

namespace Domain.ViewModel
{
    public class EstudiantesListVM
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public List<Estudiante> Estudiante { get; set; }
    }
}
