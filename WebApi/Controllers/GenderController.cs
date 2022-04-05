using Entities.Models;
using Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    public class GenderController : Controller
    {
        private IRepositoryWrapper _repository;

        public GenderController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [Route("api/genders")]
        [HttpGet]
        public async Task<IActionResult> GetAllGenders()
        {
            try
            {
                var genders = await _repository.Gender.GetAllGendersAsync();
                return Ok(genders.Select(c => new { c.Name }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/gender/get/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> GetGenderById(Guid id)
        {
            try
            {
                var gender = await _repository.Gender.GetGenderByIdAsync(id);
                if (gender == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(gender);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/gender/create")]
        [HttpPost]
        public async Task<IActionResult> CreateGender([FromBody] Gender gender)
        {
            try
            {
                if (gender == null)
                {
                    return BadRequest("Gender object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repository.Gender.CreateGender(gender);
                await _repository.SaveAsync();
                //return CreatedAtRoute("api/gender/get", new { id = gender.Id }, gender);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message + " " + ex.InnerException);
            }
        }

        [Route("api/gender/update/{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> UpdateGender(Guid id, [FromBody] Gender gender)
        {
            try
            {
                if (gender == null)
                {
                    return BadRequest("Gender object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var ownerEntity = await _repository.Gender.GetGenderByIdAsync(id);
                if (ownerEntity == null)
                {
                    return NotFound();
                }
                _repository.Gender.UpdateGender(gender);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/gender/delete/{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteGender(Guid id)
        {
            try
            {
                var gender = await _repository.Gender.GetGenderByIdAsync(id);
                if (gender == null)
                {
                    return NotFound();
                }
                _repository.Gender.DeleteGender(gender);
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
