using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public static class ReferenceText
{
    public static Dictionary<string, string> _texts;

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









