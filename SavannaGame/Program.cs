using SavannaEngine;

class Program
{
    static void Main()
    {
        var engine = new GameEngine(20, 10);

        bool running = true;
        while (running)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.A)
                    engine.AddAnimal(new Antelope());
                else if (key == ConsoleKey.L)
                    engine.AddAnimal(new Lion());
                else if (key == ConsoleKey.Q)
                    running = false;
            }

            engine.Tick();

            Console.Clear();
            Console.WriteLine("Press 'A' to add Antelope, 'L' to add Lion, 'Q' to quit.");
            Console.WriteLine("Animals on field:");
            foreach (var animal in engine.Animals)
                Console.WriteLine($"{animal.Name} at ({animal.X}, {animal.Y})");

            Thread.Sleep(500); // Slow down the loop for visibility
        }
    }
}