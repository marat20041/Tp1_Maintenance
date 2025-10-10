using System;
using System.Collections.Generic;

namespace SchoolManager
{
    public class Student : SchoolMember
    {
        private int _grade = 0;
        private static readonly List<Student> _students = new List<Student>();
        public static IReadOnlyList<Student> Students => _students.AsReadOnly();

        public Student(string name, string address, string phone, int grade)
            : base(name, address, phone)

        {
            if (grade < 0 || grade > 100)
                throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 0 and 100");

            Grade = grade;
            _students.Add(this);
        }
        public int Grade
        {
            get => _grade;
            set => _grade = value;
        }

        public static void RemoveStudent(Student student)
        {
            _students.Remove(student);
        }

        public override string Display()
        {
            string studentInfo = $"Name: {Name ?? ""}, Address: {Address ?? ""}, Phone: {Phone ?? ""}, Grade: {_grade}";

            return studentInfo;
        }

        public static double AverageGrade() => AverageGrade(Students.ToList());

        public static double AverageGrade(IEnumerable<Student> students)
        {
            if (students == null || !students.Any()) return 0;
            return students.Average(s => s.Grade);
        }

    }

}
