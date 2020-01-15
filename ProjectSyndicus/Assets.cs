using Microsoft.Xna.Framework.Graphics;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectSyndicus
{
    public class Assets
    {
        private GraphicsDevice graphicsDevice;
        private readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public Assets(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public Texture2D GetTexture(string name)
        {
            return textures[name];
        }

        public void WithTexture(string name, Action<Texture2D> func)
        {
            var texture = GetTexture(name);
            if (texture != null)
            {
                func.Invoke(texture);
            }
        }

        public void Load()
        {
            LoadAllTextures();
        }
        
        private void LoadAllTextures()
        {
            List<string> texFiles = GetAllFilesWithExtensions(Paths.GfxPath, "*.png", "*.bmp");
            Log.Debug($"found {texFiles.Count} textures to load.");

            foreach (var file in texFiles)
            {
                LoadTexture(file);
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
        
        private void LoadTexture(string fileName)
        {
            string assetName = Path.GetFileNameWithoutExtension(fileName);
            if (textures.ContainsKey(assetName))
            {
                Log.Debug($"Duplicate texture '{assetName}' will not be loaded");
            }
            else if (!File.Exists(fileName))
            {
                Log.Debug($"Cannot load texture: file '{fileName}' does not exist");
            }
            else
            {
                try
                {
                    using var fileStream = new FileStream(fileName, FileMode.Open);
                    textures[assetName] = Texture2D.FromStream(graphicsDevice, fileStream);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Could not load texture '{fileName}'");
                }
            }
        }
    }
}
