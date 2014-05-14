using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using System.IO;

namespace SpriteStomper
{
    public class MyCanvas : Canvas
    {
        BitmapImage background = null;

        public void LoadImage(string filename)
        {

            background = new BitmapImage(new Uri(filename));

            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {

            if (background != null)
            {

                dc.DrawImage(background, new Rect
                    (0, 0, background.PixelWidth, background.PixelHeight));

            }
        }

        public void GenSheet(string[] images)
        {
            BitmapFrame[] frames = new BitmapFrame[images.Length];

            int iW = 0;

            int iH = 0; 

            for (int i = 0; i < images.Length; i++)
            {

               frames[i] = BitmapDecoder.Create(new Uri(images[i]), BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First(); 

            }
            for(int i = 0; i<frames.Length; i++)
            {

                iW += frames[i].PixelWidth;

            }
            for(int i = 1; i<frames.Length; i++)
            {

                iH = Math.Max(frames[i-1].PixelHeight, frames[i].PixelHeight);
            }

            DrawingVisual drawingVisual = new DrawingVisual();

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                int prevFrames = 0;

                for (int i = 0; i < frames.Length; i++)
                {
                    drawingContext.DrawImage(frames[i], new Rect
                        (prevFrames, 0, frames[i].PixelWidth, frames[i].PixelHeight));

                    prevFrames += frames[i].PixelWidth;

                }
            }
            RenderTargetBitmap bmp = new RenderTargetBitmap
                (iW, iH, 96, 96, PixelFormats.Pbgra32);

            bmp.Render(drawingVisual);

            PngBitmapEncoder encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bmp));

            string directory = Directory.GetCurrentDirectory();

            using (Stream stream = File.Create(directory + @"\tile.png"))
                encoder.Save(stream);

            Vector[] eeek = stripSorter(frames);

            XML eek = new XML();

            eek.BuildXMLDoc(images, frames, iW, iH, eeek);

        }
        private Vector[] stripSorter(BitmapFrame[] subsprites)
        {
            Vector[] spritex = new Vector[subsprites.Length];
         
            int prevFrames = 0;

            for (int i = 0; i < subsprites.Length; i++)
            {
                spritex[i] = new Vector(prevFrames, 0);

                prevFrames += subsprites[i].PixelWidth;
            }
            return spritex;
        }
    }
}
