using System;
using System.Runtime.InteropServices.Marshalling;

namespace SchoolManager
{
    public class Teacher : SchoolMember, IPayroll
    {

        private int _income;
        private int _balance;
        private string _subject;



        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Subject cannot be empty.", nameof(value));

                _subject = value;
            }
        }



        /*Modification du type de phone
        - Ajout de "base" qui refere aux variables du parents
        - Ajout des objets dans la liste à l'appel du constructeur 
        */
        private static readonly List<Teacher> _teachers = new List<Teacher>();
        public static IReadOnlyList<Teacher> Teachers => _teachers.AsReadOnly();


        public Teacher(string name, string address, string phone, string subject, int income)
         : base(name, address, phone)
        {

            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Subject cannot be empty.", nameof(subject));

            if (income < 0)
                throw new ArgumentOutOfRangeException(nameof(income), "Income must be non-negative.");

            _income = income;
            _subject = subject;
            _balance = 0;

            _teachers.Add(this);
        }


        public static void RemoveTeacher(Teacher teacher)
        {
            _teachers.Remove(teacher);
        }

        public override string Display()
        {
            return $"Name: {Name ?? ""}, Address: {Address ?? ""}, Phone: {Phone ?? ""}, Subject: {Subject ?? ""}";

        }

        public void Pay()
        {
            try
            {
                Util.NetworkDelay.PayEntity("Teacher", Name, ref _balance, _income);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Payment failed for {Name}: {ex.Message}");
            }

        }
    }
}
