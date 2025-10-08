using System;
using System.Runtime.InteropServices.Marshalling;

namespace SchoolManager
{
    public class Teacher : SchoolMember, IPayroll
    {

        private int _income;
        private int _balance;
        private string _subject;

        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _subject = value;
                }
                else
                {
                    _subject = "";
                }
            }
        }



        /*Modification du type de phone
        - Ajout de "base" qui refere aux variables du parents
        - Ajout des objets dans la liste à l'appel du constructeur 
        */
        static public List<Teacher> Teachers = new List<Teacher>();

        public Teacher(string name, string address, string phone, string subject, int income = 25000)
         : base(name, address, phone)
        {

            _subject = subject;
            _income = income;
            _balance = 0;

            Teachers.Add(this);
        }
        //Modification de l'affichage afin de respecter les conventions en C#

        public override void Display()
        {
            Console.WriteLine($"Name: {Name}, Address: {Address}, Phone: {Phone}, Subject: {Subject}");
        }

        public void Pay()
        {
            Util.NetworkDelay.PayEntity("Teacher", Name, ref _balance, _income);
        }
    }
}
