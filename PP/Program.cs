

using Microsoft.EntityFrameworkCore;
using PP.APIResourses;
using PP.DTO;
using PP.EF;
using PP.Fake;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connection);
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSingleton<ActionsResultFake>();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddAutoMapper(new[] { typeof(DTOMappingProfile), typeof(ResourceMappingProfile) } );

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
