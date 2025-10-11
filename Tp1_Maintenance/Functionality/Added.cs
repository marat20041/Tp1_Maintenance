using Util;
using SchoolManager;
using System;


class Added
{

    //public static UndoManager Undo = new UndoManager();
    /* supprimer util car non utilisé
    -Renommer les classes vu que la classe est defini
    */
    public static void Principals()
    {
        SchoolMember member = ConsoleHelper.AskAttributes();
        int income = ConsoleHelper.AskQuestionInt("Enter income: ");
        Principal newPrincipal = new Principal(member.Name, member.Address, member.Phone, income);
        Principal.Principals.Add(newPrincipal);
        UndoManager.Push(
                name: $"Undo: add student '{newPrincipal.Name}'",
                undo: () => Principal.Principals.Remove(newPrincipal));
    }

    public static void Receptionists()
    {
        SchoolMember member = ConsoleHelper.AskAttributes();
        int income = ConsoleHelper.AskQuestionInt("Enter income: ");
        Receptionist newReceptionist = new Receptionist(member.Name, member.Address, member.Phone, income);
        Receptionist.Receptionists.Add(newReceptionist);
        UndoManager.Push(

                name: $"Undo: add student '{newReceptionist.Name}'",
                undo: () => Receptionist.Receptionists.Remove(newReceptionist));
    }
    // definir les deux classes en "public" au lieu de "private"
    public static void Students()
    {
        SchoolMember member = ConsoleHelper.AskAttributes();
        int grade = ConsoleHelper.AskQuestionInt("Enter grade: ");
        Student newStudent = new Student(member.Name, member.Address, member.Phone, grade);
        Student.Students.Add(newStudent);
        UndoManager.Push(
                name: $"Undo: add student '{newStudent.Name}'",
                undo: () => Student.Students.Remove(newStudent));
    }

    public static void Teachers()
    {
        SchoolMember member = ConsoleHelper.AskAttributes();
        string subject = ConsoleHelper.AskQuestion("Enter subject: ");
        int income = ConsoleHelper.AskQuestionInt("Enter income: ");
        Teacher newTeacher = new Teacher(member.Name, member.Address, member.Phone, subject,income);
        Teacher.Teachers.Add(newTeacher);
        UndoManager.Push(
                name: $"Undo: add student '{newTeacher.Name}'",
                undo: () => Teacher.Teachers.Remove(newTeacher));
    }
}