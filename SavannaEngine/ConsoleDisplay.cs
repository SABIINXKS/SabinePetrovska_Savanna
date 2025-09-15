using SavannaEngine;
using System;
using System.Linq;

public class ConsoleDisplay
{
    /// <summary>
    /// Renders the game field and animal information to the console.
    /// </summary>
    public void Render(GameEngine engine)
    {
        Console.Clear();
        Console.WriteLine(SavannaConstants.AddAnimalPrompt);
        Console.WriteLine(SavannaConstants.FieldLabel);

        // Draw top border
        Console.WriteLine(new string(SavannaConstants.BorderSymbol, engine.FieldWidth + 2));

        // Draw grid rows
        for (int y = 0; y < engine.FieldHeight; y++)
        {
            Console.Write(SavannaConstants.BorderSymbol);
            for (int x = 0; x < engine.FieldWidth; x++)
            {
                var animalsAtCell = engine.Animals.Where(a => a.X == x && a.Y == y).ToList();
                if (animalsAtCell.Any())
                {
                    // Show the first letter of the first animal in the cell
                    Console.Write(char.ToUpper(animalsAtCell.First().Name[0]));
                }
                else
                {
                    Console.Write(SavannaConstants.EmptyCellSymbol);
                }
            }
            Console.WriteLine(SavannaConstants.BorderSymbol);
        }

        // Draw bottom border
        Console.WriteLine(new string(SavannaConstants.BorderSymbol, engine.FieldWidth + 2));

        Console.WriteLine(SavannaConstants.AnimalsLabel);
        foreach (var animal in engine.Animals)
            Console.WriteLine($"{animal.Name} at ({animal.X}, {animal.Y}) | Health: {animal.Health}");
    }
}