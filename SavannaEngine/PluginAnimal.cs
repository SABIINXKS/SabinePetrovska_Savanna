using System.Collections.Generic;
using System.Linq;
using SavannaContracts;

namespace SavannaEngine
{
    /// <summary>
    /// Represents an animal imported via plugin, wrapping IAnimalBehavior.
    /// </summary>
    public class PluginAnimal : Animal
    {
        private static Dictionary<(int, int), int> _adjacentRounds = new();

        private readonly IAnimalBehavior _behavior;

        public PluginAnimal(IAnimalBehavior behavior)
        {
            _behavior = behavior;
            Name = behavior.Name;
            Speed = behavior.Speed;
            VisionRange = behavior.VisionRange;
            ActionInterval = behavior.ActionInterval;
            Health = behavior.DefaultHealth;
            Behavior = behavior;
        }

        public void Act()
        {
            _behavior.Act();
        }

        public bool Catch()
        {
            // Example: decrease health when caught
            Health -= 1.0;
            return Health <= 0;
        }

        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            var rand = new Random();
            X = Math.Clamp(X + rand.Next(-Speed, Speed + 1), 0, fieldWidth - 1);
            Y = Math.Clamp(Y + rand.Next(-Speed, Speed + 1), 0, fieldHeight - 1);

            Health -= _behavior.HealthDecreasePerMove;

            // Death logic
            if (Health <= 0)
            {
                animals.Remove(this);
                return;
            }

            // Birth logic for plugin animals
            HandleBirth(animals, fieldWidth, fieldHeight);
        }

        private void HandleBirth(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            var sameTypeAnimals = animals
                .Where(a => a != this && a.Name == this.Name)
                .ToList();

            foreach (var other in sameTypeAnimals)
            {
                if (DistanceTo(other) <= SavannaConstants.BirthProximity)
                {
                    var key = GetPairKey(this, other);

                    if (_adjacentRounds.ContainsKey(key))
                        _adjacentRounds[key]++;
                    else
                        _adjacentRounds[key] = 1;

                    if (_adjacentRounds[key] == SavannaConstants.BirthRoundsRequired)
                    {
                        int newX = Math.Clamp((X + other.X) / 2, 0, fieldWidth - 1);
                        int newY = Math.Clamp((Y + other.Y) / 2, 0, fieldHeight - 1);

                        var baby = new PluginAnimal(_behavior);
                        baby.X = newX;
                        baby.Y = newY;
                        animals.Add(baby);
                        _adjacentRounds[key] = 0;
                    }
                }
                else
                {
                    var key = GetPairKey(this, other);
                    _adjacentRounds[key] = 0;
                }
            }
        }

        private (int, int) GetPairKey(Animal a, Animal b)
        {
            int idA = a.GetHashCode();
            int idB = b.GetHashCode();
            return idA < idB ? (idA, idB) : (idB, idA);
        }
    }
}