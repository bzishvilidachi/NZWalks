using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilter;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Globalization;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class RegionsController : ControllerBase
	{
		private readonly IRegionRepository _regionRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<RegionsController> logger;

		public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
		{
			_regionRepository = regionRepository;
			_mapper = mapper;
			this.logger = logger;
		}
		[HttpGet]
		//[Authorize(Roles = "Reader")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				//throw new Exception("This is a custom exception");

				var regionsDomain = await _regionRepository.GetAllAsync();

				logger.LogInformation($"Finished getall request with data: {JsonSerializer.Serialize(regionsDomain)}"); 

				return Ok(_mapper.Map<List<RegionDto>>(regionsDomain));
			}
			catch (Exception ex)
			{
			    logger.LogError(ex, ex.Message);
				throw;
			}
		
			
			
		}


		[HttpGet]
		[Route("{id}")]
		//[Authorize(Roles = "Reader")]
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
		//[Authorize(Roles = "Writer")]
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
		//[Authorize(Roles = "Writer")]
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
		//[Authorize(Roles = "Writer")]
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
