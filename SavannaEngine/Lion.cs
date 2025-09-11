using System;
using System.Collections.Generic;
using System.Linq;

namespace SavannaEngine
{
    /// <summary>
    /// Represents a lion with hunting logic, birth, and death.
    /// </summary>
    public class Lion : Animal
    {
        // Tracks consecutive rounds for each lion pair
        private static Dictionary<(int, int), int> _adjacentRounds = new();

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
        /// Decreases health by LionHealthDecreasePerMove on each move.
        /// Increases health when antelope is eaten.
        /// Removes lion from simulation if health is zero or less.
        /// Handles birth logic when two lions are adjacent for 3 consecutive rounds.
        /// </summary>
        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            Health -= SavannaConstants.LionHealthDecreasePerMove;

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
                return;
            }

            // Birth logic
            HandleBirth(animals, fieldWidth, fieldHeight);
        }

        /// <summary>
        /// Removes this lion from the simulation due to lack of health.
        /// </summary>
        public void Die(List<Animal> animals)
        {
            animals.Remove(this);
        }

        /// <summary>
        /// Checks for adjacent lions and handles birth if conditions are met.
        /// </summary>
        private void HandleBirth(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            var lions = animals.OfType<Lion>().Where(l => l != this).ToList();
            foreach (var otherLion in lions)
            {
                if (DistanceTo(otherLion) <= SavannaConstants.BirthProximity)
                {
                    var key = GetPairKey(this, otherLion);

                    if (_adjacentRounds.ContainsKey(key))
                        _adjacentRounds[key]++;
                    else
                        _adjacentRounds[key] = 1;

                    if (_adjacentRounds[key] == SavannaConstants.BirthRoundsRequired)
                    {
                        // Birth: create a new lion at a nearby position
                        int newX = Math.Clamp((X + otherLion.X) / 2, 0, fieldWidth - 1);
                        int newY = Math.Clamp((Y + otherLion.Y) / 2, 0, fieldHeight - 1);
                        var babyLion = new Lion { X = newX, Y = newY };
                        animals.Add(babyLion);
                        _adjacentRounds[key] = 0; // Reset counter
                    }
                }
                else
                {
                    var key = GetPairKey(this, otherLion);
                    _adjacentRounds[key] = 0;
                }
            }
        }

        private (int, int) GetPairKey(Lion a, Lion b)
        {
            int idA = a.GetHashCode();
            int idB = b.GetHashCode();
            return idA < idB ? (idA, idB) : (idB, idA);
        }

        /// <summary>
        /// Performs a special action unique to the lion (placeholder).
        /// </summary>
        public override void PerformSpecialAction(List<Animal> animals)
        {
            // Example: Lion could "roar" (not implemented, placeholder)
        }
    }
}