using System;
using System.Collections.Generic;
using System.Drawing;

namespace RegionVoronoi
{
    public class Site : IEquatable<Site>
    {
        public PointF Position { get; set; }
        public Color Color { get; set; }
        public List<Point> RegionPoints { get; set; }

        public bool Equals(Site other) => other != null && Position == other.Position;
    }
}
