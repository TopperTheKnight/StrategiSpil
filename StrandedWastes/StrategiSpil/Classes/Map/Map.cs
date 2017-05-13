using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace StrategiSpil
{
    class Map : IDrawable, ILoadable
    {
        private static Map instance = null;
        private Terrain[,] terrainArray;
        private bool isTerrainGenerated = false;
        public Map()
        {

        }


        public static Map Instance {
            get {
                if (instance == null) {
                    instance = new Map();
                    }
                return instance;
            }
        }

        public void GenerateMap(int[,] mapTiles, float scale)
        {
            terrainArray = new Terrain[mapTiles.GetLength(0),mapTiles.GetLength(1)];
            for (int x = 0; x < mapTiles.GetLength(1); x++)
            {
                for (int y = 0; y < mapTiles.GetLength(0); y++)
                {
                    switch (mapTiles[y, x]) {
                        case 0:
                            terrainArray[y, x] = new Terrain(TerrainType.Desert, new Vector2((x * 64 * scale), (y * 64 * scale)));
                            break;
                        case 1:
                            terrainArray[y, x] = new Terrain(TerrainType.Grass, new Vector2((x * 64 * scale), (y * 64 * scale)));
                            break;
                        case 2:
                            terrainArray[y, x] = new Terrain(TerrainType.Water, new Vector2((x * 64 * scale), (y * 64 * scale)));
                            break;
                        case 3:
                            terrainArray[y, x] = new Terrain(TerrainType.Concrete, new Vector2((x * 64 * scale), (y * 64 * scale)));
                            break;
                        case 4:
                            terrainArray[y, x] = new Terrain(TerrainType.Bridge, new Vector2((x * 64 * scale), (y * 64 * scale)));
                            break;
                    }
                   
                }
            }
            isTerrainGenerated = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < terrainArray.GetLength(0); x++)
            {
                for (int y = 0; y < terrainArray.GetLength(1); y++)
                {
                    if (terrainArray[x, y] != null)
                        terrainArray[x, y].Draw(spriteBatch);
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            for (int x = 0; x < terrainArray.GetLength(0); x++)
            {
                for (int y = 0; y < terrainArray.GetLength(1); y++)
                {
                    if (terrainArray[x, y] != null)
                        terrainArray[x, y].LoadContent(content); ;
                }
            }
        }
    }
}
