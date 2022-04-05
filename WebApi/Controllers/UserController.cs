using Entities.Models;
using Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private IRepositoryWrapper _repository;

        public UserController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [Route("api/users")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _repository.User.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/user/get/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _repository.User.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/user/create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repository.User.CreateUser(user);
                await _repository.SaveAsync();
                //return CreatedAtRoute("api/user/get", new { id = user.Id }, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message + " " + ex.InnerException);
            }
        }

        [Route("api/user/update/{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var ownerEntity = await _repository.User.GetUserByIdAsync(id);
                if (ownerEntity == null)
                {
                    return NotFound();
                }
                _repository.User.UpdateUser(user);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/user/delete/{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _repository.User.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                _repository.User.DeleteUser(user);
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
