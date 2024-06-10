﻿using System;
using System.Collections.Generic;
using SkiaSharp;
using OpenTK;

namespace GravitySimulator
{
    /* TO-DO
     * 
     * - добавить массив цветов 8 шт (по максимальному объектов)
     * - добавить инициализацию массива полутонов для кждого цвета
     * 
     * 
     */
    public class Scene
    {
        private SKCanvas g;
        
        public Scene(int numObjects)
        {
            ObjectsNum = numObjects;
            Background = new SKColor(0, 0, 0);
        }

        public float Scale { get; set; } = 1f;
        public int TailsLength { get; set; } = 1000;
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
                DrawBody(i);
            }
        }

        private void UpdatePositions(Vector2d[] currentPos)
        {
            testPos = currentPos;
        }

        private void DrawBody(int index)
        {
            var paint = new SKPaint
            {
                Color = SKColor.Parse("#FF0066CC"),
                IsAntialias = true
            };

            Vector2d pos = testPos[index];

            g.DrawCircle((float)pos.X, (float)pos.Y, 5f, paint);
        }

        private void DrawTail(int index)
        {
        
        }



        //
        // Test
        //
        private Vector2d[] testPos;

        public static void Test(SKCanvas g)
        {
            SKColor color_1 = SKColor.Parse("#FF0066CC");
            SKColor color_2 = SKColor.Parse("#FF11FF00");

            SKColor[] colors = { color_1, color_2 };

            SKPoint p1 = new SKPoint(-0.5f * g.LocalClipBounds.Width, -0.5f * g.LocalClipBounds.Height);
            SKPoint p2 = new SKPoint(+0.5f * g.LocalClipBounds.Width, +0.5f * g.LocalClipBounds.Height);

            var paint = new SKPaint
            {
                IsAntialias = true,
                Shader = SKShader.CreateLinearGradient(p1, p2, colors, SKShaderTileMode.Clamp)
            };

            g.DrawCircle(1, 2, 20, paint);

            SKPoint p3 = new SKPoint(20, 20);
            SKPoint p4 = new SKPoint(500, 250);

            var paint2 = new SKPaint
            {
                StrokeWidth = 5,
                IsAntialias = true,
                StrokeCap = SKStrokeCap.Round,
                Shader = SKShader.CreateLinearGradient(p1, p2, colors, SKShaderTileMode.Clamp)
            };

            g.DrawLine(p1, p2, paint);

            var paint3 = new SKPaint
            {
                Color = SKColor.Parse("#FF0066CC"),
                IsAntialias = true
            };

            g.DrawCircle(0, 0, 2, paint);

            g.DrawLine(-100, 0, 100, 0, paint);
            g.DrawLine(0, -100, 0, 100, paint);

            g.DrawCircle(new SKPoint((float)5, (float)1), 5, paint);
        }
    }
}