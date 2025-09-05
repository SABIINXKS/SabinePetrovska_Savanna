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
        public int Health { get; set; }         // Health metric added
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

    public class Antelope : Animal
    {
        public Antelope()
        {
            Name = "Antelope";
            Speed = 2;
            VisionRange = 5;
            ActionInterval = 2; // Acts every 2 ticks
            Health = 100;       // Initial health value
        }

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

        public override void PerformSpecialAction(List<Animal> animals)
        {
            // Example: Antelope could "jump" or "hide" (not implemented, placeholder)
        }
    }

    public class Lion : Animal
    {
        public Lion()
        {
            Name = "Lion";
            Speed = 3;
            VisionRange = 7;
            ActionInterval = 1; // Acts every tick
            Health = 120;       // Initial health value
        }

        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
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
                    animals.Remove(nearestAntelope);
            }
            else
            {
                Random rnd = new();
                X = Math.Clamp(X + rnd.Next(-Speed, Speed + 1), 0, fieldWidth - 1);
                Y = Math.Clamp(Y + rnd.Next(-Speed, Speed + 1), 0, fieldHeight - 1);
            }
        }

        public override void PerformSpecialAction(List<Animal> animals)
        {
            // Example: Lion could "roar" (not implemented, placeholder)
        }
    }

    public class GameEngine
    {
        public List<Animal> Animals { get; } = new();
        public int FieldWidth { get; }
        public int FieldHeight { get; }

        public GameEngine(int fieldWidth = 20, int fieldHeight = 10)
        {
            FieldWidth = fieldWidth;
            FieldHeight = fieldHeight;
        }

        public void AddAnimal(Animal animal)
        {
            Random rnd = new();
            animal.X = rnd.Next(0, FieldWidth);
            animal.Y = rnd.Next(0, FieldHeight);
            Animals.Add(animal);
        }

        public void Tick()
        {
            var currentAnimals = Animals.ToList();
            foreach (var animal in currentAnimals)
                animal.Tick(Animals, FieldWidth, FieldHeight);
        }
    }
}