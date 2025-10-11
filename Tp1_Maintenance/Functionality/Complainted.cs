
using SchoolManager;
using System;

/// <summary>
/// Représente une plainte enregistrée dans le système.
/// Contient le moment, l’auteur et le texte de la plainte.
/// </summary>
public class Complaint : EventArgs
{

    public DateTime ComplaintTime { get; set; }
    public string? ComplaintRaised { get; set; }
    public string? ComplaintText { get; set; }



}