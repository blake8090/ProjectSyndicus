using Microsoft.Xna.Framework.Graphics;

namespace ProjectSyndicus.Screens
{
    public class GameScreen : Screen
    {
        private World world = new World();

        public GameScreen(SyndicusGame game) : base(game)
        {
        }

        public override void Start()
        {
            // TODO: load tile definitions from file using Assets
            var tile = new Tile("floor", false);
            world.SetTile(0, 0, tile);
            world.SetTile(0, 1, tile);
            world.SetTile(1, 0, tile);
            world.SetTile(1, 1, tile);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            world.Draw(game.Assets, spriteBatch);
        }
    }
}
