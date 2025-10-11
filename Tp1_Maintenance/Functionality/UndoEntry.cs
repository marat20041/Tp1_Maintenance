
using System.Text.Json;
public class UndoEntry

/// <summary>
/// Représente une action pouvant être annulée dans le système.
/// Contient le nom de l’action et la méthode à exécuter pour l’annuler.
/// </summary>
{
    /// <summary>Action à exécuter pour annuler l’opération.</summary>
    public Action Undo { get; }

    /// <summary>Nom descriptif de l’action annulable.</summary>
    public string Name { get; }

    public UndoEntry(string name, Action undo)
    {
        Name = name ?? ReferenceText.Get("UndoAction");
        Undo = undo;
    }
}