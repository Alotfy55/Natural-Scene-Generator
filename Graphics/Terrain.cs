using System;
using System.Collections.Generic;
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

        float[] sand_Arr;
        float[] grass_Arr;
        float[] rock_Arr;
        float[] snow_Arr;

        float[] colors;
        int initI, initJ;

        public Terrain(String path)
        {
            heightMap = new Bitmap(path);
            terrain = new int [(3*heightMap.Height / 4) - (heightMap.Height /4) , (3*heightMap.Width / 4) - (heightMap.Width / 4)];
            initI = heightMap.Height / 4;
            initJ = heightMap.Width / 4;
            for (int i = heightMap.Height / 4; i < 3*heightMap.Height/4; i++)
            {
                for (int j = heightMap.Width / 4; j < 3*heightMap.Width/4; j++)
                {
                    terrain[i - initI, j - initJ] = heightMap.GetPixel(i,j).G;
                }
            }
            

            
        }

        public void mapToArray()
        {
            List<List<float>> Texture_List = new List<List<float>>();
            List<float> sand_list = new List<float>();
            List<float> grass_list = new List<float>();
            List<float> rock_list = new List<float>();
            List<float> snow_list = new List<float>();
            Texture_List.Add(sand_list);
            Texture_List.Add(grass_list);
            Texture_List.Add(rock_list);
            Texture_List.Add(snow_list);
            //List<float> colors_list = new List<float>();
            //float[,] Color_Array = { {237/255f ,201 / 255f, 175 / 255f },{126 / 255f, 200 / 255f, 80 / 255f },{90 / 255f, 77 / 255f, 65 / 255f },{224 / 255f, 247 / 255f, 250 / 255f } };
            for (int i = 0; i < (heightMap.Height / 2) -1; i++)
            {
                for (int j = 0; j < (heightMap.Width/2) -1 ; j++)
                {
                    int choice = fillTexture(terrain[i, j]);
                    add_Index(Texture_List[choice],i,j);

                }
            }

            sand_Arr = sand_list.ToArray();
            grass_Arr = grass_list.ToArray();
            rock_Arr = rock_list.ToArray();
            snow_Arr = snow_list.ToArray();
        }
        public int fillTexture(float height)
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
            else if (height / 255 <= 0.7)
            {
                choice = 2;
            }
            else
            {
                choice = 3;
            }
            return choice;
        }
        void add_Index(List<float> indices_list, int i , int j)
        {
            // Original point
            indices_list.Add(i);
            indices_list.Add(terrain[i, j]);
            indices_list.Add(j);



            // Right point
            indices_list.Add(i + 1);
            indices_list.Add(terrain[i + 1, j]);
            indices_list.Add(j);



            // Down Right point
            indices_list.Add(i + 1);
            indices_list.Add(terrain[i + 1, j + 1]);
            indices_list.Add(j + 1);



            // Original point
            indices_list.Add(i);
            indices_list.Add(terrain[i, j]);
            indices_list.Add(j);


            // Down point

            indices_list.Add(i);
            indices_list.Add(terrain[i, j + 1]);
            indices_list.Add(j + 1);


            // Down Right point

            indices_list.Add(i + 1);
            indices_list.Add(terrain[i + 1, j + 1]);
            indices_list.Add(j + 1);

        }
        public float[] get_sand_Arr()
        {
            return sand_Arr;
        }
        public float[] get_grass_Arr()
        {
            return grass_Arr;
        }
        public float[] get_rock_Arr()
        {
            return rock_Arr;
        }
        public float[] get_snow_Arr()
        {
            return snow_Arr;
        }
        public float[] get_color_array()
        {
            return colors;
        }


    }
}
