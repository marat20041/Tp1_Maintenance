using System;
using System.Net.Sockets;

namespace SchoolManager
{
    public class Principal : SchoolMember, IPayroll
    {
     static public List<Principal> Principals = new List<Principal>();
        public int Income{ get; set; }
        private int _balance;

        // Modification du type de phone
        public Principal(string name, string address, string phone, int income)
        : base(name, address, phone)
        { 
            Income = income;
            _balance = 0;
          
        }

        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, Address: {Address}, Phone: {Phone}, Income: {Income}");
        }
        public void Pay()
        {
            Util.NetworkDelay.PayEntity("Principal", Name, ref _balance, Income);
        }
    }
}
