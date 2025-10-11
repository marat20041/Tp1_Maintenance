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

        private static readonly List<Principal> _principals = new List<Principal>();
        public static IReadOnlyList<Principal> Principals => _principals.AsReadOnly();

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

          public static void RemovePrincipal(Principal principal)
        {
            _principals.Remove(principal);
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
                Console.WriteLine(ReferenceText.Format("PaymentFailed", new Dictionary<string, string>
                {
                    { "name", Name },
                    { "error", ex.Message }
                }));
            }
        }
    }
}
