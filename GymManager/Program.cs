using System.Text;
using GymManager.Data;
using GymManager.Models.Identity;
using GymManager.Models.Mappers.Admin;
using GymManager.Models.Mappers.Member;
using GymManager.Models.Mappers.Trainer;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
builder.Services.AddScoped<AdminServiceRequestService>();
builder.Services.AddScoped<TrainerServiceRequestService>();
builder.Services.AddScoped<MemberServiceRequestService>();
builder.Services.AddScoped<AdminTrainerAssignmentService>();
builder.Services.AddScoped<MemberSelfTrainerAssignmentService>();
builder.Services.AddScoped<TrainerSelfTrainerAssignmentService>();
builder.Services.AddScoped<AdminWorkoutNoteService>();
builder.Services.AddScoped<MemberSelfWorkoutNoteService>();
builder.Services.AddScoped<TrainerSelfWorkoutNoteService>();
builder.Services.AddScoped<AdminTrainingSessionService>();
builder.Services.AddScoped<MemberTrainingSessionService>();
builder.Services.AddScoped<TrainerTrainingSessionService>();
builder.Services.AddScoped<MemberMessageService>();
builder.Services.AddScoped<TrainerMessageService>();
builder.Services.AddScoped<TrainerProfileService>();
builder.Services.AddScoped<AdminTrainerService>();
builder.Services.AddScoped<MemberTrainerService>();
builder.Services.AddScoped<TrainerProfileService>();


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
builder.Services.AddScoped<AdminServiceRequestMapper>();
builder.Services.AddScoped<TrainerServiceRequestMapper>();
builder.Services.AddScoped<MemberServiceRequestMapper>();
builder.Services.AddScoped<AdminProgessPhotoMapper>();
builder.Services.AddScoped<MemberProgressPhotoMapper>();
builder.Services.AddScoped<TrainerProgressPhotoMapper>();
builder.Services.AddScoped<AdminTrainerAssignmentMapper>();
builder.Services.AddScoped<MemberSelfTrainerAssignmentMapper>();
builder.Services.AddScoped<TrainerSelfTrainerAssignmentMapper>();
builder.Services.AddScoped<AdminWorkoutNoteMapper>();
builder.Services.AddScoped<MemberSelfWorkoutNoteMapper>();
builder.Services.AddScoped<TrainerWorkoutNoteMapper>();
builder.Services.AddScoped<AdminTrainingSessionMapper>();
builder.Services.AddScoped<MemberTrainingSessionMapper>();
builder.Services.AddScoped<TrainerTrainingSessionMapper>();
builder.Services.AddScoped<MemberSelfMessageMapper>();
builder.Services.AddScoped<TrainerSelfMessageMapper>();
builder.Services.AddScoped<TrainerProfileMapper>();
builder.Services.AddScoped<AdminTrainerMapper>();
builder.Services.AddScoped<MemberTrainerMapper>();
builder.Services.AddScoped<TrainerProfileMapper>();

builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

var jwtSetting = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSetting.GetValue<string>("SecretKey");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSetting.GetValue<string>("Issuer"),
        ValidAudience = jwtSetting.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
    };
    
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("JWT authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("JWT token validated successfully.");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<GymDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentityDataInitializer.Initialize(services);
}

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

app.UseAuthentication(); 
app.UseAuthorization();  
app.MapControllers();  

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();