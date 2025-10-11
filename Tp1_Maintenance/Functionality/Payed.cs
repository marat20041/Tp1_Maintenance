using SchoolManager;
using System;
using System.Text.Json;

class Payed
{

    public static void PayPrincipal(Principal Principal)
    {
        List<Task> payments = new List<Task>();
        if (Principal != null)
        {
            Task payment = new Task(Principal.Pay);
            payments.Add(payment);
            payment.Start();
        }
        Task.WaitAll(payments.ToArray());
        CompletedPayment();
    }

    public static void PayReceptionist(Receptionist Receptionist)
    {
        List<Task> payments = new List<Task>();
        if (Receptionist != null)
        {
            Task payment = new Task(Receptionist.Pay);
            payments.Add(payment);
            payment.Start();
        }

        Task.WaitAll(payments.ToArray());
        CompletedPayment();
    }

    public static void Students()
    {
        Console.WriteLine(ReferenceText.Get("StudentPayed"));
    }

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

    public static void CompletedPayment()
    {
        Console.WriteLine(ReferenceText.Get("ConfirmPayed"));
    }
}