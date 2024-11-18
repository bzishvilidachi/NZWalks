using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using System.Globalization;

namespace NZWalksAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext _context;
		public RegionsController(NZWalksDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			var regionsDomain = _context.Regions.ToList();

			var regionsDto = new List<RegionDto>();
			foreach (var region in regionsDomain)
			{
				regionsDto.Add(new RegionDto()
				{
					Id = region.Id,
					Name = region.Name,
					Code = region.Code,
					RegionImageUrl = region.RegionImageUrl,
				});
			}
			return Ok(regionsDto);
		}


		[HttpGet]
		[Route("{id}")]
		public IActionResult GetById(Guid id)
		{
			var regionDomain = _context.Regions.FirstOrDefault(u => u.Id == id);

			if (regionDomain == null)
			{
				return NotFound();
			}
			var regionDto = new RegionDto
			{
				Id = regionDomain.Id,
				Name = regionDomain.Name,
				Code = regionDomain.Code,
				RegionImageUrl = regionDomain.RegionImageUrl,
			};	
			
				return Ok(regionDto);
		}

		[HttpPost]
		public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			var regionDomainModel = new Region
			{
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name,
				RegionImageUrl = addRegionRequestDto.RegionImageUrl,
			};

			_context.Regions.Add(regionDomainModel);
			_context.SaveChanges();

			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl,
			};

			return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
		}


		[HttpPut]
		[Route("{id:Guid}")]
		public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			var regionDomainModel = _context.Regions.FirstOrDefault(u => u.Id == id);
		    
			if (regionDomainModel == null)
			{
				return NotFound();
			}
			
			regionDomainModel.Code = updateRegionRequestDto.Code;
			regionDomainModel.Name = updateRegionRequestDto.Name;
			regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

			_context.SaveChanges();

			var regionDto = new RegionDto
			{  
				Id = regionDomainModel.Id,
				RegionImageUrl = regionDomainModel.RegionImageUrl,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
			};

			return Ok(regionDto);
		}

		[HttpDelete]
		[Route("{id:Guid}")]
		public IActionResult Delete([FromRoute] Guid id)
		{
			var regionDomainModel = _context.Regions.FirstOrDefault(u => u.Id == id);

			if (regionDomainModel == null)
			{
				return NotFound();
			}

			_context.Regions.Remove(regionDomainModel);
			_context.SaveChanges();

			return Ok();
		}

	}
}
