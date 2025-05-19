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
builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<AdminEquipmentService>();
builder.Services.AddScoped<MemberEquipmentService>();
builder.Services.AddScoped<TrainerEquipmentService>();
builder.Services.AddScoped<AdminMemberService>();
builder.Services.AddScoped<MemberSelfService>();
builder.Services.AddScoped<TrainerMemberService>();
builder.Services.AddScoped<AdminMembershipTypeService>();
builder.Services.AddScoped<MemberMembershipTypeService>();
builder.Services.AddScoped<TrainerMembershipTypeService>();
builder.Services.AddScoped<AdminMembershipService>();
builder.Services.AddScoped<MemberSelfMembershipService>();
builder.Services.AddScoped<AdminProgressPhotoService>();
builder.Services.AddScoped<MemberProgressPhotoService>();
builder.Services.AddScoped<TrainerProgressPhotoService>();
builder.Services.AddScoped<AdminServiceRequestMapper>();
builder.Services.AddScoped<TrainerServiceRequestMapper>();
builder.Services.AddScoped<MemberServiceRequestMapper>();

builder.Services.AddScoped<AdminEquipmentMapper>();
builder.Services.AddScoped<MemberEquipmentMapper>();
builder.Services.AddScoped<TrainerEquipmentMapper>();
builder.Services.AddScoped<AdminMemberMapper>();
builder.Services.AddScoped<MemberSelfMapper>();
builder.Services.AddScoped<TrainerMemberMapper>();
builder.Services.AddScoped<AdminMembershipTypeMapper>();
builder.Services.AddScoped<MemberMembershipTypeMapper>();
builder.Services.AddScoped<TrainerMembershipTypeMapper>();
builder.Services.AddScoped<AdminMembershipMapper>();
builder.Services.AddScoped<MemberSelfMembershipMapper>();
builder.Services.AddScoped<AdminProgessPhotoMapper>();
builder.Services.AddScoped<MemberProgressPhotoMapper>();
builder.Services.AddScoped<TrainerProgressPhotoMapper>();
builder.Services.AddScoped<AdminServiceRequestService>();
builder.Services.AddScoped<TrainerServiceRequestService>();
builder.Services.AddScoped<MemberServiceRequestService>();

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