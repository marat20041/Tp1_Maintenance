public static class UndoManager
{
    /// <summary>
    /// Gère l’historique des actions annulables et des paiements.
    /// Permet de revenir en arrière sur les ajouts/suppressions de membres ou les paiements.
    /// </summary>

    /// <summary>Indique s’il existe des actions à annuler.</summary>
    public static bool CanUndo => _history.Count > 0;
    private static readonly Stack<UndoEntry> _history = new();

    
    /// <summary>Enregistre une action annulable dans l’historique.</summary>
    public static void Push(string name, Action undo)
    {
        _history.Push(new UndoEntry(name, undo));
    }

    /// <summary>Annule la dernière action enregistrée et retourne son nom.</summary>
    public static string Undo()
    {
        if (_history.Count == 0)
        {
            return ReferenceText.Get("NoActionToUndo");
        }
        else
        {
            UndoEntry entry = _history.Pop();
            entry.Undo();
            return entry.Name;
        }

    }


    private static readonly Stack<UndoPay> _information = new();

    /// <summary>Enregistre un paiement annulable.</summary>
    public static void PushPayment(int payement, Action undo)
    {
        _information.Push(new UndoPay(payement, undo));
    }

    /// <summary>Annule le dernier paiement et retourne le montant payé.</summary>
    public static int UndoLastPayement()
    {
        if (_information.Count == 0)
        {
            Console.WriteLine(ReferenceText.Get("NoActionToUndo"));
            return 0;

        }
        else
        {
            UndoPay entry = _information.Pop();
            entry.Undo();
            Console.WriteLine(ReferenceText.Format("LastPay", new Dictionary<string, string>
    {
        { "amount", entry.Payment.ToString() }
    }));
            return entry.Payment;
        }



    }
}