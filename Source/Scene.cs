using OpenTK;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;

namespace GravitySimulator
{
    public class Scene
    {
        private SKCanvas g;
        private static SKColor[] colors;
        private LinkedList<SKPoint>[] tracksArr;
        static Scene()
        {
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
        }

        public Scene(int numObjects)
        {
            ObjectsNum = numObjects;
            Background = new SKColor(0, 0, 0, 0);

            tracksArr = new LinkedList<SKPoint>[numObjects];
            for (int i = 0; i < numObjects; i++)
                tracksArr[i] = new LinkedList<SKPoint>();
        }

        public int TailsLength { get; set; } = 401;
        public int ObjectsNum  { get; }
        public SKColor Background { get; set; }
        public float Scale     { get; set; } = 2.0f;
        public float LineWidth { get; set; } = 1.5f;
        public float BallWidth { get; set; } = 5.0f;

        public void Render(SKCanvas canvas, Vector2d[] objPositions)
        {
            g = canvas;
            g.Clear(Background);

            g.Translate(0.5f * g.LocalClipBounds.Width,
                        0.5f * g.LocalClipBounds.Height);

            g.Scale(Scale);

            UpdatePositions(objPositions);

            for (int i = 0; i < ObjectsNum; i++)
            {
                DrawTail(i);
            }

            for (int i = 0; i < ObjectsNum; i++)
            {
                DrawBody(i);
            }
        }

        private void UpdatePositions(Vector2d[] currentPos)
        {
            for (int i = 0; i < ObjectsNum; i++)
            {
                var point = new SKPoint()
                {
                    X = +(float)currentPos[i].X,
                    Y = -(float)currentPos[i].Y
                };

                tracksArr[i].AddFirst(point);

                if (tracksArr[i].Count > TailsLength)
                {
                    tracksArr[i].RemoveLast();
                }
            }
        }

        private void DrawBody(int index)
        {
            SKPoint point = tracksArr[index].First.Value;

            float bWidth = BallWidth / Scale;
            float lWidth = bWidth * 4;

            
            var paint = new SKPaint
            {
                Color = colors[index],
                IsAntialias = true,
                Shader = SKShader.CreateRadialGradient(point, lWidth, new SKColor[] { colors[index], Background }, SKShaderTileMode.Mirror)
            };

            g.DrawCircle(point, lWidth, paint);

            paint.Shader = null;
            g.DrawCircle(point, bWidth, paint);
        }

        private void DrawTail(int index)
        {
            var track = tracksArr[index].ToArray();

            colors[index].ToHsv(out float h, out float s, out float v);
            float stepVal = v / (TailsLength - 1);

            for (int i = 0; i < track.Length - 1; i++)
            {
                var paint = new SKPaint
                {
                    IsAntialias = true,
                    Color = SKColor.FromHsv(h, s, v - stepVal * i),
                    StrokeWidth = LineWidth / Scale,
                    StrokeCap = SKStrokeCap.Round,
                };

                g.DrawLine(track[i], track[i + 1], paint);
            }
        }
    }
}
