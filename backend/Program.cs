using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowLocalhost3000");

app.MapGet("/", () => "Hello World!");


app.MapGet("/squares", () => {
    if (File.Exists("squares.json")) {
        return Results.Ok(Helper.getFromDisk());
    } else {
        return Results.BadRequest(new { error = "File is not created" });

    }
});

app.MapPost("/square/create",(object requestBody) => {
    /**
    - Varje ruta får en slumpmässig färg som inte är samma som föregående ruta.
    - Position och färg på varje ruta skickas till API:et som sparar dessa värden till disk i JSON-format.
    - När sidan laddas om, hämtar webbsidan den senaste layouten från API:et och återställer de genererade rutorna. */
    Random random = new Random();
    int number;
    string numb;
    string squarePos;
    string color;
    try
    {
        if (requestBody is not JsonElement json)
        {
            return Results.BadRequest(new { error = "Invalid JSON format." });
        }
        squarePos = json.GetProperty("square").GetString() ?? "0,0";
        color = json.GetProperty("color").GetString() ?? "ffffff";
    }
    catch (System.Exception)
    {
        return Results.BadRequest(new { error = "Missing or invalid properties in request body." });
    }
    int x = 0, y = 0;


    if (!string.IsNullOrWhiteSpace(squarePos) && squarePos.Contains(','))
    {
        var parts = squarePos.Split(',');
        int.TryParse(parts[0], out x);
        int.TryParse(parts[1], out y);
    } else {
        number = random.Next(0, 16777215);
        numb = number.ToString("x6");

        number = random.Next(0, 16777215);
        numb = number.ToString("x6");
        Helper. SaveToDisk(new { square = $"{x},{y}", color = "#" + numb });

        return Results.Json(new { square = $"{x},{y}", color = "#" + numb });
    }

    /**
        X,Y
        0,0 ->
        1,0 ->
        1,1 ->
        0,1 ->
        2,0 ->
        2,1 -> 
        2,2 -> 
        1,2 -> 
        0,2

        om X = Y då är den på apex (men inte om de är noll!)
        Gå då sedan backåt genom att subtrahera x positionen
        om Y positionen är mindre än X +1
        om X positionen är mindre än Y -1?
    */
    if (x == 0 && y == 0) {
        x++;
    } else if (x > y) {
        y++;
    } else if (x == y) {
        x--;
    } else if (x == 0) {
        x = y+1;
        y = 0;
    } else if (x < y) {
        x--;
    }


    do {
        number = random.Next(0, 16777215);
        numb = number.ToString("x6");

    } while ("#"+numb == color);
    Helper.SaveToDisk( new { square = $"{x},{y}", color = "#" +numb });

    // return Results.Ok(getFromDisk()); //Dumt att skicka all data hela tiden oskalbart
    return Results.Json(new { square = $"{x},{y}", color = "#" + numb }); //Om man laddar om sidan så försvinner de tidigare rutornar (bra eler dåligt?)

});

app.MapPost("/square/destroy", () => {
    var filePath = "squares.json";
    if (File.Exists(filePath))
    {
        File.Delete(filePath);
        return Results.Ok("Deleted successfully.");
    }

    return Results.NotFound("File not found.");
});

app.Run();
