using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Repository.Contracts;

namespace Core.Implementation
{
    public class UserCoreImpl : IUserCore
    {
        public IUserRepository _userRepository;
        private IMapper _maper;

        public UserCoreImpl(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _maper = mapper;
        }

        public async Task<UserCore> GetUserByCredentials(string username, string password)
        {
            var user = await _userRepository.GetUserByCredentials(username, password);
            return _maper.Map<UserCore>(user);
        }
    }
}
