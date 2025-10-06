using System;
using System.Collections.Generic;

namespace SchoolManager
{
    public class Student : SchoolMember
    {
        private int _grade = 0;


        /*Modification du type de phone
        - Ajout de "base" qui refere aux variables du parents
        - Ajout des objets dans la liste à l'appel du constructeur 
        - Modification de 
        */
        static public List<Student> Students = new List<Student>();

        public  Student(string name, string address, string phone, int grade)
        : base(name, address, phone)

        {
            Grade = grade;

            Students.Add(this);
        }
        public int Grade
        {
            get { return _grade; }
            set { _grade = value; }
        }
        //Modification de l'affichage afin de respecter les conventions en C#
        public void display()
        {
            
            Console.WriteLine($"Name: {Name}, Address: {Address}, Phone: {Phone}, Grade: {Grade}");
        }

        public static double averageGrade(List<Student> students)
        {
            double avg = 0;
            foreach (Student student in students)
            {
                avg += student.Grade;
            }

            return avg / students.Count;
        }
      
    }
   
}
