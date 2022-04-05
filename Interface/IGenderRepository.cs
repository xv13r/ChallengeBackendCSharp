using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface IGenderRepository : IRepositoryBase<Gender>
    {
        Task<IEnumerable<Gender>> GetAllGendersAsync();
        Task<Gender> GetGenderByIdAsync(Guid genderId);
        void CreateGender(Gender gender);
        void UpdateGender(Gender gender);
        void DeleteGender(Gender gender);
    }
}
