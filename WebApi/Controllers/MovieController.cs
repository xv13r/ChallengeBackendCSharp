using Entities.Models;
using Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    public class MovieController : Controller
    {
        private IRepositoryWrapper _repository;

        public MovieController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [Route("api/movies")]
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                var movies = await _repository.Movie.GetAllMoviesAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/movie/get/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> GetMovieById(Guid id)
        {
            try
            {
                var movie = await _repository.Movie.GetMovieByIdAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(movie);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/movie/{name=nombre}")]
        [HttpGet]
        public async Task<IActionResult> GetMovieByName(String name)
        {
            try
            {
                var movies = await _repository.Movie.GetMovieByNameAsync(name);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/movie/{gender=idGenero}")]
        [HttpGet]
        public async Task<IActionResult> GetMovieByAge(Guid gender)
        {
            try
            {
                var movies = await _repository.Movie.GetMovieByGenderAsync(gender);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/movie/{order=sort}")]
        [HttpGet]
        public async Task<IActionResult> GetMovieSort(String sort)
        {
            try
            {
                var movies = await _repository.Movie.GetMovieOrderAsync(sort);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/movie/create")]
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] Movie movie)
        {
            try
            {
                if (movie == null)
                {
                    return BadRequest("Movie object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repository.Movie.CreateMovie(movie);
                await _repository.SaveAsync();
                return CreatedAtRoute("MovieById", new { id = movie.Id }, movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/movie/update/{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> UpdateMovie(Guid id, [FromBody] Movie movie)
        {
            try
            {
                if (movie == null)
                {
                    return BadRequest("Movie object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var ownerEntity = await _repository.Movie.GetMovieByIdAsync(id);
                if (ownerEntity == null)
                {
                    return NotFound();
                }
                _repository.Movie.UpdateMovie(movie);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/movie/delete/{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            try
            {
                var movie = await _repository.Movie.GetMovieByIdAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }
                _repository.Movie.DeleteMovie(movie);
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
