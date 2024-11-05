using ApplicationCore.DTOs;
using AutoMapper;

namespace ApplicationCore.Mappings
{
    public class Estudiante: Profile
    {
        public Estudiante()
        {
            CreateMap<EstudianteDTO, Domain.Entities.Estudiante>().ForMember(x => x.Id, y => y.Ignore());
        }
    }
}
