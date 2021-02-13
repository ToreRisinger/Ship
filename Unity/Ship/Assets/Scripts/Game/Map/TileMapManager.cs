using Game.Map;
using Ship.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    enum ETerrainType
    {
        WATER = 0,
        GRASS
    }

    public Tilemap grassMap;
    public Tilemap waterMap;

    public Tile grass;
    public Tile grass_bottom;
    public Tile grass_bottom_left;
    public Tile grass_bottom_left_right;
    public Tile grass_bottom_right;
    public Tile grass_left;
    public Tile grass_left_right;
    public Tile grass_right;
    public Tile grass_top;
    public Tile grass_top_bottom;
    public Tile grass_top_bottom_left;
    public Tile grass_top_bottom_left_right;
    public Tile grass_top_bottom_right;
    public Tile grass_top_left;
    public Tile grass_top_left_right;
    public Tile grass_top_right;

    public Tile water;
    public Tile water_top;

    private Dictionary<string, Tile> grassTileMap;
    private Dictionary<string, Tile> waterTileMap;

    void Awake()
    {
        //(ETerrainType top, ETerrainType right, ETerrainType bottom, ETerrainType left
        grassTileMap = new Dictionary<string, Tile>();
        grassTileMap.Add("1111", grass);
        grassTileMap.Add("1101", grass_bottom);
        grassTileMap.Add("1100", grass_bottom_left);
        grassTileMap.Add("1000", grass_bottom_left_right);
        grassTileMap.Add("1001", grass_bottom_right);
        grassTileMap.Add("1110", grass_left);
        grassTileMap.Add("1010", grass_left_right);
        grassTileMap.Add("1011", grass_right);
        grassTileMap.Add("0111", grass_top);
        grassTileMap.Add("0101", grass_top_bottom);
        grassTileMap.Add("0100", grass_top_bottom_left);
        grassTileMap.Add("0000", grass_top_bottom_left_right);
        grassTileMap.Add("0001", grass_top_bottom_right);
        grassTileMap.Add("0110", grass_top_left);
        grassTileMap.Add("0010", grass_top_left_right);
        grassTileMap.Add("0011", grass_top_right);

        waterTileMap = new Dictionary<string, Tile>();
        waterTileMap.Add("1111", water_top);
        waterTileMap.Add("1101", water_top);
        waterTileMap.Add("1100", water_top);
        waterTileMap.Add("1000", water_top);
        waterTileMap.Add("1001", water_top);
        waterTileMap.Add("1110", water_top);
        waterTileMap.Add("1010", water_top);
        waterTileMap.Add("1011", water_top);
        waterTileMap.Add("0111", water);
        waterTileMap.Add("0101", water);
        waterTileMap.Add("0100", water);
        waterTileMap.Add("0000", water);
        waterTileMap.Add("0001", water);
        waterTileMap.Add("0110", water);
        waterTileMap.Add("0010", water);
        waterTileMap.Add("0011", water);

        int width = 100;
        int height = 100;
        int initChance = 10;
        int birthLimit = 2;
        int deathLimit = 1;
        int repitions = 10;
        int borderSize = 3;

        int[,] terrainMap = MapGenerator.generateMap(initChance, birthLimit, deathLimit, width, height, repitions, borderSize);
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x, y] == 1)
                {
                    if(x > borderSize && x < width - borderSize && y > borderSize && y < height - borderSize)
                    {
                        Tile tile = getGrassTile(terrainMap[x, y - 1], terrainMap[x - 1, y], terrainMap[x, y + 1], terrainMap[x + 1, y]);
                        grassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), tile);
                    }
                }
                else
                {
                    if (x > borderSize && x < width - borderSize && y > borderSize && y < height - borderSize)
                    {
                        Tile tile = getWaterTile(terrainMap[x, y - 1], terrainMap[x - 1, y], terrainMap[x, y + 1], terrainMap[x + 1, y]);
                        waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), tile);
                    } 
                    else
                    {
                        waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), water);
                    }
                }
            }
        }
    }

    private Tile getGrassTile(int top, int right, int bottom, int left)
    {
        string key = top + "" + right + "" + bottom + "" + left;
        return grassTileMap[key];
    }

    private Tile getWaterTile(int top, int right, int bottom, int left)
    {
        string key = top + "" + right + "" + bottom + "" + left;
        return waterTileMap[key];
    }

    void Update()
    {
        
    }
}
