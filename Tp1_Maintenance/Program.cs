using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComplaintEventArgsNamespace;

namespace SchoolManager
{
    public class Program
    {
        static public UndoManager Undo = new UndoManager();
        static public Receptionist? Receptionist;
        static public Principal? Principal;
        
        private static void AddStudent()
        {
            SchoolMember member = Util.ConsoleHelper.AskAttributes();
            int grade = Util.ConsoleHelper.AskQuestionInt("Enter grade: ");
            Student newStudent = new Student(member.Name, member.Address, member.Phone, grade);
            Undo.Push(
                    name: $"Undo: add student '{newStudent.Name}'",
                    undo: () => Student.RemoveStudent(newStudent));
        }

        private static void AddTeacher()
        {
            SchoolMember member = Util.ConsoleHelper.AskAttributes();
            string subject = Util.ConsoleHelper.AskQuestion("Enter subject: ");
            int income = Util.ConsoleHelper.AskQuestionInt("Enter income: ");
            Teacher newTeacher = new Teacher(member.Name, member.Address, member.Phone, subject, income);
            Undo.Push(
                    name: $"Undo: add student '{newTeacher.Name}'",
                    undo: () => Teacher.RemoveTeacher(newTeacher));
        }
        private static void UndoLast()
        {
            Console.WriteLine(Undo.Undo());
        }

        public static void Remove()
        {
            int memberType = Util.ConsoleHelper.AskMemberType();
            switch (memberType)
            {
                case 1:
                    UndoLast();
                    break;
                case 2:
                    UndoLast();
                    break;

                case 3:
                    UndoLast();
                    break;
                case 4:
                    UndoLast();
                    break;

                default:
                    Console.WriteLine("Invalid input. Terminating operation.");
                    break;
            }
        }

        public static void Add()
        {
            Console.WriteLine("\nPlease note that the Principal/Receptionist details cannot be added or modified now.");
            int memberType = Util.ConsoleHelper.AskMemberType();

            switch (memberType)
            {
                case 1:
                    Console.WriteLine("The Principal details cannot be added or modified now.");
                    break;
                case 2:
                    AddTeacher();
                    break;
                case 3:
                    AddStudent();
                    break;
                default:
                    Console.WriteLine("Invalid input. Terminating operation.");
                    Console.WriteLine(memberType);
                    break;
            }
        }

        private static void Display()
        {
            int memberType = Util.ConsoleHelper.AskMemberType();

            switch (memberType)
            {
                case 1:
                    Console.WriteLine("\nThe Principal's details are:");
                    Console.WriteLine(Principal?.Display());
                    break;
                case 2:
                    Console.WriteLine("\nThe teachers are:");
                    foreach (Teacher teacher in Teacher.Teachers)
                        Console.WriteLine(teacher.Display());
                    break;
                case 3:
                    Console.WriteLine("\nThe students are:");
                    foreach (Student student in Student.Students)
                        Console.WriteLine(student.Display());
                    break;
                case 4:
                    Console.WriteLine("\nThe Receptionist's details are:");
                    Console.WriteLine(Receptionist?.Display());
                    break;
                default:
                    Console.WriteLine("Invalid input. Terminating operation.");
                    break;
            }
        }

        public static void Pay()
        {
            Console.WriteLine("\nPlease note that the students cannot be paid.");
            int memberType = Util.ConsoleHelper.AskMemberType();

            Console.WriteLine("\nPayments in progress...");

            List<Task> payments = new List<Task>();
            switch (memberType)
            {

                case 1:
                    if (Principal != null)
                    {
                        Task payment = new Task(Principal.Pay);
                        payments.Add(payment);
                        payment.Start();
                    }

                    Task.WaitAll(payments.ToArray());
                    break;
                case 2:
                    foreach (Teacher teacher in Teacher.Teachers)
                    {
                        Task payment = new Task(teacher.Pay);
                        payments.Add(payment);
                        payment.Start();
                    }

                    Task.WaitAll(payments.ToArray());

                    break;
                case 4:
                    if (Receptionist != null)
                    {
                        Task payment = new Task(Receptionist.Pay);
                        payments.Add(payment);
                        payment.Start();
                    }

                    Task.WaitAll(payments.ToArray());
                    break;
                default:
                    Console.WriteLine("Invalid input. Terminating operation.");
                    break;
            }

            Console.WriteLine("Payments completed.\n");
        }

        public static void RaiseComplaint()
        {
            string complaintText = Util.ConsoleHelper.AskQuestion("Please enter your complaint: ");
            Receptionist?.HandleComplaint(complaintText);
        }

        private static void handleComplaintRaised(object? sender, ComplaintEventArgs complaint)
        {
            Console.WriteLine("\nThis is a confirmation that we received your complaint. The details are as follows:");
            Console.WriteLine($"---------\nComplaint Time: {complaint.ComplaintTime.ToLongDateString()}, {complaint.ComplaintTime.ToLongTimeString()}");
            Console.WriteLine($"Complaint Raised: {complaint.ComplaintRaised}\n---------");
        }

        private static async Task ShowPerformance()
        {
            double average = await Task.Run(() => Student.AverageGrade(Student.Students));
            Console.WriteLine($"The student average performance is: {average}");
        }
        /* Ajustement des parametres suite à la modification du type de phone
        - supprimer la boucle for   qui donne des valeurs inutiles
        */

        private static void AddData()
        {
            try
            {
                if (!File.Exists("config.json"))
                {
                    Console.WriteLine("Configuration file not found.");
                    return;
                }

                var text = File.ReadAllText("config.json");
                var members = System.Text.Json.JsonSerializer.Deserialize<List<MemberConfig>>(text);

                if (members == null)
                {
                    Console.WriteLine("No configuration data found.");
                    return;
                }

                foreach (var m in members)
                {
                    switch (m.Role)
                    {
                        case "Principal":
                            Principal = new Principal(m.Name, m.Address, m.Phone, m.Income ?? 0);
                            break;

                        case "Receptionist":
                            Receptionist = new Receptionist(m.Name, m.Address, m.Phone, m.Income ?? 0);
                            Receptionist.ComplaintRaised += handleComplaintRaised;
                            break;

                        case "Teacher":
                            if (string.IsNullOrEmpty(m.Subject))
                                Console.WriteLine($"Missing subject for teacher {m.Name}");
                            else
                                new Teacher(m.Name, m.Address, m.Phone, m.Subject, m.Income ?? 0);
                            break;

                        case "Student":
                            if (!m.Grade.HasValue)
                                Console.WriteLine($"Missing grade for student {m.Name}");
                            else
                                new Student(m.Name, m.Address, m.Phone, m.Grade.Value);
                            break;

                        default:
                            Console.WriteLine($"Unknown role: {m.Role}");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}");
            }
        }



        public static async Task Main(string[] args)
        {
            AddData();

            Console.WriteLine("-------------- Welcome ---------------\n");

            bool flag = true;
            while (flag)
            {

                int choice = Util.ConsoleHelper.AskChoices();
                switch (choice)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Display();
                        break;
                    case 3:
                        Pay();
                        break;
                    case 4:
                        RaiseComplaint();
                        break;
                    case 5:
                        await ShowPerformance();
                        break;
                    case 6:
                        Remove();
                        break;
                    case 7:
                        flag = false;
                        break;
                    default:
                        flag = false;
                        break;
                }
            }

            Console.WriteLine("\n-------------- Bye --------------");
        }
    }
}
