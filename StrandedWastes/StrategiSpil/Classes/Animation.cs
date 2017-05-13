using Microsoft.Xna.Framework;

namespace StrategiSpil
{
    internal class Animation
    {
        private Vector2 offset;
        public Vector2 Offset { get { return offset; } }
        private float animationSpeed;
        public float AnimationSpeed { get { return animationSpeed; } }
        private Rectangle[] rectangles;
        public Rectangle[] Rectangles { get { return rectangles; } }
        public int Frames { get; }
        public Animation(int frames, int yPos, int xStartFrame, int width, int height, float animationSpeed, Vector2 offset)
        {
            Frames = frames;
            rectangles = new Rectangle[frames];
            this.offset = offset;
            this.animationSpeed = animationSpeed;
            for (int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }

        }
    }
}
