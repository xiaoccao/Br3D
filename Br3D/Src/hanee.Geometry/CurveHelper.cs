using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hanee.Geometry;

namespace hanee.Geometry
{
    /// <summary>
    /// curve 관련된 공통된 도움 기능들
    /// </summary>
    static public class CurveHelper
    {
        // curve를 y좌표를 기준으로 trim한다.
        // trimDown : true - 아래쪽을 잘라내고 위쪽을 남겨서 리턴한다.
        public static List<ICurve> TrimByY(this ICurve curve, double y, bool trimDown)
        {
            Entity ent = curve as Entity;
            ICurve trimCurve = LineHelper.CreateHorizontalInfinitLine(y);
            List<ICurve> trimmedCurves = new List<ICurve>();

            // 교점을 찾아서 split한다.(없으면 그냥 추가)
            Point3D[] matchPoints = curve.IntersectWith(trimCurve);
            if (matchPoints == null || matchPoints.Length == 0)
            {
                trimmedCurves.Add(curve);
            }
            else
            {
                ICurve[] segments;
                curve.SplitBy(matchPoints, out segments);
                if (segments == null || segments.Length == 0)
                {
                    trimmedCurves.Add(curve);
                }
                else
                {
                    trimmedCurves.AddRange(segments);
                }
            }

            // split된 선분을 좌우 판단해서 필요한 것만 취한다.
            List<ICurve> validCurves = new List<ICurve>();
            foreach (var trimmedCurve in trimmedCurves)
            {
                double trimmedCurveY = (trimmedCurve.StartPoint.Y + trimmedCurve.EndPoint.Y) / 2;
                if (trimDown && trimmedCurveY < y)
                    continue;
                if (!trimDown&& trimmedCurveY > y)
                    continue;

                Entity trimmedEnt = trimmedCurve as Entity;

                if (trimmedEnt != null && ent != null)
                {
                    trimmedEnt.Color = ent.Color;
                    trimmedEnt.ColorMethod = ent.ColorMethod;
                    trimmedEnt.LineWeight = ent.LineWeight;
                    trimmedEnt.LineWeightMethod = ent.LineWeightMethod;
                }

                validCurves.Add(trimmedCurve);
            }

            return validCurves;
        }

        // curve의 x범위 안쪽만 남긴다.
        // 횡단 좌표용
        public static ICurve TrimByXRange(this ICurve curve, double minX, double maxX)
        {
            var region = Region.CreateRectangle(minX, -LineHelper.GetInfiniteLength(), maxX - minX, LineHelper.GetInfiniteLength()*2);
            var curves = curve.TrimByRegion(region, true);
            if (curves == null || curves.Count == 0)
                return null;

            return curves[0];
        }

        // curve를 x좌표를 기준으로 trim한다.
        public static List<ICurve> TrimByX(this ICurve curve, double x, bool trimLeft)
        {
            Entity ent = curve as Entity;
            ICurve trimCurve = LineHelper.CreateVerticalInfinitLine(x);
            List<ICurve> trimmedCurves = new List<ICurve>();

            // 교점을 찾아서 split한다.(없으면 그냥 추가)
            Point3D[] matchPoints = curve.IntersectWith(trimCurve);
            if(matchPoints == null || matchPoints.Length == 0)
            {
                trimmedCurves.Add(curve);
            }
            else
            {
                ICurve[] segments;
                curve.SplitBy(matchPoints, out segments);
                if (segments == null || segments.Length == 0)
                {
                    trimmedCurves.Add(curve);
                }
                else
                {
                    trimmedCurves.AddRange(segments);
                }
            }

            // split된 선분을 좌우 판단해서 필요한 것만 취한다.
            List<ICurve> validCurves = new List<ICurve>();
            foreach (var trimmedCurve in trimmedCurves)
            {
                double trimmedCurveX = (trimmedCurve.StartPoint.X + trimmedCurve.EndPoint.X) / 2;
                if (trimLeft && trimmedCurveX < x)
                    continue;
                if (!trimLeft && trimmedCurveX > x)
                    continue;

                Entity trimmedEnt = trimmedCurve as Entity;
                
                if(trimmedEnt != null && ent != null)
                {
                    trimmedEnt.Color = ent.Color;
                    trimmedEnt.ColorMethod = ent.ColorMethod;
                    trimmedEnt.LineWeight = ent.LineWeight;
                    trimmedEnt.LineWeightMethod = ent.LineWeightMethod;
                }
                
                validCurves.Add(trimmedCurve);
            }

            return validCurves;
        }

