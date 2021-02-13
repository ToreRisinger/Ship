
using System;
using System.Collections.Generic;
using System.Numerics;
using Utils;

namespace Server.Game.Model
{
    public class Map
    {
        //Bottom left corner
        private Vector2 mapPosition;
        private List<List<Sector>> sectors;
        private int mapSize;
        private int sectorSize = 50;

        public Map(int mapSize)
        {
            mapPosition = new Vector2(0, 0);
            this.mapSize = mapSize;

            createMap();
        }

        private void createMap()
        {
            createSectors();
            generateMap();
        }

        private void createSectors()
        {
            sectors = new List<List<Sector>>();
            for (int x = 0; x < mapSize; x++)
            {
                sectors.Add(new List<Sector>());
                for (int y = 0; y < mapSize; y++)
                {
                    sectors[x].Add(new Sector());
                }
            }
        }

        private void generateMap()
        {
            Vector2 forestPosition = new Vector2(50, 50);
            int forestRadius = 20;
            int numberOfTrees = 10;

            for(int i = 0; i < numberOfTrees; i++)
            {
                float randomRadians = ((float)Utilities.rand(0, 2 * (int)(Math.PI * 100.0f))) / 100.0f;
                float distance = ((float)Utilities.rand(0, forestRadius * 100)) / 100.0f;

                float x = (float)Math.Cos(randomRadians);
                float y = (float)Math.Sin(randomRadians);
                Vector2 treePosition = new Vector2(x, y) * distance;
                Sector sector = getSectorAt(treePosition);
                treePosition += forestPosition + mapPosition;
                Tree tree = new Tree(treePosition);

                sector.addGameObject(tree);
            }
        }

        private Sector getSectorAt(Vector2 position)
        {
            return sectors[(int)(position.X / (float)sectorSize)][(int)(position.Y / (float)sectorSize)];
        }
    }
}
