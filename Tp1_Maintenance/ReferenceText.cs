using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public static class ReferenceText
{
    public static Dictionary<string, string> _texts;

    static ReferenceText()
    {
        // Charger le fichier JSON
        string json = File.ReadAllText("ReferenceText.json");
        _texts = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }

    public static string Get(string key)
    {
        if (_texts.ContainsKey(key))
            return _texts[key];
        else
            return $"[Message not found: {key}]";
    }
    
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









