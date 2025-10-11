using Xunit;
using SchoolManager;
using System.Text.Json;
using System;

public class PrincipalTests
{
    [Fact]
    public void Constructor_AddsPrincipalToList()
    {
        var countBefore = Principal.Principals.Count;
        var p = new Principal("Alice", "Main St", "123", 60000);
        Assert.Equal(countBefore + 1, Principal.Principals.Count);
        Principal.RemovePrincipal(p); 
    }

    [Fact]
    public void Constructor_ThrowsIfNameIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Principal("", "Addr", "123", 50000));
    }

    [Fact]
    public void Constructor_ThrowsIfIncomeNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Principal("John", "Addr", "123", -10));
    }

    [Fact]
    public void Constructor_UsesDefaultIncomeIfNull_FromJsonConfig()
    {
        var json = File.ReadAllText("helperconfig.json");
        var config = JsonSerializer.Deserialize<HelperConfig>(json)
                     ?? throw new InvalidOperationException("Config deserialization failed");

        Principal.LoadConfig(config);

        var p = new Principal("Jane", "Addr", "123-456-7890", null);
        Assert.Equal(config.DefaultIncomePrincipal, p.Income);
        Principal.RemovePrincipal(p);
    }

    [Fact]
    public void Constructor_UsesDefaultIncomeIfNull()
    {
        var config = new HelperConfig
        {
            DefaultIncomePrincipal = 70000,
            PhonePattern = @"^\d{3}-\d{3}-\d{4}$",
            DefaultIncomeTeacher = 40000,
            DefaultIncomeReceptionist = 30000
        };

        Principal.LoadConfig(config);

        var p = new Principal("Jane", "Addr", "123-456-7890", null);
        Assert.Equal(70000, p.Income);
        Principal.RemovePrincipal(p);
    }


    [Fact]
    public void RemovePrincipal_RemovesFromList()
    {
        var p = new Principal("Rick", "Addr", "123", 50000);
        Principal.RemovePrincipal(p);
        Assert.DoesNotContain(p, Principal.Principals);
    }

    [Fact]
    public void Display_ReturnsExpectedFormat()
    {
        var p = new Principal("Anna", "Road", "555", 50000);
        var result = p.Display();
        Assert.Contains("Anna", result);
        Assert.Contains("50000", result);
        Principal.RemovePrincipal(p);
    }
}
