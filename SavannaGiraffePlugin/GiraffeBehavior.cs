using SavannaContracts;

namespace SavannaGiraffePlugin
{
    /// <summary>
    /// Represents the behavior of a giraffe for use as a plugin animal.
    /// </summary>
    public class GiraffeBehavior : IAnimalBehavior
    {
        public string Name => "Giraffe";
        public int Speed => 2;
        public int VisionRange => 6;
        public int ActionInterval => 2;
        public double DefaultHealth => 14.0;
        public double HealthDecreasePerMove => 0.3;

        public void Act()
        {
            // Giraffe-specific logic can be implemented here
        }
    }
}
