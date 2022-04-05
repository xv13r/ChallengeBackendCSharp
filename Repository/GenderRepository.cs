using Entities;
using Entities.Models;
using Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class GenderRepository : RepositoryBase<Gender>, IGenderRepository
    {
        public GenderRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Gender>> GetAllGendersAsync()
        {
            return await FindAll()
               .OrderBy(c => c.Name)
               .ToListAsync();
        }
        public async Task<Gender> GetGenderByIdAsync(Guid genderId)
        {
            return await FindByCondition(gender => gender.Id.Equals(genderId))
                .FirstOrDefaultAsync();
        }

        public void CreateGender(Gender gender)
        {
            Create(gender);
        }
        public void UpdateGender(Gender gender)
        {
            Update(gender);
        }
        public void DeleteGender(Gender gender)
        {
            Delete(gender);
        }
    }
}