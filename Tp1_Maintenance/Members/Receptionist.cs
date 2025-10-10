using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace SchoolManager
{

    public class Receptionist : SchoolMember, IPayroll
    {
        static public List<Receptionist> Receptionists = new List<Receptionist>();

        private int _balance;
        public event EventHandler<Complaint>? ComplaintRaised;
        //ComplaintRaised;
        public int Income { get; set; }
        // Modification du type de phone
        public Receptionist(string name, string address, string phone, int income)
         : base(name, address, phone)
        {

            Income = income;
            _balance = 0;

        }

        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, Address: {Address}, Phone: {Phone}");
        }
        public void Pay()
        {
            Util.NetworkDelay.PayEntity("Receptionist", Name, ref _balance, Income);
        }

        public void HandleComplaint()
        {
            Complaint complaint = new Complaint();
            complaint.ComplaintTime = DateTime.Now;
            complaint.ComplaintRaised = Util.ConsoleHelper.AskQuestion(ReferenceText.Get("AskComplaint"));

            ComplaintRaised?.Invoke(this, complaint);
        }
    }
}
