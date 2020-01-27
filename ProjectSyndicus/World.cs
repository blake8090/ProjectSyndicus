using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ProjectSyndicus
{
    public class World
    {
        private readonly Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
        // TODO: dictionary of entities

        public void SetTile(int x, int y, Tile tile)
        {
            tiles.Add(new Point(x, y), tile);
        }

        public void Draw(Assets assets, SpriteBatch spriteBatch)
        {
            foreach (var entry in tiles)
            {
                assets.WithTexture(entry.Value.Texture, tex =>
                {
                    var pos = entry.Key * new Point(tex.Width, tex.Height);
                    spriteBatch.Draw(tex, pos.ToVector2(), Color.White);
                });
            }
        }
    }
}
