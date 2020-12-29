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
        public Bitmap heightMap;
        int [,] terrain;
        float[] indices;
        float[] colors;

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

        public void mapToArray()
        {
            List<float> indices_list = new List<float>();
            List<float> colors_list = new List<float>();
            float[,] Color_Array = { {237/255f ,201 / 255f, 175 / 255f },{126 / 255f, 200 / 255f, 80 / 255f },{90 / 255f, 77 / 255f, 65 / 255f },{224 / 255f, 247 / 255f, 250 / 255f } };
            for (int i = 0; i < (heightMap.Height-1) ; i++)
            {
                for (int j = 0; j < (heightMap.Width-1) ; j++)
                {
                    // Original point
                    indices_list.Add(i);
                    indices_list.Add(terrain[i, j]);
                    indices_list.Add(j);

                    indices_list.Add(Color_Array[fillColor(terrain[i,j]),0]);
                    indices_list.Add(Color_Array[fillColor(terrain[i, j]), 1]);
                    indices_list.Add(Color_Array[fillColor(terrain[i, j]), 2]);

                    // Right point
                    indices_list.Add(i+1);
                    indices_list.Add(terrain[i+1, j]);
                    indices_list.Add(j);

                    indices_list.Add(Color_Array[fillColor(terrain[i+1, j]), 0]);
                    indices_list.Add(Color_Array[fillColor(terrain[i+1, j]), 1]);
                    indices_list.Add(Color_Array[fillColor(terrain[i+1, j]), 2]);

                    // Down Right point
                    indices_list.Add(i+1);
                    indices_list.Add(terrain[i+1, j+1]);
                    indices_list.Add(j+1);

                    indices_list.Add(Color_Array[fillColor(terrain[i+1, j+1]), 0]);
                    indices_list.Add(Color_Array[fillColor(terrain[i+1, j+1]), 1]);
                    indices_list.Add(Color_Array[fillColor(terrain[i+1, j+1]), 2]);

                    // Original point
                    indices_list.Add(i);
                    indices_list.Add(terrain[i, j]);
                    indices_list.Add(j);

                    indices_list.Add(Color_Array[fillColor(terrain[i, j]), 0]);
                    indices_list.Add(Color_Array[fillColor(terrain[i, j]), 1]);
                    indices_list.Add(Color_Array[fillColor(terrain[i, j]), 2]);
                    // Down point

                    indices_list.Add(i);
                    indices_list.Add(terrain[i, j+1]);
                    indices_list.Add(j+1);

                    indices_list.Add(Color_Array[fillColor(terrain[i, j+1]), 0]);
                    indices_list.Add(Color_Array[fillColor(terrain[i, j+1]), 1]);
                    indices_list.Add(Color_Array[fillColor(terrain[i, j+1]), 2]);
                    // Down Right point

                    indices_list.Add(i+1);
                    indices_list.Add(terrain[i+1, j+1]);
                    indices_list.Add(j+1);

                    indices_list.Add(Color_Array[fillColor(terrain[i + 1, j + 1]), 0]);
                    indices_list.Add(Color_Array[fillColor(terrain[i + 1, j + 1]), 1]);
                    indices_list.Add(Color_Array[fillColor(terrain[i + 1, j + 1]), 2]);
                }
            }

            indices = indices_list.ToArray();
            colors = colors_list.ToArray();
        }
        public int fillColor(float height)
        {
            int choice;
            if (height / 255 <= 0.25)
            {
                choice = 0;
            }
            else if (height / 255 <= 0.5)
            {
                choice = 1;
            }
            else if (height / 255 <= 0.75)
            {
                choice = 2;
            }
            else
            {
                choice = 3;
            }
            return choice;
        }
        public float[] get_index_array()
        {
            return indices;
        }
        public float[] get_color_array()
        {
            return colors;
        }

    }
}
