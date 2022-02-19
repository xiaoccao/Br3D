using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class UClick
    {
        public Point2D Position;
        public Entity Entity;
        public bool Snapped;

        public UClick(Point2D position, Entity entity = null, bool snapped = false)
        {
            Position = position;
            Entity = entity;
            Snapped = snapped;
        }

        public UClick(double x, double y)
        {
            Position = new Point2D(x, y);
            Entity = null;
            Snapped = false;
        }
    }
}