        public static void TransformBy(this ICurve curve, Transformation trans)
        {
            var ent = curve.ToEntity();
            if (ent == null)
                return;
            ent.TransformBy(trans);
        }

        /// <summary>
        /// icurve를 entity로 변환해서 리턴
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public static Entity ToEntity(this ICurve curve)
        {
            if (curve is Line)
                return curve as Line;
            else if (curve is LinearPath)
                return curve as LinearPath;
            else if (curve is Arc)
                return curve as Arc;
            else if (curve is Circle)
                return curve as Circle;
            else if (curve is CompositeCurve)
                return curve as CompositeCurve;

            return null;
        }

        // point의 개수를 리턴
        public static int GetPointNums(this ICurve curve)
        {
            var points = curve.GetAllPoint();
            return points == null ? 0 : points.Length;
        }

        /// <summary>
        /// curve의 모든 절점을 2D좌표로 리턴한다.
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public static Point2D[] GetAllPoint2D(this ICurve curve)
        {
            Point3D[] points3D = GetAllPoint(curve);
            if (points3D == null)
                return null;

            List<Point2D> points2D = new List<Point2D>();
            foreach (var pt in points3D)
            {
                points2D.Add(pt.To2D());
            }

            return points2D.ToArray();
        }

        /// <summary>
        /// curve의 모든 절점을 2D좌표로 리턴한다.
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public static Point3D[] GetAllPoint(this ICurve curve)
        {
            var points = new List<Point3D>();
            if (curve is Line)
            {
                points.Add(curve.StartPoint);
                points.Add(curve.EndPoint);
            }
            else if (curve is LinearPath)
            {
                LinearPath lp = curve as LinearPath;
                foreach (var pt in lp.Vertices)
                {
                    points.Add(pt);
                }
            }
            else if (curve is CompositeCurve)
            {
                var cc = curve as CompositeCurve;
                foreach (var c in cc.CurveList)
                {
                    Point3D[] subPoints = c.GetAllPoint();
                    if (subPoints != null)
                        points.AddRange(subPoints);
                }

            }

            return points.ToArray();
        }

        // 제일 왼쪽 좌표
        static public Point3D GetLeftPoint(this ICurve curve)
        {
            var points = curve.GetAllPoint();
            if (points == null || points.Length == 0)
                return null;
             
            var orderedPoints = points.OrderBy(x => x.X);
            if (orderedPoints == null || orderedPoints.Count() == 0)
                return null;

            return orderedPoints.ElementAt(0);
        }
        
        // 제일 오른쪽 좌표
        static public Point3D GetRightPoint(this ICurve curve)
        {
            var points = curve.GetAllPoint();
            if (points == null || points.Length == 0)
                return null;

            var orderedPoints = points.OrderBy(x => x.X);
            if (orderedPoints == null || orderedPoints.Count() == 0)
                return null;

            return orderedPoints.ElementAt(0);
        }

        static public Point3D GetLeftPoint(this List<ICurve> curves)
        {
            var leftPoints = curves.ConvertAll(x => x.GetLeftPoint());
            leftPoints.RemoveAll(x => x == null);
            var orderedPoints = leftPoints.OrderBy(x => x.X);
            return orderedPoints.ElementAtOrDefault(0);
        }

        static public Point3D GetRightPoint(this List<ICurve> curves)
        {
            var rightPoints = curves.ConvertAll(x => x.GetRightPoint());
            rightPoints.RemoveAll(x => x == null);
            var orderedPoints = rightPoints.OrderByDescending(x => x.X);
            return orderedPoints.ElementAtOrDefault(0);
        }

        // 다른 curve를 합친다.
        // 합치면 linearpath로 리턴이 된다.
        static public LinearPath Merge(this ICurve curve, ICurve otherCurve)
        {
            var points1 = curve.GetAllPoint();
            var points2 = otherCurve.GetAllPoint();
            List<Point3D> points = new List<Point3D>();
            points.AddRange(points1);
            points.AddRange(points2);

            points = Util.GetUnduplicatedPoints(points);
            return new LinearPath(points);
        }

