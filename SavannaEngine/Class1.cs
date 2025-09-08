namespace SavannaEngine
{
    public abstract class Animal
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int VisionRange { get; set; }
        public int ActionInterval { get; set; }
        public double Health { get; set; }
        private int _actionCounter = 0;

        // For birth tracking
        private int _nearSameTypeRounds = 0;
        private Animal _lastNearbySameType = null;

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
            // Death check after action
            if (Health <= 0)
                Die(animals);

            // Birth check after action
            CheckBirth(animals);
        }

        public abstract void Move(List<Animal> animals, int fieldWidth, int fieldHeight);
        public virtual void PerformSpecialAction(List<Animal> animals) { }
        protected double DistanceTo(Animal other) =>
            Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));

        protected void DecreaseHealthOnMove()
        {
            Health -= 0.5;
        }

        // Death function: removes itself from the animals list
        public virtual void Die(List<Animal> animals)
        {
            animals.Remove(this);
        }

        // Birth function logic
        private void CheckBirth(List<Animal> animals)
        {
            // Find nearest same type animal (excluding self)
            var nearestSameType = animals
                .Where(a => a.GetType() == this.GetType() && a != this)
                .OrderBy(a => DistanceTo(a))
                .FirstOrDefault(a => DistanceTo(a) <= 1); // Distance 1 means adjacent or same cell

            if (nearestSameType != null)
            {
                if (_lastNearbySameType == nearestSameType)
                    _nearSameTypeRounds++;
                else
                    _nearSameTypeRounds = 1;

                _lastNearbySameType = nearestSameType;

                if (_nearSameTypeRounds >= 3)
                {
                    // Birth: add new animal of same type at this position
                    Animal baby = null;
                    if (this is Antelope)
                        baby = new Antelope { X = this.X, Y = this.Y };
                    else if (this is Lion)
                        baby = new Lion { X = this.X, Y = this.Y };

                    if (baby != null)
                        animals.Add(baby);

                    _nearSameTypeRounds = 0; // Reset counter after birth
                }
            }
            else
            {
                _nearSameTypeRounds = 0;
                _lastNearbySameType = null;
            }
        }
    }

    public class Antelope : Animal
    {
        public Antelope()
        {
            Name = "Antelope";
            Speed = 2;
            VisionRange = 5;
            ActionInterval = 2;
            Health = 100;
        }

        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            DecreaseHealthOnMove();

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
    }

    public class Lion : Animal
    {
        public Lion()
        {
            Name = "Lion";
            Speed = 3;
            VisionRange = 7;
            ActionInterval = 1;
            Health = 120;
        }

        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            DecreaseHealthOnMove();

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
                    Health += 20; // Increase Lion's health when antelope is eaten
                }
            }
            else
            {
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