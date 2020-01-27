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
            TileSet tileSet = game.Assets.TileSet;
            world.SetTile(0, 0, tileSet.GetTile("floor"));
            world.SetTile(0, 1, tileSet.GetTile("floor"));
            world.SetTile(1, 0, tileSet.GetTile("floor"));
            world.SetTile(1, 1, tileSet.GetTile("floor"));
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            world.Draw(game.Assets, spriteBatch);
        }
    }
}
