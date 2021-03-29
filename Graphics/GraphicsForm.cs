using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace Graphics
{
    public partial class GraphicsForm : Form
    {
        Renderer renderer = new Renderer();
        Thread MainLoopThread;
        public GraphicsForm()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();

            //MoveCursor();
            

            initialize();
            MainLoopThread = new Thread(MainLoop);
            MainLoopThread.Start();

        }
        void initialize()
        {
            renderer.Initialize();   
        }
        
        void MainLoop()
        {
            while (true)
            {
                renderer.Update();
                renderer.Draw();
                simpleOpenGlControl1.Refresh();
            }
        }
        private void GraphicsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            renderer.CleanUp();
            MainLoopThread.Abort();
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            renderer.Draw();
        }

        private void simpleOpenGlControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            float speed = 10f;
            float angle = 0.3f;
            if (e.KeyChar == 'a')
                renderer.cam.Strafe(-speed, renderer.terrain.get_height(renderer.cam.Get_mPosition().x, renderer.cam.Get_mPosition().z));          
            if (e.KeyChar == 'd')
                renderer.cam.Strafe(speed, renderer.terrain.get_height(renderer.cam.Get_mPosition().x, renderer.cam.Get_mPosition().z));
            if (e.KeyChar == 's')
                renderer.cam.Walk(-speed, renderer.terrain.get_height(renderer.cam.Get_mPosition().x, renderer.cam.Get_mPosition().z));
            if (e.KeyChar == 'w')
                renderer.cam.Walk(speed, renderer.terrain.get_height(renderer.cam.Get_mPosition().x, renderer.cam.Get_mPosition().z));
            if (e.KeyChar == 'z')
                renderer.cam.Fly(-speed);
            if (e.KeyChar == 'c')
                renderer.cam.Fly(speed);
            if (e.KeyChar == 'e')
                renderer.cam.Yaw(-angle);
            if (e.KeyChar == 'q')
                renderer.cam.Yaw(angle);
            if (e.KeyChar == 't')
                renderer.cam.Pitch(-angle);
            if (e.KeyChar == 'g')
                renderer.cam.Pitch(angle);

            textBox1.Text = (renderer.cam.Get_mPosition().x).ToString();
            textBox2.Text = (renderer.cam.Get_mPosition().y).ToString();
            textBox3.Text = (renderer.cam.Get_mPosition().z).ToString();
        }

        float prevX, prevY;
        private void simpleOpenGlControl1_MouseMove(object sender, MouseEventArgs e)
        {
            //float speed = 0.05f;
            //float delta = e.X - prevX;
            //if (delta > 2)
            //    renderer.cam.Yaw(-speed);
            //else if (delta < -2)
            //    renderer.cam.Yaw(speed);

            //label1.Text = "Delta x: " + delta;

            //delta = e.Y - prevY;
            //if (delta > 2)
            //    renderer.cam.Pitch(-speed);
            //else if (delta < -2)
            //    renderer.cam.Pitch(speed);

            //label2.Text = "Delta y: " + delta;
            //MoveCursor();
        }

        private void GraphicsForm_Load(object sender, System.EventArgs e)
        {

        }

        private void label1_Click(object sender, System.EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            float x = float.Parse(textBox1.Text);
            float y = renderer.cam.Get_mPosition().y;
            float z = renderer.cam.Get_mPosition().z;
            renderer.cam.set_mPosition(x,y,z);
        }

        private void textBox2_TextChanged(object sender, System.EventArgs e)
        {
            float y = float.Parse(textBox2.Text);
            float x = renderer.cam.Get_mPosition().x;
            float z = renderer.cam.Get_mPosition().z;
            renderer.cam.set_mPosition(x, y, z);
        }

        private void textBox3_TextChanged(object sender, System.EventArgs e)
        {
            float z = float.Parse(textBox3.Text);
            float y = renderer.cam.Get_mPosition().y;
            float x = renderer.cam.Get_mPosition().x;
            renderer.cam.set_mPosition(x, y, z);
        }

        private void MoveCursor()
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Point p = PointToScreen(simpleOpenGlControl1.Location);
            Cursor.Position = new Point(simpleOpenGlControl1.Size.Width/2+p.X, simpleOpenGlControl1.Size.Height/2+p.Y);
            Cursor.Clip = new Rectangle(this.Location, this.Size);
            prevX = simpleOpenGlControl1.Location.X+simpleOpenGlControl1.Size.Width/2;
            prevY = simpleOpenGlControl1.Location.Y + simpleOpenGlControl1.Size.Height / 2;
        }
    }
}
