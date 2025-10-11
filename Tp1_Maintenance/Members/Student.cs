using System;
using System.Collections.Generic;

/// <summary>
/// Représente un étudiant de l’école.
/// Hérite de <see cref="SchoolMember"/> et gère la note (grade) de l’étudiant.
/// </summary>
namespace SchoolManager
{
    public class Student : SchoolMember
    {
        private int _grade;
        private static readonly List<Student> _students = new List<Student>();

        /// <summary>Liste en lecture seule de tous les étudiants.</summary>
        public static IReadOnlyList<Student> Students => _students.AsReadOnly();

        /// <summary>
        /// Initialise un nouvel étudiant avec nom, adresse, téléphone et note.
        /// </summary>
        public Student(string name, string address, string phone, int grade)
            : base(name, address, phone)

        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ReferenceText.Get("EmptyName"), nameof(name));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException(ReferenceText.Get("EmptyAddress"), nameof(address));

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException(ReferenceText.Get("EmptyPhone"), nameof(phone));

            if (grade < 0 || grade > 100)
                throw new ArgumentOutOfRangeException(nameof(grade), ReferenceText.Get("InvalidGrade"));

            _grade = grade;
            _students.Add(this);
        }
        
        /// <summary>Note (grade) de l’étudiant, entre 0 et 100.</summary>
        public int Grade
        {
            get => _grade;
            set => _grade = value;
        }


        ///  <summary>Supprime un étudiant de la liste globale.</summary>
        public static void RemoveStudent(Student student)
        {
            _students.Remove(student);
        }

        /// <summary>Retourne une chaîne décrivant l’étudiant.</summary>
        public override string Display()
        {
            string studentInfo = $"Name: {Name ?? ""}, Address: {Address ?? ""}, Phone: {Phone ?? ""}, Grade: {_grade}";

            return studentInfo;
        }

        /// <summary>Calcule la moyenne des notes de tous les étudiants.</summary>
        public static double AverageGrade() => AverageGrade(Students.ToList());

        /// <summary>Calcule la moyenne des notes pour une liste d’étudiants donnée.</summary>
        public static double AverageGrade(IEnumerable<Student> students)
        {
            if (students == null || !students.Any()) return 0;
            return Math.Round(students.Average(s => s.Grade), 2);
        }

    }

}
