using SavannaEngine;

class Program
{
    static void Main()
    {
        var engine = new GameEngine(SavannaConstants.DefaultFieldWidth, SavannaConstants.DefaultFieldHeight);
        var display = new ConsoleDisplay();

        bool running = true;
        while (running)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key.ToString().ToUpper();
                if (key == SavannaConstants.AddAntelopeKey)
                    engine.AddAnimal(new Antelope());
                else if (key == SavannaConstants.AddLionKey)
                    engine.AddAnimal(new Lion());
                else if (key == SavannaConstants.QuitKey)
                    running = false;
            }

            engine.Tick();
            display.Render(engine);

            Thread.Sleep(SavannaConstants.LoopDelayMs);
        }
    }
}
