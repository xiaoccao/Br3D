using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Geometry
{
    public delegate void eventDimChange(string sMark, double dValue);

    public partial class GutterSectionViewer : devDept.Eyeshot.Model
    {
        public event eventDimChange DimChangeEventHandler;
        static public SelectedItem LastSelectedItem = null;
        public bool bDimEdit;
        public GutterSectionViewer() : base()
        {
            this.MouseMove += GutterSectionViewer_MouseMove;
            this.MouseDown += GutterSectionViewer_MouseDown;
            this.HandleCreated += GutterSectionViewer_HandleCreated;
            this.MouseDoubleClick += GutterSectionViewer_MouseDoubleClick;

            string lineTypeDash = "Dash";
            LineTypes.Add(lineTypeDash, new float[] { 0.02f, -0.015f });
            string lineTypeDashDot = "DashDot";
            LineTypes.Add(lineTypeDashDot, new float[] { 0.05f, -0.015f, 0.0025f, -0.015f });

            bDimEdit = true; //Dim 변경이 적용
        }

        private void GutterSectionViewer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.ActiveViewport.ZoomFit();
            }
        }

        private void GutterSectionViewer_HandleCreated(object sender, EventArgs e)
        {
            this.ActiveViewport.Pan.MouseButton = new MouseButton(MouseButtons.Middle, modifierKeys.None);
        }

        private void GutterSectionViewer_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (bDimEdit == false)
                return;

            devDept.Eyeshot.Model.SelectedItem SelectedItem = this.GetItemUnderMouseCursor(e.Location);

            if (SelectedItem != null)
            {
                SegmentDim DimEntity = SelectedItem.Item as SegmentDim;
                if (DimEntity != null && DimEntity.sMark != " ")
                {
                    var newName = XtraInputBox.Show(DimEntity.sMark, "치수 변경", DimEntity.dValue.ToString());
                    if (string.IsNullOrEmpty(newName) == false && newName.ToDouble() != DimEntity.dValue)
                    {
                        DimChangeEventHandler(DimEntity.sMark, newName.ToDouble() / 1000);                        
                    }
                }

                SegmentDimArrow DimArrowEntity = SelectedItem.Item as SegmentDimArrow;
                if (DimArrowEntity != null && DimArrowEntity.sMark != " ")
                {
                    var newName = XtraInputBox.Show(DimArrowEntity.sMark, "치수 변경", DimArrowEntity.dValue.ToString());
                    if (string.IsNullOrEmpty(newName) == false && newName.ToDouble() != DimArrowEntity.dValue)
                    {
                        DimChangeEventHandler(DimArrowEntity.sMark, newName.ToDouble());                        
                    }
                }

                SelectedItem.Select(this, false);
                this.Invalidate();
            }
        }


        private void GutterSectionViewer_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (bDimEdit == false)
                return;

            devDept.Eyeshot.Model.SelectedItem SelectedItem = this.GetItemUnderMouseCursor(e.Location);

            if (SelectedItem != null)
            {
                if (SelectedItem != LastSelectedItem)
                {
                    if (LastSelectedItem != null)
                    {
                        LastSelectedItem.Select(this, false);

                        SegmentDimArrow ArrowEntity = LastSelectedItem.Item as SegmentDimArrow;                        
                        if (ArrowEntity != null && ArrowEntity.EntityData != null)
                        {
                            if (ArrowEntity.EntityData != null)
                                SelectEntity(ArrowEntity.EntityData as EntityList, false);
                        }
                        
                        LastSelectedItem = null;
                    }

                    SegmentDim DimEntity = SelectedItem.Item as SegmentDim;
                    if (DimEntity != null && DimEntity.sMark != " ")
                    {
                        SelectedItem.Select(this, true);
                        LastSelectedItem = SelectedItem;
                    }

                    SegmentDimArrow DimArrowEntity = SelectedItem.Item as SegmentDimArrow;
                    if (DimArrowEntity != null && DimArrowEntity.sMark != " ")
                    {
                        SelectedItem.Select(this, true);

                        if (DimArrowEntity.EntityData != null)
                            SelectEntity(DimArrowEntity.EntityData as EntityList, true);

                        LastSelectedItem = SelectedItem;
                    }

                    this.Invalidate();
                }
            }
            else if (LastSelectedItem != null)
            {
                LastSelectedItem.Select(this, false);

                SegmentDimArrow ArrowEntity = LastSelectedItem.Item as SegmentDimArrow;
                if (ArrowEntity != null)
                {
                    if (ArrowEntity.EntityData != null)
                        SelectEntity(ArrowEntity.EntityData as EntityList, false);
                }

                LastSelectedItem = null;
                this.Invalidate();
            }
        }

        private void SelectEntity(EntityList Ettlist, bool bSelect)
        {
            if (Ettlist != null)
            {
                foreach (var item in Ettlist)
                {
                    item.Selected = bSelect;
                }
            }
        }
    }

   

    public class SegmentDim : LinearDim
    {
        public SegmentDim(Plane sketchPlane, Point2D extLine1, Point2D extLine2, Point2D dimLinePos, double textHeight) : base(sketchPlane, extLine1, extLine2, dimLinePos, textHeight)
        {
            tag = null;
            sMark = "";
            dValue = 0;
        }

        public double dValue { get; set; }
        public string sMark { get; set; }
        public object tag { get; set; }
    }

    public class SegmentDimArrow : Text
    {
        public SegmentDimArrow(double x, double y, string textString, double height) : base(x, y, textString, height)
        {
            tag = null;
            sMark = "";
            dValue = 0;
        }

        public double dValue { get; set; }
        public string sMark { get; set; }
        public object tag { get; set; }
    }

    public class SetionEntityList : EntityList
    {
        public enum SetionEntityType
        {
            [Description("Gutter Entity")]
            eGutterEntity,

            [Description("Slop Entity")]
            eSlopEntity,

            [Description("Virtual Entity")]
            eVirtualEntity,

            [Description("Dim Entity")]
            eDimEntity,

            [Description("DimLineInfo Entity")]
            eDimLineInfoEntity,

            [Description("DefaultRoad Entity")]
            eDefaultRoadEntity,

            [Description("DefaultSlop Entity")]
            eDefaultSlopEntity,

            [Description("InstallPoint Entity")]
            eInstallPointEntity,

            [Description("DefaultGeo Entity")]
            eDefaultGeoEntity,
        }

        public SetionEntityList(SetionEntityType eEntityType)
        {
            this.eEntityType = eEntityType;
        }

        public SetionEntityType eEntityType { get; set; }
    }
}
