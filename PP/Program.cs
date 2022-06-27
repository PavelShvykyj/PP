

using Microsoft.EntityFrameworkCore;
using DataTier;
using PP.Fake;
using CoreTier.MappingProfiles;
using CoreTier.Services;
using CoreTier.Configs;
using Repository;
using Repository.Interfaces;
using CoreTier.Interfaces;
using DataTier.Models;
using CoreTier.CustomRequirement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using PP.Extentions;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connection);

builder.Services.AddControllers()
                .AddNewtonsoftJson();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddAutoMapper(new[] { typeof(DTOMappingProfile), typeof(ResourceMappingProfile) } );

builder.Services.AddIdentity<User, IdentityRole>(
    options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;

        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedAccount = false;

        options.User.RequireUniqueEmail = true;
    })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAuthentication(
    //options => {
    //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    //    }
    )
    .AddGoogle("Google",
        options =>
        {
            options.ClientId = builder.Configuration["Google:ClientId"];
            options.ClientSecret = builder.Configuration["Google:ClientSecret"];
            options.SignInScheme = IdentityConstants.ExternalScheme;
        });

builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.Name = "MyAuth.Cookie";
        options.ExpireTimeSpan = TimeSpan.FromSeconds(1800);
        options.Events.OnRedirectToLogin = context =>
        {
            context.HttpContext.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

builder.Services.AddEmailSevice(options => {
    options.SMTP = builder.Configuration["Email:SMTP"];
    options.POP = "";
    options.Password = builder.Configuration["Email:Password"];
    options.Address = builder.Configuration["Email:Address"];
    options.SMTPPort = 465;
    options.Login = builder.Configuration["Email:Address"]; 
}); 

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IDataService,DataManager>();
builder.Services.AddScoped<IIdentityService,IdentityService>();
builder.Services.AddHostedService<IdentityHosedService>();
builder.Services.AddTransient<IAuthorizationHandler, OwnOrdersHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyAdmin", policy =>
    {
        policy.RequireRole("Admin");
    });

    options.AddPolicy("OnlyEmployee", policy =>
    {
        policy.RequireRole(new string[] { "Admin", "Manager" });
    });

    options.AddPolicy("OnlyAuthenticated", policy =>
    {
        policy.RequireAuthenticatedUser();
    });

    options.AddPolicy("OwnOrders", policy =>
    {
        policy.AddRequirements(new EmployeeRequirement(new[] { "Admin", "Manager" }));
    });

});


builder.Services.AddSingleton<ActionsResultFake>();


WebApplication app = builder.Build();
app.Use(
    async (context, next) =>
    {
        context.Items.Add("IsDevelopment", app.Environment.IsDevelopment());
        await next.Invoke();
    }
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();


