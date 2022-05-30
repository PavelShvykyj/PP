

using PP.Fake;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<ActionsResultFake>();

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
