using Serilog;
using System.Threading;

namespace ProjectSyndicus.Screens
{
    public class LoadingScreen : Screen
    {
        public delegate void LoadingCompleteEvent();
        public event LoadingCompleteEvent OnLoadingComplete;

        public LoadingScreen(SyndicusGame game) : base(game)
        {
        }

        public override void Start()
        {
            Log.Debug("Loading screen start");
            new Thread(() =>
            {
                game.Assets.Load();
                OnLoadingComplete();
            }).Start();
        }
    }
}
