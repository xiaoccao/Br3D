using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hanee.Geometry
{
    static public class RegionHelper
    {
        // region을 x좌표 기준으로 trim한다.
        public static List<Region> TrimByX(this Region region , double x, bool trimLeft)
        {
            List<ICurve> trimCurves = new List<ICurve>();
            trimCurves.Add(LineHelper.CreateVerticalInfinitLine(x));

            if (trimLeft)
                trimCurves[0].Reverse();

            // trim을 한다.
            List<Region> trimmedRegions = new List<Region>();
            {
                Region[] regions;
                Region.Trim(region, trimCurves, out regions);
                if (regions == null || regions.Length == 0)
                {
                    trimmedRegions.Add(region);
                }
                else
                {
                    trimmedRegions.AddRange(regions);
                }
            }

            // valid regions
            List<Region> validRegions = new List<Region>();
            foreach (var trimmedRegion in trimmedRegions)
            {
                trimmedRegion.Regen(0.0001);
                double trimmedRegionX = (trimmedRegion.BoxMin.X + trimmedRegion.BoxMax.X) / 2;
                if (trimLeft && trimmedRegionX < x)
                    continue;
                if (!trimLeft && trimmedRegionX > x)
                    continue;

                trimmedRegion.Color = region.Color;
                trimmedRegion.ColorMethod = region.ColorMethod;
                validRegions.Add(trimmedRegion);
            }

            return validRegions;
        }


        /// <summary>
        /// curve상에 붙어 잇는 절점을 리턴
        /// </summary>
        /// <param name="region"></param>
        /// <param name="curve"></param>
        /// <param name="exceptEndOfCurve"> curve의 양 끝에 있는점은 제외</param>
        /// <returns></returns>
        static public List<Point3D>  ContourPointsOnCurve(Region region, ICurve curve, bool exceptEndOfCurve) 
        {
            if (region == null)
                return null;

            List<Point3D> points = new List<Point3D>();

            foreach(var c in region.ContourList)
            {
                Point3D [] curvePoints = c.GetAllPoint();
                foreach(var p in curvePoints)
                {
                    if (!p.IsOnCurve(curve, 0.05))
                        continue;

                    // curve에 붙여서 추가한다.
                    double t = 0;
                    if (!curve.Project(p, out t))
                        continue;
                    Point3D ptAttached = curve.PointAt(t);

                    // curve에 붙였는데, curve를 벗어나 있을 수 있음
                    if (!ptAttached.IsOnCurve(curve, 0.000001))
                        continue;

                    points.Add(ptAttached);
                }
            }

            // curve를 나눌수 있는 유효한 점만 리턴
            points = hanee.Geometry.Util.GetValidPointsToSplitCurve(curve, points, exceptEndOfCurve);

          



            return points;
        }
    }
}
