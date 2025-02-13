using Application.DTO;
using AutoMapper;
using Core.Contracts;
using MediatR;

namespace Application.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserDTO>
    {
        public IUserCore _userCore;
        public IMapper _mapper;

        public LoginCommandHandler(IUserCore userCore, IMapper mapper)
        {
            _userCore = userCore;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userCore.GetUserByCredentials(request.loginDTO.Username, request.loginDTO.Password);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
