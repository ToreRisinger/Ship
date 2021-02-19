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

        public static int[,] generateMap(int initChance, int birthLimit, int deathLimit, int width, int height, int repitions, int borderSize, List<Biome> biomes)
        {
            int[,] terrainMap = new int[width, height];
            initPos(initChance, width, height, borderSize, terrainMap, biomes);

            
            for(int i = 0; i < repitions; i++)
            {
                terrainMap = genTilePos(terrainMap, birthLimit, deathLimit, width, height, borderSize);
            }

            addBeach(terrainMap, width, height);

            return terrainMap;
        }

        private static void addBiomes(int[,] terrainMap, List<Biome> biomes, int width, int height, int borderSize)
        {
            foreach(Biome biome in biomes)
            {
                for (int x = borderSize + 1; x < width - borderSize; x++)
                {
                    for (int y = borderSize + 1; y < height - borderSize; y++)
                    {
                        Vector2 tilePos = new Vector2(x, y);
                        int distance = (int)(tilePos - biome.position).Length();
                        if(distance <= biome.radius)
                        {
                            if (terrainMap[x, y] > MapGenerator.WATER)
                            {
                                terrainMap[x, y] = (int)biome.type;
                            }
                        }
                    }
                }
            }
            
        }

        private static void addBeach(int[,] terrainMap, int width, int height)
        {
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    if (terrainMap[x, y] > MapGenerator.BEACH)
                    {
                        if (isNextTo(x, y, terrainMap, ETerrainType.WATER))
                        {
                            terrainMap[x, y] = MapGenerator.BEACH;

                            foreach (var vec in bounds)
                            {
                                terrainMap[x + (int)vec.X, y + (int)vec.Y] = MapGenerator.BEACH;
                            }
                        }
                    }
                }
            }
        }

        private static void initPos(int initChance, int width, int height, int borderSize, int[,] terrainMap, List<Biome> biomes)
        {
            Vector2 middle = new Vector2(width / 2, height / 2);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x <= borderSize || x >= width - borderSize || y <= borderSize || y >= height - borderSize)
                    {
                        terrainMap[x, y] = WATER;
                    }
                    else
                    {
                        Vector2 pos = new Vector2(x, y);
                        float distanceFromMiddle = (pos - middle).Length();
                        float maxDistance = width / 2;
                        float ratio = (distanceFromMiddle / maxDistance);
                        int chance = (int)(initChance * (1 - ratio * ratio));
                        int rand = Utilities.rand(0, 100);
                        terrainMap[x, y] = rand < chance ? (int)getTerrainType(x, y, biomes) : (int)ETerrainType.WATER;
                    }
                }
            }
        }

        private static ETerrainType getTerrainType(int x, int y, List<Biome> biomes)
        {
            foreach (Biome biome in biomes)
            {
                Vector2 tilePos = new Vector2(x, y);
                int distance = (int)(tilePos - biome.position).Length();
                if (distance <= biome.radius)
                {    
                    return biome.type;         
                }   
            }

            return ETerrainType.GRASS;
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
                        newMap[x, y] = (int)ETerrainType.WATER;
                        continue;
                    }

                    neighb = 0;
                    foreach(var vec in bounds)
                    {
                        if(x + vec.X >= 0 && x + vec.X < width && y + vec.Y >= 0 && y + vec.Y < height)
                        {
                            bool land = oldMap[x + (int)vec.X, y + (int)vec.Y] > (int)ETerrainType.WATER; 
                            neighb += land ? 1 : 0; 
                        } else
                        {
                            //neighb++; border?
                        }
                    }
                    if (oldMap[x, y] > (int)ETerrainType.WATER)
                    {
                        if(neighb < deathLimit)
                        {
                            newMap[x, y] = (int)ETerrainType.WATER;
                        } 
                        else
                        {
                            newMap[x, y] = oldMap[x, y];
                        }
                    }

                    if (oldMap[x, y] == (int)ETerrainType.WATER)
                    {
                        if (neighb > birthLimit)
                        {
                            newMap[x, y] = (int)getTerrainOfNeighbours(x, y, oldMap);
                        }
                        else
                        {
                            newMap[x, y] = (int)ETerrainType.WATER;
                        }
                    }
                }
            }
            
            return newMap;
        }

        private static bool isNextTo(int x, int y, int[,] terrainMap, ETerrainType type)
        {
            return terrainMap[x - 1, y] == (int)type
                || terrainMap[x, y + 1] == (int)type
                || terrainMap[x + 1, y] == (int)type
                || terrainMap[x, y - 1] == (int)type
                || terrainMap[x - 1, y - 1] == (int)type
                || terrainMap[x + 1, y - 1] == (int)type
                || terrainMap[x + 1, y + 1] == (int)type
                || terrainMap[x - 1, y + 1] == (int)type;
        }

        private static ETerrainType getTerrainOfNeighbours(int x, int y, int[,] terrainMap)
        {
            Dictionary<ETerrainType, int> count = new Dictionary<ETerrainType, int>();
            count.Add(ETerrainType.DESERT, 0);
            count.Add(ETerrainType.GRASS, 0);
            foreach (var vec in bounds)
            {
                ETerrainType terrain = (ETerrainType)terrainMap[x + (int)vec.X, y + (int)vec.Y];
                if(count.ContainsKey(terrain))
                {
                    count[terrain]++;
                }
            }

            ETerrainType chosenTerrain = ETerrainType.GRASS;
            int highestCount = 0;
            foreach(var entry in count)
            {
                if(entry.Value > highestCount)
                {
                    chosenTerrain = entry.Key;
                    highestCount = entry.Value;
                }
            }

            return chosenTerrain;
        }

    }
}
