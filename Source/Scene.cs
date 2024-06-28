using OpenTK;
using SkiaSharp;
using System.Collections.Generic;

namespace GravitySimulator
{
    public class Scene
    {
        private static SKCanvas g;
        private static SKColor[] colors;

        // Colors HSV arrays + temp
        private static float[] h_Arr;
        private static float[] s_Arr;
        private static float[] v_Arr;
        private static float[] t_Arr;

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
            t_Arr = new float[colors.Length];

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

        public int TailsLength    { get; set; } = 401;
        public int ObjCount     { get; }
        public static SKColor Background { get; set; }
        public static float Scale { get; set; } = 2.0f;
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
            float galoWidth = bodyWidth * 5;

            for (int i = 0; i < ObjCount; i++)
            {
                SKPoint c = points[i];
                SKPaint p = new SKPaint
                {
                    IsAntialias = true,
                    Color = colors[i],
                    Shader = SKShader.CreateRadialGradient(c, galoWidth, new SKColor[] { colors[i], Background }, SKShaderTileMode.Mirror)
                };

                g.DrawCircle(c, galoWidth, p);
                p.Shader = null;
                g.DrawCircle(c, bodyWidth, p);
            }
        }

        private void DrawTails()
        {
            v_Arr.CopyTo(t_Arr, 0);

            /*
               Внешний цикл    - итерация по состояниям от самого старого к самому новому
               Внутренний цикл - итерация по объектам i = 0 .. ObjectsNum

               SKColor t = colors[i]; - получение цвета i-го объекта

               float h   = h_Arr[i];  - получение H цвета i-го объекта
               float s   = s_Arr[i];  - получение S цвета i-го объекта
               float v   = v_Arr[i];  - получение V цвета i-го объекта
             */

            //LinkedListNode<SKPoint[]> node = stateStore.Last;
            //colors[index].ToHsv(out float h, out float s, out float v);
            //float stepVal = v / (TailsLength - 1);
            //for (int i = 0; i < track.Length - 1; i++)
            //{
            //    var paint = new SKPaint
            //    {
            //        IsAntialias = true,
            //        Color = SKColor.FromHsv(h, s, v - stepVal * i),
            //        StrokeWidth = LineWidth / Scale,
            //        StrokeCap = SKStrokeCap.Round,
            //    };
            //    g.DrawLine(track[i], track[i + 1], paint);
            //}
        }
    }
}
