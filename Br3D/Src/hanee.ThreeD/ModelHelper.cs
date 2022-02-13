using System;
using System.Collections.Generic;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;

namespace hanee.ThreeD
{
    public static class ModelHelper
    {
        // 기본 라인 타입을 만든다.
        static public void AddDefaultLineTypes(this Model model)
        {
            // hidden
            if (!model.LineTypes.Contains("hidden"))
            {
                model.LineTypes.Add(LineTypeHelper.hidden);
            }

            // Center
            if (!model.LineTypes.Contains("center"))
            {
                model.LineTypes.Add(LineTypeHelper.center);
            }

            // Phantom
            if (!model.LineTypes.Contains("phantom"))
            {
                model.LineTypes.Add(LineTypeHelper.phantom);
            }

        }
        // 선택한 모든 객체 리턴
        static public List<Entity> GetAllSelectedEntities(this Model model)
        {
            List<Entity> selectedEntity = new List<Entity>();
            foreach (Entity ent in model.Entities)
                if (ent.Selected)
                    selectedEntity.Add(ent);


            return selectedEntity;
        }

        static public void UnselectEntity(this Model model, Entity ent)
        {
            if (ent == null)
                return;

            ent.Selected = false;
            if (ent is BlockReference)
            {
                BlockReference block = (BlockReference)ent;
                IList<Entity> entities = block.GetEntities(model.Blocks);
                foreach (var subEnt in entities)
                {
                    model.UnselectEntity(subEnt);
                }
            }
        }

        static public void UnselectAll(this Model model)
        {
            model.SetCurrent(null);
            foreach (var ent in model.Entities)
            {
                model.UnselectEntity(ent);
            }

            foreach (var label in model.ActiveViewport.Labels)
            {
                label.Selected = false;
            }
        }

        // jittering 제거용 block 이름 리턴
        static public String GetElementJitteringBlockName(this Model model, Element element)
        {
            return $"ElementJitteringBlock_{element.id.id.ToString()}";
        }

        static public Point3D GetInsertPointByElement(this Model model, Element element)
        {
            var br = model.FindBlockReferenceByElement(element);
            if (br == null)
                return null;

            return br.InsertionPoint;
        }

        static public BlockReference FindBlockReferenceByElement(this Model model, Element element)
        {
            string blockName = model.GetElementJitteringBlockName(element);

            foreach(var ent in model.Entities)
            {
                if(ent is BlockReference)
                {
                    BlockReference br = ent as BlockReference;
                    if (br.BlockName == blockName)
                        return br;
                }
            }

            return null;
        }

       
        // element에 대해서 remove jittering을 한다.
        static public BlockReference RemoveJittering(this Model model, Element element)
        {
            var entities = hanee.ThreeD.Util.GetElementEntities(model, element);
            if (entities == null || entities.Count == 0)
                return null;

            // 모두 선택 해제
            model.UnselectAll();

            // element와 연결된 객체 선택
            foreach (var ent in entities)
            {
                ent.Selected = true;
                ent.EntityData = null;
            }

            // jittering 실행
            string blockName = model.GetElementJitteringBlockName(element);

            // 이미 블럭이 있으면 블럭을 제거하고 다시 만들어야 한다.
            if(model.Blocks.Contains(blockName))
            {
                model.Blocks.Remove(blockName);
            }

            var br = model.RemoveJittering(blockName);
            if(br != null)
            {
                element.Attach(br);
            }
            return br;
        }

    }
}
