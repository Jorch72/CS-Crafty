using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Craft.Net.Client;
using Ionic.Zip;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Crafty
{
    public static class Textures
    {
        // TODO: Put in a different class
        public static Bitmap Terrain { get; set; }
        public static int TerrainId { get; set; }

        public static void LoadTerrainTextures()
        {
            // TODO: This only works on 1.4.7 and earlier
            if (File.Exists("terrain.png"))
                Terrain = (Bitmap)Image.FromFile("terrain.png");
            else
            {
                if (File.Exists(Path.Combine(DotMinecraft.GetDotMinecraftPath(), "bin", "minecraft.jar")))
                {
                    using (var file = new ZipFile(Path.Combine(DotMinecraft.GetDotMinecraftPath(), "bin", "minecraft.jar")))
                    {
                        if (file.ContainsEntry("terrain.png"))
                        {
                            var ms = new MemoryStream();
                            file["terrain.png"].Extract(ms);
                            ms.Seek(0, SeekOrigin.Begin);
                            Terrain = (Bitmap)Image.FromStream(ms);
                        }
                        else
                            throw new FileNotFoundException("Missing terrain.png!");
                    }
                }
                else
                    throw new FileNotFoundException("Missing terrain.png!");
            }
            // Load into OpenGL
            TerrainId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TerrainId);
            var data = Terrain.LockBits(new Rectangle(0, 0, Terrain.Width, Terrain.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height,
                0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            Terrain.UnlockBits(data);
            // Disable mipmaps?
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
    }
}
