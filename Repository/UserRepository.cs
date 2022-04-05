using Entities;
using Entities.Models;
using Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<User> GetValidUserAsync(User u)
        {
            return await FindByCondition(user => user.Email.Equals(u.Email) && user.Password.Equals(u.Password))
                 .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAsync(String email)
        {
            return await FindByCondition(user => user.Email.Equals(email))
                 .FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await FindByCondition(user => user.Id.Equals(userId))
                .FirstOrDefaultAsync();
        }
        public void CreateUser(User user)
        {
            Create(user);
        }
        public void UpdateUser(User user)
        {
            Update(user);
        }
        public void DeleteUser(User user)
        {
            Delete(user);
        }
    }
}