using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface ICharacterRepository : IRepositoryBase<Character>
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync();
        Task<Character> GetCharacterByIdAsync(Guid characterId);
        Task<IEnumerable<Character>> GetCharacterByNameAsync(String name);
        Task<IEnumerable<Character>> GetCharacterByAgeAsync(int age);
        Task<IEnumerable<Character>> GetCharacterByMovieAsync(Guid movieId);
        Task<Character> GetCharacterWithDetailsAsync(Guid characterId);
        void CreateCharacter(Character character);
        void UpdateCharacter(Character character);
        void DeleteCharacter(Character character);
    }
}
