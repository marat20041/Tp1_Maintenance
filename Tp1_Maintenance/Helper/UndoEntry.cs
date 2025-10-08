public class UndoEntry
{
    public Action Undo { get; }
    public string Name { get; }

    public UndoEntry(string name, Action undo)
    {
        Name = name ?? "Undo last action";
        Undo = undo;
    }
}