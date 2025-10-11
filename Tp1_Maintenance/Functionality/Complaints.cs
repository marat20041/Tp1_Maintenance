using SchoolManager;
public class Complaints

    /// <summary>
    /// Gère la création et le traitement des plaintes dans le système scolaire.
    /// Permet à un réceptionniste de recevoir une plainte et d’afficher ses détails.
    /// </summary>
{

    /// <summary>
    /// Demande à l’utilisateur d’entrer une plainte et la transmet au réceptionniste.
    /// </summary>
    public static void RaiseComplaint(Receptionist Receptionist)
    {
        string complaintText = Util.ConsoleHelper.AskQuestion("Please enter your complaint: ");
        Receptionist?.HandleComplaint(complaintText);
    }


    /// <summary>
    /// Gère l’événement déclenché lorsqu’une plainte est déposée
    /// et affiche ses informations formatées.
    /// </summary>
    private static void handleComplaintRaised(object? sender, Complaint e)
    {
        Console.WriteLine(ReferenceText.Get("ConfirmComplaint"));

        string complaintValue = e.ComplaintRaised ?? "No complaint text provided";

        string informationComplaint = ReferenceText.Format("ComplaintDetails", new Dictionary<string, string>
        {
        { "date", e.ComplaintTime.ToLongDateString() },
        { "time", e.ComplaintTime.ToLongTimeString() },
        { "complaint", complaintValue }
    });

        Console.WriteLine(informationComplaint);
    }
}