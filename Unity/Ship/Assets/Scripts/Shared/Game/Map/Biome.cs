
using System.Numerics;

namespace Game.Map
{
    public class Biome
    {
        public Vector2 position;
        public int radius;
        public ETerrainType type;

        public Biome(Vector2 position, int radius, ETerrainType type)
        {
            this.position = position;
            this.radius = radius;
            this.type = type;
        }
    }
}
