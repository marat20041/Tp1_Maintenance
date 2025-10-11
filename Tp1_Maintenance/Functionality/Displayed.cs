using SchoolManager;
using System.Text.Json;

class Displayed
{
    public static void Principals()
    {
        Console.WriteLine(ReferenceText.Get("ListPrincipal"));
        foreach (Principal principal in Principal.Principals)
            principal.Display();
    }

    public static void Receptionists()
    {
        Console.WriteLine(ReferenceText.Get("ListReceptionist"));
        foreach (Receptionist receptionist in Receptionist.Receptionists)
            receptionist.Display();

    }

    public static void Students()
    {
        Console.WriteLine(ReferenceText.Get("Liststudent"));
        foreach (Student student in Student.Students)
            student.Display();
    }

    public static void Teachers()
    {
        Console.WriteLine(ReferenceText.Get("ListTeacher"));
        foreach (Teacher teacher in Teacher.Teachers)
            teacher.Display();
    }

    public static void InvalidInput()
    {
        Console.WriteLine(ReferenceText.Get("InvalidInput"));
    }

}

