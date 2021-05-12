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
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;
        
        public Assets Assets { get; private set; }
        public Config Config { get; private set; }

        // TODO: move to separate class
        private Screen _currentScreen;
        public Screen CurrentScreen
        {
            get => _currentScreen;
            set
            {
                _currentScreen?.Stop();
                _currentScreen = value;
                _currentScreen?.Start();
            }
        }

        public SyndicusGame()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            SetupLogging();

            // TODO: handle case where config file is missing
            Config = Toml.ReadFile<Config>(Paths.ConfigFile);

            _graphicsDeviceManager.PreferredBackBufferWidth = Config.ScreenWidth;
            _graphicsDeviceManager.PreferredBackBufferHeight = Config.ScreenHeight;
            _graphicsDeviceManager.IsFullScreen = Config.Fullscreen;
            _graphicsDeviceManager.ApplyChanges();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Assets = new Assets(GraphicsDevice);
            
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Content.RootDirectory = "data";

            Assets.Load();
            CurrentScreen = new GameScreen(this);
        }

        protected override void UnloadContent()
        {
            Log.Information("Shutting down");
            Log.CloseAndFlush();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _currentScreen?.Input();
            _currentScreen?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _currentScreen?.Render(_spriteBatch);
            _spriteBatch.End();

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
