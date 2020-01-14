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

        public void LoadTexture(string fileName)
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

        public Texture2D GetTexture(string name)
        {
            return textures[name];
        }
    }
}
