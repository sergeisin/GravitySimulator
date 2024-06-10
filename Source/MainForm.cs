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
                new PhyObject(1, new Vector2d(10, 20), new Vector2d()),
                new PhyObject(1, new Vector2d(30, 40), new Vector2d()),
                new PhyObject(1, new Vector2d(50, 10), new Vector2d()),
            };

            physicsModel = new Model(objects, deltaT: 0.05);
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                scaleFactor *= 1.15f;
            else
                scaleFactor /= 1.15f;
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            physicsModel.Advance();
            skglSurface.Invalidate();
            counterFPS.UpdateFPS();
        }

        private void SkglSurface_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            SKCanvas g = e.Surface.Canvas;

            g.Clear(SKColor.Parse("#000000"));
            g.Translate(0.5f * skglSurface.Width, 0.5f * skglSurface.Height);
            g.Scale(scaleFactor);

            Renderer.Render(g, physicsModel.RenderObjects);

            // Mouse capture test
            if (!clickPosition.IsEmpty)
            {
                Renderer.DrawCircle(g, clickPosition.X, clickPosition.Y);
                Text = clickPosition.ToString();
            }
        }

        private void SkglSurface_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
            {
                scaleFactor *= 1.15f;
            }

            if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                scaleFactor /= 1.15f;
            }

            if (e.KeyCode == Keys.Enter)
            {
                scaleFactor = 1.0f;
            }
        }

        private void SkglSurface_Click(object sender, EventArgs e)
        {
            var a = (MouseEventArgs)e;

            clickPosition = new SKPoint()
            {
                X = (a.X - 0.5f * skglSurface.Width)  / scaleFactor,
                Y = (a.Y - 0.5f * skglSurface.Height) / scaleFactor
            };
        }

        private Model physicsModel;

        private CounterFPS counterFPS;
        
        // Last mouse click position (model)
        private SKPoint clickPosition;
        
        private float scaleFactor = 1f;
    }
}
