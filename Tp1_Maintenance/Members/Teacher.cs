using System;
using System.Runtime.InteropServices.Marshalling;

/// <summary>
/// Représente un enseignant de l’école.
/// Hérite de <see cref="SchoolMember"/> et implémente <see cref="IPayroll"/>.
/// Gère le revenu, le solde et la matière enseignée.
/// </summary>
namespace SchoolManager
{
    public class Teacher : SchoolMember, IPayroll
    {
        private static HelperConfig? _config;

        
        /// <summary>
        /// Charge la configuration pour déterminer les revenus par défaut.
        /// </summary>
        public static void LoadConfig(HelperConfig config)
        {
            _config = config;
        }

        private int _balance;
        private int _income;
        private string _subject;

        /// <summary>
        /// Revenu du professeur.
        /// </summary>
        public int Income => _income;

        /// <summary>
        /// Solde actuel du professeur.
        /// </summary>
        public int Balance => _balance;

        /// <summary>
        /// Matière enseignée par le professeur.
        /// </summary>
        public string Subject
        {
            get => _subject;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ReferenceText.Get("InvalidSubject"), nameof(value));
                _subject = value;
            }
        }

        private static readonly List<Teacher> _teachers = new List<Teacher>();

        
        /// <summary>Liste en lecture seule de tous les enseignants.</summary>
        public static IReadOnlyList<Teacher> Teachers => _teachers.AsReadOnly();

 /// <summary>
        /// Initialise un nouvel enseignant avec nom, adresse, téléphone, matière et revenu optionnel.
        /// </summary>
        public Teacher(string name, string address, string phone, string subject, int? income)
    : base(ValidateName(name), ValidateAddress(address), ValidatePhone(phone))
        {
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException(ReferenceText.Get("EmptySubject"), nameof(subject));

            if (income < 0)
                throw new ArgumentOutOfRangeException(nameof(income), ReferenceText.Get("NegativeIncome"));

            _income = income ?? _config?.DefaultIncomeTeacher ?? 30000;
            _subject = subject;
            _balance = 0;

            _teachers.Add(this);
        }

        private static string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ReferenceText.Get("EmptyName"), nameof(name));
            return name;
        }

        private static string ValidateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException(ReferenceText.Get("EmptyAddress"), nameof(address));
            return address;
        }

        private static string ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException(ReferenceText.Get("EmptyPhone"), nameof(phone));
            return phone;
        }


        /// <summary>Supprime un enseignant de la liste globale.</summary>
        public static void RemoveTeacher(Teacher teacher)
        {
            _teachers.Remove(teacher);
        }

        /// <summary>Retourne une chaîne décrivant l’enseignant.</summary>
        public override string Display()
        {
            return $"Name: {Name ?? ""}, Address: {Address ?? ""}, Phone: {Phone ?? ""}, Subject: {Subject ?? ""} , Income: {Income}";

        }

        /// <summary>Paye le professeur en ajoutant son revenu à son solde.</summary>
        public void Pay()
        {
            try
            {
                Util.NetworkDelay.PayEntity("Teacher", Name, ref _balance, _income);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ReferenceText.Format("PaymentFailed", new Dictionary<string, string>
                    {
                        { "Name", Name },
                        { "Error", ex.Message }
                    }));
            }

        }
    }
}
