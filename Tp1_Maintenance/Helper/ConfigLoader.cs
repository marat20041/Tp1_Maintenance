using System.IO;
using System.Text.Json;

public static class ConfigLoader
{
    public static HelperConfig LoadConfig(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<HelperConfig>(json)!;
    }
}
