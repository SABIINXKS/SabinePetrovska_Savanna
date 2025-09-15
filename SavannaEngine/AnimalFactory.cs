using SavannaContracts;
using SavannaBehaviors;

namespace SavannaEngine
{
    public static class AnimalFactory
    {
        public static IAnimalBehavior CreateAntelope() => new AntelopeBehavior();
        public static IAnimalBehavior CreateLion() => new LionBehavior();
    }
}