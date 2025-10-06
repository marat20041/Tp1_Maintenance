using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager
{
    public class Complaint : EventArgs
    {
        public DateTime ComplaintTime { get; set; }
        public string ComplaintRaised { get; set; }
    }

    public class Receptionist : SchoolMember, IPayroll
    {
        private int income;
        private int balance;
        public event EventHandler<Complaint> ComplaintRaised;

        public Receptionist(int income = 10000)
        {
            this.income = income;
            balance = 0;
        }
        // Modification du type de phone
        public Receptionist(string name, string address, string phoneNum, int income = 10000)
        {
            Name = name;
            Address = address;
            Phone = phoneNum;
            this.income = income;
            balance = 0;
        }

        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, Address: {Address}, Phone: {Phone}");
        }
        public void Pay()
        {
            Util.NetworkDelay.PayEntity("Receptionist", Name, ref balance, income);
        }

        public void HandleComplaint()
        {
            Complaint complaint = new Complaint();
            complaint.ComplaintTime = DateTime.Now;
            complaint.ComplaintRaised = Util.Console.AskQuestion("Please enter your Complaint: ");

            ComplaintRaised?.Invoke(this, complaint);
        }
    }
}
