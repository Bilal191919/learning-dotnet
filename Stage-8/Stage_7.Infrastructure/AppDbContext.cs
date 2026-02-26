using Microsoft.EntityFrameworkCore;
using Stage_7.Domain;      
using Stage_7.Application; 

namespace Stage_7.Infrastructure
{

	public class AppDbContext : DbContext, IAppDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<TodoItem> Todos { get; set; }
		public DbSet<User> Users { get; set; }

		
	}
}
