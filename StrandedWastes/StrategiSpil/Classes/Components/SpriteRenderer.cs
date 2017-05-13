using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace StrategiSpil
{
    public class SpriteRenderer : Component, ILoadable, IDrawable
    {
        private Rectangle rectangle;
        private Texture2D sprite;
        private string spriteName;
        private float layerDepth;
        private float scale;
        private Vector2 offset;
        private Color overlayColor;
        private float rotation;
        private Vector2 origin;
        private SpriteEffects effect;
        public Rectangle Rectangle { get {return rectangle; } set { rectangle = value; } }
        public Texture2D Sprite { get { return sprite; } }
        public Vector2 Offset { get {return offset; } set { offset = value; } }
        public float Scale { get {return scale; }}
        public Color OverlayColor { set { overlayColor = value; } }

        public SpriteRenderer(GameObject gameObject, string spriteName, float layerDepth, float scale, Color color, float rotation, SpriteEffects effect) : base(gameObject) 
        {
            this.spriteName = spriteName;
            this.layerDepth = layerDepth;
            this.scale = scale;
            this.overlayColor = color;
            this.rotation = rotation;
            this.effect = effect;
            
        }

        public void LoadContent(ContentManager content)
        {
            
            sprite = content.Load<Texture2D>(spriteName);
            if (base.GameObject.GetComponent("Animator") == null)
             rectangle = new Rectangle(0,0 + (int)offset.Y,sprite.Width,sprite.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!(GameObject.Transform.Position.X < 0-GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Right) || 
                !(GameObject.Transform.Position.X > GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Width) ||
                !(GameObject.Transform.Position.Y > GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height) ||
                !(GameObject.Transform.Position.Y > 0-GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height))
            spriteBatch.Draw(sprite,GameObject.Transform.Position,rectangle,overlayColor,rotation,origin,scale,effect,layerDepth);
        }
    }
}