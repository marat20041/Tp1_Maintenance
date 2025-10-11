using Util;
using SchoolManager;
using System;


/// <summary>
/// Gère la création des membres de l’école (principal, réceptionniste, étudiant, enseignant)
/// et enregistre une action d’annulation dans l’UndoManager après chaque ajout.
/// </summary>
class Added
{

    /// <summary>Crée un principal et ajoute une action d’annulation.</summary>
    public static void CreateAPrincipal()
    {
        SchoolMember member = ConsoleHelper.AskAttributes();
        int income = ConsoleHelper.AskQuestionInt("Enter income (optional, 0 for default): ");
        Principal newPrincipal = new Principal(member.Name, member.Address, member.Phone, income == 0 ? null : income);

        UndoManager.Push(
            name: $"Undo: add principal '{newPrincipal.Name}'",
            undo: () => Principal.RemovePrincipal(newPrincipal));
    }


    /// <summary>Crée un réceptionniste et ajoute une action d’annulation.</summary>
    public static void CreateAReceptionist()
    {
        SchoolMember member = ConsoleHelper.AskAttributes();
        int income = ConsoleHelper.AskQuestionInt("Enter income (optional, 0 for default): ");
        Receptionist newReceptionist = new Receptionist(member.Name, member.Address, member.Phone, income == 0 ? null : income);

        UndoManager.Push(
            name: $"Undo: add receptionist '{newReceptionist.Name}'",
            undo: () => Receptionist.RemoveReceptionist(newReceptionist));
    }


    /// <summary>Crée un étudiant et ajoute une action d’annulation.</summary>
    public static void CreateAStudent()
    {
        SchoolMember member = ConsoleHelper.AskAttributes();
        int grade = ConsoleHelper.AskQuestionInt("Enter grade: ");
        Student newStudent = new Student(member.Name, member.Address, member.Phone, grade);

        UndoManager.Push(
                name: $"Undo: add student '{newStudent.Name}'",
                undo: () => Student.RemoveStudent(newStudent));
    }

    /// <summary>Crée un enseignant et ajoute une action d’annulation.</summary>
    public static void CreateATeacher()
    {
        SchoolMember member = ConsoleHelper.AskAttributes();
        string subject = ConsoleHelper.AskQuestion("Enter subject: ");
        int income = ConsoleHelper.AskQuestionInt("Enter income (optional, 0 for default): ");
        Teacher newTeacher = new Teacher(member.Name, member.Address, member.Phone, subject, income == 0 ? null : income);

        UndoManager.Push(
                name: $"Undo: add teacher '{newTeacher.Name}'",
                undo: () => Teacher.RemoveTeacher(newTeacher));
    }
}