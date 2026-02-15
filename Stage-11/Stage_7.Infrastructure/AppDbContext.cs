using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stage_7.Application;
using Stage_7.Domain;

namespace Stage_7.Infrastructure
{
	public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IAppDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<TodoItem> Todos { get; set; }
	}
}