using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory; 
using Stage_7.Application.Common;
using Stage_7.Application.DTOs;
using System.Diagnostics;
using System.Linq;

namespace Stage_7.Application.Features.Todos.Queries
{
	public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, PagedResult<TodoDto>>
	{
		private readonly IAppDbContext _context;
		private readonly ILogger<GetTodosQueryHandler> _logger;
		private readonly IMemoryCache _cache; 

		public GetTodosQueryHandler(IAppDbContext context, ILogger<GetTodosQueryHandler> logger, IMemoryCache cache)
		{
			_context = context;
			_logger = logger;
			_cache = cache; 
		}

		public async Task<PagedResult<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
		{
			
			string cacheKey = $"Todos_User{request.UserId}_Page{request.PageNumber}_Size{request.PageSize}_Opt{request.UseOptimized}";

			
			if (_cache.TryGetValue(cacheKey, out PagedResult<TodoDto> cachedResult))
			{
				_logger.LogInformation($"⚡ CACHE HIT: Data memory se fauran mil gaya! Key: {cacheKey}");
				return cachedResult;
			}

			_logger.LogInformation($"🗄️ CACHE MISS: Data pehli baar database se laa rahe hain... Key: {cacheKey}");

			var stopwatch = Stopwatch.StartNew();
			List<TodoDto> items;

			int totalCount = await _context.Todos.CountAsync(t => t.UserId == request.UserId, cancellationToken);

			if (request.UseOptimized)
			{
				items = await _context.Todos
					.Where(t => t.UserId == request.UserId)
					.OrderBy(t => t.Id)
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
				var allTodos = await _context.Todos
					.Where(t => t.UserId == request.UserId)
					.OrderBy(t => t.Id)
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

			var result = new PagedResult<TodoDto>(items, totalCount, request.PageNumber, request.PageSize);

			
			var cacheOptions = new MemoryCacheEntryOptions()
				.SetAbsoluteExpiration(TimeSpan.FromMinutes(1)); 

			_cache.Set(cacheKey, result, cacheOptions);

			return result;
		}
	}
}