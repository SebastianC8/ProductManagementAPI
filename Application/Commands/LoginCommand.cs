using Application.DTO;
using Core.Entities;
using MediatR;

namespace Application.Commands
{
    public record LoginCommand(LoginDTO loginDTO): IRequest<UserDTO>;

}
