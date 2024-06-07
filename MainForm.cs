using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace GravitySimulator
{
    public partial class MainForm : Form
    {
        private readonly CounterFPS counterFPS;
        private float scaleFactor;

        public MainForm()
        {
            InitializeComponent();

            MouseWheel += new MouseEventHandler(MainForm_MouseWheel);
            counterFPS = new CounterFPS(this);
            scaleFactor = 1.0f;
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
            skglSurface.Invalidate();
            counterFPS.UpdateFPS();
        }

        private void SkglSurface_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            SKCanvas g = e.Surface.Canvas;

            g.Clear(SKColor.Parse("#000000"));
            g.Translate(0.5f * skglSurface.Width, 0.5f * skglSurface.Height);
            g.Scale(scaleFactor);

            Renderer.Render(g);

            if (!pos.IsEmpty)
                Renderer.DrawCircle(g, pos.X, pos.Y);
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
            var args = (MouseEventArgs)e;

            pos = new SKPoint()
            {
                X = args.X / scaleFactor - 0.5f * skglSurface.Width,
                Y = args.Y / scaleFactor - 0.5f * skglSurface.Height,
            };
        }

        private SKPoint pos;

        private void skglSurface_MouseMove(object sender, MouseEventArgs e)
        {
            pos = new SKPoint()
            {
                X = e.X / scaleFactor - 0.5f * skglSurface.Width,
                Y = e.Y / scaleFactor - 0.5f * skglSurface.Height,
            };
        }
    }
}
