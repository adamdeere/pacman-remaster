using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using OpenGL_Game.Objects;
using System.IO;
using System.Linq;

namespace OpenGL_Game.Managers
{
    static class ResourceManager
    {
        static Dictionary<string, Geometry> geometryDictionary = new Dictionary<string, Geometry>();
        static Dictionary<string, Texture> textureDictionary = new Dictionary<string, Texture>();

        public static void LoadGeometry(string tag, string filename)
        {
            Geometry geometry;
            geometryDictionary.TryGetValue(filename, out geometry);
            if (geometry == null)
            {
                geometry = new Geometry(tag);
                geometry.LoadObject(filename);
                geometryDictionary.Add(tag, geometry);
            }
        }

       
        public static void LoadTexture(string tag, string filepath)
        {
            Texture tex = new Texture(filepath)
            {
                TextureTag = tag
            };

            if (File.Exists(filepath))
            {

                tex.TextureBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                tex.TextureData = tex.TextureBitmap.LockBits(
               new Rectangle(0, 0, tex.TextureBitmap.Width,
               tex.TextureBitmap.Height), ImageLockMode.ReadOnly,
               System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            }
            else
            {
                throw new Exception("Could not find file " + filepath);
            }
            int mTexture_ID;
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.GenTextures(1, out mTexture_ID);
            tex.Texture_ID = mTexture_ID;
            GL.BindTexture(TextureTarget.Texture2D, tex.Texture_ID);
            GL.TexImage2D(TextureTarget.Texture2D,
            0, PixelInternalFormat.Rgba, tex.TextureData.Width, tex.TextureData.Height,
            0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
            PixelType.UnsignedByte, tex.TextureData.Scan0);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
            (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
            (int)TextureMagFilter.Linear);

            tex.TextureBitmap.UnlockBits(tex.TextureData);
            textureDictionary.Add(tag, tex);
        }

        public static Texture FindTexture(string name)
        {
            foreach (var item in textureDictionary)
            {
                if (item.Key == name)
                {
                    return item.Value;
                }
            }
            return null;
        }

        public static Geometry FindGeometry(string name)
        {
             foreach (var item in geometryDictionary)
            {
                if (item.Key == name)
                {
                    return item.Value;
                }
            }
            return null;
        }
    }
}
