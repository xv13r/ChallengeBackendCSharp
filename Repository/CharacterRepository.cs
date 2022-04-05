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
    public class CharacterRepository : RepositoryBase<Character>, ICharacterRepository
    {
        public CharacterRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await FindAll()
               .Include(c => c.Contents)
               .OrderBy(c => c.Name)
               .ToListAsync();
        }

        public async Task<IEnumerable<Character>> GetCharacterByNameAsync(String name)
        {
            return await FindByCondition(character => character.Name.Equals(name))
                 .ToListAsync();
        }

        public async Task<IEnumerable<Character>> GetCharacterByAgeAsync(int age)
        {
            return await FindByCondition(character => character.Age.Equals(age))
                 .ToListAsync();
        }

        public async Task<IEnumerable<Character>> GetCharacterByMovieAsync(Guid movieId)
        {
            return null;
            //return await FindByCondition(character => character.Contents.FirstOrDefault(m => m.Id == movieId));
        }

        public async Task<Character> GetCharacterByIdAsync(Guid characterId)
        {
            return await FindByCondition(character => character.Id.Equals(characterId))
                .FirstOrDefaultAsync();
        }
        public async Task<Character> GetCharacterWithDetailsAsync(Guid characterId)
        {
            return await FindByCondition(character => character.Id.Equals(characterId))
                .Include(c => c.Contents)
                .FirstOrDefaultAsync();
        }
        public void CreateCharacter(Character character)
        {
            Create(character);
        }
        public void UpdateCharacter(Character character)
        {
            Update(character);
        }
        public void DeleteCharacter(Character character)
        {
            Delete(character);
        }
    }
}