using System;
using System.Linq;

namespace SchoolManager
{
    public class Principal : SchoolMember, IPayroll
    {
        private static HelperConfig? _config;
        public static void LoadConfig(HelperConfig config)
        {
            _config = config;
        }

        private int _income;
        private int _balance;

        public int Income => _income;
        public int Balance => _balance;

        public Principal()
            : this("Inconnu", "Inconnu", "000-0000", null)
        {
        }

        public Principal(int? income)
            : this("Inconnu", "Inconnu", "000-0000", income)
        {
        }

        public Principal(string name, string address, string phone, int? income)
            : base(name, address, phone)
        {
            if (string.IsNullOrWhiteSpace(name) || name.All(char.IsDigit))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address cannot be empty.", nameof(address));

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone cannot be empty.", nameof(phone));

            _income = income ?? _config?.DefaultIncome ?? 50000;
            if (_income < 0)
                throw new ArgumentOutOfRangeException(nameof(income), "Income must be non-negative.");

            _balance = 0;
        }

        public override string Display()
        {
            return $"Name: {Name ?? ""}, Address: {Address ?? ""}, Phone: {Phone ?? ""}, Income: {_income}";
        }

        public void Pay()
        {
            try
            {
                Util.NetworkDelay.PayEntity("Principal", Name, ref _balance, _income);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Payment failed for {Name}: {ex.Message}");
            }
        }
    }
}
