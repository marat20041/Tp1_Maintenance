using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManager
{
    public class Program
    {
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
        public static void UndoLast()
        {
            Console.WriteLine(UndoManager.Undo());
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
                    Added.Principals();
                    break;
                case 2:
                    Added.Teachers();
                    break;
                case 3:
                    Added.Students();
                    break;
                case 4:
                    Added.Receptionists();
                    break;
                default:
                    Displayed.InvalidInput();
                    break;
            }
        }

        /*
        * Cette méthode gère l'affichage des informations selon le type 
        * de membre selectionné
        */
        private static void display()
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

        /*
        * Cette recoit un choix de membre à partir de la console et traite la paie  
        */
        public static void Pay()
        {
            int memberType = Util.ConsoleHelper.AskMemberType();

            Console.WriteLine("\nPayments in progress...");

            switch (memberType)
            {
                case 1:
                    Payed.Principals();
                    break;
                case 2:
                    Payed.Teachers();
                    break;
                case 3:
                    Payed.Students();
                    break;
                case 4:
                    Payed.Receptionists();
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
                        display();
                        break;
                    case 3:
                        Pay();
                        break;
                    case 4:
                        Complaints.RaiseComplaint();
                        break;
                    case 5:
                        await Performance.showPerformance();
                        break;
                    case 6:
                        Remove();
                        break;
                    case 7:
                        RemovePay();
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
