using System;
using System.Collections.Generic;

namespace SchoolManager
{
    public class Student : SchoolMember
    {
        private int grade;
        

        // Modification du type de phone
        public Student(string name = "", string address = "", string phoneNum = "", int grade = 0)
        {
            Name = name;
            Address = address;
            Phone = phoneNum;
            this.grade = grade;
        }
        public int Grade
        {
            get { return grade; }
            set { grade = value; }
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
