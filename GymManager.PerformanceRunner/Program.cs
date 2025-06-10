using System.Net;
using NBomber.Contracts;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Http.CSharp;

namespace PerformanceRunner;
using NBomber;

class Program
{
    static void Main(string[] args)
    {
       GetTrainersTest.Run();
    }
}

public class GetTrainersTest
{
    public static void Run()
    {
        using var httpClient = new HttpClient();

        int totalRequests = 100;
        int users = 50;
        double requestsPerUser = (double)totalRequests / users;
        TimeSpan testDuration = TimeSpan.FromSeconds(requestsPerUser * 1);

        var scenario = Scenario.Create("GET /api/trainers/public", async context =>
            {
                var request = Http.CreateRequest("GET", "http://localhost:5119/api/trainers/public")
                    .WithHeader("Content-Type", "application/json");

                var response = await Http.Send(httpClient, request);
                return response;
            })
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.Inject(
                    rate: users,
                    interval: TimeSpan.FromSeconds(1),
                    during: testDuration
                )
            );

        NBomberRunner
            .RegisterScenarios(scenario)
            .WithReportFileName("nbomber-raport") 
            .WithReportFolder("raports")
            .WithReportFormats(ReportFormat.Html)
            .Run();
    }
}

