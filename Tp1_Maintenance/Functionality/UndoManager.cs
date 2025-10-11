public static class UndoManager
{
    /* ce code enregistre les informations des membres selon leur types 
    lorsque activé retire et retourne la liste demandée a l'exception du dernier membre 
    enregistré
    */
    public static bool CanUndo => _history.Count > 0;
    private static readonly Stack<UndoEntry> _history = new();

    public static void Push(string name, Action undo)
    {
        _history.Push(new UndoEntry(name, undo));
    }

    public static string Undo()
    {
        if (_history.Count == 0)
        {
            return "No action to undo";
        }
        else
        {
            UndoEntry entry = _history.Pop();
            entry.Undo();
            return entry.Name;
        }

    }


    private static readonly Stack<UndoPay> _information = new();

    public static void PushPayment(int payement, Action undo)
    {
        _information.Push(new UndoPay(payement, undo));
    }

    public static int UndoLastPayement()
    {
        if (_information.Count == 0)
        {
            Console.WriteLine("No action to undo ");
            return 0;

        }
        else
        {
            UndoPay entry = _information.Pop();
            entry.Undo();
            Console.WriteLine($"Last pay : {entry.Payment}");
            return entry.Payment;
        }
       

    }
}