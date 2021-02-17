using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
using GlmNet;
using System.IO;

namespace Graphics
{
    class Renderer
    {
        Shader sh;
        uint vertexBufferID1;
        uint vertexBufferID20;
        uint vertexBufferID30;
        uint vertexBufferID40;
        uint vertexBufferID50;


        uint vertexBufferID3;
        uint vertexBufferID4;
        uint vertexBufferID5;
        uint vertexBufferID6;
        uint vertexBufferID7;
        uint vertexBufferID8;


        int transID;
        int viewID;
        int projID;
        int sand_size;
        int grass_size;
        int rock_size;
        int snow_size;
        int water7_size;
        float Time=0;

        float waveAmplitude = 2;

        mat4 scaleMat;

        mat4 ProjectionMatrix;
        mat4 ViewMatrix;


        public Camera cam;

        Texture tex1;
        Texture tex2;
        Texture tex3;
        Texture tex4;
        Texture tex5;

        Texture texSky1;
        Texture texSky2;
        Texture texSky3;
        Texture texSky4;
        Texture texSky5;
        Texture texSky6;

        float[] sand_Arr;
        float[] grass_Arr;
        float[] rock_Arr;
        float[] snow_Arr;
        float[] water7;
        Terrain terrain;
        public void Initialize()
        {
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");


            tex1 = new Texture(projectPath + "\\Textures\\sand.jpg", 1);
            tex2 = new Texture(projectPath + "\\Textures\\grass.jpg", 2);
            tex3 = new Texture(projectPath + "\\Textures\\stone.png", 3);
            tex4 = new Texture(projectPath + "\\Textures\\snow.jpg", 4);
            tex5 = new Texture(projectPath + "\\Textures\\water3.jpg", 5);
            

            texSky1 = new Texture(projectPath + "\\Textures\\bluecloud_bk.jpg", 1);
            texSky2 = new Texture(projectPath + "\\Textures\\bluecloud_dn.jpg", 2);
            texSky3 = new Texture(projectPath + "\\Textures\\bluecloud_ft.jpg", 3);
            texSky4 = new Texture(projectPath + "\\Textures\\bluecloud_lf.jpg", 4);
            texSky5 = new Texture(projectPath + "\\Textures\\bluecloud_rt.jpg", 5);
            texSky6 = new Texture(projectPath + "\\Textures\\bluecloud_up.jpg", 6);

            Gl.glClearColor(0, 0, 0.4f, 1);

            terrain = new Terrain(projectPath + "\\Textures\\noise_simplex.png");

            terrain.mapToArray();

            //indices_size = (terrain.heightMap.Height-1)*(terrain.heightMap.Width-1)*18;
            //indices_size = terrain_indices.Count();
            //float[] terrain_colors = terrain.get_color_array();

            sand_Arr = terrain.get_sand_Arr();
            grass_Arr = terrain.get_grass_Arr();
            rock_Arr = terrain.get_rock_Arr();
            snow_Arr = terrain.get_snow_Arr();
            water7 = terrain.get_water7_Arr();

            sand_size = sand_Arr.Count();
            grass_size = grass_Arr.Count();
            rock_size = rock_Arr.Count();
            snow_size = snow_Arr.Count();
            water7_size = water7.Count();
            

            float[] verts = {
                -1.0f, -1.0f, 0.0f,
                 1,0,0,
                 0,0,

                 1.0f, -1.0f, 0.0f,
                 1,0,0,
                 0,1,

                 0.0f,  1.0f, 0.0f,
                 1,0,0,
                 1,0

            };


            //SKYBOX COORDS
            float[] sky1 = {
                 10.0f, 10.0f, -10.0f,//1
                 0,0,1,
                 0,0,

                 -10.0f, -10.0f, -10.0f,//2
                 0,0,1,
                 1,1,

                -10.0f, 10.0f, -10.0f,
                 0,0,1,
                 1,0,

                 10.0f, -10.0f, -10.0f,
                 0,0,1,
                 0,1,

                10.0f, 10.0f, -10.0f,//1
                 0,0,1,
                 0,0,

                 -10.0f, -10.0f, -10.0f,//2
                 0,0,1,
                 1,1
            };
            float[] sky2 = {
                -10.0f, -10.0f, 10.0f,//1
                 0,0,1,
                 1,1,

                 10.0f, -10.0f, -10.0f,//2
                 0,0,1,
                 0,0,

                -10.0f, -10.0f, -10.0f,
                 0,0,1,
                 1,0,

                 10.0f, -10.0f, 10.0f,
                 0,0,1,
                 0,1,

                -10.0f, -10.0f, 10.0f,//1
                 0,0,1,
                 1,1,

                 10.0f, -10.0f, -10.0f,//2
                 0,0,1,
                 0,0
            };
            float[] sky3 = {
                -10.0f, -10.0f, 10.0f,//1
                 0,0,1,
                 1,1,

                 10.0f, 10.0f, 10.0f,//2
                 0,0,1,
                 0,0,

                10.0f, -10.0f, 10.0f,
                 0,0,1,
                 0,1,

                 -10.0f, 10.0f, 10.0f,
                 0,0,1,
                 1,0,

                -10.0f, -10.0f, 10.0f,//1
                 0,0,1,
                 1,1,

                 10.0f, 10.0f, 10.0f,//2
                 0,0,1,
                 0,0
            };
            float[] sky4 = {
                -10.0f, 10.0f,10.0f,//1
                 0,0,1,
                 1,1,

                 -10.0f, -10.0f, -10.0f,//2
                 0,0,1,
                 0,0,

                -10.0f,-10.0f, 10.0f,
                 0,0,1,
                 1,0,

                 -10.0f, 10.0f, -10.0f,
                 0,0,1,
                 0,1,

                -10.0f, 10.0f,10.0f,//1
                 0,0,1,
                 1,1,

                 -10.0f, -10.0f, -10.0f,//2
                 0,0,1,
                 0,0
            };
            float[] sky5 = {
                10.0f, 10.0f,-10.0f,//1
                 0,0,1,
                 1,1,

                 10.0f, -10.0f, 10.0f,//2
                 0,0,1,
                 0,0,

                10.0f,10.0f, 10.0f,
                 0,0,1,
                 0,1,

                 10.0f, -10.0f, -10.0f,
                 0,0,1,
                 1,0,

                10.0f, 10.0f,-10.0f,//1
                 0,0,1,
                 1,1,

                 10.0f, -10.0f, 10.0f,//2
                 0,0,1,
                 0,0
            };

            float[] sky6 = {
                10.0f, 10.0f, 10.0f,//1
                 0,0,1,
                 1,1,

                 -10.0f, 10.0f, -10.0f,//2
                 0,0,1,
                 0,0,

                -10.0f, 10.0f, 10.0f,
                 0,0,1,
                 1,0,

                 10.0f, 10.0f, -10.0f,
                 0,0,1,
                 0,1,

                10.0f, 10.0f, 10.0f,//1
                 0,0,1,
                 1,1,

                 -10.0f, 10.0f, -10.0f,//2
                 0,0,1,
                 0,0
            };
            //END

            vertexBufferID1 = GPU.GenerateBuffer(sand_Arr);
            vertexBufferID20 = GPU.GenerateBuffer(grass_Arr);
            vertexBufferID30 = GPU.GenerateBuffer(rock_Arr);
            vertexBufferID40 = GPU.GenerateBuffer(snow_Arr);
            vertexBufferID50 = GPU.GenerateBuffer(water7);

            //SKYBOX BUFFERS
            vertexBufferID3 = GPU.GenerateBuffer(sky1);
            vertexBufferID4 = GPU.GenerateBuffer(sky2);
            vertexBufferID5 = GPU.GenerateBuffer(sky3);
            vertexBufferID6 = GPU.GenerateBuffer(sky4);
            vertexBufferID7 = GPU.GenerateBuffer(sky5);
            vertexBufferID8 = GPU.GenerateBuffer(sky6);

            //END

            scaleMat = glm.scale(new mat4(1), new vec3(2f, 2f, 2.0f));

            cam = new Camera();

            ProjectionMatrix = cam.GetProjectionMatrix();
            ViewMatrix = cam.GetViewMatrix();

            transID = Gl.glGetUniformLocation(sh.ID, "model");
            projID = Gl.glGetUniformLocation(sh.ID, "projection");
            viewID = Gl.glGetUniformLocation(sh.ID, "view");

        }

