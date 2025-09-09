namespace SavannaEngine
{
    public abstract class Animal
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int VisionRange { get; set; }
        public int ActionInterval { get; set; } // How many ticks between actions
        private int _actionCounter = 0;         // Internal tick counter

        // Called by GameEngine each tick
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

        public abstract void Move(List<Animal> animals, int fieldWidth, int fieldHeight);
        public virtual void PerformSpecialAction(List<Animal> animals) { }
        protected double DistanceTo(Animal other) =>
            Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
    }
}