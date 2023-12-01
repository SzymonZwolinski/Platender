﻿using Microsoft.AspNetCore.Mvc;
using Platender.Application.Messages;
using Platender.Application.Providers;

namespace Platender.Api.Controllers
{
	[Route("[Controller]")]
	public class PlateController
	{
		private readonly IPlateProvider _plateProvider;

		public PlateController(IPlateProvider plateProvider)
		{
			_plateProvider = plateProvider;
		}

		[HttpGet]
		public async Task<IActionResult> GetPlate([FromQuery] string numbers)
		{
			var plate = await _plateProvider.GetPlateAsync(numbers);
			return new JsonResult(plate);
		}

		[HttpPost]
		public async Task<IResult> AddPlate([FromBody] AddPlate plate)
		{
			var plateId = await _plateProvider.AddPlateAsync(plate);
			return Results.Ok(plateId);
		}

		[HttpPost("{id}/comment")]
		public async Task<IResult> AddCommentToPlate([FromRoute] Guid plateId, [FromBody] string content)
		{
			var comment = new AddComment(plateId, content);

			await _plateProvider.AddCommentAsync(comment);
			return Results.Ok();
		}
	}
}
