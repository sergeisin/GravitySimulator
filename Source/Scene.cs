using OpenTK;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;

namespace GravitySimulator
{
    public class Scene
    {

        private SKCanvas g;
        private static SKColor[] colors;
        private LinkedList<SKPoint>[] tracksArr;

        static Scene()
        {
            // "#FF0066CC" - HotTrack
            SKColor color_1 = SKColor.Parse("#FF0066CC");
            SKColor color_2 = SKColor.Parse("#FF11FF00");

            colors = new SKColor[8]
            {
                SKColors.DeepSkyBlue,
                SKColors.MediumPurple,
                SKColors.NavajoWhite,
                SKColors.Orange,
                SKColors.MediumSpringGreen,
                SKColors.PowderBlue,
                SKColors.BlueViolet,
                SKColors.Lavender
            };
        }

        public Scene(int numObjects)
        {
            ObjectsNum = numObjects;
            Background = new SKColor(0, 0, 0, 00);

            tracksArr = new LinkedList<SKPoint>[numObjects];
            for (int i = 0; i < numObjects; i++)
                tracksArr[i] = new LinkedList<SKPoint>();
        }

        public float Scale { get; set; } = 1f;
        public int TailsLength { get; set; } = 401;
        public int ObjectsNum  { get; }

        public SKColor Background { get; set; }

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
                    X = (float)currentPos[i].X,
                    Y = (float)currentPos[i].Y
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
            
            var paint = new SKPaint
            {
                Color = colors[index],
                IsAntialias = true,
                Shader = SKShader.CreateRadialGradient(point, 20, new SKColor[] { colors[index], Background }, SKShaderTileMode.Mirror)
            };

            g.DrawCircle(point, 20f, paint);

            paint.Shader = null;
            g.DrawCircle(point, 5f, paint);
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
                    Color = SKColor.FromHsl(h, s, v - stepVal * i),
                    StrokeWidth = 1F,
                    StrokeCap = SKStrokeCap.Round,
                };

                g.DrawLine(track[i], track[i + 1], paint);
            }
        }
    }
}
