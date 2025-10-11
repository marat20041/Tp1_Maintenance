using SchoolManager;
using System.Text.Json;

class Displayed
{
    public static void Principals(Principal Principal)
    {
        Console.WriteLine("\nThe Principal's details are:");
                    Console.WriteLine(Principal?.Display());
    }

    public static void Receptionists(Receptionist Receptionist)
    {
        Console.WriteLine("\nThe Receptionist's details are:");
                    Console.WriteLine(Receptionist?.Display());

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

