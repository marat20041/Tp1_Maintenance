using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;


/// <summary>
/// Fournit un accès centralisé aux textes et messages de référence de l'application.
/// Les textes sont chargés depuis un fichier JSON et peuvent être récupérés ou formatés.
/// </summary>
public static class ReferenceText
{
    /// <summary>Dictionnaire interne contenant les clés et valeurs de texte chargées depuis le JSON.</summary>
    public static Dictionary<string, string> _texts;

    /// <summary>
    /// Initialise la classe et charge les textes depuis le fichier "ReferenceText.json".
    /// </summary>
    static ReferenceText()
    {
        string json = File.ReadAllText("ReferenceText.json");
        var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json)
                   ?? new Dictionary<string, JsonElement>();

        _texts = new Dictionary<string, string>();
        foreach (var kv in dict)
        {
            if (kv.Value.ValueKind == JsonValueKind.String)
            {
                _texts[kv.Key] = kv.Value.GetString()!;
            }
        }
    }


    /// <summary>
    /// Récupère le texte correspondant à la clé spécifiée.
    /// </summary>
    /// <param name="key">Clé du texte à récupérer.</param>
    /// <returns>Texte correspondant ou un message d'erreur si la clé n'existe pas.</returns>
    public static string Get(string key)
    {
        if (_texts.ContainsKey(key))
            return _texts[key];
        else
            return $"[Message not found: {key}]";
    }

    /// <summary>
    /// Récupère un texte et remplace les clés de paramètres par leurs valeurs correspondantes.
    /// </summary>
    /// <param name="key">Clé du texte à récupérer.</param>
    /// <param name="values">Dictionnaire de valeurs à remplacer dans le texte.</param>
    /// <returns>Texte formaté.</returns>
    public static string Format(string key, Dictionary<string, string> values)
    {
        string text = Get(key);
        foreach (var pair in values)
        {
            text = text.Replace("{" + pair.Key + "}", pair.Value);
        }
        return text;
    }
}









