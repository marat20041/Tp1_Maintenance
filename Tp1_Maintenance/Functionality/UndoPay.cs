/// <summary>
/// Représente un paiement pouvant être annulé.
/// Contient le montant payé et l’action à exécuter pour annuler le paiement.
/// </summary>
public class UndoPay
{
    /// <summary>Action à exécuter pour annuler le paiement.</summary>
    public Action Undo { get; }


    /// <summary>Montant du paiement.</summary>
    public int Payment { get; }

    /// <summary>
    /// Initialise une nouvelle entrée de paiement annulable.
    /// </summary>
    public UndoPay(int payement, Action undo)
    {
        Payment = payement;
        Undo = undo;
    }
}