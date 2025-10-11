/// <summary>
/// Fournit des données pour un événement de plainte.
/// Contient le texte de la plainte et la date/heure de sa création.
/// </summary>

namespace ComplaintEventArgsNamespace
{
    public class ComplaintEventArgs : EventArgs
    {

        /// <summary>Moment où la plainte a été déposée.</summary>
        public DateTime ComplaintTime { get; }

        /// <summary>Texte de la plainte déposée.</summary>
        public string ComplaintRaised { get; }


        /// <summary>
        /// Initialise une nouvelle instance de ComplaintEventArgs avec le texte de la plainte.
        /// </summary>
        /// <param name="complaintRaised">Texte de la plainte (ne peut pas être vide).</param>
        /// <exception cref="ArgumentException">Si la plainte est vide ou null.</exception>
        public ComplaintEventArgs(string complaintRaised)
        {
            if (string.IsNullOrWhiteSpace(complaintRaised))
                throw new ArgumentException(ReferenceText.Get("EmptyComplaint"), nameof(complaintRaised));

            ComplaintRaised = complaintRaised;
            ComplaintTime = DateTime.Now;
        }
    }
}