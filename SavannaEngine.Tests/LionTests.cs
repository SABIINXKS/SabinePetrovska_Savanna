using Xunit;
using SavannaEngine;
using System.Collections.Generic;

public class LionTests
{
    [Fact]
    public void Lion_Initializes_With_Default_Health()
    {
        var lion = new Lion();
        Assert.Equal(SavannaConstants.LionDefaultHealth, lion.Health);
    }

    [Fact]
    public void Lion_Health_Decreases_On_Move()
    {
        var lion = new Lion();
        var animals = new List<Animal> { lion };
        double initialHealth = lion.Health;

        lion.Move(animals, SavannaConstants.DefaultFieldWidth, SavannaConstants.DefaultFieldHeight);

        Assert.Equal(initialHealth - 0.5, lion.Health);
    }

    [Fact]
    public void Lion_Dies_When_Health_Reaches_Zero()
    {
        var lion = new Lion();
        lion.Health = 0.5;
        var animals = new List<Animal> { lion };

        lion.Move(animals, SavannaConstants.DefaultFieldWidth, SavannaConstants.DefaultFieldHeight);

        Assert.DoesNotContain(lion, animals);
    }

    [Fact]
    public void Lion_Health_Increases_When_Eating_Antelope()
    {
        var lion = new Lion { X = 0, Y = 0 };
        var antelope = new Antelope { X = 0, Y = 0 };
        var animals = new List<Animal> { lion, antelope };

        double initialHealth = lion.Health;
        lion.Move(animals, SavannaConstants.DefaultFieldWidth, SavannaConstants.DefaultFieldHeight);

        Assert.Equal(initialHealth - 0.5 + 5.0, lion.Health);
        Assert.DoesNotContain(antelope, animals);
    }

    [Fact]
    public void Lion_Birth_Occurs_After_Three_Consecutive_Proximity_Rounds()
    {
        var lion1 = new Lion { X = 0, Y = 0 };
        var lion2 = new Lion { X = 0, Y = 1 };
        var animals = new List<Animal> { lion1, lion2 };

        // Simulate 3 consecutive rounds of proximity
        for (int i = 0; i < SavannaConstants.BirthRoundsRequired; i++)
        {
            lion1.Move(animals, SavannaConstants.DefaultFieldWidth, SavannaConstants.DefaultFieldHeight);
            lion2.Move(animals, SavannaConstants.DefaultFieldWidth, SavannaConstants.DefaultFieldHeight);
        }

        // There should be a new lion added
        Assert.True(animals.Count > 2);
        Assert.Contains(animals, a => a != lion1 && a != lion2 && a is Lion);
    }
}