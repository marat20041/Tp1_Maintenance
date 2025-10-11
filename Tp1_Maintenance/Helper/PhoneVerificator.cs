using System;
using System.Text.RegularExpressions;

public class PhoneVerificator
{
    private static HelperConfig? _config;

    public static void LoadConfig(HelperConfig config)
    {
        _config = config;
    }

    public static bool IsValidPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            Console.WriteLine("The number is empty");
            return false;
        }

        if (_config == null) throw new Exception("Config not loaded");

        string cleaned = phone.Replace("-", "").Replace(" ", "");

        if (cleaned.Length < _config.MinPhoneLength || cleaned.Length > _config.MaxPhoneLength)
        {
            Console.WriteLine($"The number must be between {_config.MinPhoneLength} and {_config.MaxPhoneLength} digits");
            return false;
        }

        string pattern = _config.PhonePattern;
        if (!Regex.IsMatch(phone, pattern))
        {
            Console.WriteLine(ReferenceText.Get("PhoneChar"));
            return false;
        }

        return true;
    }
}
