var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/square",() => {
    Random random = new Random();
    int number = random.Next(0, 16777215);
    return Results.Json( new { message = "This is the square route!", magicNumber = number});
});
app.Run();
