using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace FoggelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController(BirdsService birdsService) : ControllerBase
    {
        private readonly BirdsService _birdsService = birdsService;
               
        [HttpGet]
        public async Task<IActionResult> GetAllBirds()
        {
            var birdsResult = await _birdsService.GetAllBirdsAsync();
            if (birdsResult.Success)
            {
                return Ok(birdsResult);
            }
            else if (birdsResult.Success
                && (birdsResult.Model == null || birdsResult.Model.Count == 0))
            {
                return NotFound(birdsResult);
            }
            else
            {
                return BadRequest(birdsResult);
            }
        }
    }
}
