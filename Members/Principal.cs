using System;

namespace SchoolManager
{
    public class Principal : SchoolMember, IPayroll
    {
        private int income;
        private int balance;

        public Principal(int income = 50000)
        {
            this.income = income;
            balance = 0;
        }
        // Modification du type de phone
        public Principal(string name, string address, string phone, int income = 50000)
        {
            Name = name;
            Address = address;
            Phone = phone;
            this.income = income;
            balance = 0;
        }

        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, Address: {Address}, Phone: {Phone}");
        }



        public void Pay()
        {
            Util.NetworkDelay.PayEntity("Principal", Name, ref balance, income);
        }
    }
}