        // curves 와 curve의 첫번째 교점을 리턴
        static public Point3D FindFirstMatchPointWithCurve(this List<ICurve> curves, ICurve curve)
        {
            foreach (var c in curves)
            {
                var matchPoints = c.IntersectWith(curve);
                if (matchPoints == null || matchPoints.Length == 0)
                    continue;

                return matchPoints.First();
            }

            return null;
        }

        /// <summary>
        /// curve를 cut_curve를 기준으로 위아래로 나눈다.
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="cut_curves"></param>
        /// <param name="returnBeforeIntersection"> 교점 이전 curve를 리턴할지? </param>
        /// <param name="tol"></param>
        /// <returns></returns>
        static public bool SplitCurveByCurves(ICurve curve, List<ICurve> cut_curves, out ICurve lower, out ICurve upper)
        {
            lower = null;
            upper = null;

            if (curve == null)
                return false;

            Entity ent = curve.ToEntity();

            // 교점을 찾는다.
            Point3D matchPoint = cut_curves.FindFirstMatchPointWithCurve(curve);

            // 교점이 없으면 가장 왼쪽 점과 가장 오른쪽 점을 옆으로 늘려서 교점을 찾는다.
            if (matchPoint == null)
            {
                Point3D leftPoint = cut_curves.GetLeftPoint();
                Point3D rightPoint = cut_curves.GetRightPoint();
                if (leftPoint != null && rightPoint != null)
                {
                    var leftLine = LineHelper.CreateOneDirHorizontalInfinitLine(leftPoint.X, leftPoint.Y, true);
                    var rightLine = LineHelper.CreateOneDirHorizontalInfinitLine(rightPoint.X, rightPoint.Y, false);
                    List<ICurve> tmpCurves = new List<ICurve>();
                    tmpCurves.AddRange(cut_curves);
                    tmpCurves.Add(leftLine);
                    tmpCurves.Add(rightLine);
                    matchPoint = tmpCurves.FindFirstMatchPointWithCurve(curve);

                    // 그래도 null 이면 위로 뻣어서 검사
                    if (matchPoint == null)
                    {
                        // 교점이 없으면 점한개를 검사해서 기준선 위에 있는지 아래에 있는지 판단한다.
                        var curves = FindVerticalMatchCurvesFromPoint(tmpCurves, curve.StartPoint, true);
                        if (curves != null && curves.Count > 0)
                        {
                            lower = ent?.Clone() as ICurve;
                            upper = null;
                        }
                        else
                        {
                            lower = null;
                            upper = ent?.Clone() as ICurve;
                        }
                        return true;
                    }
                }
            }


            // 교점을 기준으로 나눈다.
            if(matchPoint != null && !curve.SplitBy(matchPoint, out lower, out upper))
            {
                double tol = 0.001;

                
                // 안 나눠지면 끝쩜에 붙어 있는 경우이다.
                if (curve.StartPoint.Equals(matchPoint, tol))
                {
                    if (curve.StartPoint.Y > curve.EndPoint.Y)
                    {
                        lower = ent?.Clone() as ICurve;
                        upper = null;
                    }
                    else
                    {
                        lower = null;
                        upper = ent?.Clone() as ICurve;
                    }
                }
                else
                {
                    if (curve.StartPoint.Y > curve.EndPoint.Y)
                    {

                        lower = null;
                        upper = ent?.Clone() as ICurve;
                    }
                    else
                    {

                        lower = ent?.Clone() as ICurve;
                        upper = null;
                    }
                }
                
            }

            return true;
        }



        // z값을 0으로 만든다.
        static public void Make2DCurve(this ICurve curve)
        {
            var ent = curve.ToEntity();
            foreach (var v in ent.Vertices)
                v.Z = 0;
        }

