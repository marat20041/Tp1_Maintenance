
namespace ComplaintEventArgsNamespace
{
    public class ComplaintEventArgs : EventArgs
    {
        public DateTime ComplaintTime { get; }
        public string ComplaintRaised { get; }

        public ComplaintEventArgs(string complaintRaised)
        {
            if (string.IsNullOrWhiteSpace(complaintRaised))
                throw new ArgumentException("Complaint cannot be empty", nameof(complaintRaised));

            ComplaintRaised = complaintRaised;
            ComplaintTime = DateTime.Now;
        }
    }
}