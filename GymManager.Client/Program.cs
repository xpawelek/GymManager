using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GymManager.Client;
using GymManager.Client.Services;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5119/") });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<EquipmentService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<MembershipService>();
builder.Services.AddScoped<MembershipTypeService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<ProgressPhotoService>();
builder.Services.AddScoped<ServiceRequestService>();
builder.Services.AddScoped<TrainerAssignmentService>();
builder.Services.AddScoped<TrainerProfileService>();
builder.Services.AddScoped<TrainerService>();
builder.Services.AddScoped<TrainingSessionService>();
builder.Services.AddScoped<WorkoutNoteService>();
builder.Services.AddScoped<AuthStateService>();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var host = builder.Build();

var authState = host.Services.GetRequiredService<AuthStateService>();
await authState.InitializeAsync(); 


await host.RunAsync();
