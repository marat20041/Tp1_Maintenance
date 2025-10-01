using System;

namespace SchoolManager
{
    public class Teacher : SchoolMember, IPayroll
    {
        public string Subject{ get; set; }
        private int income;
        private int balance;

        /*Modification du type de phone
        - Ajout de "base" qui refere aux variables du parents
        - Ajout des objets dans la liste à l'appel du constructeur 
        */
        static public List<Teacher> Teachers = new List<Teacher>();
        
        public Teacher(string name, string address, string phone, string subject = "", int income = 25000)
         : base(name, address, phone)
        {

            Subject = subject;
            this.income = income;
            balance = 0;

            Teachers.Add(this);
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
