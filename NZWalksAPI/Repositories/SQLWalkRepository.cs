using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

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

		public async Task<List<Walk>> GetAllAsync()
		{
			return await _context.Walks.Include("Difficulty").Include("Region").ToListAsync();
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
			existingWalk.LenghtInKm = walk.LenghtInKm;
			existingWalk.WalkImageUrl = walk.WalkImageUrl;
			existingWalk.DifficultyId = walk.DifficultyId;
			existingWalk.RegionId = walk.RegionId;

			await _context.SaveChangesAsync();
			return existingWalk;
		}
	}
}
