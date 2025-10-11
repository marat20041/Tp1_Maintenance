using System;
using System.Runtime.InteropServices.Marshalling;

namespace SchoolManager
{
    public class Teacher : SchoolMember, IPayroll
    {
        private static HelperConfig? _config;
        public static void LoadConfig(HelperConfig config)
        {
            _config = config;
        }


        public int Income => _income;
        public int Balance => _balance;
        private int _balance;
        private int _income;
        private string _subject;

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



        /*Modification du type de phone
        - Ajout de "base" qui refere aux variables du parents
        - Ajout des objets dans la liste à l'appel du constructeur 
        */
        private static readonly List<Teacher> _teachers = new List<Teacher>();
        public static IReadOnlyList<Teacher> Teachers => _teachers.AsReadOnly();

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



        public static void RemoveTeacher(Teacher teacher)
        {
            _teachers.Remove(teacher);
        }

        public override string Display()
        {
            return $"Name: {Name ?? ""}, Address: {Address ?? ""}, Phone: {Phone ?? ""}, Subject: {Subject ?? ""} , Income: {Income}";

        }

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
