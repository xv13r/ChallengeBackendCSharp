using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetValidUserAsync(User u);
        Task<User> GetUserByEmailAsync(String email);
        Task<User> GetUserByIdAsync(Guid userId);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}