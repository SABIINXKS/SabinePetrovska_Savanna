namespace SavannaEngine
{
    /// <summary>
    /// Contains constant values used throughout the Savanna simulation.
    /// </summary>
    public static class SavannaConstants
    {
        public const int DefaultFieldWidth = 20;
        public const int DefaultFieldHeight = 10;
        public const int LoopDelayMs = 500;
        public const string AddAntelopeKey = "A";
        public const string AddLionKey = "L";
        public const string QuitKey = "Q";
        public const char LionSymbol = 'L';
        public const char AntelopeSymbol = 'A';
        public const char EmptyCellSymbol = ' ';
        public const char BorderSymbol = '#';
        public const string AddAnimalPrompt = "Press 'A' to add Antelope, 'L' to add Lion, 'Q' to quit.";
        public const string FieldLabel = "Field:";
        public const string AnimalsLabel = "Animals on field:";

        // Antelope-specific constants
        public const string AntelopeName = "Antelope";
        public const int AntelopeSpeed = 2;
        public const int AntelopeVisionRange = 5;
        public const int AntelopeActionInterval = 2;
    }
}