using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext _context;
		public RegionsController(NZWalksDbContext context)
		{
			context = _context;
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			var regions = _context.Regions.ToList();
			return Ok(regions);
		}


		[HttpGet]
		[Route("{id}")]
		public IActionResult GetById(Guid id)
		{
			var region = _context.Regions.FirstOrDefault(u => u.Id == id);

			if (region == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(region);
			}
		}
	}
}
