using System;
using System.Collections.Generic;
using System.Linq;

namespace SavannaEngine
{
    /// <summary>
    /// Manages the game state, animals, and game ticks.
    /// </summary>
    public class GameEngine
    {
        /// <summary>
        /// Gets the list of animals in the game.
        /// </summary>
        public List<Animal> Animals { get; } = new();

        /// <summary>
        /// Gets the width of the field.
        /// </summary>
        public int FieldWidth { get; }

        /// <summary>
        /// Gets the height of the field.
        /// </summary>
        public int FieldHeight { get; }

        /// <summary>
        /// Initializes a new instance of the GameEngine class with specified field dimensions.
        /// </summary>
        /// <param name="fieldWidth">Width of the field.</param>
        /// <param name="fieldHeight">Height of the field.</param>
        public GameEngine(int fieldWidth = 20, int fieldHeight = 10)
        {
            FieldWidth = fieldWidth;
            FieldHeight = fieldHeight;
        }

        /// <summary>
        /// Adds an animal to the game at a random position.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(Animal animal)
        {
            Random rnd = new();
            animal.X = rnd.Next(0, FieldWidth);
            animal.Y = rnd.Next(0, FieldHeight);
            Animals.Add(animal);
        }

        /// <summary>
        /// Advances the game by one tick, updating all animals.
        /// </summary>
        public void Tick()
        {
            var currentAnimals = Animals.ToList();
            foreach (var animal in currentAnimals)
                animal.Tick(Animals, FieldWidth, FieldHeight);
        }
    }
}