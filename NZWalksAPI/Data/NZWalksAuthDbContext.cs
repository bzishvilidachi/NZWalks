using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAPI.Data
{
	public class NZWalksAuthDbContext : IdentityDbContext
	{
		public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var readerRoleId = "0a060296-2360-4649-a24e-82cac0986224";
			var writerRoleId = "1c254dd5-4d4b-4f43-b362-fe3f12536852";

			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Id = readerRoleId,
					ConcurrencyStamp = readerRoleId,
					Name = "Reader",
					NormalizedName = "Reader".ToUpper(),
				},

				new IdentityRole
				{
					Id = writerRoleId,
					ConcurrencyStamp = writerRoleId,
					Name = "Writer",
					NormalizedName = "Writer".ToUpper(),
				},
			};

			builder.Entity<IdentityRole>().HasData(roles);
		}
	}
}
