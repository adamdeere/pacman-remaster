using System.Drawing;
using System.Drawing.Imaging;

namespace OpenGL_Game.Objects
{
    class Texture
    {
        public string TextureTag { get;  set; }
        public  Bitmap TextureBitmap;
        public  BitmapData TextureData;
        public int Texture_ID { get;  set; }

        public Texture(string filepath)
        {
            TextureBitmap = new Bitmap(filepath);
        }
    }
}
