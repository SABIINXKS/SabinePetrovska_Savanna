using SavannaContracts;
using System.Collections.Generic;

namespace SavannaEngine
{
    /// <summary>
    /// Represents an animal imported via plugin, wrapping IAnimalBehavior.
    /// </summary>
    public class PluginAnimal : Animal
    {
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
            // Basic movement logic for plugin animals (can be customized)
            // Example: move randomly within bounds
            var rand = new Random();
            X = Math.Clamp(X + rand.Next(-Speed, Speed + 1), 0, fieldWidth - 1);
            Y = Math.Clamp(Y + rand.Next(-Speed, Speed + 1), 0, fieldHeight - 1);

            // Decrease health per move
            Health -= _behavior.HealthDecreasePerMove;
        }
    }
}