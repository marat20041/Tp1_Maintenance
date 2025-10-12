using Xunit;
using SchoolManager;
using System.Text.Json;
using System;
using System.Collections.Generic;

/// <summary>
/// Tests unitaires pour la classe Teacher.
/// Vérifie la construction, la validation des entrées, les valeurs par défaut, la suppression et l'affichage.
/// </summary>
public class TeacherTests
{
    [Fact]
    public void Constructor_AddsTeacherToList()
    {
        var countBefore = Teacher.Teachers.Count;
        var t = new Teacher("Alice", "Main St", "123-456-7890", "Math", 40000);
        Assert.Equal(countBefore + 1, Teacher.Teachers.Count);
        Teacher.RemoveTeacher(t);
    }

    [Theory]
    [InlineData("", "Addr", "123-456-7890", "Math", "EmptyName")]
    [InlineData("Bob", "", "123-456-7890", "Math", "EmptyAddress")]
    [InlineData("Bob", "Addr", "", "Math", "EmptyPhone")]
    public void Constructor_ThrowsOnInvalidInputs(string name, string address, string phone, string subject, string expectedSubstring)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Teacher(name, address, phone, subject, 10000));
        Assert.Contains(ReferenceText.Get(expectedSubstring), ex.Message);
    }

    [Fact]
    public void Constructor_ThrowsIfIncomeNegative()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Teacher("John", "Addr", "123-456-7890", "Math", -1));
        Assert.Contains(ReferenceText.Get("NegativeIncome"), ex.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Subject_SetInvalid_Throws(string? subject)

    {
        var t = new Teacher("Valid Name", "Valid Addr", "123-456-7890", "Valid Subject", 20000);
        var ex = Assert.Throws<ArgumentException>(() => t.Subject = subject);
        Assert.Contains(ReferenceText.Get("InvalidSubject"), ex.Message);
    }

    [Fact]
    public void Constructor_UsesDefaultIncomeIfNull_FromJsonConfig()
    {
        var json = File.ReadAllText("Resource/HelperConfig.json");
        var config = JsonSerializer.Deserialize<HelperConfig>(json)
                     ?? throw new InvalidOperationException("Config deserialization failed");

        Teacher.LoadConfig(config);

        var t = new Teacher("Jane", "Addr", "123-456-7890", "Science", null);
        Assert.Equal(config.DefaultIncomeTeacher, t.Income);
        Teacher.RemoveTeacher(t);
    }

    [Fact]
    public void Display_ReturnsExpectedFormat()
    {
        var t = new Teacher("Jane", "Addr", "123-456-7890", "Science", 30000);
        var display = t.Display();
        Assert.Contains("Name: Jane", display);
        Assert.Contains("Address: Addr", display);
        Assert.Contains("Phone: 123-456-7890", display);
        Assert.Contains("Subject: Science", display);
        Assert.Contains("Income: 30000", display);
        Teacher.RemoveTeacher(t);
    }

    [Fact]
    public void RemoveTeacher_RemovesFromList()
    {
        var t = new Teacher("Rick", "Addr", "123-456-7890", "History", 20000);
        Teacher.RemoveTeacher(t);
        Assert.DoesNotContain(t, Teacher.Teachers);
    }
}