using InMemoryCachingDemo.Models;
using InMemoryCachingDemo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryCachingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationRepository _repository;
        public LocationController(LocationRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _repository.GetCountriesAsync();
            return Ok(countries);
        }
        [HttpGet("states/{countryId}")]
        public async Task<IActionResult> GetStates(int countryId)
        {
            var states = await _repository.GetStatesAsync(countryId);
            return Ok(states);
        }
        [HttpGet("cities/{stateId}")]
        public async Task<IActionResult> GetCities(int stateId)
        {
            var cities = await _repository.GetCitiesAsync(stateId);
            return Ok(cities);
        }
        [HttpPost("countries")]
        public async Task<IActionResult> AddCountry(Country country)
        {
            try
            {
                await _repository.AddCountry(country);
                return Ok(); 
            }
            catch (Exception ex)
            {
                var customResponse = new
                {
                    Code = 500,
                    Message = "Internal Server Error",
                    ErrorMessage = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, customResponse);
            }
        }
        [HttpPut("countries/{id}")]
        public async Task<IActionResult> UpdateCountry(int id, Country country)
        {
            if (id != country.CountryId)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateCountry(country);
                return Ok(); 
            }
            catch (Exception ex)
            {
                if (!CountryExists(id))
                {
                    return NotFound("Country Does Not Exists");
                }
                else
                {
                    var customResponse = new
                    {
                        Code = 500,
                        Message = "Internal Server Error",
                        ErrorMessage = ex.Message
                    };
                    return StatusCode(StatusCodes.Status500InternalServerError, customResponse);
                }
            }
        }
        private bool CountryExists(int id)
        {
            return _repository.GetCountriesAsync().Result.Any(e => e.CountryId == id);
        }
    }
}
