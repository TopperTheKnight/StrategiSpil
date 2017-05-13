using Microsoft.Xna.Framework;

namespace StrategiSpil
{
    public class Transform : Component
    {
        private Vector2 position;
        //returns the position
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Transform(GameObject gameObject, Vector2 position) : base(gameObject)
        {
            this.position = position;
        }
    }
}