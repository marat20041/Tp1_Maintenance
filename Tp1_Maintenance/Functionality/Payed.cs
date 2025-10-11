using SchoolManager;
using System;
using System.Text.Json;


/// <summary>
/// Gère le paiement des membres de l’école (principal, réceptionniste, enseignants)
/// et fournit une confirmation une fois le paiement effectué.
/// </summary>
class Payed
{

    /// <summary>Paye un principal et attend la fin de l’opération.</summary>
    public static void PayPrincipal()
    {
        List<Task> payments = new List<Task>();

        foreach (Principal principal in Principal.Principals.ToList())
        {
            Task payment = new Task(principal.Pay);
            payments.Add(payment);
            payment.Start();
            int amountPaidPrincipal = principal.Income;

            UndoManager.PushPayment(
                payement: amountPaidPrincipal,
                undo: () => Principal.RemovePrincipal(principal));
        }
        Task.WaitAll(payments.ToArray());
        CompletedPayment();
    }

    /// <summary>Paye un réceptionniste et attend la fin de l’opération.</summary>
    public static void PayReceptionist()
    {

        List<Task> payments = new List<Task>();
        foreach (Receptionist receptionist in Receptionist.Receptionists.ToList())
        {
            Task payment = new Task(receptionist.Pay);
            payments.Add(payment);
            payment.Start();
            int amountPaidTeacher = receptionist.Income;

            UndoManager.PushPayment(
                payement: amountPaidTeacher,
                undo: () => Receptionist.RemoveReceptionist(receptionist));

        }
        Task.WaitAll(payments.ToArray());
        CompletedPayment();
    }

    /// <summary>Affiche un message indiquant que les étudiants ne peuvent pas recevoir de paiement.</summary>
    public static void Students()
    {
        Console.WriteLine(ReferenceText.Get("StudentPayed"));
    }

    // <summary>Paye tous les enseignants, enregistre chaque paiement pour un undo, et attend la fin.</summary>
    public static void PayAllTeachers()
    {
        List<Task> payments = new List<Task>();
        foreach (Teacher teacher in Teacher.Teachers.ToList())
        {
            Task payment = new Task(teacher.Pay);
            payments.Add(payment);
            payment.Start();
            int amountPaidTeacher = teacher.Income;

            UndoManager.PushPayment(
                payement: amountPaidTeacher,
                undo: () => Teacher.RemoveTeacher(teacher));
        }

        Task.WaitAll(payments.ToArray());
        CompletedPayment();
    }

    /// <summary>Affiche un message confirmant que le paiement est complété.</summary>
    public static void CompletedPayment()
    {
        Console.WriteLine(ReferenceText.Get("ConfirmPayed"));
    }
}