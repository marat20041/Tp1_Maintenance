using System;

namespace SchoolManager
{
    public class Principal : SchoolMember, IPayroll
    {
        static public List<Principal> Principals = new List<Principal>();
        private int income;
        private int balance;

        // Modification du type de phone
        public Principal(string name, string address, string phone, int income)
        {
            Name = name;
            Address = address;
            Phone = phone;
            this.income = income;
            balance = 0;
            Principals.Add(this);
        }

        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, Address: {Address}, Phone: {Phone}, Income: {income}");
        }



        public void Pay()
        {
            Util.NetworkDelay.PayEntity("Principal", Name, ref balance, income);
        }
    }
}
