using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectSyndicus.Screens
{
    public class TestScreen : Screen
    {
        public TestScreen(SyndicusGame game) : base(game)
        {
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            game.Assets.WithTexture("floor", 
                tex => spriteBatch.Draw(tex, new Vector2(0, 0), Color.White));
        }
    }
}
