using Game.Map;
using Ship.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{

    public Tilemap beachMap;
    public Tilemap grassMap;
    public Tilemap waterMap;

    //BEACH
    public Tile beach_bottom;
    public Tile beach_bottom_left;
    public Tile beach_bottom_left_right;
    public Tile beach_bottom_right;
    public Tile beach_left;
    public Tile beach_left_right;
    public Tile beach_right;
    public Tile beach_top;
    public Tile beach_top_bottom;
    public Tile beach_top_bottom_left;
    public Tile beach_top_bottom_right;
    public Tile beach_top_left;
    public Tile beach_top_left_right;
    public Tile beach_top_right;

    public Tile beach_top_right_corner;
    public Tile beach_top_left_corner;
    public Tile beach_bottom_right_corner;
    public Tile beach_bottom_left_corner;

    //GRASS
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

    //WATER
    public Tile water;

    private Dictionary<string, Tile> grassTileMap;
    private Dictionary<string, Tile> beachTileMap;
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

        beachTileMap = new Dictionary<string, Tile>();
        //beachTileMap.Add("1111", grass);
        beachTileMap.Add("1101", beach_bottom);
        beachTileMap.Add("1100", beach_bottom_left);
        beachTileMap.Add("1000", beach_bottom_left_right);
        beachTileMap.Add("1001", beach_bottom_right);
        beachTileMap.Add("1110", beach_left);
        beachTileMap.Add("1010", beach_left_right);
        beachTileMap.Add("1011", beach_right);
        beachTileMap.Add("0111", beach_top);
        beachTileMap.Add("0101", beach_top_bottom);
        beachTileMap.Add("0100", beach_top_bottom_left);
        //beachTileMap.Add("0000", grass_top_bottom_left_right);
        beachTileMap.Add("0001", beach_top_bottom_right);
        beachTileMap.Add("0110", beach_top_left);
        beachTileMap.Add("0010", beach_top_left_right);
        beachTileMap.Add("0011", beach_top_right);

        /*
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
        */

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
                //grass tile
                if (terrainMap[x, y] == 1)
                {
                    //grass tile
                    if(x > borderSize && x < width - borderSize && y > borderSize && y < height - borderSize)
                    {
                        string key = getTileKey(terrainMap[x, y - 1], terrainMap[x - 1, y], terrainMap[x, y + 1], terrainMap[x + 1, y]);
                        Tile grassTile = grassTileMap[key];
                        grassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), grassTile);

                        //beach tile
                        if(beachTileMap.ContainsKey(key))
                        {
                            Tile beachTile = beachTileMap[key];
                            beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), beachTile);
                        }

                        if(terrainMap[x - 1, y] == 1 && terrainMap[x, y + 1] == 1 && terrainMap[x - 1, y + 1] == 0)
                        {
                            Tile beachTile = beach_bottom_right_corner;
                            beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), beachTile);
                        }

                        if (terrainMap[x + 1, y] == 1 && terrainMap[x, y + 1] == 1 && terrainMap[x + 1, y + 1] == 0)
                        {
                            Tile beachTile = beach_bottom_left_corner;
                            beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 1), beachTile);
                        }

                        if (terrainMap[x + 1, y] == 1 && terrainMap[x, y - 1] == 1 && terrainMap[x + 1, y - 1] == 0)
                        {
                            Tile beachTile = beach_top_left_corner;
                            beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 2), beachTile);
                        }

                        if (terrainMap[x, y - 1] == 1 && terrainMap[x - 1, y] == 1 && terrainMap[x - 1, y - 1] == 0)
                        {
                            Tile beachTile = beach_top_right_corner;
                            beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 3), beachTile);
                        }
                    }

                    //water background
                    bool grassHasWaterNeighbour = terrainMap[x, y - 1] == 0 || terrainMap[x - 1, y] == 0 || terrainMap[x, y + 1] == 0 || terrainMap[x + 1, y] == 0;
                    if (grassHasWaterNeighbour)
                    {
                        waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), water);
                    }
                }
                //water
                else
                {
                    waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), water);
                }
            }
        }
    }

    private string getTileKey(int top, int right, int bottom, int left)
    {
        return top + "" + right + "" + bottom + "" + left;
    }

    /*
    private Tile getWaterTile(int top, int right, int bottom, int left)
    {
        string key = top + "" + right + "" + bottom + "" + left;
        return waterTileMap[key];
    }
    */

    /*
    if (x > borderSize && x < width - borderSize - 1 && y > borderSize && y < height - borderSize - 1)
    {
        Tile tile = getWaterTile(terrainMap[x, y - 1], terrainMap[x - 1, y], terrainMap[x, y + 1], terrainMap[x + 1, y]);
        waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), tile);
    } 
    else
    {
        waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), water);
    }
    */

    void Update()
    {
        
    }
}
