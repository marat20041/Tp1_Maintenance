using SchoolManager;
public class Complaints
{
    public static void RaiseComplaint()
    {
        // Receptionists.HandleComplaint();
        if (Receptionist.Receptionists.Count == 0)
        {

            Receptionist receptionistExple = new Receptionist("Alice", "123 Rue", "0123456789", 12000);
            Receptionist.Receptionists.Add(receptionistExple);
        }

        Receptionist receptionist = Receptionist.Receptionists[0];
        receptionist.ComplaintRaised += handleComplaintRaised;
        receptionist.HandleComplaint();

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