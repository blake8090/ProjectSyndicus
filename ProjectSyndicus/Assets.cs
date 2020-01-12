using Microsoft.Xna.Framework.Graphics;
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
                Console.WriteLine($"Duplicate texture '{assetName}' will not be loaded");
            }
            else if (File.Exists(fileName))
            {
                try
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Open))
                    {
                        Texture2D texture = Texture2D.FromStream(graphicsDevice, fileStream);
                        textures[assetName] = texture;
                    }
                }
                catch (Exception e)
                {
                    // TODO: use logger
                    Console.WriteLine($"Could not load texture '{fileName}'");
                }
            }
            // TODO: log error when file does not exist
        }

        public Texture2D GetTexture(string name)
        {
            return textures[name];
        }
    }
}
