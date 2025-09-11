using System;
using System.Collections.Generic;
using System.Linq;

namespace SavannaEngine
{
    /// <summary>
    /// Represents an antelope with specific movement, birth, and behavior.
    /// </summary>
    public class Antelope : Animal
    {
        // Tracks consecutive rounds for each antelope pair
        private static Dictionary<(int, int), int> _adjacentRounds = new();

        /// <summary>
        /// Initializes a new instance of the Antelope class with default properties.
        /// </summary>
        public Antelope()
        {
            Name = SavannaConstants.AntelopeName;
            Speed = SavannaConstants.AntelopeSpeed;
            VisionRange = SavannaConstants.AntelopeVisionRange;
            ActionInterval = SavannaConstants.AntelopeActionInterval;
            Health = SavannaConstants.AntelopeDefaultHealth;
        }

        /// <summary>
        /// Moves the antelope away from nearby lions or randomly if no lions are nearby.
        /// Decreases health by 0.5 on each move.
        /// Handles birth logic when two antelopes are adjacent for 3 consecutive rounds.
        /// Removes antelope from simulation if health is zero or less.
        /// </summary>
        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            Health -= 0.5;

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

            // Death logic: remove antelope if health is zero or less
            if (Health <= 0)
            {
                Die(animals);
                return;
            }

            // Birth logic
            HandleBirth(animals, fieldWidth, fieldHeight);
        }

        /// <summary>
        /// Removes this antelope from the simulation due to lack of health.
        /// </summary>
        public void Die(List<Animal> animals)
        {
            animals.Remove(this);
        }

        /// <summary>
        /// Checks for adjacent antelopes and handles birth if conditions are met.
        /// </summary>
        private void HandleBirth(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            var antelopes = animals.OfType<Antelope>().Where(a => a != this).ToList();
            foreach (var otherAntelope in antelopes)
            {
                if (DistanceTo(otherAntelope) <= SavannaConstants.BirthProximity)
                {
                    var key = GetPairKey(this, otherAntelope);

                    if (_adjacentRounds.ContainsKey(key))
                        _adjacentRounds[key]++;
                    else
                        _adjacentRounds[key] = 1;

                    if (_adjacentRounds[key] == SavannaConstants.BirthRoundsRequired)
                    {
                        // Birth: create a new antelope at a nearby position
                        int newX = Math.Clamp((X + otherAntelope.X) / 2, 0, fieldWidth - 1);
                        int newY = Math.Clamp((Y + otherAntelope.Y) / 2, 0, fieldHeight - 1);
                        var babyAntelope = new Antelope { X = newX, Y = newY };
                        animals.Add(babyAntelope);
                        _adjacentRounds[key] = 0; // Reset counter
                    }
                }
                else
                {
                    var key = GetPairKey(this, otherAntelope);
                    _adjacentRounds[key] = 0;
                }
            }
        }

        private (int, int) GetPairKey(Antelope a, Antelope b)
        {
            int idA = a.GetHashCode();
            int idB = b.GetHashCode();
            return idA < idB ? (idA, idB) : (idB, idA);
        }

        /// <summary>
        /// Performs a special action unique to the antelope (placeholder).
        /// </summary>
        public override void PerformSpecialAction(List<Animal> animals)
        {
            // Example: Antelope could "jump" or "hide" (not implemented, placeholder)
        }
    }
}