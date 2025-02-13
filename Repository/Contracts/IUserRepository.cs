using Repository.Data.Entities;

namespace Repository.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByIdAsync(int id);

        Task<User> GetUserByCredentials(string username, string password);

        Task AddAsync(User user);

        Task UpdateAsync(User user);
    }
}
