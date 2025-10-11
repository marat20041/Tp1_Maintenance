using SchoolManager;
public class Complaints
{
    public static void RaiseComplaint(Receptionist Receptionist)
    {
        string complaintText = Util.ConsoleHelper.AskQuestion("Please enter your complaint: ");
        Receptionist?.HandleComplaint(complaintText);
    }

    private static void handleComplaintRaised(object? sender, Complaint e)
    {
        Console.WriteLine(ReferenceText.Get("ConfirmComplaint"));
        string informationComplaint = ReferenceText.Format("ComplaintDetails", new Dictionary<string, string>
        {
        { "date", e.ComplaintTime.ToLongDateString() },
        { "time", e.ComplaintTime.ToLongTimeString() },
        { "complaint", e.ComplaintRaised }
    });

        Console.WriteLine(informationComplaint);
    }
}