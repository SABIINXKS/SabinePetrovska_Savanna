using SavannaEngine;

class Program
{
    static void Main()
    {
        var engine = new GameEngine();

        Console.WriteLine("Press 'A' to add Antelope, 'L' to add Lion, 'Q' to quit. ");

        while (true)
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.A)
            {
                engine.AddAnimal(new Antelope());
                Console.WriteLine("Antelope added.");
            }
            else if (key == ConsoleKey.L)
            {
                engine.AddAnimal(new Lion());
                Console.WriteLine("Lion added.");
            }
            else if (key == ConsoleKey.Q)
            {
                break;
            }
        }
        Console.WriteLine("Animals on field:");
        foreach (var animal in engine.Animals)
            Console.WriteLine(animal.Name);
    }
}