using System;
using System.Linq;


/// <summary>
/// Représente un principal de l’école. 
/// Hérite de <see cref="SchoolMember"/> et implémente <see cref="IPayroll"/>.
/// Gère le revenu, le solde et fournit des méthodes de paiement.
/// </summary>
namespace SchoolManager
{
    public class Principal : SchoolMember, IPayroll
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

        public int Income => _income;
        public int Balance => _balance;

        private static readonly List<Principal> _principals = new List<Principal>();

        /// <summary>Liste en lecture seule de tous les principals.</summary>
        public static IReadOnlyList<Principal> Principals => _principals.AsReadOnly();


        /// <summary>
        /// Initialise un nouveau principal avec nom, adresse, téléphone et revenu optionnel.
        /// </summary>
        public Principal(string name, string address, string phone, int? income)
            : base(name, address, phone)
        {
            if (string.IsNullOrWhiteSpace(name) || name.All(char.IsDigit))
                throw new ArgumentException(ReferenceText.Get("EmptyName"), nameof(name));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException(ReferenceText.Get("EmptyAddress"), nameof(address));

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException(ReferenceText.Get("EmptyPhone"), nameof(phone));

            _income = income ?? _config?.DefaultIncomePrincipal ?? 50000;
            if (_income < 0)
                throw new ArgumentOutOfRangeException(nameof(income), ReferenceText.Get("NegativeIncome"));

            _balance = 0;
            _principals.Add(this);
        }

        /// <summary>Supprime un principal de la liste globale.</summary>
        public static void RemovePrincipal(Principal principal)
        {
            _principals.Remove(principal);
        }

        /// <summary>Retourne une chaîne décrivant le principal.</summary>
        public override string Display()
        {
            return $"Name: {Name ?? ""}, Address: {Address ?? ""}, Phone: {Phone ?? ""}, Income: {_income}";
        }

        /// <summary>Effectue le paiement du principal et met à jour le solde.</summary>
        public void Pay()
        {
            try
            {
                Util.NetworkDelay.PayEntity("Principal", Name, ref _balance, _income);
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
    }
}
