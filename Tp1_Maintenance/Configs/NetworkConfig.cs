/// <summary>
/// Contient les paramètres de configuration généraux : délais, format de téléphone
/// et revenus par défaut selon le rôle.
/// </summary>
public class HelperConfig
{
    /// <summary>Délai minimal autorisé.</summary>
    public int MinDelay { get; set; }

    /// <summary>Délai maximal autorisé.</summary>
    public int MaxDelay { get; set; }

    /// <summary>Longueur minimale d’un numéro de téléphone.</summary>
    public int MinPhoneLength { get; set; }

    /// <summary>Longueur maximale d’un numéro de téléphone.</summary>
    public int MaxPhoneLength { get; set; }

    /// <summary>Expression régulière de validation du téléphone.</summary>
    public required string PhonePattern { get; set; }

    /// <summary>Revenu par défaut du principal.</summary>
    public int DefaultIncomePrincipal { get; set; }

    /// <summary>Revenu par défaut du réceptionniste.</summary>
    public int DefaultIncomeReceptionist { get; set; }

    /// <summary>Revenu par défaut de l’enseignant.</summary>
    public int DefaultIncomeTeacher { get; set; }
}
