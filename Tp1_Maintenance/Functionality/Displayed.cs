using SchoolManager;
using System.Text.Json;

/// <summary>
/// Fournit des méthodes pour afficher les listes de membres de l’école
/// et des messages d’erreur pour les entrées invalides.
/// </summary>
class Displayed
{
    /// <summary>Affiche tous les principals existants.</summary>
    public static void Principals()
    {
        Console.WriteLine(ReferenceText.Get("ListPrincipals"));
        foreach (Principal principal in Principal.Principals)
            Console.WriteLine(principal.Display());
    }

/// <summary>Affiche tous les réceptionnistes existants.</summary>
    public static void Receptionists()
    {
        Console.WriteLine(ReferenceText.Get("ListReceptionists"));
        foreach (Receptionist receptionist in Receptionist.Receptionists)
            Console.WriteLine(receptionist.Display());

    }

    /// <summary>Affiche tous les étudiants existants.</summary>
    public static void Students()
    {
        Console.WriteLine(ReferenceText.Get("ListStudents"));
        foreach (Student student in Student.Students)
            Console.WriteLine(student.Display());
    }


    /// <summary>Affiche tous les enseignants existants.</summary>
    public static void Teachers()
    {
        Console.WriteLine(ReferenceText.Get("ListTeachers"));
        foreach (Teacher teacher in Teacher.Teachers)
            Console.WriteLine(teacher.Display());
    }

    
    /// <summary>Affiche un message d’erreur pour une entrée invalide.</summary>
    public static void InvalidInput()
    {
        Console.WriteLine(ReferenceText.Get("InvalidInput"));
    }

}