        static public ICurve Clone(this ICurve curve)
        {
            return curve.ToEntity().Clone() as ICurve;
        }
        // curve를 여러개의 region 으로 trim한다.
        static public List<ICurve> TrimByRegions(this ICurve curve, List<Region> regions, bool returnInside, double tol=0.001)
        {
            // 모든 교점
            List<Point3D> points = new List<Point3D>();
            foreach(var r in regions)
            {
                var tmp = curve.IntersectWith(r);
                if (tmp == null)
                    continue;
                points.AddRange(tmp);
            }

            // 교점으로 나눈다.
            List<ICurve> curves = new List<ICurve>();
            ICurve[] splitCurves = splitCurves = new List<ICurve>() { curve }.ToArray();
            if (points.Count == 0 || curve.SplitBy(points, out splitCurves))
            {
                foreach (var c in splitCurves)
                {
                    c.Make2DCurve();
                    var pt = c.PointAt(c.Length() / 2);
                    bool isInside = pt.IsInsideRegions(regions);// region.IsPointInside(pt);
                    if (returnInside != isInside)
                        continue;

                    curves.Add(c);
                }
            }

            return curves;
        }

        static public List<Point3D> IntersectWith(this ICurve curve, Region region)
        {
            List<Point3D> points = new List<Point3D>();
            foreach (var c in region.ContourList)
            {
                var matchPoints = curve.IntersectWith(c);
                if (matchPoints == null)
                    continue;
                points.AddRange(matchPoints);
            }

            return points;
        }

        static public List<ICurve> TrimByRegion(this ICurve curve, Region region, bool returnInside, double tol=0.001)
        {
            if (region.ContourList.Count == 0)
                return null;

            List<ICurve> curves = new List<ICurve>();
            List<Point3D> points = curve.IntersectWith(region);
            

            // 교점으로 나눈다.
            ICurve[] splitCurves;
            var result = curve.SplitBy(points, out splitCurves);
            if(!result)
            {
                splitCurves = new List<ICurve>() { curve }.ToArray();
            }
            
            foreach (var c in splitCurves)
            {
                c.Make2DCurve();
                var pt = c.PointAt(c.Length() / 2);
                bool isInside = region.IsPointInside(pt);
                if (returnInside != isInside)
                    continue;

                curves.Add(c);
            }
            
            return curves;
        }
        /// <summary>
        /// region으로 curve를 trim한다.
        /// region의 edge에 붙어 있는것들을 trim한다.
        /// </summary>
        /// <param name="lp"></param>
        /// <param name="enumerable"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        static public List<ICurve> TrimCurveByRegionEdges(ICurve curve, List<Region> regions, bool returnInside, double tol=0.001)
        {
            List<ICurve> curves = new List<ICurve>();
            foreach (var region in regions)
            {
                // region에 붙어 있는 점
                List<Point3D> points = RegionHelper.ContourPointsOnCurve(region, curve, false);

                // 점이 2개가 있었는데, 유효한 점만 남긴후 사라진다면 curve가 완전히 region에 겹쳐져 있다는 의미임
                int count = points.Count();
                // curve를 나눌수 있는 유효한 점만 리턴
                points = hanee.Geometry.Util.GetValidPointsToSplitCurve(curve, points, true);
                if(count == 2 && (points == null || points.Count == 0))
                {
                    curves.Clear();
                    curves.Add(curve);
                    break;
                }


                // 붙어 있는 점으로 나눈다.
                ICurve[] splitCurves;
                if (curve.SplitBy(points, out splitCurves))
                {
                    foreach (var c in splitCurves)
                    {
                        if (!region.IsPointOnContour(c.PointAt(c.Length()/2), tol))
                            continue;

                        curves.Add(c);
                    }
                }
                // 나누기 실패시 바로 판단하고 리턴시킨다.
                else
                {
                    if(points.Count > 0)
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    if (region.IsPointInside(curve.PointAt(0.5)) == returnInside)
                    {
                        Entity ent = curve.ToEntity();
                        if(ent != null)
                        {
                            curves.Add(ent.Clone() as ICurve);
                            
                        }
                        return curves;

                    }
                }
            }

            return curves;
        }

