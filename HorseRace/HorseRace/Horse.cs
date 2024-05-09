using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HorseRace
{
    public class Horse
    {
        private string name;
        private Color colour;
        private float odds;
        private float rawOdds;
        private double speed ;
        private Point coords = new Point(100, 120);
        private Image image;

        private static double trackLength = 200;
        private static double speedFactor = 0.015;


        public string Name => name;

        public Color Colour
        {
            get => colour;
            set => colour = value;
        }

        public float Odds
        {
            get => odds;
            set => odds = value;
        }

        public float RawOdds => rawOdds;

        public Image IMG => image;

        public Point Coords
        {
            get => coords;
            set => coords = value;
        }

        public static double TrackLength
        {
            get => trackLength;
            set => trackLength = value;
        }
        public Horse(string horseName, Color horseColour, Random seed)
        {
            name = horseName;
            colour = horseColour;
            speed = seed.NextDouble(); 
            odds = (float)Math.Round(seed.NextDouble() * speed, 4); 
            rawOdds = odds;
            image = new Image();
            Uri tempUri;
            try
            {
                string path = @"horseIMGscaled.gif";
                tempUri = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, path));
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Unable to use image file \"horseIMGscaled.gif\"!");
                throw;
            } 
            try
            {
                image.Source = ColouringTool(tempUri, colour);
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Colouring tool broke");
                throw;
            } 
            UpdateImage();
        }

        public void Move()
        {
            coords.X += (trackLength * (Math.Sqrt(speed)) * speedFactor);
            UpdateImage();
        }

        public static double LineX(double y, Line line)
        {
            double m = (line.Y2 - line.Y1) / (line.X2 - line.X1);
            double b = (line.Y1 - (m * line.X1));

            return (y - b) / m;
        }

        public void UpdateImage()
        {
            image.Margin = new Thickness(coords.X - 50, coords.Y, 0, 0);
        }

        public async Task ChangeAcceleration()
        {
            Random rand = new Random();
            await Task.Delay(rand.Next(100, 1000)); 
            double acceleration = rand.NextDouble() * (1 - 0.7) + 0.2; 
            speed *= acceleration;
        }

        private BitmapSource ColouringTool(Uri imageUri, Color imageColour)
        {
            BitmapImage bitmap = new BitmapImage(imageUri); 
            int bitmapHeight = bitmap.PixelHeight; 
            int bitmapWidth = bitmap.PixelWidth;
            PixelFormat pixelFormat = PixelFormats.Indexed8; 
            int stride = bitmapWidth * ((bitmap.Format.BitsPerPixel + 7) / 8);

            List<Color> originalPalette = bitmap.Palette.Colors.ToList();

            
            List<Color> paletteList = new List<Color>() 
            {
                Colors.Transparent
            };
            for (int i = 1; i < originalPalette.Count; i++) 
            {
                paletteList.Add(imageColour);
            }
            BitmapPalette palette = new BitmapPalette(paletteList); 

            WriteableBitmap wBitmap = new WriteableBitmap(bitmap); 
            byte[] rawPixelData = new byte[bitmapHeight * stride]; 
            wBitmap.CopyPixels(rawPixelData, stride, 0); 

            BitmapSource sBitmap = BitmapSource.Create(bitmapWidth, bitmapHeight, bitmap.DpiX, bitmap.DpiY, pixelFormat, palette, rawPixelData, stride);
            return sBitmap;
        }
    }
}
