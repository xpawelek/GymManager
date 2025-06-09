using System.Net.Mail;
using GymManager.Data;
using Microsoft.EntityFrameworkCore;

namespace GymManager.BackgroundServices;

public class OpenOrderReportBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OpenOrderReportBackgroundService> _logger;

    public OpenOrderReportBackgroundService(IServiceProvider serviceProvider,
        ILogger<OpenOrderReportBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<GymDbContext>();

                string openOrdersPath = await GenerateOpenOrdersPdfAsync(db);
                string membershipsPath = await GenerateMembershipsPdfAsync(db);
                
                await SendEmailWithAttachments(openOrdersPath, membershipsPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating and sending reports.");
            }

            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
        }
    }

    private async Task<string> GenerateOpenOrdersPdfAsync(GymDbContext db)
    {
        var orders = await db.ServiceRequests.Where(r => r.IsResolved).ToListAsync();

        var path = "raports/open_orders.pdf";
        Directory.CreateDirectory(path);
        
        using var writer = new StreamWriter(path);
        await writer.WriteLineAsync("Open Service Requests:");
        foreach (var order in orders)
        {
            await writer.WriteLineAsync($"- #{order.Id} | {order.ServiceProblemTitle}");
            await writer.WriteLineAsync($"- #Description: {order.ProblemNote}");
        }

        return path;
    }

    private async Task<string> GenerateMembershipsPdfAsync(GymDbContext db)
    {
        var now = DateTime.Now;
        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        
        var count = await db.Memberships.Where(m => m.StartDate >= startOfMonth).CountAsync();

        var path = "raports/mebmerships_raport.pdf";
        await File.WriteAllTextAsync(path, $"Memberships bought in {now:MMMM yyyy}: {count}");
        
        return path;
    }

    private async Task SendEmailWithAttachments(string openOrdersPath, string membershipsPath)
    {
        using var client = new SmtpClient("smtp.gmail.com", 587)
        {
            
        }
    }
}