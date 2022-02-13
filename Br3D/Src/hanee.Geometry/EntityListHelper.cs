using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    static public class EntityListHelper
    {
        //// entity를 추가할때 layer가 없으면 예외가 난다.
        //// 이런 예외를 던지지 않게 추가하는 함수이다.
        //static public void SafeAddRange(this EntityList entityList, IEnumerable<Entity> entities)
        //{
            
        //    foreach (var )
        //}

        // entities의 block을 깨서 일반 객체로 만들어서 리턴
        static public EntityList ExplodeEntities(this EntityList entities, Model model)
        {
            // block을 깬다.
            EntityList explodedEntities = new EntityList();
            foreach (var ent in entities)
            {
                BlockReference br = ent as BlockReference;
                if (br == null)
                {
                    explodedEntities.Add(ent);
                }
                else
                {
                    var tmp = br.ExplodeDeep(model.Blocks);
                    if (tmp != null)
                        explodedEntities.AddRange(tmp);
                }
            }
            return explodedEntities;
        }

        // test depth test always 객체로 변환
        public static EntityList ToDepthTestAlwaysEntities(this EntityList entities)
        {
            EntityList newEntities = new EntityList();
            foreach(var ent in entities)
            {
                var newEnt = ent.CloneToDepthTestAlwaysEntity();
                if (newEnt == null)
                    continue;

                newEntities.Add(newEnt);
            }

            return newEntities;
        }
        /// <summary>
        /// 객체 2D방식으로 회전 한다.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="angleInRadian"></param>
        public static void Rotate2D(this EntityList entities, double x, double y, double angleInRadian)
        {
            entities.Rotate(angleInRadian, new Vector3D(0, 0, 1), new Point3D(x, y, 0));
        }

        // entitylist를 x좌표를 기준으로 trim한다.
        public static EntityList TrimByX(this EntityList entities, double x, bool trimLeft)
        {
            List<ICurve> trimCurves = new List<ICurve>();
            trimCurves.Add(LineHelper.CreateVerticalInfinitLine(x));

            if (trimLeft)
                trimCurves[0].Reverse();

            EntityList trimmedEntities = new EntityList();
            foreach (var geo in entities)
            {
                if (geo is Region)
                {
                    Region region = (Region)geo;
                    var trimmedRegions = region.TrimByX(x, trimLeft);
                    if (trimmedRegions == null)
                        continue;

                    trimmedEntities.AddRange(trimmedRegions);
                }
                else if (geo is ICurve)
                {
                    ICurve curve = (ICurve)geo;
                    var trimmedCurves = curve.TrimByX(x, trimLeft);
                    if (trimmedCurves == null)
                        continue;

                    foreach (var tc in trimmedCurves)
                        trimmedEntities.Add(tc.ToEntity());
                }
            }

            return trimmedEntities;
        }

        
    }
}
