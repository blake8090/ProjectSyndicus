using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectSyndicus
{
    public class TileSet
    {
        private readonly List<Tile> tiles = new List<Tile>();
        private readonly Dictionary<string, Tile> tilesByName = new Dictionary<string, Tile>();
        private readonly Tile defaultTile = new Tile();

        public TileSet()
        {
            LoadDefaultTileSet();
        }

        public Tile GetTile(int id)
        {
            if (id < 0 || id > tiles.Count)
            {
                return defaultTile;
            }
            return tiles[id];
        }

        public Tile GetTile(string name)
        {
            if (tilesByName.ContainsKey(name))
            {
                return tilesByName[name];
            }
            else
            {
                return defaultTile;
            }
        }

        private void LoadDefaultTileSet()
        {
            tiles.Clear();
            tilesByName.Clear();

            AddTile("floor", new Tile("floor", false));
            AddTile("wall", new Tile("wall", true));
        }

        private void AddTile(string name, Tile tile)
        {
            tilesByName[name] = tile;
            tiles.Add(tile);
        }
    }
}
