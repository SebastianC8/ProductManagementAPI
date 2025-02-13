using Repository.Contracts;
using Repository.Data.Entities;

namespace Repository.Implementation
{
    public class UserRepositoryImpl : IUserRepository
    {
        public List<User> Users { get; set; }

        public UserRepositoryImpl()
        {
            Users = new List<User>
            {
                new User {
                    Id = 1, Fullname = "Sebastián Corrales", Email = "sebasscan@gmail.com", Username = "scorrales", Password = "456", Role = "Admin"
                },
                new User {
                    Id = 2, Fullname = "Macnelly Torres", Email = "mac@gmail.com", Username = "mtoress", Password = "123", Role = "User"
                },
                new User {
                    Id = 3, Fullname = "Edwin Cardona", Email = "e.cardona@gmail.com", Username = "ecardona", Password = "789", Role = "Boss"
                },
                new User {
                    Id = 4, Fullname = "Lionel Messi", Email = "leomessi@gmail.com", Username = "lmessi", Password = "159", Role = "User"
                },
                new User {
                    Id = 3, Fullname = "Andrea Guzmán", Email = "aguzman@gmail.com", Username = "aguzman", Password = "753", Role = "Client"
                }
            };
        }

        public Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByCredentials(string username, string password)
        {
            var user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
