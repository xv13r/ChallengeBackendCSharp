using Entities.Models;
using Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    public class SeriesController : Controller
    {
        private IRepositoryWrapper _repository;

        public SeriesController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [Route("api/series")]
        [HttpGet]
        public async Task<IActionResult> GetAllSeries()
        {
            try
            {
                var series = await _repository.Series.GetAllSeriesAsync();
                return Ok(series);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/series/get/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> GetSeriesById(Guid id)
        {
            try
            {
                var series = await _repository.Series.GetSeriesByIdAsync(id);
                if (series == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(series);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/series/{name=nombre}")]
        [HttpGet]
        public async Task<IActionResult> GetSeriesByName(String name)
        {
            try
            {
                var series = await _repository.Series.GetSeriesByNameAsync(name);
                return Ok(series);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/series/{gender=idGenero}")]
        [HttpGet]
        public async Task<IActionResult> GetSeriesByAge(Guid gender)
        {
            try
            {
                var series = await _repository.Series.GetSeriesByGenderAsync(gender);
                return Ok(series);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/series/{order=sort}")]
        [HttpGet]
        public async Task<IActionResult> GetSeriesSort(String sort)
        {
            try
            {
                var series = await _repository.Series.GetSeriesOrderAsync(sort);
                return Ok(series);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/series/create")]
        [HttpPost]
        public async Task<IActionResult> CreateSeries([FromBody] Series series)
        {
            try
            {
                if (series == null)
                {
                    return BadRequest("Series object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repository.Series.CreateSeries(series);
                await _repository.SaveAsync();
                return CreatedAtRoute("SeriesById", new { id = series.Id }, series);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/series/update/{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> UpdateSeries(Guid id, [FromBody] Series series)
        {
            try
            {
                if (series == null)
                {
                    return BadRequest("Series object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var ownerEntity = await _repository.Series.GetSeriesByIdAsync(id);
                if (ownerEntity == null)
                {
                    return NotFound();
                }
                _repository.Series.UpdateSeries(series);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/series/delete/{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSeries(Guid id)
        {
            try
            {
                var series = await _repository.Series.GetSeriesByIdAsync(id);
                if (series == null)
                {
                    return NotFound();
                }
                _repository.Series.DeleteSeries(series);
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
