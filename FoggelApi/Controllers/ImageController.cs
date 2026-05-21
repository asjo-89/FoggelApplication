using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace FoggelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController(ImageService imageService) : ControllerBase
    {
        private readonly ImageService _imageService = imageService;

        [HttpGet]
        public async Task<IActionResult> GetImageUrlById(Guid? fileId)
        {
            if (fileId == Guid.Empty)
            {
                return BadRequest();
            }

            var image = await _imageService.GetImageByIdAsync(fileId);

            return File(image.FileData, "image/jpeg");
        }
    }
}
