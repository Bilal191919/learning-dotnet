using System.Diagnostics;

namespace Stage_4.Middlewares; 

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
		
		var timer = new Stopwatch();
		timer.Start();

		
		await _next(context);

		
		timer.Stop();

		
		var timeTaken = timer.ElapsedMilliseconds;

	
		if (timeTaken > 500)
		{
			_logger.LogWarning("⏱️ SLOW REQUEST: {Method} {Path} took {Time} ms",
				context.Request.Method, context.Request.Path, timeTaken);
		}
		else
		{
			_logger.LogInformation("⏱️ FAST REQUEST: {Method} {Path} took {Time} ms",
				context.Request.Method, context.Request.Path, timeTaken);
		}
	}
}