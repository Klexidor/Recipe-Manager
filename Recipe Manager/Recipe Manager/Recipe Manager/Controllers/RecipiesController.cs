using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipe_Manager.Domain;
using Recipe_Manager.Application;
using Recipe_Manager.Application.Recipies;

namespace Recipe_Manager.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipiesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public RecipiesController(IMediator mediator)
		{
			this._mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<List<Recipe>>> GetRecipes(string query)
		{
			return await _mediator.Send(new List.Query() { RequestQuery = query});
		}


	}
}
