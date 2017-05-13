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
    enum TerrainType { Desert, Grass, Water, Concrete, Bridge};
    class Terrain : IDrawable, ILoadable
    {
        protected Texture2D texture;
        Vector2 position;
        Color overlayColor = Color.White;
        float rotation = 0;
        Vector2 origin = new Vector2(0,0);
        float scale = 1;
        SpriteEffects effect = SpriteEffects.None;
        float layerDepth = 0;
        private Rectangle rectangle;
        protected bool isLoaded = false;
        public Rectangle Rectangle { get { return rectangle; } protected set { rectangle = value; } }
        TerrainType myType;
        public TerrainType MyType { get { return myType; } set {myType = value; } }

        public float Scale { get => scale; set => scale = value; }

        public Terrain(TerrainType type, Vector2 pos)
        {
            myType = type;
            position = pos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(isLoaded)
           spriteBatch.Draw(texture, position, rectangle, overlayColor, rotation, origin, Scale, effect, layerDepth);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("sprite_" + (int)myType);
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            isLoaded = true;
        }
    }
}
