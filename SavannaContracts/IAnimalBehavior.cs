namespace SavannaContracts
{
    public interface IAnimalBehavior
    {
        string Name { get; }
        int Speed { get; }
        int VisionRange { get; }
        int ActionInterval { get; }
        double DefaultHealth { get; }
        double HealthDecreasePerMove { get; }

        void Act();
    }
}