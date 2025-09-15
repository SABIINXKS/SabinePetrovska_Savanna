using SavannaContracts;

namespace SavannaBehaviors
{
    public class LionBehavior : IAnimalBehavior
    {
        public string Name => "Lion";
        public int Speed => 3;
        public int VisionRange => 7;
        public int ActionInterval => 1;
        public double DefaultHealth => 12.0;
        public double HealthDecreasePerMove => 0.5;

        public void Act()
        {
            // Lion-specific logic here
        }
    }
}