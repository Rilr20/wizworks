
using System.Text.Json;
public static class Helper
{
    public static void SaveToDisk(object data)
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
    public static List<Object> getFromDisk()
    {
        var filePath = "squares.json";
        var existingJson = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<List<object>>(existingJson) ?? new List<object>();
    }

}