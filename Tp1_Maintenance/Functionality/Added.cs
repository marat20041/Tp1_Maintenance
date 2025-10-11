using Util;
using SchoolManager;
using System;


class Added
{   
    public static void CreateAStudent()
    {
        SchoolMember member = ConsoleHelper.AskAttributes();
        int grade = ConsoleHelper.AskQuestionInt("Enter grade: ");
        Student newStudent = new Student(member.Name, member.Address, member.Phone, grade);

        UndoManager.Push(
                name: $"Undo: add student '{newStudent.Name}'",
                undo: () => Student.RemoveStudent(newStudent));
    }

    public static void CreateATeacher()
    {
        SchoolMember member = ConsoleHelper.AskAttributes();
        string subject = ConsoleHelper.AskQuestion("Enter subject: ");
        int income = ConsoleHelper.AskQuestionInt("Enter income: ");
        Teacher newTeacher = new Teacher(member.Name, member.Address, member.Phone, subject,income);

        UndoManager.Push(
                name: $"Undo: add student '{newTeacher.Name}'",
                undo: () => Teacher.RemoveTeacher(newTeacher));
    }
}