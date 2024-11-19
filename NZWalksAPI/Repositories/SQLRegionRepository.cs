using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
	public class SQLRegionRepository: IRegionRepository
	{
		private readonly NZWalksDbContext _context;
		public SQLRegionRepository(NZWalksDbContext context)
		{
			_context = context;
		}

		public async Task<Region> CreateAsync(Region region)
		{
			await _context.Regions.AddAsync(region);
			await _context.SaveChangesAsync();
			return region;
		}

		public async Task<Region?> DeleteAsync(Guid id)
		{
			var regionDomainModel = await _context.Regions.FirstOrDefaultAsync(u => u.Id == id);
			if (regionDomainModel == null)
			{
				return null;
			}

			_context.Regions.Remove(regionDomainModel);
			await _context.SaveChangesAsync();
			return regionDomainModel;
		}

		public async Task<List<Region>> GetAllAsync()
		{
			return await _context.Regions.ToListAsync();
		}

		public async Task<Region?> GetByIdAsync(Guid id)
		{
			return await _context.Regions.FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task<Region?> UpdateAsync(Guid id, Region region)
		{
			var existingRegion = await _context.Regions.FirstOrDefaultAsync(s => s.Id == id);
			if (existingRegion == null)
			{
				return null;
			}
			existingRegion.Code = region.Code;
			existingRegion.Name = region.Name;
			existingRegion.RegionImageUrl = region.RegionImageUrl;

			await _context.SaveChangesAsync();
			return existingRegion;
			
		}
	}
}
