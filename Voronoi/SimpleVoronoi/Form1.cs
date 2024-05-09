using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RandomColorGenerator;
using RegionVoronoi;
using System.Threading;
using System.Runtime.InteropServices;


namespace SimpleVoronoi
{
    public partial class Form1 : Form
    {
        private bool parallelMode = true;
        private List<Thread> threads = new List<Thread>();
        private Bitmap _drawingArea;
        private object lockObject = new object();

        private int DrawingWidth => pictureBox1.Width;
        private int DrawingHeight => pictureBox1.Height;

        private VoronoiByRegion _voronoi;



        public Form1()
        {
            InitializeComponent();
            CreateNewDiagram();
        }

        private void AssignColors()
        {
            AssignRandomColors();
        }

        private void AssignRandomColors()
        {
            var colors = RandomColor.GetColors(ColorScheme.Random, Luminosity.Light,
                _voronoi.Sites.Count);
            for (int i = 0; i < _voronoi.Sites.Count; ++i)
            {
                _voronoi.Sites[i].Color = colors[i];
            }
        }

        private void AssignRandomColor(Site s)
        {
            s.Color = RandomColor.GetColor(ColorScheme.Random, Luminosity.Light);
        }

        private void CreateNewDiagram()
        {
            CreateDrawingArea();
            _voronoi = new VoronoiByRegion(pictureBox1.Bounds, (int)numberOfPoints.Value);
            AssignColors();

            if (parallelMode)
            {
                CalculateRegionsParallel();
                AssignColors();
            }
            else
            {
                _voronoi.Calculate();
            }

            DrawPicture(_voronoi);
        }
        private void CalculateRegionsParallel()
        {
            int numberOfRegionsX = 3;
            int numberOfRegionsY = 3;
            int regionWidth = _drawingArea.Width / numberOfRegionsX; 
            int regionHeight = _drawingArea.Height / numberOfRegionsY;

            threads.Clear();

            for (int i = 0; i < numberOfRegionsX; i++)
            {
                for (int j = 0; j < numberOfRegionsY; j++)
                {
                    int startX = i * regionWidth;
                    int startY = j * regionHeight;

                    Rectangle regionBounds = new Rectangle(startX, startY, regionWidth, regionHeight);
                    VoronoiByRegion regionVoronoi = new VoronoiByRegion(regionBounds, (int)numberOfPoints.Value);

                    Thread thread = new Thread(() =>
                    {
                        regionVoronoi.Calculate();
                    });

                    threads.Add(thread);
                    thread.Start();
                }
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            _voronoi = MergeRegionVoronoiResults();

            threads.Clear();
        }
        private VoronoiByRegion MergeRegionVoronoiResults()
        {
            VoronoiByRegion mergedVoronoi = new VoronoiByRegion(pictureBox1.Bounds, (int)numberOfPoints.Value);

            foreach (Thread thread in threads)
            {
                mergedVoronoi.Sites.AddRange(mergedVoronoi.Sites);
            }

            return mergedVoronoi;
        }

        private void DrawPicture(VoronoiByRegion rg)
        {
            Graphics g = Graphics.FromImage(_drawingArea);
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            rg.DrawPicture(g,_drawingArea.Width,_drawingArea.Height);

            pictureBox1.Image = _drawingArea;
            pictureBox1.Refresh();
        }

        private void CreateDrawingArea()
        {
            _drawingArea = new Bitmap(DrawingWidth,DrawingHeight);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CreateNewDiagram();
        }



        public void RemoveSite(Site s)
        {
            if (_voronoi.Sites.Count > 2)
            {
                lock (lockObject)
                {
                    _voronoi.Sites.Remove(s);
                    _voronoi.Calculate();
                    CreateDrawingArea();
                    DrawPicture(_voronoi);
                }
            }
        }
        public void RemoveSites(int count)
        {
            List<Site> Sites = new List<Site>(_voronoi.Sites);
            if (_voronoi.Sites.Count > 2)
            {
                for (int i = 0; i < count; i++)
                {
                    lock (lockObject)
                    {
                        _voronoi.Sites.Remove(Sites[i]);
                        _voronoi.Calculate();
                        CreateDrawingArea();
                        DrawPicture(_voronoi);
                    }
                }
            }
        }
        private void DeletePoints_Click(object sender, EventArgs e)
        {
            RemoveSites((int)numericUpDown1.Value);
            DrawPicture(_voronoi);
        }

        public void AddSite(Point newLocation)
        {
            Site newSite = new Site {Position = new PointF(newLocation.X, newLocation.Y)};
            AssignRandomColor(newSite);
            if (_voronoi.Sites.Contains(newSite)) return;

            _voronoi.Sites.Add(newSite);
            _voronoi.Calculate();
            CreateDrawingArea();
            DrawPicture(_voronoi);
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (deleteEdit.Checked)
            {
                Site s = _voronoi.NearestSite(e.Location);
                RemoveSite(s);
            }

            if (addEdit.Checked)
            {
                AddSite(e.Location);
            }

            if (noneEdit.Checked)
            {
                Site s = _voronoi.NearestSite(e.Location);
                var connectedSites = _voronoi.HaveCommonEdges(s);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
