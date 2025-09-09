using SavannaEngine;
using Xunit;
using System.Collections.Generic;

public class AnimalTests
{
    [Fact]
    public void Antelope_Health_Decreases_On_Move()
    {
        var antelope = new Antelope { X = 0, Y = 0 };
        var animals = new List<Animal> { antelope };
        double initialHealth = antelope.Health;

        antelope.Move(animals, 20, 10);

        Assert.Equal(initialHealth - 0.5, antelope.Health);
    }

    [Fact]
    public void Animal_Dies_When_Health_Is_Zero()
    {
        var antelope = new Antelope { X = 0, Y = 0, Health = 0 };
        var animals = new List<Animal> { antelope };

        antelope.Die(animals);

        Assert.DoesNotContain(antelope, animals);
    }

    [Fact]
    public void Lion_Health_Increases_When_Eating_Antelope()
    {
        var lion = new Lion { X = 0, Y = 0 };
        var antelope = new Antelope { X = 0, Y = 0 };
        var animals = new List<Animal> { lion, antelope };
        double initialHealth = lion.Health;

        lion.Move(animals, 20, 10);

        Assert.Equal(initialHealth + 20 - 0.5, lion.Health); // +20 for eating, -0.5 for move
        Assert.DoesNotContain(antelope, animals);
    }

    [Fact]
    public void Birth_Occurs_When_Two_Antelopes_Are_Near_For_Three_Rounds()
    {
        var antelope1 = new Antelope { X = 0, Y = 0 };
        var antelope2 = new Antelope { X = 0, Y = 1 };
        var animals = new List<Animal> { antelope1, antelope2 };
        int initialCount = animals.Count;

        // Simulate 3 rounds
        for (int i = 0; i < 3; i++)
        {
            antelope1.Tick(animals, 20, 10);
            antelope2.Tick(animals, 20, 10);
        }

        Assert.True(animals.Count > initialCount); // New antelope born
        Assert.Contains(animals, a => a != antelope1 && a != antelope2 && a is Antelope);
    }
}