using Microsoft.EntityFrameworkCore;
using Stage_4.Models;

namespace Stage_4.Data
{
	
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}


		public DbSet<TodoItem> TodoItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			
			modelBuilder.Entity<TodoItem>().HasData(
				new TodoItem
				{
					Id = 1,
					Title = "Environment Setup and Database Integration",
					IsCompleted = true,
					CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
				},
				new TodoItem
				{
					Id = 2,
					Title = "Implement JWT Authentication (Stage 6)",
					IsCompleted = false,
					CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
				},
				new TodoItem
				{
					Id = 3,
					Title = "Write Unit Tests for Todo Controller",
					IsCompleted = false,
					CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
				}
			);
		}
	}
}
