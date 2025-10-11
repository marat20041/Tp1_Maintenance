using SchoolManager;
using System;
using System.Text.Json;

class Payed
{

    public static void Principals()
    {

        List<Task> payments = new List<Task>();
        foreach (Principal principal in Principal.Principals)
        {
            Task payment = new Task(principal.Pay);
            payments.Add(payment);
            payment.Start();
            int amountPaid = principal.Income;
             UndoManager.PushPayment(
                payement: amountPaid,
                undo: () => Principal.Principals.Remove(principal));
        }

        Task.WaitAll(payments.ToArray());
        CompletedPayment();
    }

    public static void Receptionists()
    {
        List<Task> payments = new List<Task>();
        foreach (Receptionist receptionist in Receptionist.Receptionists)
        {
            Task payment = new Task(receptionist.Pay);
            payments.Add(payment);
            payment.Start();
            int amountPaid = receptionist.Income;

            UndoManager.PushPayment(
                payement: amountPaid,
                undo: () => Receptionist.Receptionists.Remove(receptionist));
            
        }

        Task.WaitAll(payments.ToArray());
        CompletedPayment();
    }

    public static void Students()
    {
        Console.WriteLine(ReferenceText.Get("StudentPayed"));
    }

    public static void Teachers()
    {
        List<Task> payments = new List<Task>();
        foreach (Teacher teacher in Teacher.Teachers)
        {
            Task payment = new Task(teacher.Pay);
            payments.Add(payment);
            payment.Start();
               int amountPaidTeacher = teacher.Income;

            UndoManager.PushPayment(
                payement: amountPaidTeacher,
                undo: () => Teacher.Teachers.Remove(teacher));
        }

        Task.WaitAll(payments.ToArray());
        CompletedPayment();
    }

    public static void CompletedPayment()
    {
        Console.WriteLine(ReferenceText.Get("ConfirmPay"));
    }
}