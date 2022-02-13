using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    public class Boundary
    {
        public Boundary()
        {
            Center = new Point2D();
            Size = new Vector2D();
        }
        public Boundary(double left, double top, double right, double bottom)
        {
            Center = new Point2D();
            Size = new Vector2D();
            SetLTRB(left, top, right, bottom);
        }



        public Point2D Center { get; set; }
        public Vector2D Size { get; set; }

        public double X => Center.X;
        public double Y => Center.Y;
        public double Left => Center.X - Size.X / 2;
        public double Right => Center.X + Size.X / 2;
        public double Bottom => Center.Y - Size.Y / 2;
        public double Top => Center.Y + Size.Y / 2;
        public double Width => Right - Left;
        public double Height => Top - Bottom;

        public Point2D RightBottom => new Point2D(Right, Bottom);

        public void SetLTRB(double left, double top, double right, double bottom)
        {
            Center.X = (left + right) / 2;
            Center.Y = (top + bottom) / 2;
            Size.X = right - left;
            Size.Y = top - bottom;
        }

        public void Union(Boundary other)
        {
            double newLeft = Math.Min(Left, other.Left);
            double newRight = Math.Max(Right, other.Right);
            double newBottom = Math.Min(Bottom, other.Bottom);
            double newTop = Math.Max(Top, other.Top);
            SetLTRB(newLeft, newTop, newRight, newBottom);
        }
    }
}
