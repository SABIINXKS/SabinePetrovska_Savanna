using System;
using System.Collections.Generic;
using System.Linq;

namespace SavannaEngine
{
    /// <summary>
    /// Represents a lion with hunting logic and special actions.
    /// </summary>
    public class Lion : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Lion class with default properties.
        /// </summary>
        public Lion()
        {
            Name = SavannaConstants.LionName;
            Speed = SavannaConstants.LionSpeed;
            VisionRange = SavannaConstants.LionVisionRange;
            ActionInterval = SavannaConstants.LionActionInterval;
            Health = SavannaConstants.LionDefaultHealth;
        }

        /// <summary>
        /// Moves the lion towards the nearest antelope or randomly if none are nearby.
        /// Decreases health by 0.5 on each move.
        /// Increases health when antelope is eaten.
        /// Removes lion from simulation if health is zero or less.
        /// </summary>
        /// <param name="animals">List of all animals in the field.</param>
        /// <param name="fieldWidth">Width of the field.</param>
        /// <param name="fieldHeight">Height of the field.</param>
        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            Health -= 0.5;

            var nearestAntelope = animals
                .OfType<Antelope>()
                .OrderBy(a => DistanceTo(a))
                .FirstOrDefault(a => DistanceTo(a) <= VisionRange);

            if (nearestAntelope != null)
            {
                int dx = nearestAntelope.X - X;
                int dy = nearestAntelope.Y - Y;
                int moveX = Math.Abs(dx) < Speed ? dx : Math.Sign(dx) * Speed;
                int moveY = Math.Abs(dy) < Speed ? dy : Math.Sign(dy) * Speed;
                X = Math.Clamp(X + moveX, 0, fieldWidth - 1);
                Y = Math.Clamp(Y + moveY, 0, fieldHeight - 1);

                if (DistanceTo(nearestAntelope) == 0)
                {
                    animals.Remove(nearestAntelope);
                    Health += 5.0; // Increase health when antelope is eaten
                }
            }
            else
            {
                Random rnd = new();
                X = Math.Clamp(X + rnd.Next(-Speed, Speed + 1), 0, fieldWidth - 1);
                Y = Math.Clamp(Y + rnd.Next(-Speed, Speed + 1), 0, fieldHeight - 1);
            }

            // Death logic: remove lion if health is zero or less
            if (Health <= 0)
            {
                Die(animals);
            }
        }

        /// <summary>
        /// Removes this lion from the simulation due to lack of health.
        /// </summary>
        /// <param name="animals">List of all animals in the field.</param>
        public void Die(List<Animal> animals)
        {
            animals.Remove(this);
        }

        /// <summary>
        /// Performs a special action unique to the lion (placeholder).
        /// </summary>
        /// <param name="animals">List of all animals in the field.</param>
        public override void PerformSpecialAction(List<Animal> animals)
        {
            // Example: Lion could "roar" (not implemented, placeholder)
        }
    }
}