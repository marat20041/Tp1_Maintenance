using System;
using ComplaintEventArgsNamespace;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace SchoolManager
{

    public class Receptionist : SchoolMember, IPayroll
    {
        private int _income;
        private int _balance;

        public int Income => _income;
        public int Balance => _balance;

        public event EventHandler<ComplaintEventArgs>? ComplaintRaised;

        public Receptionist(string name, string address, string phoneNum, int income) : base(name, address, phoneNum)
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

        public override string Display()
        {
            return $"Name: {Name}, Address: {Address}, Phone: {Phone}, Income: {_income}";
        }

        public void Pay()
        {   
            try
            {
                Util.NetworkDelay.PayEntity("Receptionist", Name, ref _balance, _income);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Payment failed for {Name}: {ex.Message}");
            }
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
