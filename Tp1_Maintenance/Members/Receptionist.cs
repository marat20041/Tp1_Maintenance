using System;
using ComplaintEventArgsNamespace;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


/// <summary>
/// Représente un réceptionniste de l’école. 
/// Hérite de <see cref="SchoolMember"/> et implémente <see cref="IPayroll"/>.
/// Gère le revenu, le solde et les plaintes des utilisateurs.
/// </summary>
namespace SchoolManager
{

    public class Receptionist : SchoolMember, IPayroll
    {

        private static HelperConfig? _config;

        /// <summary>
        /// Charge la configuration pour déterminer les revenus par défaut.
        /// </summary>
        public static void LoadConfig(HelperConfig config)
        {
            _config = config;
        }

        private int _income;
        private int _balance;

        /// <summary>Revenu du réceptionniste.</summary>
        public int Income => _income;

        /// <summary>Solde actuel du réceptionniste.</summary>
        public int Balance => _balance;

        /// <summary>Événement déclenché lorsqu'une plainte est déposée.</summary>
        public event EventHandler<ComplaintEventArgs>? ComplaintRaised;

        private static readonly List<Receptionist> _receptionists = new List<Receptionist>();

        /// <summary>Liste en lecture seule de tous les réceptionnistes.</summary>
        public static IReadOnlyList<Receptionist> Receptionists => _receptionists.AsReadOnly();


        /// <summary>
        /// Initialise un nouveau réceptionniste avec nom, adresse, téléphone et revenu optionnel.
        /// </summary>
        public Receptionist(string name, string address, string phoneNum, int? income) : base(name, address, phoneNum)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ReferenceText.Get("EmptyName"), nameof(name));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException(ReferenceText.Get("EmptyAddress"), nameof(address));

            if (string.IsNullOrWhiteSpace(phoneNum))
                throw new ArgumentException(ReferenceText.Get("EmptyPhone"), nameof(phoneNum));

            if (income < 0)
                throw new ArgumentOutOfRangeException(nameof(income), ReferenceText.Get("NegativeIncome"));

            _income = income ?? _config?.DefaultIncomeReceptionist ?? 10000;
            _balance = 0;

            _receptionists.Add(this);
        }


        /// <summary>Supprime un réceptionniste de la liste globale.</summary>
        public static void RemoveReceptionist(Receptionist receptionist)
        {
            _receptionists.Remove(receptionist);
        }

        /// <summary>Retourne une chaîne décrivant le réceptionniste.</summary>
        public override string Display()
        {
            return $"Name: {Name}, Address: {Address}, Phone: {Phone}, Income: {_income}";
        }


        /// <summary>Effectue le paiement du réceptionniste et met à jour le solde.</summary>
        public void Pay()
        {
            try
            {
                Util.NetworkDelay.PayEntity("Receptionist", Name, ref _balance, _income);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ReferenceText.Format("PaymentFailed", new Dictionary<string, string>
                {
                    { "name", Name },
                    { "error", ex.Message }
                }));
            }
        }


        /// <summary>Gère le dépôt d'une plainte et déclenche l'événement associé.</summary>
        /// <param name="complaintText">Texte de la plainte.</param>
        /// <exception cref="ArgumentException">Si le texte est vide ou trop long.</exception>

        public void HandleComplaint(string complaintText)
        {
            if (string.IsNullOrWhiteSpace(complaintText))
                throw new ArgumentException(ReferenceText.Get("EmptyComplaint"), nameof(complaintText));

            if (complaintText.Length > 1000)
                throw new ArgumentException(ReferenceText.Get("LongComplaint"), nameof(complaintText));

            var args = new ComplaintEventArgs(complaintText);
            OnComplaintRaised(args);
        }

        /// <summary>Déclenche l'événement <see cref="ComplaintRaised"/> en toute sécurité.</summary>

        protected virtual void OnComplaintRaised(ComplaintEventArgs e)
        {
            try
            {
                ComplaintRaised?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ReferenceText.Format("ComplaintEventError", new Dictionary<string, string>
                {
                    { "error", ex.Message }
                }));
            }
        }
    }
}
