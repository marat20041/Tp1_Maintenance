using Xunit;
using SchoolManager;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Tests unitaires pour la classe Student.
/// Vérifie la construction, la validation des entrées, les valeurs par défaut, la suppression et l'affichage.
/// </summary>
public class StudentTests
{
    [Fact]
    public void Constructor_AddsStudentToList()
    {
        var countBefore = Student.Students.Count;
        var s = new Student("Alice", "Main St", "123-456-7890", 85);
        Assert.Equal(countBefore + 1, Student.Students.Count);
        Student.RemoveStudent(s);
    }

    [Theory]
    [InlineData("", "Addr", "123-456-7890", 50)]
    [InlineData("Bob", "", "123-456-7890", 50)]
    [InlineData("Bob", "Addr", "", 50)]
    public void Constructor_ThrowsIfNameAddressOrPhoneEmpty(string name, string address, string phone, int grade)
    {
        Assert.Throws<ArgumentException>(() => new Student(name, address, phone, grade));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Constructor_ThrowsIfGradeOutOfRange(int grade)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Student("John", "Addr", "123-456-7890", grade));
    }

    [Fact]
    public void Display_ReturnsCorrectFormat()
    {
        var s = new Student("Jane", "Addr", "123-456-7890", 90);
        var display = s.Display();
        Assert.Contains("Name: Jane", display);
        Assert.Contains("Address: Addr", display);
        Assert.Contains("Phone: 123-456-7890", display);
        Assert.Contains("Grade: 90", display);
        Student.RemoveStudent(s);
    }

    [Fact]
    public void AverageGrade_CalculatesCorrectly()
    {
        var s1 = new Student("Student1", "Addr1", "111-111-1111", 80);
        var s2 = new Student("Student2", "Addr2", "222-222-2222", 90);
        var s3 = new Student("Student3", "Addr3", "333-333-3333", 70);

        var avg = Student.AverageGrade(new List<Student> { s1, s2, s3 });
        Assert.Equal(80.00, avg);

        Student.RemoveStudent(s1);
        Student.RemoveStudent(s2);
        Student.RemoveStudent(s3);
    }

    [Fact]
    public void AverageGrade_ReturnsZeroForEmptyList()
    {
        var avg = Student.AverageGrade(new List<Student>());
        Assert.Equal(0, avg);
    }

    [Fact]
    public void GradeProperty_CanGetAndSet()
    {
        var s = new Student("Test", "Addr", "123-456-7890", 75);
        Assert.Equal(75, s.Grade);
        s.Grade = 85;
        Assert.Equal(85, s.Grade);
        Student.RemoveStudent(s);
    }
}