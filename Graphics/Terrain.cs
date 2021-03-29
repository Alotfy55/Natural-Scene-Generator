using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace Graphics
{
    class Terrain
    {

        public Bitmap heightMap;

        float[] sand_Arr;
        float[] grass_Arr;
        float[] rock_Arr;
        float[] snow_Arr;
        float[] water7;
        int MAP_SCALE = 3;
        public int water_level = 39;
        //vec3[] Water7;

        public int sizeW, sizeH;

        public Terrain(String path)
        {
            heightMap = new Bitmap(path);

            water_level *= MAP_SCALE;

            sizeH = heightMap.Height / 4;
            sizeW = heightMap.Width / 4;

            List<List<float>> Texture_List = new List<List<float>>();
            List<float> sand_list = new List<float>();
            List<float> grass_list = new List<float>();
            List<float> rock_list = new List<float>();
            List<float> snow_list = new List<float>();
            List<float> water_list = new List<float>();
            Texture_List.Add(sand_list);
            Texture_List.Add(grass_list);
            Texture_List.Add(rock_list);
            Texture_List.Add(snow_list);
            Texture_List.Add(water_list);

            for (int i = 0; i < sizeH; i++)
            {
                for (int j = 0; j < sizeW; j++)
                {
                    int choice = fillTexture(heightMap.GetPixel(i, j).G);
                    if (choice == 4)
                    {
                        add_Index(Texture_List[0], i, j, false);
                        add_Index(Texture_List[choice], i, j, true);
                    }
                    else
                        add_Index(Texture_List[choice], i, j, false);

                }
            }

            sand_Arr = sand_list.ToArray();
            grass_Arr = grass_list.ToArray();
            rock_Arr = rock_list.ToArray();
            snow_Arr = snow_list.ToArray();
            water7 = water_list.ToArray();
           
        }

        public int fillTexture(float height)
        {
            int choice;
            if (height / 255 <= 0.15)
            {
                choice = 4;
            }
            else if (height / 255 <= 0.25) 
            {
                choice = 0;
            }
            else if (height / 255 <= 0.4)
            {
                choice = 1;
            }
            else if (height / 255 <= 0.6)
            {
                choice = 2;
            }
            else
            {
                choice = 3;
            }
            return choice;
        }

        void add_Index(List<float> indices_list, int i, int j,bool water)
        {
            // Original point
            

            indices_list.Add(i * MAP_SCALE);
            if(!water)
                indices_list.Add(heightMap.GetPixel(i, j).G * MAP_SCALE);
            else
                indices_list.Add(water_level);

            indices_list.Add(j * MAP_SCALE);
            indices_list.Add(0);
            indices_list.Add(0);
            indices_list.Add(0);

            indices_list.Add(0);
            indices_list.Add(0);



            // Right point
            indices_list.Add((i + 1) * MAP_SCALE);

            if (!water)
                indices_list.Add(heightMap.GetPixel(i+1, j).G * MAP_SCALE);
            else
                indices_list.Add(water_level);

            indices_list.Add(j * MAP_SCALE);
            indices_list.Add(0);
            indices_list.Add(0);
            indices_list.Add(0);

            indices_list.Add(1);
            indices_list.Add(0);



            // Down Right point
            indices_list.Add((i + 1) * MAP_SCALE);

            if (!water)
                indices_list.Add(heightMap.GetPixel(i+1, j+1).G * MAP_SCALE);
            else
                indices_list.Add(water_level);

            indices_list.Add((j + 1) * MAP_SCALE);
            indices_list.Add(0);
            indices_list.Add(0);
            indices_list.Add(0);

            indices_list.Add(1);
            indices_list.Add(1);



            // Original point
            indices_list.Add(i * MAP_SCALE);
            if (!water)
                indices_list.Add(heightMap.GetPixel(i, j).G * MAP_SCALE);
            else
                indices_list.Add(water_level);
            indices_list.Add(j * MAP_SCALE);
            indices_list.Add(0);
            indices_list.Add(0);
            indices_list.Add(0);

            indices_list.Add(0);
            indices_list.Add(0);


            // Down point

            indices_list.Add(i * MAP_SCALE);
            if (!water)
                indices_list.Add(heightMap.GetPixel(i, j+1).G * MAP_SCALE);
            else
                indices_list.Add(water_level);
            indices_list.Add((j + 1) * MAP_SCALE);
            indices_list.Add(0);
            indices_list.Add(0);
            indices_list.Add(0);

            indices_list.Add(0);
            indices_list.Add(1);


            // Down Right point

            indices_list.Add((i + 1) * MAP_SCALE);
            if (!water)
                indices_list.Add(heightMap.GetPixel(i+1, j+1).G * MAP_SCALE);
            else
                indices_list.Add(water_level);
            indices_list.Add((j + 1) * MAP_SCALE);
            indices_list.Add(0);
            indices_list.Add(0);
            indices_list.Add(0);

            indices_list.Add(1);
            indices_list.Add(1);

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
        public float[] get_water7_Arr() 
        {
            return water7;
        }
        public float get_height(float x , float z) 
        {

            if (x < 0 || x > 2*heightMap.Height/4 || z > 2*heightMap.Width/4 || z < 0)
                return 500 * MAP_SCALE;
            else if (heightMap.GetPixel((int)x, (int)z).G < water_level)
                return 45*MAP_SCALE;
            else
                return heightMap.GetPixel((int)x, (int)z).G * MAP_SCALE;
        }
    }
}
