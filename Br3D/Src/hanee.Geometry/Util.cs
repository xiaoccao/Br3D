using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace hanee.Geometry
{
    public class Util
    {
        static public bool IsCCW2D(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double temp = x1 * y2 + x2 * y3 + x3 * y1; 
            temp = temp - y1 * x2 - y2 * x3 - y3 * x1; 
            // 반시계
            if (temp > 0) 
            { 
                return true; 
            } 
            // 시계
            else if (temp < 0) 
            { 
                return false; 
            } 
            // 일직선
            else 
            { 
                return false; 
            }

        }

        /// <summary>
        /// 여러 point의 중심 좌표 리턴
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        static public Point3D GetCenter(List<Point3D> points)
        {
            Point3D center = new Point3D();
            foreach(var p in points)
            {
                center += p;
            }

            center = center / points.Count;

            return center;
        }
        // Point2D까지 모드 clone해서 리턴
        static public Point2D[] CloneArray(Point2D[] points)
        {
            Point2D[] newPoints = new Point2D[points.Length];
            for(int i = 0; i < points.Length; ++i)
            {
                newPoints[i] = points[i].Clone() as Point2D;
            }
            return newPoints;
        }

        // 연속된 중복좌표를 제거해서 리턴
        static public List<double> GetUnduplicatedDoubles(List<double> values, double tol=0.00001)
        {
            if (values == null)
                return null;

            for (int i = 0; i < values.Count; ++i)
            {
                var pt1 = i > 0 ? values[i - 1] : values.Last();
                var pt2 = values[i];
                if (pt1.Equals(pt2, tol))
                    values.RemoveAt(i--);

                if (values.Count == 1)
                    break;
            }

            return values;
        }

        /// <summary>
        /// 연속된 중복 좌표를 제거한다.(시점과 종점도 비교..)
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        static public List<Point3D> GetUnduplicatedPoints(List<Point3D> points, double tol=0.0001)
        {
            if (points == null)
                return null;

            for(int i = 0; i < points.Count; ++i)
            {
                Point3D pt1 = i > 0 ? points[i - 1] : points.Last();
                Point3D pt2 = points[i];
                if (pt1.Equals(pt2, tol))
                    points.RemoveAt(i--);

                if (points.Count == 1)
                    break;
            }

            return points;
        }

        /// <summary>
        /// curve를 split할 수 있는 유효한 점들을 리턴
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<Point3D> GetValidPointsToSplitCurve(ICurve curve, List<Point3D> points, bool exceptEndOfCurve=true)
        {
            // contour순서대로 정렬
            points = CurveHelper.SortPointsOn(curve, points);

            // 중복 좌표 제거
            points = hanee.Geometry.Util.GetUnduplicatedPoints(points);

            // 시종점 좌표 제거
            if(exceptEndOfCurve)
            {
                if (points != null && points.Count > 0)
                {
                    if (points.First().Equals(curve.StartPoint, 0.001))
                    {
                        points.RemoveAt(0);
                    }
                }

                if (points != null && points.Count > 0)
                {
                    if (points.Last().Equals(curve.EndPoint, 0.001))
                    {
                        points.RemoveAt(points.Count - 1);
                    }
                }
            }
            

            return points;
        }

        /// <summary>
        /// 교차점으로 curve을 나눈다.
        /// </summary>
        /// <param name="curves"></param>
        /// <returns></returns>
        public static List<ICurve> SplitCurvesByIntersection(List<ICurve> curves)
        {
            List<ICurve> dividedCurves = new List<ICurve>();

            foreach (var c1 in curves)
            {
                // c1에 대해서 모든 match point를 찾는다.
                List<Point3D> matchPoints = new List<Point3D>();
                foreach (var c2 in curves)
                {
                    if (c1 == c2)
                        continue;

                    Point3D[] tmpMatchPoints = c1.IntersectWith(c2);
                    if (tmpMatchPoints == null)
                        continue;

                    matchPoints.AddRange(tmpMatchPoints);
                }


                // curve를 split할 수 있는 유효한 점만 추출
                matchPoints = GetValidPointsToSplitCurve(c1, matchPoints);
              


                if (matchPoints != null && matchPoints.Count > 0)
                {
                    // match point들로 divide 한다.
                    ICurve[] tmpDividedCurves = null;
                    c1.SplitBy(matchPoints, out tmpDividedCurves);

                    if (tmpDividedCurves != null)
                        dividedCurves.AddRange(tmpDividedCurves);
                }
                else
                {
                    dividedCurves.Add(c1);
                }


            }

            return dividedCurves;
        }
    }
}
