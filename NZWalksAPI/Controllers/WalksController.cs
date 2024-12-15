using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilter;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{ 
		private readonly IMapper _mapper;
		private readonly IWalkRepository _walkRepository;

		public WalksController(IMapper mapper, IWalkRepository walkRepository)
		{
			_mapper = mapper;
			_walkRepository = walkRepository;
		}

		

		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			
				var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);

				await _walkRepository.CreateAsync(walkDomainModel);

			    return Ok(_mapper.Map<WalkDto>(walkDomainModel));
			
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
			[FromQuery] string? sortBy, [FromQuery] bool? IsAscending,
			[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
		{
			
				var walkDomainModel = await _walkRepository.GetAllAsync(filterOn, filterQuery,
					sortBy, IsAscending ?? true, pageNumber, pageSize);

		        
				return Ok(_mapper.Map<List<WalkDto>>(walkDomainModel));
		
		}

		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var walkDomainModel = await _walkRepository.GetByIdAsync(id);
			if (walkDomainModel == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<WalkDto>(walkDomainModel));
		}


		[HttpPut]
		[Route("{id:Guid}")]
		[ValidateModel]
		public async Task<IActionResult>Update([FromRoute]Guid id, [FromBody]UpdateWalkRequestDto updateWalkDto)
		{
			
				var walkDomainModel = _mapper.Map<Walk>(updateWalkDto);

						var updatedDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);
						if(updatedDomainModel == null)
						{
							return NotFound();
						}

						return Ok(_mapper.Map<WalkDto>(updatedDomainModel));
		
		
			
		}

		[HttpDelete]
		[Route("{id:Guid}")]

		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var walkToBeDeleted = await _walkRepository.DeleteAsync(id);
			if(walkToBeDeleted == null)
			{
				return NotFound();
			}
			return Ok();
		}
	}
}
