
using System.Collections.Generic;
using System.Numerics;
using Utils;

namespace Game.Map
{
    public class MapGenerator
    {
        public static int WATER = 0;
        public static int BEACH = 1;
        public static int GRASS = 2;

        private static List<Vector2> bounds = new List<Vector2>()
        {
            new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1),
            new Vector2(-1, 0),                    new Vector2(1, 0),
            new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1),
        };

        public static int[,] generateMap(int initChance, int birthLimit, int deathLimit, int width, int height, int repitions, int borderSize)
        {
            int[,] terrainMap = new int[width, height];
            initPos(initChance, width, height, terrainMap);

            
            for(int i = 0; i < repitions; i++)
            {
                terrainMap = genTilePos(terrainMap, birthLimit, deathLimit, width, height, borderSize);
            }
            

            return terrainMap;
        }

        private static void initPos(int initChance, int width, int height, int[,] terrainMap)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int rand = Utilities.rand(0, 100);
                    terrainMap[x, y] = rand < initChance ? GRASS : WATER;
                }
            }
        }

        private static int[,] genTilePos(int[,] oldMap, int birthLimit, int deathLimit, int width, int height, int borderSize)
        {
            int[,] newMap = new int[width, height];
            int neighb;
            

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(x <= borderSize || x >= width - borderSize || y <= borderSize || y >= height - borderSize)
                    {
                        newMap[x, y] = WATER;
                        continue;
                    }

                    neighb = 0;
                    foreach(var vec in bounds)
                    {
                        if(x + vec.X >= 0 && x + vec.X < width && y + vec.Y >= 0 && y + vec.Y < height)
                        {
                            neighb += oldMap[x + (int)vec.X, y + (int)vec.Y]; 
                        } else
                        {
                            //neighb++; border?
                        }
                    }
                    if (oldMap[x, y] == GRASS)
                    {
                        if(neighb < deathLimit)
                        {
                            newMap[x, y] = WATER;
                        } 
                        else
                        {
                            newMap[x, y] = GRASS;
                        }
                    }

                    if (oldMap[x, y] == WATER)
                    {
                        if (neighb > birthLimit)
                        {
                            newMap[x, y] = GRASS;
                        }
                        else
                        {
                            newMap[x, y] = WATER;
                        }
                    }
                }
            }
            
            return newMap;
        }

    }

   
}
