using SavannaContracts;

namespace SavannaBehaviors
{
    public class AntelopeBehavior : IAnimalBehavior
    {
        public string Name => "Antelope";
        public int Speed => 2;
        public int VisionRange => 5;
        public int ActionInterval => 2;
        public double DefaultHealth => 10.0;
        public double HealthDecreasePerMove => 0.5;

        public void Act()
        {
            // Antelope-specific logic here
        }
    }
}