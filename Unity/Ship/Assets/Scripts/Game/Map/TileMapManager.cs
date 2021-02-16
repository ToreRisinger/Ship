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
    public Tile water_bottom;
    public Tile water_bottom_left;
    public Tile water_bottom_left_right;
    public Tile water_bottom_right;
    public Tile water_left;
    public Tile water_left_right;
    public Tile water_right;
    public Tile water_top;
    public Tile water_top_bottom;
    public Tile water_top_bottom_left;
    public Tile water_top_bottom_left_right;
    public Tile water_top_bottom_right;
    public Tile water_top_left;
    public Tile water_top_left_right;
    public Tile water_top_right;

    private Dictionary<string, Tile> grassTileMap;
    //private Dictionary<string, Tile> beachTileMap;
    private Dictionary<string, Tile> waterTileMap;

    int[,] terrainMap;

    void Awake()
    {
        //(ETerrainType top, ETerrainType right, ETerrainType bottom, ETerrainType left
        grassTileMap = new Dictionary<string, Tile>();
        grassTileMap.Add("0000", grass_top_bottom_left_right);
        grassTileMap.Add("0001", grass_bottom);
        grassTileMap.Add("0010", grass_right);
        grassTileMap.Add("0011", grass_bottom_right);
        grassTileMap.Add("0100", grass_top);
        grassTileMap.Add("0101", grass_top_bottom);
        grassTileMap.Add("0110", grass_top_right);
        grassTileMap.Add("0111", grass_top_bottom_right);
        grassTileMap.Add("1000", grass_left);
        grassTileMap.Add("1001", grass_bottom_left);
        grassTileMap.Add("1010", grass_left_right);
        grassTileMap.Add("1011", grass_bottom_left_right);
        grassTileMap.Add("1100", grass_top_left);
        grassTileMap.Add("1101", grass_top_bottom_left);
        grassTileMap.Add("1110", grass_top_left_right);
        grassTileMap.Add("1111", grass);

        //beachTileMap = new Dictionary<string, Tile>();

        waterTileMap = new Dictionary<string, Tile>();
        waterTileMap.Add("0000", water_top_bottom_left_right);
        waterTileMap.Add("0001", water_bottom);
        waterTileMap.Add("0010", water_right);
        waterTileMap.Add("0011", water_bottom_right);
        waterTileMap.Add("0100", water_top);
        waterTileMap.Add("0101", water_top_bottom);
        waterTileMap.Add("0110", water_top_right);
        waterTileMap.Add("0111", water_top_bottom_right);
        waterTileMap.Add("1000", water_left);
        waterTileMap.Add("1001", water_bottom_left);
        waterTileMap.Add("1010", water_left_right);
        waterTileMap.Add("1011", water_bottom_left_right);
        waterTileMap.Add("1100", water_top_left);
        waterTileMap.Add("1101", water_top_bottom_left);
        waterTileMap.Add("1110", water_top_left_right);
        waterTileMap.Add("1111", water);

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
                if(terrainMap[x, y] == MapGenerator.WATER)
                {
                    if(isNextToLand(x, y))
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
                    
                    string key = getTileKey(x, y, MapGenerator.WATER);
                    waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waterTileMap[key]);
                    if (isNextTo(x, y, MapGenerator.BEACH))
                    {
                        beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), beach);
                    }
                    
                }
                else if (checkTerrain(x, y, MapGenerator.GRASS))
                {
                    //waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), grass);
                    
                    string key = getTileKey(x, y, MapGenerator.GRASS);
                    grassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), grassTileMap[key]);
                    if (isNextTo(x, y, MapGenerator.BEACH))
                    {
                        beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), beach);
                    }
                    
                }
                else if (checkTerrain(x, y, MapGenerator.BEACH))
                {
                    beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), beach);
                }
            }
        }




                    /*
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
                                //Tile beachTile = beach_bottom_right_corner;
                                //beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), beachTile);
                            }

                            if (terrainMap[x + 1, y] == 1 && terrainMap[x, y + 1] == 1 && terrainMap[x + 1, y + 1] == 0)
                            {
                                //Tile beachTile = beach_bottom_left_corner;
                                //beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 1), beachTile);
                            }

                            if (terrainMap[x + 1, y] == 1 && terrainMap[x, y - 1] == 1 && terrainMap[x + 1, y - 1] == 0)
                            {
                                //Tile beachTile = beach_top_left_corner;
                                //beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 2), beachTile);
                            }

                            if (terrainMap[x, y - 1] == 1 && terrainMap[x - 1, y] == 1 && terrainMap[x - 1, y - 1] == 0)
                            {
                                //Tile beachTile = beach_top_right_corner;
                                //beachMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 3), beachTile);
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
                    */
    }

    private string getTileKey(int x, int y, int value)
    {
        //return (terrainMap[x - 1, y] == value ? 1 : 0) + "" + (terrainMap[x, y + 1] == value ? 1 : 0) + "" + (terrainMap[x + 1, y] == value ? 1 : 0) + "" + (terrainMap[x, y - 1] == value ? 1 : 0);
        return (terrainMap[x + 1, y] == value ? 1 : 0) + "" + (terrainMap[x, y - 1] == value ? 1 : 0) + "" + (terrainMap[x - 1, y] == value ? 1 : 0) + "" + (terrainMap[x, y + 1] == value ? 1 : 0);
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

    private bool checkTerrain(int x, int y, int value)
    {
        return terrainMap[x, y] == value;
    }

    private bool isNextToLand(int x, int y)
    {
        return terrainMap[x - 1, y] > MapGenerator.BEACH || terrainMap[x, y + 1] > MapGenerator.BEACH || terrainMap[x + 1, y] > MapGenerator.BEACH || terrainMap[x, y - 1] > MapGenerator.BEACH;
    }

    private bool isNextTo(int x, int y, int value)
    {
        return terrainMap[x - 1, y] == value || terrainMap[x, y + 1] == value || terrainMap[x + 1, y] == value || terrainMap[x, y - 1] == value;
    }

    void Update()
    {
        
    }
}
