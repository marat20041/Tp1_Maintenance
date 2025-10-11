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
                throw new ArgumentException(ReferenceText.Get("EmptyName"), nameof(name));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException(ReferenceText.Get("EmptyAddress"), nameof(address));

            if (string.IsNullOrWhiteSpace(phoneNum))
                throw new ArgumentException(ReferenceText.Get("EmptyPhone"), nameof(phoneNum));

            if (income < 0)
                throw new ArgumentOutOfRangeException(nameof(income), ReferenceText.Get("NegativeIncome"));

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
                Console.WriteLine(ReferenceText.Format("PaymentFailed", new Dictionary<string, string>
                {
                    { "name", Name },
                    { "error", ex.Message }
                }));
            }
        }

        public void HandleComplaint(string complaintText)
        {
            if (string.IsNullOrWhiteSpace(complaintText))
                throw new ArgumentException(ReferenceText.Get("EmptyComplaint"), nameof(complaintText));

            if (complaintText.Length > 1000)
                throw new ArgumentException(ReferenceText.Get("LongComplaint"), nameof(complaintText));

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
                Console.WriteLine(ReferenceText.Format("ComplaintEventError", new Dictionary<string, string>
                {
                    { "error", ex.Message }
                }));
            }
        }
    }
}
