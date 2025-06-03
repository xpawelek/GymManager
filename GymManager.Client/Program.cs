using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GymManager.Client;
using GymManager.Client.Services;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthStateService>();
builder.Services.AddTransient<AuthHeaderHandler>();

builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
});

builder.Services.AddHttpClient<TrainerService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<EquipmentService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<MemberService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<MembershipService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<MembershipTypeService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<MessageService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<ProgressPhotoService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<ServiceRequestService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<TrainerAssignmentService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<TrainerProfileService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<TrainingSessionService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<WorkoutNoteService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Logging.SetMinimumLevel(LogLevel.Information);

var host = builder.Build();
var authState = host.Services.GetRequiredService<AuthStateService>();
await authState.InitializeAsync();

await host.RunAsync();
