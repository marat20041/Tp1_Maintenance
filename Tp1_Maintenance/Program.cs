using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManager
{
    public class Program
    {
        static public UndoManager Undo = new UndoManager();
        static public Receptionist Receptionist;


        public static void AddPrincipal()
        {
            SchoolMember member = Util.ConsoleHelper.AskAttributes();
            int income = Util.ConsoleHelper.AskQuestionInt("Enter income: ");
            Principal newPrincipal = new Principal(member.Name, member.Address, member.Phone, income);
            Undo.Push(
                    name: $"Undo: add student '{newPrincipal.Name}'",
                    undo: () => Principal.Principals.Remove(newPrincipal));
        }

        public static void AddReceptionist()
        {
            SchoolMember member = Util.ConsoleHelper.AskAttributes();
            int income = Util.ConsoleHelper.AskQuestionInt("Enter income: ");
            Receptionist newReceptionist = new Receptionist(member.Name, member.Address, member.Phone, income);
            Undo.Push(
                    name: $"Undo: add student '{newReceptionist.Name}'",
                    undo: () => Receptionist.Receptionists.Remove(newReceptionist));
        }

        private static void addStudent()
        {
            SchoolMember member = Util.ConsoleHelper.AskAttributes();
            int grade = Util.ConsoleHelper.AskQuestionInt("Enter grade: ");
            Student newStudent = new Student(member.Name, member.Address, member.Phone, grade);
            Undo.Push(
                    name: $"Undo: add student '{newStudent.Name}'",
                    undo: () => Student.Students.Remove(newStudent));
        }

        private static void addTeacher()
        {
            SchoolMember member = Util.ConsoleHelper.AskAttributes();
            string subject = Util.ConsoleHelper.AskQuestion("Enter subject: ");
            Teacher newTeacher = new Teacher(member.Name, member.Address, member.Phone, subject);
            Undo.Push(
                    name: $"Undo: add student '{newTeacher.Name}'",
                    undo: () => Teacher.Teachers.Remove(newTeacher));
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

                    AddPrincipal();
                    break;
                case 2:
                    addTeacher();
                    break;
                case 3:
                    addStudent();
                    break;
                case 4:
                    AddReceptionist();
                    break;
                default:
                    Console.WriteLine("Invalid input. Terminating operation.");
                    Console.WriteLine(memberType);
                    break;
            }
        }

        private static void display()
        {
            int memberType = Util.ConsoleHelper.AskMemberType();

            switch (memberType)
            {
                case 1:
                    Console.WriteLine("\nThe Principal's details are:");
                    foreach (Principal principal in Principal.Principals)
                        principal.Display();
                    break;
                case 2:
                    Console.WriteLine("\nThe teachers are:");
                    foreach (Teacher teacher in Teacher.Teachers)
                        teacher.Display();
                    break;
                case 3:
                    Console.WriteLine("\nThe students are:");
                    foreach (Student student in Student.Students)
                        student.Display();
                    break;
                case 4:
                    Console.WriteLine("\nThe Receptionist's details are:");
                    foreach (Receptionist receptionist in Receptionist.Receptionists)
                        receptionist.Display();
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
                    foreach (Principal principal in Principal.Principals)
                    {
                        Task payment = new Task(principal.Pay);
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
                    foreach (Receptionist receptionist in Receptionist.Receptionists)
                    {
                        Task payment = new Task(receptionist.Pay);
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
            Receptionist.HandleComplaint();
        }

        private static void handleComplaintRaised(object sender, Complaint complaint)
        {
            Console.WriteLine("\nThis is a confirmation that we received your complaint. The details are as follows:");
            Console.WriteLine($"---------\nComplaint Time: {complaint.ComplaintTime.ToLongDateString()}, {complaint.ComplaintTime.ToLongTimeString()}");
            Console.WriteLine($"Complaint Raised: {complaint.ComplaintRaised}\n---------");
        }

        private static async Task showPerformance()
        {
            double average = await Task.Run(() => Student.averageGrade(Student.Students));
            Console.WriteLine($"The student average performance is: {average}");
        }
        /* Ajustement des parametres suite à la modification du type de phone
        - supprimer la boucle for   qui donne des valeurs inutiles
        */



        /*  private static void addData()
          {
              Receptionist = new Receptionist("Receptionist", "address", "123");
              Receptionist.ComplaintRaised += handleComplaintRaised;

              Principal = new Principal("Principal", "address", "123");

               for (int i = 0; i < 10; i++)
               {
                   Student.Students.Add(new Student(i.ToString(), i.ToString(), i.ToString()));
                   Teacher.Teachers.Add(new Teacher(i.ToString(), i.ToString(), i.ToString()));
               } 

          }*/

        public static async Task Main(string[] args)
        {
            // Just for manual testing purposes.
            //  addData();

            Console.WriteLine("-------------- Welcome ---------------\n");

            //Console.WriteLine("Please enter the Princpals information.");
            //AddPrincpal();

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
                        display();
                        break;
                    case 3:
                        Pay();
                        break;
                    case 4:
                        RaiseComplaint();
                        break;
                    case 5:
                        await showPerformance();
                        break;
                    case 6:
                        Remove();
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
