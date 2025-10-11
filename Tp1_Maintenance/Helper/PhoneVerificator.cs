using System;
using System.Text.RegularExpressions;


/// <summary>
/// Fournit une méthode pour valider les numéros de téléphone selon la configuration spécifiée.
/// Vérifie la présence, la longueur et le format via une expression régulière.
/// </summary>
public class PhoneVerificator
{

    /// <summary>
    /// Vérifie si un numéro de téléphone est valide.
    /// </summary>
    /// <param name="phone">Numéro de téléphone à valider.</param>
    /// <param name="config">Configuration contenant les contraintes de validation.</param>
    /// <returns>True si le numéro est valide, false sinon.</returns>
    /// <exception cref="Exception">Si la configuration n’est pas fournie.</exception>
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
