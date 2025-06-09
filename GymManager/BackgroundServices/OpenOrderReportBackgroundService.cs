using System.Net;
using System.Net.Mail;
using GymManager.Data;
using Microsoft.EntityFrameworkCore;

namespace GymManager.BackgroundServices;

public class OpenOrderReportBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OpenOrderReportBackgroundService> _logger;
    private readonly IConfiguration _configuration;

    public OpenOrderReportBackgroundService(IServiceProvider serviceProvider,
        ILogger<OpenOrderReportBackgroundService> logger, 
        IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<GymDbContext>();

                string openOrdersPath = await GenerateOpenRequestsPdfAsync(db);
                string membershipsPath = await GenerateMembershipsPdfAsync(db);
                
                await SendEmailWithAttachments(openOrdersPath, membershipsPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating and sending reports.");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task<string> GenerateOpenRequestsPdfAsync(GymDbContext db)
    {
        var now = DateTime.Now;
        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        
        var requests = await db.ServiceRequests.Where(r => r.RequestDate >= startOfMonth).OrderBy(r => r.RequestDate).ToListAsync();
        
        var dir = "raports";
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, "service_request.pdf");

        using var stream = new FileStream(path, FileMode.Create);
        var doc = new PdfSharpCore.Pdf.PdfDocument();
        var page = doc.AddPage();
        var pen = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);
        var titleFont = new PdfSharpCore.Drawing.XFont("Verdana", 20);
        var font = new PdfSharpCore.Drawing.XFont("Verdana", 12);

        double y = 40;
        pen.DrawString("Open Service Requests:", titleFont, PdfSharpCore.Drawing.XBrushes.Black, new PdfSharpCore.Drawing.XPoint(20, y));
        y += 30;

        foreach (var request in requests)
        {
            pen.DrawString($"- #{request.ServiceProblemTitle}, {request.RequestDate.ToShortDateString()}", font, PdfSharpCore.Drawing.XBrushes.Black, new PdfSharpCore.Drawing.XPoint(20, y));
            y += 25;
            pen.DrawString($"   Description: {request.ProblemNote}", font, PdfSharpCore.Drawing.XBrushes.Gray, new PdfSharpCore.Drawing.XPoint(20, y));
            y += 40;
        }

        doc.Save(stream);
        return path;
    }


    private async Task<string> GenerateMembershipsPdfAsync(GymDbContext db)
    {
        var now = DateTime.Now;
        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        
        var count = await db.Memberships.Where(m => m.StartDate >= startOfMonth).CountAsync();
        
        var dir = "raports";
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, "memberships.pdf");

        using var stream = new FileStream(path, FileMode.Create);
        var doc = new PdfSharpCore.Pdf.PdfDocument();
        var page = doc.AddPage();
        var pen = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);
        var titleFont = new PdfSharpCore.Drawing.XFont("Verdana", 20);
        var font = new PdfSharpCore.Drawing.XFont("Verdana", 12);

        double y = 40;
        pen.DrawString("Memberships bought during that month:", titleFont, PdfSharpCore.Drawing.XBrushes.Black, new PdfSharpCore.Drawing.XPoint(20, y));
        y += 30;
        
        pen.DrawString($"Memberships bought in {now:MMMM yyyy}: {count}", font, PdfSharpCore.Drawing.XBrushes.Black, new PdfSharpCore.Drawing.XPoint(20, y));
        y += 25;

        doc.Save(stream);
        return path;
    }
    private async Task SendEmailWithAttachments(string openOrdersPath, string membershipsPath)
    {
        var userEmail = _configuration["Smtp:User"];
        var pass = _configuration["Smtp:Pass"];
        
        using var client = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(userEmail, pass),
            EnableSsl = true
        };
        
        var mail = new MailMessage("paul.trzupek@gmail.com", "paul.trzupek@gmail.com")
        {
            Subject = $"Daily Report - {DateTime.Now.ToShortDateString()}",
            Body = "Attached: today's service requests and new memberships."
        };
        
        mail.Attachments.Add(new Attachment(openOrdersPath));
        mail.Attachments.Add(new Attachment(membershipsPath));
        
        await client.SendMailAsync(mail);
    }
}