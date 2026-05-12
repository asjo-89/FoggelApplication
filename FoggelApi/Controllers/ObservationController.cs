
using FoggelApi.MiddleWare;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.FormModels;
using Services.Models;
using Services.Services;

namespace FoggelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservationController(ObservationService service) : ControllerBase
    {
        private readonly ObservationService _service = service;


        [HttpGet]
        public async Task<IActionResult> GetAllObservations()
        {
            var listResult = await _service.GetAllObservationsAsync();

            if(!listResult.Success || listResult.Model == null)
            {
                return NotFound(listResult.Message);
            }

            return Ok(listResult.Model);
        }

        [HttpPost]
        public async Task<IActionResult> AddObservation([FromBody] ObservationFormModel form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.AddObservationAsync(form);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message });    
        }        
    }
}
