using Game.Map;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{

    public Tilemap beachMap;
    public Tilemap grassMap;
    public Tilemap waterMap;

    //BEACH
    public Tile beach;

    //GRASS
    public Tile grass;
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

    private Dictionary<string, Tile> grassSideTileMap;
    private Dictionary<string, Tile> grassCornerTileMap;

    private Dictionary<string, Tile> waterSideTileMap;
    private Dictionary<string, Tile> waterCornerTileMap;

    int[,] terrainMap;

    void Awake()
    {
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
        grassCornerTileMap.Add("0011", grass_0011_c);
        grassCornerTileMap.Add("0100", grass_0100_c);
        grassCornerTileMap.Add("0101", grass_0101_c);
        grassCornerTileMap.Add("0110", grass_0110_c);
        grassCornerTileMap.Add("0111", grass_0111_c);
        grassCornerTileMap.Add("1000", grass_1000_c);
        grassCornerTileMap.Add("1001", grass_1001_c);
        grassCornerTileMap.Add("1010", grass_1010_c);
        grassCornerTileMap.Add("1011", grass_1011_c);
        grassCornerTileMap.Add("1100", grass_1100_c);
        grassCornerTileMap.Add("1101", grass_1101_c);
        grassCornerTileMap.Add("1110", grass_1110_c);
        grassCornerTileMap.Add("1111", grass_1111_c);


        waterSideTileMap = new Dictionary<string, Tile>();
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

        waterCornerTileMap = new Dictionary<string, Tile>();
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

        int width = 100;
        int height = 100;
        int initChance = 10;
        int birthLimit = 2;
        int deathLimit = 1;
        int repitions = 3;
        int borderSize = 3;

        terrainMap = MapGenerator.generateMap(initChance, birthLimit, deathLimit, width, height, repitions, borderSize);

        //Change water close to land into beach
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                if(terrainMap[x, y] == MapGenerator.GRASS)
                {
                    if(isNextToWater(x, y))
                    {
                        terrainMap[x, y] = MapGenerator.BEACH;
                    }
                }
            }

        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), water);
                    continue;
                }

                if (checkTerrain(x, y, MapGenerator.WATER))
                {
                    waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), water);

                }
                else if (checkTerrain(x, y, MapGenerator.GRASS))
                {
                    grassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), grass);

                }
                else if (checkTerrain(x, y, MapGenerator.BEACH))
                {
                    beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), beach);

                    string key = getSideTileKey(x, y, MapGenerator.WATER);
                    if(waterSideTileMap.ContainsKey(key))
                    {
                        waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 1), waterSideTileMap[key]);
                    }

                    key = getCornerTileKey(x, y, MapGenerator.WATER);
                    if (waterCornerTileMap.ContainsKey(key))
                    {
                        waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waterCornerTileMap[key]);
                    }

                    key = getSideTileKey(x, y, MapGenerator.GRASS);
                    if (waterSideTileMap.ContainsKey(key))
                    {
                        grassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 1), grassSideTileMap[key]);
                    }

                    key = getCornerTileKey(x, y, MapGenerator.GRASS);
                    if (waterCornerTileMap.ContainsKey(key))
                    {
                        grassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), grassCornerTileMap[key]);
                    }

                }
                
            }
        }
    }

    private string getSideTileKey(int x, int y, int value)
    {
        return (terrainMap[x + 1, y] == value ? 1 : 0) + ""
            + (terrainMap[x, y - 1] == value ? 1 : 0) + "" 
            + (terrainMap[x - 1, y] == value ? 1 : 0) + ""
            + (terrainMap[x, y + 1] == value ? 1 : 0);
    }

    private string getCornerTileKey(int x, int y, int value)
    {
        return (terrainMap[x + 1, y + 1] == value ? 1 : 0) + "" 
            + (terrainMap[x + 1, y - 1] == value ? 1 : 0) + "" 
            + (terrainMap[x - 1, y - 1] == value ? 1 : 0) + "" 
            + (terrainMap[x - 1, y + 1] == value ? 1 : 0);
    }

    private bool checkTerrain(int x, int y, int value)
    {
        return terrainMap[x, y] == value;
    }

    private bool isNextToLand(int x, int y)
    {
        return terrainMap[x - 1, y] > MapGenerator.BEACH || terrainMap[x, y + 1] > MapGenerator.BEACH || terrainMap[x + 1, y] > MapGenerator.BEACH || terrainMap[x, y - 1] > MapGenerator.BEACH;
    }

    private bool isNextToWater(int x, int y)
    {
        return terrainMap[x - 1, y] == MapGenerator.WATER 
            || terrainMap[x, y + 1] == MapGenerator.WATER 
            || terrainMap[x + 1, y] == MapGenerator.WATER 
            || terrainMap[x, y - 1] == MapGenerator.WATER
            || terrainMap[x - 1, y - 1] == MapGenerator.WATER
            || terrainMap[x + 1, y - 1] == MapGenerator.WATER
            || terrainMap[x + 1, y + 1] == MapGenerator.WATER
            || terrainMap[x - 1, y + 1] == MapGenerator.WATER;
    }

    private bool isNextTo(int x, int y, int value)
    {
        return terrainMap[x - 1, y] == value || terrainMap[x, y + 1] == value || terrainMap[x + 1, y] == value || terrainMap[x, y - 1] == value;
    }

    void Update()
    {
        
    }
}
