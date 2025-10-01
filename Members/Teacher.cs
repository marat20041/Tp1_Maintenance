using System;

namespace SchoolManager
{
    public class Teacher : SchoolMember, IPayroll
    {
        public string Subject;
        private int income;
        private int balance;
        // Modification du type de phone
        public Teacher(string name, string address, string phoneNum, string subject = "", int income = 25000)
        {
            Name = name;
            Address = address;
            Phone = phoneNum;
            Subject = subject;
            this.income = income;
            balance = 0;
        }
        //Modification de l'affichage afin de respecter les conventions en C#
        public void display()
        {
            Console.WriteLine($"Name: {Name}, Address: {Address}, Phone: {Phone}, Subject: {Subject}");
        }

        public void Pay()
        {
            Util.NetworkDelay.PayEntity("Teacher", Name, ref balance, income);
        }
    }
}
