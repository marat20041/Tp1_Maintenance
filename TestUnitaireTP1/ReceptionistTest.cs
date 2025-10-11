using Xunit;
using SchoolManager;
using ComplaintEventArgsNamespace;
using System;
using System.Collections.Generic;
using System.Text.Json;

/// <summary>
/// Tests unitaires pour la classe Receptionist.
/// Vérifie la construction, la validation des entrées, les valeurs par défaut, la suppression et l'affichage.
/// </summary>
public class ReceptionistTests
{
    private HelperConfig GetTestConfig() => new HelperConfig
    {
        DefaultIncomeReceptionist = 30000,
        DefaultIncomePrincipal = 50000,
        DefaultIncomeTeacher = 40000,
        PhonePattern = @"^\d{3}-\d{3}-\d{4}$"
    };

    [Fact]
    public void Constructor_AddsReceptionistToList()
    {
        var countBefore = Receptionist.Receptionists.Count;
        var r = new Receptionist("Alice", "Main St", "123-456-7890", 40000);
        Assert.Equal(countBefore + 1, Receptionist.Receptionists.Count);
        Receptionist.RemoveReceptionist(r);
    }

    [Theory]
    [InlineData("", "Addr", "123-456-7890", "Name cannot be empty")]
    [InlineData("Bob", "", "123-456-7890", "Address cannot be empty")]
    [InlineData("Bob", "Addr", "", "The number is empty")]
    public void Constructor_ThrowsOnInvalidInputs(string name, string address, string phone, string expectedSubstring)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Receptionist(name, address, phone, 10000));
        Assert.Contains(expectedSubstring, ex.Message);
    }

    [Fact]
    public void Constructor_ThrowsIfIncomeNegative()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Receptionist("John", "Addr", "123-456-7890", -1));
        Assert.Contains("Income must be non-negative", ex.Message);
    }


    [Fact]
    public void Constructor_UsesDefaultIncomeIfNull()
    {
        var config = GetTestConfig();
        Receptionist.LoadConfig(config);

        var r = new Receptionist("Jane", "Addr", "123-456-7890", null);
        Assert.Equal(config.DefaultIncomeReceptionist, r.Income);
        Receptionist.RemoveReceptionist(r);
    }

    [Fact]
    public void RemoveReceptionist_RemovesFromList()
    {
        var r = new Receptionist("Rick", "Addr", "123-456-7890", 20000);
        Receptionist.RemoveReceptionist(r);
        Assert.DoesNotContain(r, Receptionist.Receptionists);
    }

    [Fact]
    public void Display_ReturnsExpectedFormat()
    {
        var r = new Receptionist("Anna", "Road", "555-555-5555", 15000);
        var result = r.Display();
        Assert.Contains("Anna", result);
        Assert.Contains("15000", result);
        Receptionist.RemoveReceptionist(r);
    }

    [Fact]
    public void HandleComplaint_RaisesEvent()
    {
        var r = new Receptionist("Kate", "Addr", "123-456-7890", 20000);
        bool eventRaised = false;
        r.ComplaintRaised += (sender, args) =>
        {
            eventRaised = true;
            Assert.Equal("Test complaint", args.ComplaintRaised);
        };
        r.HandleComplaint("Test complaint");
        Assert.True(eventRaised);
        Receptionist.RemoveReceptionist(r);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void HandleComplaint_ThrowsOnEmpty(string? complaint)
    {
        var r = new Receptionist("Tom", "Addr", "123-456-7890", 20000);
        Assert.Throws<ArgumentException>(() => r.HandleComplaint(complaint));
        Receptionist.RemoveReceptionist(r);
    }

    [Fact]
    public void HandleComplaint_ThrowsOnTooLong()
    {
        var r = new Receptionist("Tom", "Addr", "123-456-7890", 20000);
        string longComplaint = new string('a', 1001);
        Assert.Throws<ArgumentException>(() => r.HandleComplaint(longComplaint));
        Receptionist.RemoveReceptionist(r);
    }
}
