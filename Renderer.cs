using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace GravitySimulator
{
    public static class Renderer
    {
        public static void Render(SKCanvas g)
        {
            var paint = new SKPaint
            {
                Color = SKColor.Parse("#FF0066CC"),
                IsAntialias = true
            };

            g.DrawCircle(0, 0, 50, paint);
        }

        public static void DrawCircle(SKCanvas g, float x, float y)
        {
            var paint = new SKPaint
            {
                Color = SKColor.Parse("#FF0066CC"),
                IsAntialias = true
            };

            g.DrawCircle(x, y, 20, paint);
        }

        private static void DrawLine(SKCanvas g)
        {
            SKColor color_1 = SKColor.Parse("#FF0066CC");
            SKColor color_2 = SKColor.Parse("#FF11FF00");

            SKColor[] colors = { color_1, color_2 };

            SKPoint p1 = new SKPoint(20, 20);
            SKPoint p2 = new SKPoint(500, 250);

            var paint = new SKPaint
            {
                StrokeWidth = 5,
                IsAntialias = true,
                StrokeCap = SKStrokeCap.Round,
                Shader = SKShader.CreateLinearGradient(p1, p2, colors, SKShaderTileMode.Clamp)
            };

            g.DrawLine(p1, p2, paint);
        }
    }
}
