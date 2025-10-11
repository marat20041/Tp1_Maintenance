using SchoolManager;
using System.Text.Json;

class Displayed
{
    public static void Principals()
    {
        Console.WriteLine("\nThe Principal's details are:");
        foreach (Principal principal in Principal.Principals)
            Console.WriteLine(principal.Display());
    }

    public static void Receptionists()
    {
        Console.WriteLine("\nThe Receptionist's details are:");
        foreach (Receptionist receptionist in Receptionist.Receptionists)
            Console.WriteLine(receptionist.Display());

    }

    public static void Students()
    {
        Console.WriteLine(ReferenceText.Get("ListStudents"));
        foreach (Student student in Student.Students)
            Console.WriteLine(student.Display());
    }

    public static void Teachers()
    {
        Console.WriteLine(ReferenceText.Get("ListTeachers"));
        foreach (Teacher teacher in Teacher.Teachers)
            Console.WriteLine(teacher.Display());
    }

    public static void InvalidInput()
    {
        Console.WriteLine(ReferenceText.Get("InvalidInput"));
    }

}

