using Entities.Models;
using Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    public class CharacterController : Controller
    {
        private IRepositoryWrapper _repository;

        public CharacterController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [Route("api/characters")]
        [HttpGet]
        public async Task<IActionResult> GetAllCharacters()
        {
            try
            {
                var characters = await _repository.Character.GetAllCharactersAsync();
                return Ok(characters.Select(c => new { c.Image, c.Name })); // 3. Listado de Personajes
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/character/get/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> GetCharacterById(Guid id)
        {
            try
            {
                var character = await _repository.Character.GetCharacterByIdAsync(id);
                if (character == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(character);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/characters/{name=nombre}")]
        [HttpGet]
        public async Task<IActionResult> GetCharacterByName(String name)
        {
            try
            {
                var characters = await _repository.Character.GetCharacterByNameAsync(name);
                return Ok(characters.Select(c => new { c.Image, c.Name })); // 3. Listado de Personajes
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/characters/{age=edad}")]
        [HttpGet]
        public async Task<IActionResult> GetCharacterByAge(int age)
        {
            try
            {
                var characters = await _repository.Character.GetCharacterByAgeAsync(age);
                return Ok(characters.Select(c => new { c.Image, c.Name })); // 3. Listado de Personajes
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/characters/{movie=idMovie}")]
        [HttpGet]
        public async Task<IActionResult> GetCharacterByMovie(Guid movie)
        {
            try
            {
                var characters = await _repository.Character.GetCharacterByMovieAsync(movie);
                return Ok(characters.Select(c => new { c.Image, c.Name })); // 3. Listado de Personajes
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/character/create")]
        [HttpPost]
        public async Task<IActionResult> CreateCharacter([FromBody] Character character)
        {
            try
            {
                if (character == null)
                {
                    return BadRequest("Character object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repository.Character.CreateCharacter(character);
                await _repository.SaveAsync();
                return CreatedAtRoute("CharacterById", new { id = character.Id }, character);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/character/update/{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(Guid id, [FromBody] Character character)
        {
            try
            {
                if (character == null)
                {
                    return BadRequest("Character object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var ownerEntity = await _repository.Character.GetCharacterByIdAsync(id);
                if (ownerEntity == null)
                {
                    return NotFound();
                }
                _repository.Character.UpdateCharacter(character);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/character/delete/{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCharacter(Guid id)
        {
            try
            {
                var character = await _repository.Character.GetCharacterByIdAsync(id);
                if (character == null)
                {
                    return NotFound();
                }
                _repository.Character.DeleteCharacter(character);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
