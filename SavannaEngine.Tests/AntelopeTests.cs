using Xunit;
using SavannaEngine;
using System.Collections.Generic;

public class AntelopeTests
{
    [Fact]
    public void Antelope_Initializes_With_Default_Health()
    {
        var antelope = new Antelope();
        Assert.Equal(SavannaConstants.AntelopeDefaultHealth, antelope.Health);
    }

    [Fact]
    public void Antelope_Health_Decreases_On_Move()
    {
        var antelope = new Antelope();
        var animals = new List<Animal> { antelope };
        double initialHealth = antelope.Health;

        antelope.Move(animals, SavannaConstants.DefaultFieldWidth, SavannaConstants.DefaultFieldHeight);

        Assert.Equal(initialHealth - 0.5, antelope.Health);
    }

    [Fact]
    public void Antelope_Dies_When_Health_Reaches_Zero()
    {
        var antelope = new Antelope();
        antelope.Health = 0.5;
        var animals = new List<Animal> { antelope };

        antelope.Move(animals, SavannaConstants.DefaultFieldWidth, SavannaConstants.DefaultFieldHeight);

        Assert.DoesNotContain(antelope, animals);
    }

    [Fact]
    public void Antelope_Birth_Occurs_After_Three_Consecutive_Proximity_Rounds()
    {
        var antelope1 = new Antelope { X = 0, Y = 0 };
        var antelope2 = new Antelope { X = 0, Y = 1 };
        var animals = new List<Animal> { antelope1, antelope2 };

        // Simulate 3 consecutive rounds of proximity
        for (int i = 0; i < SavannaConstants.BirthRoundsRequired; i++)
        {
            antelope1.Move(animals, SavannaConstants.DefaultFieldWidth, SavannaConstants.DefaultFieldHeight);
            antelope2.Move(animals, SavannaConstants.DefaultFieldWidth, SavannaConstants.DefaultFieldHeight);
        }

        // There should be a new antelope added
        Assert.True(animals.Count > 2);
        Assert.Contains(animals, a => a != antelope1 && a != antelope2 && a is Antelope);
    }
}