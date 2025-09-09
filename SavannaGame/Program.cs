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
            Console.WriteLine("Field:");

            // Draw top border
            Console.WriteLine(new string('#', engine.FieldWidth + 2));

            // Draw grid rows
            for (int y = 0; y < engine.FieldHeight; y++)
            {
                Console.Write("#");
                for (int x = 0; x < engine.FieldWidth; x++)
                {
                    var animalsAtCell = engine.Animals.Where(a => a.X == x && a.Y == y).ToList();
                    if (animalsAtCell.Any(a => a is Lion))
                        Console.Write("L");
                    else if (animalsAtCell.Any(a => a is Antelope))
                        Console.Write("A");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine("#");
            }

            // Draw bottom border
            Console.WriteLine(new string('#', engine.FieldWidth + 2));

            Console.WriteLine("Animals on field:");
            foreach (var animal in engine.Animals)
                Console.WriteLine($"{animal.Name} at ({animal.X}, {animal.Y}) | Health: {animal.Health}");

            Thread.Sleep(500); // Slow down the loop for visibility
        }
    }
}