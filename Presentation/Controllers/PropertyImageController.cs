using Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/propertyimages/{propertyId}/images")]
	public class PropertyImageController : ControllerBase
	{
		private readonly IServiceManager serviceManager;

		public PropertyImageController(IServiceManager serviceManager)
		{
			this.serviceManager = serviceManager;
		}


		[HttpGet]
		public async Task<IActionResult> GetAllImages(int propertyId, CancellationToken cancellationToken)
		{
			var response = await serviceManager.PropertyImageService.GetAll(propertyId, cancellationToken);
			return Ok(response);
		}


		[HttpPost("upload")]
		public async Task<IActionResult> UploadImage(int propertyId, [FromBody] PropertyImageCreateDto imageDto, CancellationToken cancellationToken)
		{
			
			var response = await serviceManager.PropertyImageService.Create(propertyId, imageDto, cancellationToken);
			return Ok(response);
		}


		[HttpGet("{imageId}")]
		public async Task<IActionResult> GetImageById(int propertyId, int imageId, CancellationToken cancellationToken)
		{
			var response = await serviceManager.PropertyImageService.GetById(propertyId, imageId, cancellationToken);
			return Ok(response);
		}

        [HttpPut("update")]
        public async Task<IActionResult> UpdateImages(int propertyId, [FromBody] IEnumerable<PropertyImageUpdateDto> imageDtos, CancellationToken cancellationToken)
        {
            var response = await serviceManager.PropertyImageService.Update(propertyId, imageDtos, cancellationToken);

            if (response.IsSuccess)
            {
                return Ok(new { Message = response.Message });
            }
            else
            {
                return BadRequest(new { Message = response.Message });
            }
        }


        [HttpDelete("delete/{imageId}")]
		public async Task<IActionResult> DeleteImage(int imageId, CancellationToken cancellationToken)
		{
			var response = await serviceManager.PropertyImageService.Delete(imageId, cancellationToken);

			if (!response.IsSuccess)
			{
				return NotFound(new { Message = response.Message }); 
			}

			return Ok(new { Message = response.Message }); 
		}



	}
}
