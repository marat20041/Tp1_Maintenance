using System;
using ComplaintEventArgsNamespace;

namespace SchoolManager
{

    public class Receptionist : SchoolMember, IPayroll
    {
        private int _income;
        private int _balance;

        public int Income => _income;
        public int Balance => _balance;

        public event EventHandler<ComplaintEventArgs>? ComplaintRaised;

        public Receptionist(string name, string address, string phoneNum, int income = 10000) : base(name, address, phoneNum)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));


            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address cannot be empty", nameof(address));

            if (string.IsNullOrWhiteSpace(phoneNum))
                throw new ArgumentException("Phone number cannot be empty", nameof(phoneNum));

            if (income < 0)
                throw new ArgumentOutOfRangeException(nameof(income), "Income must be non-negative.");

            _income = income;
            _balance = 0;
        }

        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, Address: {Address}, Phone: {Phone}, Income: {_income}");
        }

        public void Pay()
        {
            if (_income <= 0)
                throw new InvalidOperationException("Income must be greater than zero to process payment.");

            if (_balance < 0)
                throw new InvalidOperationException("Balance cannot be negative.");
            
            Util.NetworkDelay.PayEntity("Receptionist", Name, ref _balance, _income);
        }

        public void HandleComplaint(string complaintText)
        {
            if (string.IsNullOrWhiteSpace(complaintText))
                throw new ArgumentException("Complaint cannot be empty", nameof(complaintText));

            if (complaintText.Length > 1000)
                throw new ArgumentException("Complaint is too long", nameof(complaintText));

            var args = new ComplaintEventArgs(complaintText);
            OnComplaintRaised(args);
        }

        protected virtual void OnComplaintRaised(ComplaintEventArgs e)
        {
            try
            {
                 ComplaintRaised?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while raising complaint event: {ex.Message}");
            }
        }
    }
}
