namespace SavannaEngine
{
    public class Lion : Animal
    {
        public Lion()
        {
            Name = "Lion";
            Speed = 3;
            VisionRange = 7;
            ActionInterval = 1; // Acts every tick
        }

        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            var nearestAntelope = animals
                .OfType<Antelope>()
                .OrderBy(a => DistanceTo(a))
                .FirstOrDefault(a => DistanceTo(a) <= VisionRange);

            if (nearestAntelope != null)
            {
                int dx = nearestAntelope.X - X;
                int dy = nearestAntelope.Y - Y;
                int moveX = Math.Abs(dx) < Speed ? dx : Math.Sign(dx) * Speed;
                int moveY = Math.Abs(dy) < Speed ? dy : Math.Sign(dy) * Speed;
                X = Math.Clamp(X + moveX, 0, fieldWidth - 1);
                Y = Math.Clamp(Y + moveY, 0, fieldHeight - 1);

                if (DistanceTo(nearestAntelope) == 0)
                    animals.Remove(nearestAntelope);
            }
            else
            {
                Random rnd = new();
                X = Math.Clamp(X + rnd.Next(-Speed, Speed + 1), 0, fieldWidth - 1);
                Y = Math.Clamp(Y + rnd.Next(-Speed, Speed + 1), 0, fieldHeight - 1);
            }
        }

        public override void PerformSpecialAction(List<Animal> animals)
        {
            // Example: Lion could "roar" (not implemented, placeholder)
        }
    }
}