using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace StrategiSpil
{
    public class Collider : Component, ILoadable, IUpdateable, IDrawable
    {/// <summary>
     /// A reference to the boxcolliders spriterenderer
     /// </summary>
        public SpriteRenderer spriteRenderer { get; private set; }

        private Animator animator;

        private int[] Intersection;
        private bool doPixelCheck;

        /// <summary>
        /// A reference to the colliders texture
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Indicates if we need to use pixel collision or not
        /// </summary>
        public bool UsePixelCollision { get; set; }

        /// <summary>
        /// Indicates if this collider needs to check for collisions
        /// </summary>
        public bool DoCollisionChecks { get; set; }

        /// <summary>
        /// Contains all the colliders that this collider is colliding with
        /// </summary>
        private HashSet<Collider> otherColliders = new HashSet<Collider>();

        /// <summary>
        /// Dictionary that contains pixels for all animations
        /// </summary>
        private Lazy<Dictionary<string, Color[][]>> pixels;

        private Color[] CurrentPixels
        {
            get
            {
                if (animator != null)
                    return pixels.Value[animator.AnimationName][animator.Index];
                else return null;
            }
        }

        /// <summary>
        /// The colliders collisionbox
        /// </summary>
        public Rectangle CollisionBox
        {
            get
            {
                    return new Rectangle
                        (
                            (int)(GameObject.Transform.Position.X + spriteRenderer.Offset.X),
                            (int)(GameObject.Transform.Position.Y + spriteRenderer.Offset.Y),
                            (int)(spriteRenderer.Rectangle.Width * spriteRenderer.Scale),
                            (int)(spriteRenderer.Rectangle.Height * spriteRenderer.Scale)
                        );
            }
        }

        public Collider(GameObject gameObject,bool usePixelCollision) : base(gameObject)
        {
            Intersection = new int[4];
            doPixelCheck = true;

            pixels = new Lazy<Dictionary<string, Color[][]>>(() => CachePixels());

            DoCollisionChecks = true;

            UsePixelCollision = usePixelCollision;

            spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");

            animator = (Animator)gameObject.GetComponent("Animator");

            GameWorld.Instance.Colliders.Add(this);

        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("collider");
            if(UsePixelCollision)
            CachePixels();
        }

        public void Update()
        {
            CheckCollision();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
#if DEBUG
            

            Rectangle topLine = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(CollisionBox.X, CollisionBox.Y + CollisionBox.Height, CollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(CollisionBox.X + CollisionBox.Width, CollisionBox.Y, 1, CollisionBox.Height);
            Rectangle leftLine = new Rectangle(CollisionBox.X, CollisionBox.Y, 1, CollisionBox.Height);

            spriteBatch.Draw(texture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
#endif
        }

        private Dictionary<string, Color[][]> CachePixels()
        {
            Dictionary<string, Color[][]> tmpPixels = new Dictionary<string, Color[][]>();
            if(animator != null)
            foreach (KeyValuePair<string, Animation> pair in animator.Animations)
            {
                Animation animation = pair.Value;

                Color[][] colors = new Color[animation.Frames][];

                for (int i = 0; i < animation.Frames; i++)
                {
                    colors[i] = new Color[animation.Rectangles[i].Width * animation.Rectangles[i].Height];

                    spriteRenderer.Sprite.GetData(0, animation.Rectangles[i], colors[i], 0, animation.Rectangles[i].Width * animation.Rectangles[i].Height);
                }

                tmpPixels.Add(pair.Key, colors);
            }

            return tmpPixels;
        }

        private void CheckCollision()
        {
            if (DoCollisionChecks)
            {
                foreach (Collider other in GameWorld.Instance.Colliders)
                {
                    
                    if (other != this)
                    {
                        if(Vector2.Distance(GameObject.Transform.Position,other.GameObject.Transform.Position) < 150)
                        {
                            if (CollisionBox.Intersects(other.CollisionBox) && ((UsePixelCollision && CheckPixelCollision(other)) || !UsePixelCollision))
                            {
                                GameObject.OnCollisionStay(other);

                                if (!otherColliders.Contains(other))
                                {
                                    otherColliders.Add(other);
                                    GameObject.OnCollisionEnter(other);
                                }
                            }
                            else if ((otherColliders.Contains(other) && !UsePixelCollision) || (CollisionBox.Intersects(other.CollisionBox) && (UsePixelCollision && !CheckPixelCollision(other))))
                            {
                                otherColliders.Remove(other);
                                GameObject.OnCollisionExit(other);
                            }
                        }
                        
                    }

                }
            }

        }

        private bool CheckPixelCollision(Collider other)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(CollisionBox.Top, other.CollisionBox.Top);
            int bottom = Math.Min(CollisionBox.Bottom, other.CollisionBox.Bottom);
            int left = Math.Max(CollisionBox.Left, other.CollisionBox.Left);
            int right = Math.Min(CollisionBox.Right, other.CollisionBox.Right);

            int[] temp = new int[4] { top, bottom, left, right };
            //Checks if the intersection is the same as last time, if it is, we wont do a pixel collision check.
            if (temp[0] == Intersection[0] && temp[1] == Intersection[1] && temp[2] == Intersection[2] && temp[3] == Intersection[3])
                doPixelCheck = false;
            else
            {
                doPixelCheck = true;
                Intersection = temp;
            }

            if (animator != null && other.animator != null && doPixelCheck) { 
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    int firstIndex = (x - CollisionBox.Left) + (y - CollisionBox.Top) * CollisionBox.Width;
                    int secondIndex = (x - other.CollisionBox.Left) + (y - other.CollisionBox.Top) * other.CollisionBox.Width;

                    //Get the color of both pixels at this point 
                    Color colorA = CurrentPixels[firstIndex];
                    Color colorB = other.CurrentPixels[secondIndex];

                    // If both pixels are not completely transparent
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        //Then an intersection has been found
                        return true;
                    }
                }
            }
            }
            return false;
        }
    }
}
