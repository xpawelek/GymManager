using GymManager.Data;
using GymManager.Models.Mappers.Admin;
using GymManager.Models.Mappers.Member;
using GymManager.Models.Mappers.Trainer;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GymDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AdminEquipmentService>();
builder.Services.AddScoped<MemberEquipmentService>();
builder.Services.AddScoped<TrainerEquipmentService>();

builder.Services.AddScoped<AdminEquipmentMapper>();
builder.Services.AddScoped<MemberEquipmentMapper>();
builder.Services.AddScoped<TrainerEquipmentMapper>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();