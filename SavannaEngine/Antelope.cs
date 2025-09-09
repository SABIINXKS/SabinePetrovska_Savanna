namespace SavannaEngine
{
    public class Antelope : Animal
    {
        public Antelope()
        {
            Name = "Antelope";
            Speed = 2;
            VisionRange = 5;
            ActionInterval = 2; // Acts every 2 ticks
        }

        public override void Move(List<Animal> animals, int fieldWidth, int fieldHeight)
        {
            var nearestLion = animals
                .OfType<Lion>()
                .OrderBy(l => DistanceTo(l))
                .FirstOrDefault(l => DistanceTo(l) <= VisionRange);

            if (nearestLion != null)
            {
                int dx = X - nearestLion.X;
                int dy = Y - nearestLion.Y;
                int moveX = Math.Abs(dx) < Speed ? dx : Math.Sign(dx) * Speed;
                int moveY = Math.Abs(dy) < Speed ? dy : Math.Sign(dy) * Speed;
                X = Math.Clamp(X + moveX, 0, fieldWidth - 1);
                Y = Math.Clamp(Y + moveY, 0, fieldHeight - 1);
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
            // Example: Antelope could "jump" or "hide" (not implemented, placeholder)
        }
    }
}