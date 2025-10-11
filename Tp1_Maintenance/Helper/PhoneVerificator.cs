using System;
using System.Text.RegularExpressions;

public class PhoneVerificator
{
    public static bool IsValidPhone(string phone, HelperConfig config)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            Console.WriteLine(ReferenceText.Get("EmptyPhone"));
            return false;
        }

        if (config == null) throw new Exception(ReferenceText.Get("ConfigNotLoaded"));

        string cleaned = phone.Replace("-", "").Replace(" ", "");

        if (cleaned.Length < config.MinPhoneLength || cleaned.Length > config.MaxPhoneLength)
        {
            Console.WriteLine(ReferenceText.Format("InvalidPhoneLength", new Dictionary<string, string>
            {
                { "min", "10" },
                { "max", "15" }
            }));
            return false;
        }

        string pattern = config.PhonePattern;
        if (!Regex.IsMatch(phone, pattern))
        {
            Console.WriteLine(ReferenceText.Get("PhoneChar"));
            return false;
        }

        return true;
    }
}
