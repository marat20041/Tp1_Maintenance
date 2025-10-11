using System.Linq;
using System.Text.RegularExpressions;
using System.Text.Json;
class PhoneVerificator

{
    /*- Ce programme vérifie 
        - les valeurs entrées 
        - la longueur des chiffres 
        - vérifie les caractères autorisés
        - Les recherches de PhoneVerificator sont en partie soutenue par une IA
        */
    public static bool IsValidPhone(string phone)
    {

        if (string.IsNullOrWhiteSpace(phone))
        {
            Console.WriteLine(ReferenceText.Get("PhoneEmpty"));
            return false;
        }


        string cleaned = phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

        if (!cleaned.All(char.IsDigit))
        {
            Console.WriteLine(ReferenceText.Get("PhoneType"));
            return false;
        }

        if (cleaned.Length < 10 || cleaned.Length > 15)
        {
            Console.WriteLine(ReferenceText.Get("PhoneLonger"));
            return false;
        }


        string pattern = @"^\+?[0-9\s\-\(\)]{10,20}$";
        if (!Regex.IsMatch(phone, pattern))
        {
            Console.WriteLine(ReferenceText.Get("PhoneChar"));
            return false;
        }
        return true;
    }
}
