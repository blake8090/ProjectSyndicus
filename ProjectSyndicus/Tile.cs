using System;

namespace ProjectSyndicus
{
    public class Tile
    {
        public String Texture { get; private set; }
        public bool BlockMovement { get; private set; } = false;

        public Tile(string texture, bool blockMovement)
        {
            Texture = texture;
            BlockMovement = blockMovement;
        }
    }
}
