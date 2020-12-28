using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    class Terrain
    {
        private Bitmap heightMap;
        int [,]terrain;


        public Terrain(String path)
        {
            heightMap = new Bitmap(path);
            terrain = new int [heightMap.Height , heightMap.Width];
            for (int i = 0; i < heightMap.Height; i++)
            {
                for (int j = 0; j < heightMap.Width; j++)
                {
                    terrain[i, j] = heightMap.GetPixel(i,j).G;
                }
            }
            
        }
    }
}