        public void Draw()
        {
            if (Time % 25 == 0)
            {
                for (int i = 0; i < water7_size / 8; i++)
                {
                    water7[i * 8 + 1] = terrain.water_level;
                    water7[i * 8 + 1] += (float)(Math.Sin(Time * water7[i * 8]) + Math.Cos(Time * water7[i * 8 + 2])) * waveAmplitude;

                }
            }
            Time++;

            vertexBufferID50 = GPU.GenerateBuffer(water7);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            sh.UseShader();
            
            Gl.glUniformMatrix4fv(transID, 1, Gl.GL_FALSE, scaleMat.to_array());
            Gl.glUniformMatrix4fv(projID, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());
            Gl.glUniformMatrix4fv(viewID, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //DRAWING SAND//
            
            GPU.BindBuffer(vertexBufferID1);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), IntPtr.Zero);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            tex1.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, sand_size);

            //DRAWING GRASS//

            GPU.BindBuffer(vertexBufferID20);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), IntPtr.Zero);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            tex2.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, grass_size);

            //DRAWING ROCK//

            GPU.BindBuffer(vertexBufferID30);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), IntPtr.Zero);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));


            tex3.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, rock_size);

            //DRAWING SNOW//

            GPU.BindBuffer(vertexBufferID40);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), IntPtr.Zero);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));


            tex4.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, snow_size);

            //DRAWING WATER//
           // Gl.glEnable(Gl.GL_BLEND);
            //Gl.glBlendFunc(Gl.GL_SRC_ALPHA,Gl.GL_ONE_MINUS_SRC_ALPHA);
            GPU.BindBuffer(vertexBufferID50);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), IntPtr.Zero);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));


            tex5.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, water7_size);
           // Gl.glDisable(Gl.GL_BLEND);

            //SKYBOX BINDING
            GPU.BindBuffer(vertexBufferID3);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), IntPtr.Zero);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            texSky1.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);


            GPU.BindBuffer(vertexBufferID4);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), IntPtr.Zero);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            texSky2.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);


            GPU.BindBuffer(vertexBufferID5);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), IntPtr.Zero);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            texSky3.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);


            GPU.BindBuffer(vertexBufferID6);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), IntPtr.Zero);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            texSky4.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);


            GPU.BindBuffer(vertexBufferID7);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), IntPtr.Zero);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            texSky5.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);


            GPU.BindBuffer(vertexBufferID8);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), IntPtr.Zero);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            texSky6.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);


            //END BINDING

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            Gl.glDisableVertexAttribArray(2);
        }
        public void Update()
        {
            cam.UpdateViewMatrix();
            ProjectionMatrix = cam.GetProjectionMatrix();
            ViewMatrix = cam.GetViewMatrix();
        }
        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}
