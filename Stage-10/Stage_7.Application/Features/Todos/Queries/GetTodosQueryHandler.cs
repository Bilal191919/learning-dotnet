using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stage_7.Application.Common;
using Stage_7.Application.DTOs;
using System.Diagnostics;

namespace Stage_7.Application.Features.Todos.Queries
{
	public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, PagedResult<TodoDto>>
	{
		private readonly IAppDbContext _context;
		private readonly ILogger<GetTodosQueryHandler> _logger;

		public GetTodosQueryHandler(IAppDbContext context, ILogger<GetTodosQueryHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<PagedResult<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
		{
			var stopwatch = Stopwatch.StartNew();
			List<TodoDto> items;


			int totalCount = await _context.Todos.CountAsync(t => t.UserId == request.UserId, cancellationToken);

			if (request.UseOptimized)
			{
				// Optimized Query: Sirf zaroori columns select kar raha hai
				items = await _context.Todos
					.Where(t => t.UserId == request.UserId)
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.Select(t => new TodoDto
					{
						Id = t.Id,
						Title = t.Title,
						IsCompleted = t.IsCompleted
					})
					.ToListAsync(cancellationToken);
			}
			else
			{
				// Naive Query: Poora data utha kar memory mein la raha hai (SLOW)
				var allTodos = await _context.Todos
					.Where(t => t.UserId == request.UserId)
					.ToListAsync(cancellationToken);

				items = allTodos
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.Select(t => new TodoDto
					{
						Id = t.Id,
						Title = t.Title,
						IsCompleted = t.IsCompleted
					})
					.ToList();
			}

			stopwatch.Stop();

			_logger.LogInformation($"Benchmark Result: Mode={(request.UseOptimized ? "Optimized" : "Naive")}, Time={stopwatch.ElapsedMilliseconds}ms, Items={items.Count}");

			return new PagedResult<TodoDto>(items, totalCount, request.PageNumber, request.PageSize);
		}
	}
}