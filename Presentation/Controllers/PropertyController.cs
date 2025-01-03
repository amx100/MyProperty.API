﻿using Contract;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/properties")]
	public class PropertyController(IServiceManager serviceManager) : ControllerBase
	{

		[HttpGet]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			var response = await serviceManager.PropertyService.GetAll(cancellationToken);
			return Ok(response);
		}

		// Ova metoda kreira svojstvo
		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] PropertyCreateDto propertyDto, CancellationToken cancellationToken)
		{
			var response = await serviceManager.PropertyService.Create(propertyDto, cancellationToken);
			return Ok(response);
		}

		// Ova metoda dobijaju svojstvo po ID-u, uključujući slike
		[HttpGet("{propertyId}")]
		public async Task<IActionResult> GetById(int propertyId, CancellationToken cancellationToken)
		{
			var response = await serviceManager.PropertyService.GetById(propertyId, cancellationToken);
			return Ok(response);
		}

		// Ova metoda briše svojstvo po ID-u
		[HttpDelete("delete/{propertyId}")]
		public async Task<IActionResult> DeleteProperty(int propertyId, CancellationToken cancellationToken)
		{
			var response = await serviceManager.PropertyService.Delete(propertyId, cancellationToken);

			if (response.IsSuccess)
			{
				return Ok(new { Message = "Property deleted successfully!" });
			}

			return BadRequest(new { Message = response.Message });
		}

		[HttpPut("update/{propertyId}")]
		public async Task<IActionResult> UpdateProperty(int propertyId, [FromBody] PropertyUpdateDto propertyDto, CancellationToken cancellationToken)
		{
			var response = await serviceManager.PropertyService.Update(propertyId, propertyDto, cancellationToken);
			return Ok(response);
		}
	}
}
