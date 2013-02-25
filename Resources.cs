using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SFML.Graphics;
using SFML.Audio;

namespace OpenEmpires
{
    public static class Resources
    {
        private static readonly Dictionary<string, Texture> Textures = new Dictionary<string, Texture>();
        public static Texture LoadTexture(string name)
        {
            Texture texture;

            if (Textures.TryGetValue(name, out texture))
                return texture;

            texture = new Texture(Path.Combine(Configuration.TextureLocation, name));
            Textures.Add(name, texture);

            return texture;
        }
    }
}