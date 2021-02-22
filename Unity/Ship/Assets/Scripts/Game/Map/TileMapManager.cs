using Game.Map;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public Tilemap waterMap;
    public Tilemap beachMap;
    public Tilemap grassMap;
    public Tilemap desertMap;

    public Tilemap isometricGrassTileMap;
    public Tilemap isometricDesertTileMap;
    public Tilemap isometricBeachTileMap;
    public Tilemap isometricWaterTileMap;

    private class TileData
    {
        public Tile tile;
        public int chance;

        public TileData(Tile tile, int chance)
        {
            this.tile = tile;
            this.chance = chance;
        }
    }

    //NEW
    public Tile grass_1_tile;
    public Tile grass_2_tile;
    public Tile grass_dirt_1_tile;
    public Tile grass_dirt_2_tile;
    public Tile grass_small_stone_1_tile;
    private List<TileData> grassTiles;




    public Tile waterTile;
    public Tile beachTile;
    public Tile desertTile;

    //BEACH
    public Tile beach;

    //GRASS
    public Tile grass;
    public Tile grass_2;
    public Tile grass_0001_s;
    public Tile grass_0010_s;
    public Tile grass_0011_s;
    public Tile grass_0100_s;
    public Tile grass_0101_s;
    public Tile grass_0110_s;
    public Tile grass_0111_s;
    public Tile grass_1000_s;
    public Tile grass_1001_s;
    public Tile grass_1010_s;
    public Tile grass_1011_s;
    public Tile grass_1100_s;
    public Tile grass_1101_s;
    public Tile grass_1110_s;
    public Tile grass_1111_s;

    public Tile grass_0001_c;
    public Tile grass_0010_c;
    public Tile grass_0011_c;
    public Tile grass_0100_c;
    public Tile grass_0101_c;
    public Tile grass_0110_c;
    public Tile grass_0111_c;
    public Tile grass_1000_c;
    public Tile grass_1001_c;
    public Tile grass_1010_c;
    public Tile grass_1011_c;
    public Tile grass_1100_c;
    public Tile grass_1101_c;
    public Tile grass_1110_c;
    public Tile grass_1111_c;

    //WATER
    public Tile water;
    public Tile water_0001_s;
    public Tile water_0010_s;
    public Tile water_0011_s;
    public Tile water_0100_s;
    public Tile water_0101_s;
    public Tile water_0110_s;
    public Tile water_0111_s;
    public Tile water_1000_s;
    public Tile water_1001_s;
    public Tile water_1010_s;
    public Tile water_1011_s;
    public Tile water_1100_s;
    public Tile water_1101_s;
    public Tile water_1110_s;
    public Tile water_1111_s;

    public Tile water_0001_c;
    public Tile water_0010_c;
    public Tile water_0011_c;
    public Tile water_0100_c;
    public Tile water_0101_c;
    public Tile water_0110_c;
    public Tile water_0111_c;
    public Tile water_1000_c;
    public Tile water_1001_c;
    public Tile water_1010_c;
    public Tile water_1011_c;
    public Tile water_1100_c;
    public Tile water_1101_c;
    public Tile water_1110_c;
    public Tile water_1111_c;

    //DESERT
    public Tile desert;
    public Tile desert_0001_s;
    public Tile desert_0010_s;
    public Tile desert_0011_s;
    public Tile desert_0100_s;
    public Tile desert_0101_s;
    public Tile desert_0110_s;
    public Tile desert_0111_s;
    public Tile desert_1000_s;
    public Tile desert_1001_s;
    public Tile desert_1010_s;
    public Tile desert_1011_s;
    public Tile desert_1100_s;
    public Tile desert_1101_s;
    public Tile desert_1110_s;
    public Tile desert_1111_s;

    public Tile desert_0001_c;
    public Tile desert_0010_c;
    public Tile desert_0011_c;
    public Tile desert_0100_c;
    public Tile desert_0101_c;
    public Tile desert_0110_c;
    public Tile desert_0111_c;
    public Tile desert_1000_c;
    public Tile desert_1001_c;
    public Tile desert_1010_c;
    public Tile desert_1011_c;
    public Tile desert_1100_c;
    public Tile desert_1101_c;
    public Tile desert_1110_c;
    public Tile desert_1111_c;

    private Dictionary<string, Tile> grassSideTileMap;
    private Dictionary<string, Tile> grassCornerTileMap;

    private Dictionary<string, Tile> waterSideTileMap;
    private Dictionary<string, Tile> waterCornerTileMap;

    private Dictionary<string, Tile> desertSideTileMap;
    private Dictionary<string, Tile> desertCornerTileMap;

    int[,] terrainMap;

    void Awake()
    {
        grassTiles = new List<TileData>()
        {
            new TileData(grass_1_tile, 450),
            new TileData(grass_2_tile, 950),
            new TileData(grass_small_stone_1_tile, 980),
            new TileData(grass_dirt_1_tile, 990),
            new TileData(grass_dirt_2_tile, 1000),
        };

        grassSideTileMap = new Dictionary<string, Tile>();
        
        grassSideTileMap.Add("0001", grass_0001_s);
        grassSideTileMap.Add("0010", grass_0010_s);
        grassSideTileMap.Add("0011", grass_0011_s);
        grassSideTileMap.Add("0100", grass_0100_s);
        grassSideTileMap.Add("0101", grass_0101_s);
        grassSideTileMap.Add("0110", grass_0110_s);
        grassSideTileMap.Add("0111", grass_0111_s);
        grassSideTileMap.Add("1000", grass_1000_s);
        grassSideTileMap.Add("1001", grass_1001_s);
        grassSideTileMap.Add("1010", grass_1010_s);
        grassSideTileMap.Add("1011", grass_1011_s);
        grassSideTileMap.Add("1100", grass_1100_s);
        grassSideTileMap.Add("1101", grass_1101_s);
        grassSideTileMap.Add("1110", grass_1110_s);
        grassSideTileMap.Add("1111", grass_1111_s);

        grassCornerTileMap = new Dictionary<string, Tile>();
        grassCornerTileMap.Add("0001", grass_0001_c);
        grassCornerTileMap.Add("0010", grass_0010_c);
        //grassCornerTileMap.Add("0011", grass_0011_c);
        grassCornerTileMap.Add("0100", grass_0100_c);
        //grassCornerTileMap.Add("0101", grass_0101_c);
        //grassCornerTileMap.Add("0110", grass_0110_c);
        //grassCornerTileMap.Add("0111", grass_0111_c);
        grassCornerTileMap.Add("1000", grass_1000_c);
        //grassCornerTileMap.Add("1001", grass_1001_c);
        //grassCornerTileMap.Add("1010", grass_1010_c);
        //grassCornerTileMap.Add("1011", grass_1011_c);
        //grassCornerTileMap.Add("1100", grass_1100_c);
        //grassCornerTileMap.Add("1101", grass_1101_c);
        //grassCornerTileMap.Add("1110", grass_1110_c);
        //grassCornerTileMap.Add("1111", grass_1111_c);


        waterSideTileMap = new Dictionary<string, Tile>();
        /*
        waterSideTileMap.Add("0001", water_0001_s);
        waterSideTileMap.Add("0010", water_0010_s);
        waterSideTileMap.Add("0011", water_0011_s);
        waterSideTileMap.Add("0100", water_0100_s);
        waterSideTileMap.Add("0101", water_0101_s);
        waterSideTileMap.Add("0110", water_0110_s);
        waterSideTileMap.Add("0111", water_0111_s);
        waterSideTileMap.Add("1000", water_1000_s);
        waterSideTileMap.Add("1001", water_1001_s);
        waterSideTileMap.Add("1010", water_1010_s);
        waterSideTileMap.Add("1011", water_1011_s);
        waterSideTileMap.Add("1100", water_1100_s);
        waterSideTileMap.Add("1101", water_1101_s);
        waterSideTileMap.Add("1110", water_1110_s);
        waterSideTileMap.Add("1111", water_1111_s);
        */

        waterCornerTileMap = new Dictionary<string, Tile>();
        /*
        waterCornerTileMap.Add("0001", water_0001_c);
        waterCornerTileMap.Add("0010", water_0010_c);
        waterCornerTileMap.Add("0011", water_0011_c);
        waterCornerTileMap.Add("0100", water_0100_c);
        waterCornerTileMap.Add("0101", water_0101_c);
        waterCornerTileMap.Add("0110", water_0110_c);
        waterCornerTileMap.Add("0111", water_0111_c);
        waterCornerTileMap.Add("1000", water_1000_c);
        waterCornerTileMap.Add("1001", water_1001_c);
        waterCornerTileMap.Add("1010", water_1010_c);
        waterCornerTileMap.Add("1011", water_1011_c);
        waterCornerTileMap.Add("1100", water_1100_c);
        waterCornerTileMap.Add("1101", water_1101_c);
        waterCornerTileMap.Add("1110", water_1110_c);
        waterCornerTileMap.Add("1111", water_1111_c);
        */

        desertSideTileMap = new Dictionary<string, Tile>();
        //desertSideTileMap.Add("0001", desert_0001_s);
        //desertSideTileMap.Add("0010", desert_0010_s);
        //desertSideTileMap.Add("0011", desert_0011_s);
        //desertSideTileMap.Add("0100", desert_0100_s);
        //desertSideTileMap.Add("0101", desert_0101_s);
        //desertSideTileMap.Add("0110", desert_0110_s);
        //desertSideTileMap.Add("0111", desert_0111_s);
        //desertSideTileMap.Add("1000", desert_1000_s);
        //desertSideTileMap.Add("1001", desert_1001_s);
        //desertSideTileMap.Add("1010", desert_1010_s);
        //desertSideTileMap.Add("1011", desert_1011_s);
        //desertSideTileMap.Add("1100", desert_1100_s);
        //desertSideTileMap.Add("1101", desert_1101_s);
        //desertSideTileMap.Add("1110", desert_1110_s);
        //desertSideTileMap.Add("1111", desert_1111_s);


        desertCornerTileMap = new Dictionary<string, Tile>();
        /*
        desertCornerTileMap.Add("0001", desert_0001_c);
        desertCornerTileMap.Add("0010", desert_0010_c);
        desertCornerTileMap.Add("0011", desert_0011_c);
        desertCornerTileMap.Add("0100", desert_0100_c);
        desertCornerTileMap.Add("0101", desert_0101_c);
        desertCornerTileMap.Add("0110", desert_0110_c);
        desertCornerTileMap.Add("0111", desert_0111_c);
        desertCornerTileMap.Add("1000", desert_1000_c);
        desertCornerTileMap.Add("1001", desert_1001_c);
        desertCornerTileMap.Add("1010", desert_1010_c);
        desertCornerTileMap.Add("1011", desert_1011_c);
        desertCornerTileMap.Add("1100", desert_1100_c);
        desertCornerTileMap.Add("1101", desert_1101_c);
        desertCornerTileMap.Add("1110", desert_1110_c);
        desertCornerTileMap.Add("1111", desert_1111_c);
        */

        int width = 100;
        int height = 100;
        int initChance = 10;
        int birthLimit = 2;
        int deathLimit = 1;
        int repitions = 18;
        int borderSize = 3;

        List<Biome> biomes = new List<Biome>();
        biomes.Add(new Biome(new System.Numerics.Vector2(width / 2, height / 4), 20, ETerrainType.DESERT));

        terrainMap = MapGenerator.generateMap(initChance, birthLimit, deathLimit, width, height, repitions, borderSize, biomes);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (checkTerrain(x, y, ETerrainType.WATER))
                {
                    isometricWaterTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waterTile);

                }
                else if (checkTerrain(x, y, ETerrainType.GRASS))
                {
                    int random = Utils.Utilities.rand(1, 1000);
                    bool foundTile = false;
                    
                    for(int i = 0; i < grassTiles.Count; i++)
                    {
                        TileData tileData = grassTiles[i];
                        if (tileData.chance >= random)
                        {
                            isometricGrassTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), tileData.tile);
                            foundTile = true;
                            break;

                        }

                        //random += tileData.chance;
                    }

                    if(!foundTile)
                    {
                        isometricGrassTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), grassTiles[0].tile);
                    }
                    
                }
                else if (checkTerrain(x, y, ETerrainType.DESERT))
                {
                    isometricDesertTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), desertTile);

                    String sideKey = getSideTileKey(x, y, ETerrainType.GRASS);
                    if (grassSideTileMap.ContainsKey(sideKey))
                    {
                        isometricDesertTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 1), grassSideTileMap[sideKey]);
                    }

                    String cornerKey = mergeCornerKey(getCornerTileKey(x, y, ETerrainType.GRASS), sideKey);
                    char[] cornerKeyArray = cornerKey.ToCharArray();
                    for (int i = 0; i < cornerKeyArray.Length; i++)
                    {
                        char[] k = new char[] { '0', '0', '0', '0' };
                        if (cornerKeyArray[i] == '1')
                        {
                            k[i] = '1';
                        }
                        string key = new string(k);
                        if (grassCornerTileMap.ContainsKey(key))
                        {
                            isometricDesertTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 2 + i), grassCornerTileMap[key]);
                        }
                    }
                }
                else if (checkTerrain(x, y, ETerrainType.BEACH))
                {
                    isometricBeachTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), beachTile);

                    String sideKey = getSideTileKey(x, y, ETerrainType.GRASS);
                    if (grassSideTileMap.ContainsKey(sideKey))
                    {
                        isometricGrassTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 1), grassSideTileMap[sideKey]);
                    }

                    String cornerKey = mergeCornerKey(getCornerTileKey(x, y, ETerrainType.GRASS), sideKey);
                    char[] cornerKeyArray = cornerKey.ToCharArray();
                    for(int i = 0; i < cornerKeyArray.Length; i++)
                    {
                        char[] k = new char[] { '0', '0', '0', '0' };
                        if(cornerKeyArray[i] == '1')
                        {
                            k[i] = '1';
                        }
                        string key = new string(k);
                        if (grassCornerTileMap.ContainsKey(key))
                        {
                            isometricGrassTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 2 + 1), grassCornerTileMap[key]);
                        }
                    }

                    sideKey = getSideTileKey(x, y, ETerrainType.DESERT);
                    if (desertSideTileMap.ContainsKey(sideKey))
                    {
                        isometricDesertTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 1), desertSideTileMap[sideKey]);
                    }

                    cornerKey = mergeCornerKey(getCornerTileKey(x, y, ETerrainType.DESERT), sideKey);
                    cornerKeyArray = cornerKey.ToCharArray();
                    for (int i = 0; i < cornerKeyArray.Length; i++)
                    {
                        char[] k = new char[] { '0', '0', '0', '0' };
                        if (cornerKeyArray[i] == '1')
                        {
                            k[i] = '1';
                        }
                        string key = new string(k);
                        if (desertCornerTileMap.ContainsKey(key))
                        {
                            isometricDesertTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 2 + i), desertCornerTileMap[key]);
                        }
                    }
                }
            }
        }

        /*
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), water);
                    continue;
                }

                if (checkTerrain(x, y, ETerrainType.WATER))
                {
                    waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), water);

                }
                else if (checkTerrain(x, y, ETerrainType.GRASS))
                {
                    Tile t = Utilities.rand(0, 100) < 10 ? grass_2 : grass;
                    grassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), t);
                }
                else if(checkTerrain(x, y, ETerrainType.DESERT))
                {
                    desertMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), desert);

                    string sideKey = getSideTileKey(x, y, ETerrainType.GRASS);
                    if (grassSideTileMap.ContainsKey(sideKey))
                    {
                        grassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 1), grassSideTileMap[sideKey]);
                    }

                    string cornerKey = mergeCornerKey(getCornerTileKey(x, y, ETerrainType.GRASS), sideKey);
                    if (grassCornerTileMap.ContainsKey(cornerKey))
                    {
                        grassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), grassCornerTileMap[cornerKey]);
                    }

                }
                else if (checkTerrain(x, y, ETerrainType.BEACH))
                {
                    beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), beach);

                    string sideKey = getSideTileKey(x, y, ETerrainType.WATER);
                    if(waterSideTileMap.ContainsKey(sideKey))
                    {
                        waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 1), waterSideTileMap[sideKey]);
                    }

                    string cornerKey = getCornerTileKey(x, y, ETerrainType.WATER);
                    if (waterCornerTileMap.ContainsKey(cornerKey))
                    {
                        waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waterCornerTileMap[cornerKey]);
                    }

                    sideKey = getSideTileKey(x, y, ETerrainType.DESERT);
                    if (desertSideTileMap.ContainsKey(sideKey))
                    {
                        desertMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 1), desertSideTileMap[sideKey]);
                    }

                    cornerKey = getCornerTileKey(x, y, ETerrainType.DESERT);
                    if (desertCornerTileMap.ContainsKey(cornerKey))
                    {
                        desertMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), desertCornerTileMap[cornerKey]);
                    }

                    sideKey = getSideTileKey(x, y, ETerrainType.GRASS);
                    if (grassSideTileMap.ContainsKey(sideKey))
                    {
                        grassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 1), grassSideTileMap[sideKey]);
                    }

                    cornerKey = mergeCornerKey(getCornerTileKey(x, y, ETerrainType.GRASS), sideKey);
                    if (grassCornerTileMap.ContainsKey(cornerKey))
                    {
                        grassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), grassCornerTileMap[cornerKey]);
                    }


                }

            }
        }
        */
    }

    private string mergeCornerKey(string cornerKey, string sideKey)
    {
        char[] corner = cornerKey.ToCharArray();
        char[] sides = sideKey.ToCharArray();
        if(sides[0] == '1')
        {
            corner[0] = '0';
            corner[1] = '0';
        }
        if (sideKey[1] == '1')
        {
            corner[1] = '0';
            corner[2] = '0';
        }
        if (sideKey[2] == '1')
        {
            corner[2] = '0';
            corner[3] = '0';
        }
        if (sideKey[3] == '1')
        {
            corner[3] = '0';
            corner[0] = '0';
        }
        return new string(corner);
    }

    private string getSideTileKey(int x, int y, ETerrainType type)
    {
        return (terrainMap[x + 1, y] == (int)type ? 1 : 0) + ""
            + (terrainMap[x, y - 1] == (int)type ? 1 : 0) + "" 
            + (terrainMap[x - 1, y] == (int)type ? 1 : 0) + ""
            + (terrainMap[x, y + 1] == (int)type ? 1 : 0);
    }

    private string getCornerTileKey(int x, int y, ETerrainType type)
    {
        return (terrainMap[x + 1, y + 1] == (int)type ? 1 : 0) + "" 
            + (terrainMap[x + 1, y - 1] == (int)type ? 1 : 0) + "" 
            + (terrainMap[x - 1, y - 1] == (int)type ? 1 : 0) + "" 
            + (terrainMap[x - 1, y + 1] == (int)type ? 1 : 0);
    }

    private bool checkTerrain(int x, int y, ETerrainType type)
    {
        return terrainMap[x, y] == (int)type;
    }

    private bool isNextTo(int x, int y, int value)
    {
        return terrainMap[x - 1, y] == value || terrainMap[x, y + 1] == value || terrainMap[x + 1, y] == value || terrainMap[x, y - 1] == value;
    }

    void Update()
    {
        
    }
}
