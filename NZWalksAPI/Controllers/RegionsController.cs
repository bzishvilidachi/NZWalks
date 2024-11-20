using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilter;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Globalization;

namespace NZWalksAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly IRegionRepository _regionRepository;
		private readonly IMapper _mapper;

		public RegionsController(IRegionRepository regionRepository, IMapper mapper)
		{
			_regionRepository = regionRepository;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var regionsDomain = await _regionRepository.GetAllAsync();
		
			return Ok(_mapper.Map<List<RegionDto>>(regionsDomain));
		}


		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var regionDomain = await _regionRepository.GetByIdAsync(id);

			if (regionDomain == null)
			{
				return NotFound();
			}
			
			
				return Ok(_mapper.Map<RegionDto>(regionDomain));
		}

		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			
			var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

			regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);
			

			var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

			return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);

		
		}


		[HttpPut]
		[Route("{id:Guid}")]
		[ValidateModel]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			
				var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

				regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);
			
			if (regionDomainModel == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<RegionDto>(regionDomainModel));

		}

		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var regionDomainModel = await _regionRepository.DeleteAsync(id);

			if (regionDomainModel == null)
			{
				return NotFound();
			}


			return Ok();
		}

	}
}
