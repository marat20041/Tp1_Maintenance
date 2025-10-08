public class UndoManager
{
    public bool CanUndo => _history.Count > 0;
    private readonly Stack<UndoEntry> _history = new();

    public void Push(string name, Action undo)
    {
        _history.Push(new UndoEntry(name, undo));
    }

    public string Undo()
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
}