        /// <summary>
        /// point를 curve 진행방향대로 정렬
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<Point3D> SortPointsOn(ICurve curve, List<Point3D> points)
        {
            // 거리, 좌표
            SortedDictionary<double, Point3D> pointWithDistances = new SortedDictionary<double, Point3D>();
            foreach(var p in points)
            {
                double dist = 0;
                if (!curve.Project(p, out dist))
                    continue;

                // 이미 있는 좌표는 추가하지  않는다.
                if (pointWithDistances.ContainsKey(dist))
                    continue;

                pointWithDistances.Add(dist, p);
            }

            List<Point3D> sortedPoints = new List<Point3D>();
            foreach(var pd in pointWithDistances)
            {
                sortedPoints.Add(pd.Value);
            }

            return sortedPoints;
        }

      

        /// <summary>
        /// curve의 start end point들의 최소 최대 좌표를 계산한다.
        /// curve만 빠르게 계산하고자 하는 용도이다.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        static public bool CalcMinMaxOfStartEndPointOfCurves(EntityList entities, out Point3D min, out Point3D max)
        {
            bool first = true;

            min = new Point3D();
            max = new Point3D();

            foreach(var ent in entities)
            {
                if (!(ent is ICurve))
                    continue;

                ICurve curve = ent as ICurve;
                if (first)
                {
                    min.X = Math.Min(curve.StartPoint.X, curve.EndPoint.X);
                    min.Y = Math.Min(curve.StartPoint.Y, curve.EndPoint.Y);

                    max.X = Math.Max(curve.StartPoint.X, curve.EndPoint.X);
                    max.Y = Math.Max(curve.StartPoint.Y, curve.EndPoint.Y);
                    first = false;
                }
                else
                {
                    min.X = Math.Min(curve.StartPoint.X, min.X);
                    min.X = Math.Min(curve.EndPoint.X, min.X);

                    min.Y = Math.Min(curve.StartPoint.Y, min.Y);
                    min.Y = Math.Min(curve.EndPoint.Y, min.Y);

                    max.X = Math.Max(curve.StartPoint.X, max.X);
                    max.X = Math.Max(curve.EndPoint.X, max.X);

                    max.Y = Math.Max(curve.StartPoint.Y, max.Y);
                    max.Y = Math.Max(curve.EndPoint.Y, max.Y);
                }

            }

            return first ? false : true;
        }

        /// <summary>
        /// 점에서 위아래로 걸리는 교점 찾기
        /// </summary>
        /// <param name="curves"></param>
        /// <param name="center"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static List<Point3D> FindVerticalMatchPointsFromPoint(List<ICurve> curves, Point3D point)
        {
            List<Point3D> matchPoints = new List<Point3D>();

            Line verticalLine = LineHelper.CreateVerticalInfinitLine(point.X);
            if (verticalLine == null)
                return matchPoints;

            foreach (var curve in curves)
            {
                Point3D[] points = curve.IntersectWith(verticalLine);
                if (points == null || points.Length == 0)
                {
                    continue;
                }

                // 체크점과 다른 좌표이어야 함
                foreach (var p in points)
                {
                    if (p.Equals(point, 0.001))
                        continue;
                    matchPoints.Add(p);
                    break;
                }
            }


            return matchPoints;
        }


        /// <summary>
        /// 점에서 위아래로 걸리는 curve 찾기
        /// </summary>
        /// <param name="curves"></param>
        /// <param name="center"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static List<ICurve> FindVerticalMatchCurvesFromPoint(List<ICurve> curves, Point3D point, bool toUpper)
        {
            List<ICurve> matchCurves = new List<ICurve>();

            Line verticalLine = LineHelper.CreateOneDirVerticalInfinitLine(point.X, point.Y, toUpper);
            if (verticalLine == null)
                return matchCurves;

            foreach(var curve in curves)
            {
                Entity ent = curve as Entity;
                if (ent == null)
                    continue;
                if(ent.BoxMin == null || ent.BoxMax == null)
                {
                    ent.Regen(0.0001);
                }

                if (ent.BoxMin.X > point.X || ent.BoxMax.X < point.X)
                    continue;

              
                Point3D [] points = curve.IntersectWith(verticalLine);
                if (points == null || points.Length == 0)
                {
                    continue;
                }
                
                // 체크점과 다른 좌표이어야 함
                bool differentPoint = false;
                foreach(var p in points)
                {
                    if (p.Equals(point, 0.001))
                        continue;
                    differentPoint = true;
                    break;
                }
                if(differentPoint)
                    matchCurves.Add(curve);
            }


            return matchCurves;
        }
    }
}
