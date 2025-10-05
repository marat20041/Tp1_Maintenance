using System.Linq;
using System.Text.RegularExpressions;

class PhoneVerificator
{
   public static bool IsValidPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            Console.WriteLine(" The number is empty");
            return false;
        }

        // Nettoyage
        string cleaned = phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

        if (!cleaned.All(char.IsDigit))
        {
            Console.WriteLine("The number must contain only numbers");
            return false;
        }

        if (cleaned.Length < 10 || cleaned.Length > 15)
        {
            Console.WriteLine(" The number must be between 10 and 15 digits.");
            return false;
        }

        // Regex (facultatif)
        string pattern = @"^\+?[0-9\s\-\(\)]{10,20}$";
        if (!Regex.IsMatch(phone, pattern))
        {
            Console.WriteLine("The number format is invalid");
            return false;
        }
        return true;
    }
}
