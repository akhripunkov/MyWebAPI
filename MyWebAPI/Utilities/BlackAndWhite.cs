using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyWebAPI.Utilities
{
    public class BlackAndWhite
    {
        public string path;
        public BlackAndWhite(string _path)
        {
            path = _path;
        }

        public Bitmap BlackAndWhiteBrush()
        {
            Stream rtn = null;
            HttpWebRequest aRequest = (HttpWebRequest)WebRequest.Create(path);
            HttpWebResponse aResponse = (HttpWebResponse)aRequest.GetResponse();

            rtn = aResponse.GetResponseStream();

            Image img = Image.FromStream(rtn);
            Bitmap newIm = new Bitmap(img);

            for (int i = 0; i < newIm.Width; i++)
            {
                for (int j = 0; j < newIm.Height; j++)
                {
                    UInt32 pixel = (UInt32)(newIm.GetPixel(i, j).ToArgb());
                    float R = (float)((pixel & 0x00FF0000) >> 16); 
                    float G = (float)((pixel & 0x0000FF00) >> 8); 
                    float B = (float)(pixel & 0x000000FF); 
                    R = G = B = (R + G + B) / 3.0f;
                    UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                    newIm.SetPixel(i, j, Color.FromArgb((int)newPixel));
                }
            }
            newIm.Save("Black.jpg");

            return newIm;
        }
    }
}
