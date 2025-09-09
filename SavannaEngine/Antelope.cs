using System;
using System.Collections.Generic;
using System.Linq;

namespace SavannaEngine
{
    /// <summary>
    /// Represents an antelope with specific movement and behavior.
    /// </summary>
    public class Antelope : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Antelope class with default properties.
        /// </summary>
        public Antelope()
        {
            Name = "Antelope";
            Speed = 2;
            VisionRange = 5;
            ActionInterval = 2; // Acts every 2 ticks
        }

        /// <summary>
        /// Moves the antelope away from nearby lions or randomly if no lions are nearby.
        /// </summary>
        /// <param name="animals">List of all animals in the field.</param>
        /// <param name="fieldWidth">Width of the field.</param>
        /// <param name="fieldHeight">Height of the field.</param>
        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            var nearestLion = animals
                .OfType<Lion>()
                .OrderBy(l => DistanceTo(l))
                .FirstOrDefault(l => DistanceTo(l) <= VisionRange);

            if (nearestLion != null)
            {
                int dx = X - nearestLion.X;
                int dy = Y - nearestLion.Y;
                int moveX = Math.Abs(dx) < Speed ? dx : Math.Sign(dx) * Speed;
                int moveY = Math.Abs(dy) < Speed ? dy : Math.Sign(dy) * Speed;
                X = Math.Clamp(X + moveX, 0, fieldWidth - 1);
                Y = Math.Clamp(Y + moveY, 0, fieldHeight - 1);
            }
            else
            {
                Random rnd = new();
                X = Math.Clamp(X + rnd.Next(-Speed, Speed + 1), 0, fieldWidth - 1);
                Y = Math.Clamp(Y + rnd.Next(-Speed, Speed + 1), 0, fieldHeight - 1);
            }
        }

        /// <summary>
        /// Performs a special action unique to the antelope (placeholder).
        /// </summary>
        /// <param name="animals">List of all animals in the field.</param>
        public override void PerformSpecialAction(List<Animal> animals)
        {
            // Example: Antelope could "jump" or "hide" (not implemented, placeholder)
        }
    }
}