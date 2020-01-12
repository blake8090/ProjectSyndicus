using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectSyndicus.Screens
{
    public abstract class Screen
    {
        protected readonly SyndicusGame game;

        protected Screen(SyndicusGame game)
        {
            this.game = game;
        }

        public virtual void Start() { }
        public virtual void Stop() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Input() { }
        public virtual void Render(SpriteBatch spriteBatch) { }
    }
}
