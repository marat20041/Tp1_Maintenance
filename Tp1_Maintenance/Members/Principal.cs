using System;

namespace SchoolManager
{
    public class Principal : SchoolMember, IPayroll
    {
        private const int DefaultIncome = 50000;


        private int _income;
        private int _balance;

        public int Income => _income;
        public int Balance => _balance;

        public Principal(int income = DefaultIncome)
            : this("Inconnu", "Inconnu", "000-0000", income)
        {
        }

        public Principal(string name, string address, string phone, int income = DefaultIncome)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address cannot be empty.", nameof(address));
            if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Phone cannot be empty.", nameof(phone));
            if (income < 0) throw new ArgumentOutOfRangeException(nameof(income), "Income must be non-negative.");

            Name = name;
            Address = address;
            Phone = phone;
            _income = income;
            _balance = 0;
        }

        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, Address: {Address}, Phone: {Phone}, Income: {_income}");
        }

        public void Pay()
        {
            Util.NetworkDelay.PayEntity("Principal", Name, ref _balance, _income);
        }

        
    }
}
