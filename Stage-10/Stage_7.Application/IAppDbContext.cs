using Microsoft.EntityFrameworkCore;
using Stage_7.Domain;

namespace Stage_7.Application
{
	public interface IAppDbContext
	{
		DbSet<TodoItem> Todos { get; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}