using Core.Entities;

namespace Core.Contracts
{
    public interface IUserCore
    {
        Task<UserCore> GetUserByCredentials(string username, string password);
    }
}
