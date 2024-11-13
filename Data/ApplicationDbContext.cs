//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace DoJoinTask.Data
//{
//	public class ApplicationDbContext
//	{
//	}
//}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoJoinTask.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		// Optionally, add DbSet properties for other entities here
		// e.g., public DbSet<YourEntity> YourEntities { get; set; }
	}
}
