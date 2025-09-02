namespace SavannaEngine
{
    public abstract class Animal
    {
        public string Name { get; set; }
    }

    public class Antelope : Animal
    {
        public Antelope() => Name = "Antelope";
    }

    public class Lion : Animal
    {
        public Lion() => Name = "Lion";
    }

    public class GameEngine
    {
        public List<Animal> Animals { get; } = new();

        public void AddAnimal(Animal animal)
        {
            Animals.Add(animal);
        }
    }
}