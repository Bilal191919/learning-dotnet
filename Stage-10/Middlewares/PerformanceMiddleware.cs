using System.Diagnostics;

namespace Stage_4.Middlewares
{
	public class PerformanceMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<PerformanceMiddleware> _logger;

		public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var stopwatch = Stopwatch.StartNew();

			await _next(context);

			stopwatch.Stop();

			var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

			
			_logger.LogInformation($"🕒 Request [{context.Request.Method}] {context.Request.Path} took {elapsedMilliseconds}ms");
		}
	}
}