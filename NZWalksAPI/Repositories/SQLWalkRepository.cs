using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Repositories
{
	public class SQLWalkRepository : IWalkRepository
	{
		private readonly NZWalksDbContext _context;

		public SQLWalkRepository(NZWalksDbContext context)
		{
			_context = context;
		}
		public async Task<Walk> CreateAsync(Walk walk)
		{
			await _context.Walks.AddAsync(walk);
			await _context.SaveChangesAsync();
			return walk;
		}

		public async Task<Walk?> DeleteAsync(Guid id)
		{
			var existingWalk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
			if (existingWalk == null)
			{
				return null;
			}
			 _context.Walks.Remove(existingWalk);
			 await _context.SaveChangesAsync();

			return existingWalk;
		}


		public  async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true,
			int pageNumber = 1, int pageSize = 1000)
		{
			var walks = _context.Walks.Include("Difficulty").Include("Region").AsQueryable();

			if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterOn))
			{
				if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					walks = walks.Where(u => u.Name.Contains(filterQuery));
				}
				
			}

			if (!string.IsNullOrWhiteSpace(sortBy))
			{
				if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					walks = isAscending ? walks.OrderBy(u => u.Name): walks.OrderByDescending(u => u.Name);
				}else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
				{
					walks = isAscending ? walks.OrderBy(u => u.LengthInKm): walks.OrderByDescending(u => u.LengthInKm);
				}
			}

			var skipResults = (pageNumber - 1) * pageSize;

		    return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
		}

	

		public async Task<Walk?> GetByIdAsync(Guid id)
		{
			var walkDomainModel = _context.Walks.Include("Difficulty")
				.Include("Region")
				.FirstOrDefault(x => x.Id == id);

			if (walkDomainModel == null)
			{
				return null;
			}
			
				return  walkDomainModel;
		}

		public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
		{
			var existingWalk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
			
			if(existingWalk == null)
			{
				return null;
			}
		   
			existingWalk.Name = walk.Name;
			existingWalk.Description = walk.Description;
			existingWalk.LengthInKm = walk.LengthInKm;
			existingWalk.WalkImageUrl = walk.WalkImageUrl;
			existingWalk.DifficultyId = walk.DifficultyId;
			existingWalk.RegionId = walk.RegionId;

			await _context.SaveChangesAsync();
			return existingWalk;
		}
	}
}
