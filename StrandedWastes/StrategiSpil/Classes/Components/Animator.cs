using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StrategiSpil
{
    class Animator : Component, IUpdateable
    {
        private SpriteRenderer spriteRenderer;
        private int index;
        private float timeElapsed;
        private float animationSpeed;
        private Rectangle[] rectangles;
        private string animationName;
        public string AnimationName { get { return animationName; } set { animationName = value; } }
        public int Index { get { return index; } }
        private Dictionary<string, Animation> animations;

        public Dictionary<string, Animation> Animations { get { return animations; } }

        public Animator(GameObject gameObject, float animationSpeed) : base(gameObject)
        {
            animations = new Dictionary<string, Animation>();
            this.animationSpeed = animationSpeed;
            this.spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");

        }

        public void CreateAnimation(string name,Animation animation)
        {
            animations.Add(name, animation);

        }

        public void PlayAnimation(string animationName)
        {
            if (this.animationName != animationName)
            {
                this.AnimationName = animationName;
                //Sets the rectangles
                this.rectangles = animations[animationName].Rectangles;
                //Resets the rectangle
                this.spriteRenderer.Rectangle = rectangles[0];
                //Sets the offset
                this.spriteRenderer.Offset = animations[animationName].Offset;
                //Sets the animation name
                this.animationName = animationName;
                //Sets the fps
                this.animationSpeed = animations[animationName].AnimationSpeed;
                //Resets the animation
                timeElapsed = 0;
                
                    index = 0;
                
                    
                
            }
        }

        public void Update()
        {
            timeElapsed += GameWorld.Instance.Deltatime;
            index = (int)(timeElapsed * animationSpeed);
            if (animationName != null)
            {
                if (index > rectangles.Length - 1)
                {
                    GameObject.OnAnimationDone(animationName);
                    timeElapsed = 0;
                    if (animationName.Contains("Death"))
                    {
                      index =  rectangles.Length - 1;
                    }
                    else
                    {
                        index = 0;
                    }

                }
                spriteRenderer.Rectangle = rectangles[index];
            }
        }
    }
}