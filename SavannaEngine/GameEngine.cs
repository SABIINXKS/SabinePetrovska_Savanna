namespace SavannaEngine
{
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