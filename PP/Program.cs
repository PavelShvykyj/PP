

using Microsoft.EntityFrameworkCore;
using DataTier;
using PP.Fake;
using CoreTier.MappingProfiles;
using CoreTier.Services;
using Repository;
using Repository.Interfaces;
using CoreTier.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connection);
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSingleton<ActionsResultFake>();


builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddAutoMapper(new[] { typeof(DTOMappingProfile), typeof(ResourceMappingProfile) } );

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IDataService,DataService>();

var app = builder.Build();
app.Use(
    async (context, next) =>
    {
        context.Items.Add("IsDevelopment", app.Environment.IsDevelopment());
        await next.Invoke();
    }
);

app.MapControllers();
app.MapGet("/", () => "Hello World!");
app.Run();
