

using Microsoft.EntityFrameworkCore;
using DataTier;
using PP.Fake;
using CoreTier.MappingProfiles;
using CoreTier.Services;
using Repository;
using Repository.Interfaces;
using CoreTier.Interfaces;
using DataTier.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connection);

builder.Services.AddControllers()
                .AddNewtonsoftJson();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddAutoMapper(new[] { typeof(DTOMappingProfile), typeof(ResourceMappingProfile) } );

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IDataService,DataService>();
builder.Services.AddSingleton<ActionsResultFake>();

var app = builder.Build();
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
