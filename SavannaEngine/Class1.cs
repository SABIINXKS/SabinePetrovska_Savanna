namespace SavannaEngine
{
    public abstract class Animal
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int VisionRange { get; set; }

        public abstract void Move(List<Animal> animals, int fieldWidth, int fieldHeight);
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
        }

        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            // Find nearest Lion within vision range
            var nearestLion = animals
                .OfType<Lion>()
                .OrderBy(l => DistanceTo(l))
                .FirstOrDefault(l => DistanceTo(l) <= VisionRange);

            if (nearestLion != null)
            {
                // Move away from Lion
                int dx = X - nearestLion.X;
                int dy = Y - nearestLion.Y;
                if (dx != 0) X += Math.Sign(dx) * Speed;
                if (dy != 0) Y += Math.Sign(dy) * Speed;
            }
            else
            {
                // Random move if no Lion nearby
                Random rnd = new();
                X = Math.Clamp(X + rnd.Next(-Speed, Speed + 1), 0, fieldWidth - 1);
                Y = Math.Clamp(Y + rnd.Next(-Speed, Speed + 1), 0, fieldHeight - 1);
            }
        }
    }

    public class Lion : Animal
    {
        public Lion()
        {
            Name = "Lion";
            Speed = 3;
            VisionRange = 7;
        }

        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            // Find nearest Antelope within vision range
            var nearestAntelope = animals
                .OfType<Antelope>()
                .OrderBy(a => DistanceTo(a))
                .FirstOrDefault(a => DistanceTo(a) <= VisionRange);

            if (nearestAntelope != null)
            {
                // Move toward Antelope
                int dx = nearestAntelope.X - X;
                int dy = nearestAntelope.Y - Y;
                if (dx != 0) X += Math.Sign(dx) * Speed;
                if (dy != 0) Y += Math.Sign(dy) * Speed;

                // Eat Antelope if reached
                if (DistanceTo(nearestAntelope) == 0)
                    animals.Remove(nearestAntelope);
            }
            else
            {
                // Random move if no Antelope nearby
                Random rnd = new();
                X = Math.Clamp(X + rnd.Next(-Speed, Speed + 1), 0, fieldWidth - 1);
                Y = Math.Clamp(Y + rnd.Next(-Speed, Speed + 1), 0, fieldHeight - 1);
            }
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
            // Place animal at random position
            Random rnd = new();
            animal.X = rnd.Next(0, FieldWidth);
            animal.Y = rnd.Next(0, FieldHeight);
            Animals.Add(animal);
        }

        public void Tick()
        {
            // Copy list to avoid modification during iteration
            var currentAnimals = Animals.ToList();
            foreach (var animal in currentAnimals)
                animal.Move(Animals, FieldWidth, FieldHeight);
        }
    }
}