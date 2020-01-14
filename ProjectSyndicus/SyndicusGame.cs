using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nett;
using ProjectSyndicus.Screens;
using Serilog;
using System.IO;

namespace ProjectSyndicus
{
    public class SyndicusGame : Game
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;

        private Screen currentScreen;

        public Assets assets { get; private set; }
        public Config config { get; private set; }

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
            SetupLogging();

            // TODO: handle case where config file is missing
            config = Toml.ReadFile<Config>(Paths.ConfigFile);
            graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = config.ScreenWidth,
                PreferredBackBufferHeight = config.ScreenHeight,
                IsFullScreen = config.Fullscreen
            };
            graphicsDeviceManager.ApplyChanges();

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
                Log.Information("Loading has completed.");
                currentScreen = new TestScreen(this);
            };
            CurrentScreen = loadingScreen;
        }

        protected override void UnloadContent()
        {
            Log.Information("Shutting down");
            Log.CloseAndFlush();
            base.UnloadContent();
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

        private void SetupLogging()
        {
            File.Delete(Paths.LogFile);

            string outputTemplate = "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .WriteTo.File(Paths.LogFile, outputTemplate: outputTemplate, fileSizeLimitBytes: 100_000_000, rollOnFileSizeLimit: true)
                .WriteTo.Console(outputTemplate: outputTemplate)
                .CreateLogger();

            Log.Debug("Logging initialized");
        }
    }
}
