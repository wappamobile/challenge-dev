using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wappa.Api.DataLayer;
using Wappa.Api.DomainModel;
using Wappa.Api.Requests;

namespace Wappa.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DriversController : ControllerBase
	{
		private IUnitOfWork unitOfWork;

		public DriversController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
	
		[HttpPost]
		public async Task<ActionResult<Driver>> Post([FromBody] CreateDriverRequest request)
		{
			if (request == null) { return this.BadRequest(); }

			try
			{
				var driver = Mapper.Map<Driver>(request);
				this.unitOfWork.DriversRepository.Add(driver);
				await this.unitOfWork.SaveChanges();
				return this.CreatedAtRoute(nameof(Post), driver);
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

	}
}
