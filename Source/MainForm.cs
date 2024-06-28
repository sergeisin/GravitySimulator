using System;
using System.Windows.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using OpenTK;

namespace GravitySimulator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            MouseWheel += new MouseEventHandler(MainForm_MouseWheel);

            counterFPS = new CounterFPS(this);

            PhyObject[] objects =
            {
                new PhyObject(1, new Vector2d(-20, -20)),
                new PhyObject(1, new Vector2d(-20, 20)),
                new PhyObject(1, new Vector2d(10, -20)),
            };

            model = new Model(objects, deltaT: 0.001, cycles: 1000);
            scene = new Scene(objects.Length);
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                Scene.Scale *= 1.15f;
            else
                Scene.Scale /= 1.15f;
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            model.Advance();
            skglSurface.Invalidate();
            counterFPS.UpdateFPS();
        }

        private void SkglSurface_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            Scene.InitFrame(e.Surface.Canvas);
            scene.Render(model.ObjectsPos);
        }

        private void SkglSurface_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
            {
                Scene.Scale *= 1.15f;
            }

            if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                Scene.Scale /= 1.15f;
            }

            if (e.KeyCode == Keys.Enter)
            {
                Scene.Scale = 1f;
            }
        }

        private void SkglSurface_Click(object sender, EventArgs e)
        {
            var a = (MouseEventArgs)e;

            clickPosition = new SKPoint()
            {
                X = (a.X - 0.5f * skglSurface.Width)  / Scene.Scale,
                Y = (a.Y - 0.5f * skglSurface.Height) / Scene.Scale
            };
        }

        private CounterFPS counterFPS;

        private Model model;            // Physics model
        private Scene scene;            // Graphic scene

        private SKPoint clickPosition;  // Last mouse click position (model)
    }
}
