using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSyndicus.Screens;
using System;

namespace ProjectSyndicus
{
    public class SyndicusGame : Game
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;

        private Screen currentScreen;

        public Assets assets;

        public Screen CurrentScreen
        {
            get => currentScreen;
            set
            {
                currentScreen?.Stop();
                currentScreen = value;
                currentScreen?.Start();
            }
        }

        public SyndicusGame()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);

            // TODO: load config

            Content.RootDirectory = "Data";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            assets = new Assets(GraphicsDevice);

            // Loads all assets up front
            LoadingScreen loadingScreen = new LoadingScreen(this);
            loadingScreen.OnLoadingComplete += () =>
            {
                Console.WriteLine("Loading has completed.");
                currentScreen = new TestScreen(this);
            };
            CurrentScreen = loadingScreen;
        }

        protected override void Update(GameTime gameTime)
        {
            currentScreen?.Input();
            currentScreen?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            currentScreen?.Render(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
