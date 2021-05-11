using System;

namespace ProjectSyndicus
{
    public class Tile
    {
        public String Texture { get; private set; }
        public bool BlockMovement { get; private set; }

        public Tile(string texture = "", bool blockMovement = false)
        {
            Texture = texture;
            BlockMovement = blockMovement;
        }
    }
}
