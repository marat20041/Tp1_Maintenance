using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComplaintEventArgsNamespace;


/// <summary>
/// Point d'entrée principal de l'application de gestion scolaire.
/// Gère les ajouts, affichages, paiements, plaintes et annulations pour les membres de l'école.
/// </summary>
namespace SchoolManager
{
    public class Program
    {

        static public Receptionist? Receptionist;

        /// <summary>Annule la dernière opération de paie enregistrée.</summary>
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


        /// <summary>Annule le dernier paiement et affiche la confirmation.</summary>
        public static void UndoLastPay()
        {
            UndoManager.UndoLastPayement();
        }

        /// <summary>Annule la dernière opération d'enregistrement après confirmation utilisateur.</summary>
        public static void Remove()
        {
            if (!UndoManager.CanUndo)
            {
                Console.WriteLine(ReferenceText.Get("NoActionToUndo"));
                return;
            }

            Console.Write("Are you sure you want to cancel the last registration operation (y/n)? ");
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "y" || input == "yes")
            {
                UndoManager.Undo();
                Console.WriteLine(ReferenceText.Get("UndoConfirmed"));
            }
            else
            {
                Console.WriteLine(ReferenceText.Get("UndoCancelled"));
            }
        }

        /// <summary>Ajoute un nouveau membre selon le type choisi par l'utilisateur.</summary>
        public static void Add()
        {
            int memberType = Util.ConsoleHelper.AskMemberType();

            switch (memberType)
            {
                case 1:
                    Added.CreateAPrincipal();
                    break;
                case 2:
                    Added.CreateATeacher();
                    break;
                case 3:
                    Added.CreateAStudent();
                    break;
                case 4:
                    Added.CreateAReceptionist();
                    break;

                default:
                    Displayed.InvalidInput();
                    break;
            }
        }

        /// <summary>Affiche les informations d'un membre selon le type choisi par l'utilisateur.</summary>
        private static void Display()
        {
            int memberType = Util.ConsoleHelper.AskMemberType();

            switch (memberType)
            {
                case 1:
                    Displayed.Principals();
                    break;
                case 2:
                    Displayed.Teachers();
                    break;
                case 3:
                    Displayed.Students();
                    break;
                case 4:
                    Displayed.Receptionists();
                    break;
                default:
                    Displayed.InvalidInput();
                    break;
            }
        }


        /// <summary>Traite la paie d'un membre ou groupe de membres selon le type choisi.</summary>
        public static void Pay()
        {
            int memberType = Util.ConsoleHelper.AskMemberType();

            Console.WriteLine(ReferenceText.Get("PaymentInProgress"));

            switch (memberType)
            {
                case 1:
                    Payed.PayPrincipal();
                    break;
                case 2:
                    Payed.PayAllTeachers();
                    break;
                case 4:
                    Payed.PayReceptionist();
                    break;
                default:
                    Displayed.InvalidInput();
                    break;
            }


        }

        /// <summary>
        /// Boucle principale de l'application, gère le menu et les choix de l'utilisateur.
        /// </summary>
        public static async Task Main(string[] args)
        {
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
                        await Performance.ShowPerformance();
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
