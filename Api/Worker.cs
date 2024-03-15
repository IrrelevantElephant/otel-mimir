using System.Diagnostics;

namespace Api;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var activity = ActivitySources.Main.StartActivity("WorkerLoop");
            activity?.AddEvent(new ActivityEvent("Doing some work"));
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
            activity?.Stop();
        }
    }
}
