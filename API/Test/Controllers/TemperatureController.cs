using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Test.Services;

namespace Test.Controllers
{
    //[EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : BaseController<TemperatureController>
    {
        private readonly ITemperatureService _temperatureService;

        public TemperatureController(ITemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
        }

        /// <summary>
        /// Get the temperature list beyond 84.3 c
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status204NoContent), ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("getTemperatures")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var temperatures = await _temperatureService.LoadData();
                if (temperatures?.Count == 0)
                    return NoContent();

                _temperatureService.SaveData(temperatures); //Save data in file
                return Ok(temperatures);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while getting a temperature: {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}