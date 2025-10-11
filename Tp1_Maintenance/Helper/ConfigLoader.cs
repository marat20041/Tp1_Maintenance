using System.IO;
using System.Text.Json;

/// <summary>
/// Fournit une méthode pour charger la configuration de l’application depuis un fichier JSON.
/// </summary>
public static class ConfigLoader
{
    /// <summary>
    /// Lit un fichier JSON et désérialise son contenu en une instance de <see cref="HelperConfig"/>.
    /// </summary>
    public static HelperConfig LoadConfig(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<HelperConfig>(json)!;
    }
}
