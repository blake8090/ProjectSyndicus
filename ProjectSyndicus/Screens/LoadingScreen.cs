using Microsoft.Xna.Framework;
using Serilog;
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
            Log.Debug("Loading screen start");
            new Thread(() =>
            {
                LoadAllTextures();
                OnLoadingComplete();
            }).Start();
        }

        private void LoadAllTextures()
        {
            List<string> texFiles = GetAllFilesWithExtensions(Paths.GfxPath, "*.png", "*.bmp");
            Log.Debug($"found {texFiles.Count} textures to load.");

            foreach (var file in texFiles)
            {
                game.Assets.LoadTexture(file);
            }
        }

        // TODO: fix this up to automatically include wildcards
        private List<string> GetAllFilesWithExtensions(string path, params string[] extensions)
        {
            List<string> files = new List<string>();
            foreach (var extension in extensions)
            {
                files.AddRange(Directory.GetFiles(Paths.GfxPath, extension, SearchOption.AllDirectories));
            }
            return files;
        }
    }
}
