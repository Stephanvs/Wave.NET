namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public sealed class PositionTracker
    {
        private static int _position = 0;

        public IRelativePosition GetPosition1()
        {
            return new FirstPosition();
        }

        public IRelativePosition GetPosition2()
        {
            return new SecondPosition();
        }

        class FirstPosition
            : IRelativePosition
        {
            public void Increase(int amount)
            {
                _position += amount;
            }

            public int Get()
            {
                return _position;
            }
        }

        class SecondPosition
            : IRelativePosition
        {
            public void Increase(int amount)
            {
                _position -= amount;
            }

            public int Get()
            {
                return -_position;
            }
        }
    }
}