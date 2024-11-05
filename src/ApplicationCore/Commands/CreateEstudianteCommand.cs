using ApplicationCore.DTOs;
using ApplicationCore.Wrappers;
using MediatR;


namespace ApplicationCore.Commands
{
    public class CreateEstudianteCommand: EstudianteDTO, IRequest<Response<int>>
    {
    }
}
