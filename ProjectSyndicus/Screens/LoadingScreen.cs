using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            Console.WriteLine("Loading screen start");
            new Thread(() =>
            {
                LoadAllTextures();
                OnLoadingComplete();
            }).Start();
        }

        private void LoadAllTextures()
        {
            string[] texFiles = Directory.GetFiles(Paths.GfxPath, "*.png",
                         SearchOption.TopDirectoryOnly);
            Console.WriteLine($"found {texFiles.Length} textures to load.");

            foreach (var file in texFiles)
            {
                game.assets.LoadTexture(file);
            }
        }
    }
}
