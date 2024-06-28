using OpenTK;
using SkiaSharp;
using System.Collections.Generic;

namespace GravitySimulator
{
    public class Scene
    {
        private static SKCanvas g;
        private static SKColor[] colors;

        // Colors HSV arrays
        private static float[] h_Arr;
        private static float[] s_Arr;
        private static float[] v_Arr;

        // Archive of previous states
        private LinkedList<SKPoint[]> stateStore;

        static Scene()
        {
            Background = new SKColor();

            colors = new SKColor[]
            {
                SKColors.DeepSkyBlue,           // Синий
                SKColors.PowderBlue,            // Свеило-синий
                SKColors.NavajoWhite,           // Желтый
                SKColors.MediumSpringGreen,     // Слишком зелёный
                SKColors.Lavender,          
                SKColors.BlueViolet,            // Так себе
                SKColors.MediumPurple,          // Так себе
                SKColors.Orange,                // Так себе
            };

            h_Arr = new float[colors.Length];
            s_Arr = new float[colors.Length];
            v_Arr = new float[colors.Length];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i].ToHsv(out h_Arr[i], out s_Arr[i], out v_Arr[i]);
            }
        }

        public Scene(int numObjects)
        {
            ObjCount = numObjects;
            stateStore = new LinkedList<SKPoint[]>();
        }

        public int TailsLength  { get; set; } = 300;
        public int ObjCount     { get; }
        public static SKColor Background { get; set; }
        public static float Scale { get; set; } = 4.0f;
        public float LineWidth    { get; set; } = 1.5f;
        public float BallWidth    { get; set; } = 5.0f;

        public static void InitFrame(SKCanvas canvas)
        {
            g = canvas;

            g.Clear(Background);

            g.Translate(0.5f * g.LocalClipBounds.Width,
            0.5f * g.LocalClipBounds.Height);

            g.Scale(Scale);
        }

        public void Render(Vector2d[] objPositions)
        {
            UpdatePositions(objPositions);

            if (stateStore.Count > 1)
                DrawTails();

            DrawCircles();
        }

        private void UpdatePositions(Vector2d[] currentPos)
        {
            SKPoint[] positions = new SKPoint[ObjCount];

            for (int i = 0; i < ObjCount; i++)
            {
                positions[i].X =  (float)currentPos[i].X;
                positions[i].Y = -(float)currentPos[i].Y;
            }

            stateStore.AddFirst(positions);

            while (stateStore.Count > TailsLength)
                stateStore.RemoveLast();
        }

        private void DrawCircles()
        {
            SKPoint[] points = stateStore.First.Value;

            float bodyWidth = BallWidth / Scale;
            float galoWidth = bodyWidth * 4;

            for (int i = 0; i < ObjCount; i++)
            {
                SKColor color = colors[i];
                SKPoint point = points[i];
                SKPaint paint = new SKPaint
                {
                    IsAntialias = true,
                    Color = color,
                    Shader = SKShader.CreateRadialGradient(point, galoWidth, new SKColor[] { color, Background }, SKShaderTileMode.Mirror)
                };

                g.DrawCircle(point, galoWidth, paint);
                paint.Shader = null;
                g.DrawCircle(point, bodyWidth, paint);
            }
        }

        private void DrawTails()
        {
            var paint = new SKPaint
            {
                IsAntialias = true,
                StrokeWidth = LineWidth / Scale,
                StrokeCap = SKStrokeCap.Round,
            };

            int step = 0;

            var node = stateStore.Last;
            while (node != stateStore.First)
            {
                SKPoint[] state_A = node.Value;
                SKPoint[] state_B = node.Previous.Value;

                for (int i = 0; i < ObjCount; i++)
                {
                    paint.Color = SKColor.FromHsv(h_Arr[i], s_Arr[i], step * v_Arr[i] / TailsLength);
                    g.DrawLine(state_A[i], state_B[i], paint);
                }

                step++;
                node = node.Previous;
            }
        }
    }
}
