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

            PhyObject[] objects_1 =
            {
                new PhyObject(1, new Vector2d(-20, -20)),
                new PhyObject(1, new Vector2d(-20, 20)),
                new PhyObject(1, new Vector2d(10, -20)),
            };

            PhyObject[] objects_2 =
{
                new PhyObject(1, new Vector2d(-20, -20)),
                new PhyObject(1, new Vector2d(-20, 20)),
                new PhyObject(1, new Vector2d(10, -20)),
            };

            model_1 = new Model(objects_1, deltaT: 0.001);
            model_2 = new Model(objects_2, deltaT: 0.002);

            scene_1 = new Scene(objects_1.Length);
            scene_2 = new Scene(objects_2.Length);
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                scene_1.Scale *= 1.15f;
                scene_2.Scale *= 1.15f;
            }
            else
            {
                scene_1.Scale /= 1.15f;
                scene_2.Scale /= 1.15f;
            }
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            model_1.Advance((int)(1.0 / model_1.DT));
            model_2.Advance((int)(1.0 / model_2.DT));
            skglSurface.Invalidate();
            counterFPS.UpdateFPS();
        }

        private void SkglSurface_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            scene_1.Render(e.Surface.Canvas, model_1.ObjectsPos, true);
            scene_2.Render(e.Surface.Canvas, model_2.ObjectsPos, false);
        }

        private void SkglSurface_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
            {
                scene_1.Scale *= 1.15f;
            }

            if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                scene_1.Scale /= 1.15f;
            }

            if (e.KeyCode == Keys.Enter)
            {
                scene_1.Scale = 1f;
            }
        }

        private void SkglSurface_Click(object sender, EventArgs e)
        {
            var a = (MouseEventArgs)e;

            clickPosition = new SKPoint()
            {
                X = (a.X - 0.5f * skglSurface.Width)  / scene_1.Scale,
                Y = (a.Y - 0.5f * skglSurface.Height) / scene_1.Scale
            };
        }

        private CounterFPS counterFPS;

        private Model model_1;            // Physics model
        private Scene scene_1;            // Graphic scene

        private Scene scene_2;
        private Model model_2;

        private SKPoint clickPosition;  // Last mouse click position (model)
    }
}
