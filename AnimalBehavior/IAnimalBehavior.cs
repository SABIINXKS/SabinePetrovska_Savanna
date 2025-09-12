namespace AnimalBehavior
{
    public interface IAnimalBehavior
    {
        void Move(object animal, List<object> animals, int fieldWidth, int fieldHeight);
        void PerformSpecialAction(object animal, List<object> animals);
    }
}