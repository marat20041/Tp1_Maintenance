using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComplaintEventArgsNamespace;

namespace SchoolManager
{
    public class Program
    {
        static public Receptionist? Receptionist;
        static public Principal? Principal;

        private static void UndoLast()
        {
            Console.WriteLine(UndoManager.Undo());
        }

        private static void handleComplaintRaised(object? sender, ComplaintEventArgs complaint)
        {
            Console.WriteLine("\nThis is a confirmation that we received your complaint. The details are as follows:");
            Console.WriteLine($"---------\nComplaint Time: {complaint.ComplaintTime.ToLongDateString()}, {complaint.ComplaintTime.ToLongTimeString()}");
            Console.WriteLine($"Complaint Raised: {complaint.ComplaintRaised}\n---------");
        }
        
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
                            Console.WriteLine($"Principal loaded: {Principal != null}");
                            break;

                        case "Receptionist":
                            Receptionist = new Receptionist(m.Name, m.Address, m.Phone, m.Income ?? 0);
                            Receptionist.ComplaintRaised += handleComplaintRaised;
                            Console.WriteLine($"Receptionist loaded: {Receptionist != null}");
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
        
        /* 
        * Cette méthode annule la dernière opération de paie enregistrée  
        */
        public static void RemovePay()
        {
            int memberType = Util.ConsoleHelper.AskMemberType();

            switch (memberType)
            {
                case 1:
                    UndoLastPay();
                    break;
                case 2:
                    UndoLastPay();
                    break;
                case 4:
                    UndoLastPay();
                    break;

                default:
                    Displayed.InvalidInput();
                    break;
            }
        }
        public static void UndoLastPay()
        {
            UndoManager.UndoLastPayement();
        }
        /*
        * Cette méthode annule la dernière opération d'enregistrement enregistrée  
        */

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
                    Displayed.InvalidInput();
                    break;
            }
        }
        /* 
        * Cette méthode traite les demandes d'ajout d'information en fonction
        * du type de membre 
        */
        public static void Add()
        {
            int memberType = Util.ConsoleHelper.AskMemberType();

            switch (memberType)
            {
                case 1:
                    Console.WriteLine(ReferenceText.Get("PrincipalAddDenied"));
                    break;
                case 2:
                    Added.CreateATeacher();
                    break;
                case 3:
                    Added.CreateAStudent();
                    break;
                case 4:
                    Console.WriteLine(ReferenceText.Get("ReceptionistAddDenied"));
                    break;

                default:
                    Displayed.InvalidInput();
                    break;
            }
        }

        private static void Display()
        {
            int memberType = Util.ConsoleHelper.AskMemberType();

            switch (memberType)
            {
                case 1:
                    Displayed.Principals(Principal!);
                    break;
                case 2:
                    Displayed.Teachers();
                    break;
                case 3:
                    Displayed.Students();
                    break;
                case 4:
                    Displayed.Receptionists(Receptionist!);
                    break;
                default:
                    Displayed.InvalidInput();
                    break;
            }
        }

        /*
        * Cette recoit un choix de membre à partir de la console et traite la paie  
        */
        public static void Pay()
        {
            int memberType = Util.ConsoleHelper.AskMemberType();

            Console.WriteLine(ReferenceText.Get("PaymentInProgress"));

            switch (memberType)
            {
                case 1:
                    Payed.PayPrincipal(Principal!);
                    break;
                case 2:
                    Payed.PayAllTeachers();
                    break;
                case 4:
                    Payed.PayReceptionist(Receptionist!);
                    break;
                default:
                   Displayed.InvalidInput();
                    break;
            }


        }

        /* 
        * Ce gestionnaire d'école offre 7 fonctionnalités permettant de traiter les choix de 
        * de l'utilisateur 
        */
        public static async Task Main(string[] args)
        {
            AddData();
            Console.WriteLine(ReferenceText.Get("Welcome"));

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
                        Complaints.RaiseComplaint(Receptionist!);
                        break;
                    case 5:
                        await Performance.showPerformance();
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

            Console.WriteLine(ReferenceText.Get("Bye"));
        }
    }
}
