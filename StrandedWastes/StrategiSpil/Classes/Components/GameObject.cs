using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StrategiSpil
{
    public class GameObject : Component
    {
        private List<Component> components;
        private Transform transform;
        private bool isLoaded;
        public Transform Transform { get { return transform; } }
        public GameObject(Vector2 posistion)
        {
            components = new List<Component>();
            this.transform = new Transform(this, posistion);
        }

        public void LoadContent(ContentManager content)
        {
            if (!isLoaded)
            {
                foreach (Component component in components)
                {
                    if (component is ILoadable)
                    {
                        (component as ILoadable).LoadContent(content);
                    }
                }
                isLoaded = true;
            }
        }

        public void Update()
        {
            foreach (Component component in components)
            {
                if (component is IUpdateable)
                {
                    (component as IUpdateable).Update();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                if (component is IDrawable)
                {
                    (component as IDrawable).Draw(spriteBatch);
                }
            }
        }

        public Component GetComponent(string component)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().Name == component)
                {
                    return components[i];
                }
            }
            return null;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public void OnAnimationDone(string animationName)
        {
            foreach (Component component in components)
            {
                if (component is IAnimateable) //Checks if any components are IAnimateable
                {
                    //If a component is IAnimateable we call the local implementation of the method
                    (component as IAnimateable).OnAnimationDone(animationName);
                }
            }
        }
        public void OnCollisionStay(Collider other)
        {
            foreach (Component component in components)
            {
                if (component is ICollider) 
                {
                    (component as ICollider).OnCollisionStay(other);
                }
            }
        }
        public void OnCollisionEnter(Collider other)
        {
            foreach (Component component in components)
            {
                if (component is ICollider) 
                {
                    (component as ICollider).OnCollisionEnter(other);
                }
            }
        }
        public void OnCollisionExit(Collider other)
        {
            foreach (Component component in components)
            {
                if (component is ICollider)
                {
                    (component as ICollider).OnCollisionExit(other);
                }
            }
        }
    }
}