namespace SavannaEngine
{
    /// <summary>
    /// Represents a base animal with movement and action logic.
    /// </summary>
    public abstract class Animal
    {
        /// <summary>
        /// Gets or sets the name of the animal.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the X position of the animal.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y position of the animal.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the movement speed of the animal.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the vision range of the animal.
        /// </summary>
        public int VisionRange { get; set; }

        /// <summary>
        /// Gets or sets the interval (in ticks) between actions.
        /// </summary>
        public int ActionInterval { get; set; }

        private int _actionCounter = 0; // Internal tick counter

        /// <summary>
        /// Advances the animal's internal tick counter and triggers actions at the specified interval.
        /// </summary>
        /// <param name="animals">List of all animals in the field.</param>
        /// <param name="fieldWidth">Width of the field.</param>
        /// <param name="fieldHeight">Height of the field.</param>
        public void Tick(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            _actionCounter++;
            if (_actionCounter >= ActionInterval)
            {
                Move(animals, fieldWidth, fieldHeight);
                PerformSpecialAction(animals);
                _actionCounter = 0;
            }
        }

        /// <summary>
        /// Moves the animal according to its logic.
        /// </summary>
        /// <param name="animals">List of all animals in the field.</param>
        /// <param name="fieldWidth">Width of the field.</param>
        /// <param name="fieldHeight">Height of the field.</param>
        public abstract void Move(List<Animal> animals, int fieldWidth, int fieldHeight);

        /// <summary>
        /// Performs a special action unique to the animal type.
        /// </summary>
        /// <param name="animals">List of all animals in the field.</param>
        public virtual void PerformSpecialAction(List<Animal> animals) { }

        /// <summary>
        /// Calculates the Euclidean distance to another animal.
        /// </summary>
        /// <param name="other">The other animal.</param>
        /// <returns>Distance as a double.</returns>
        protected double DistanceTo(Animal other) =>
            Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
    }
}