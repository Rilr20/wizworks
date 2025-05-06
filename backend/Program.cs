using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

void SaveToDisk(object data)
{
    List<object> allData;
    var filePath = "squares.json";
    var jsonData = JsonSerializer.Serialize(data);

    if (File.Exists(filePath))
    {
        var existingJson = File.ReadAllText(filePath);
        allData = JsonSerializer.Deserialize<List<object>>(existingJson) ?? new List<object>();
    }
    else
    {
        allData = new List<object>();
    }

    allData.Add(data);
    var newJson = JsonSerializer.Serialize(allData, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(filePath, newJson);
}

app.MapPost("/square/create",(object requestBody) => {
    /**
    - Varje ruta får en slumpmässig färg som inte är samma som föregående ruta.
    - Position och färg på varje ruta skickas till API:et som sparar dessa värden till disk i JSON-format.
    - När sidan laddas om, hämtar webbsidan den senaste layouten från API:et och återställer de genererade rutorna. */
    if (requestBody is not JsonElement json)
    {
        return Results.BadRequest(new { error = "Invalid JSON format." });
    }
    string squarePos = json.GetProperty("square").GetString();
    string color = json.GetProperty("color").GetString();
    int x = 0, y = 0;

    if (!string.IsNullOrWhiteSpace(squarePos) && squarePos.Contains(','))
    {
        var parts = squarePos.Split(',');
        int.TryParse(parts[0], out x);
        int.TryParse(parts[1], out y);
    } else {
        return Results.BadRequest(new { error = "Invalid JSON format." });
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


    Random random = new Random();
    int number;
    string numb;
    do {
        number = random.Next(0, 16777215);
        numb = number.ToString("x6");

    } while (numb == color);
    SaveToDisk( new { square = $"{x},{y}", color = numb });

    return Results.Json(new { square = $"{x},{y}", color = numb });
});

app.MapPost("/square/destroy", () => {
});

app.Run();
