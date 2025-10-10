using System;
using ComplaintEventArgsNamespace;

namespace SchoolManager
{

    public class Receptionist : SchoolMember, IPayroll
    {
        private int _income;
        private int _balance;

        public event EventHandler<ComplaintEventArgs>? ComplaintRaised;

        public Receptionist(string name, string address, string phoneNum, int income = 10000)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            Name = name;
            Address = address;
            Phone = phoneNum;
            _income = income;
            _balance = 0;
        }

        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, Address: {Address}, Phone: {Phone}, Income: {_income}");
        }

        public void Pay()
        {
            Util.NetworkDelay.PayEntity("Receptionist", Name, ref _balance, _income);
        }

        public void HandleComplaint(string complaintText)
        {
            if (string.IsNullOrWhiteSpace(complaintText))
                throw new ArgumentException("Complaint cannot be empty", nameof(complaintText));

            var args = new ComplaintEventArgs(complaintText);
            OnComplaintRaised(args);
        }

        protected virtual void OnComplaintRaised(ComplaintEventArgs e)
        {
            ComplaintRaised?.Invoke(this, e);
        }
    }
}
