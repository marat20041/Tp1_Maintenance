
using System.Text.Json;
public class UndoEntry

{
    public Action Undo { get; }
    public string Name { get; }

    public UndoEntry(string name, Action undo)
    {
        Name = name ?? ReferenceText.Get("UndoAction");
        Undo = undo;
    }
}