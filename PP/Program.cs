

using Microsoft.EntityFrameworkCore;
using PP.EF;
using PP.Fake;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connection);
builder.Services.AddControllers();
builder.Services.AddSingleton<ActionsResultFake>();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));


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
