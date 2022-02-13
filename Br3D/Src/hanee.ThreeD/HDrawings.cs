using System;
using System.Drawing;
using System.Windows.Forms;
using devDept.Eyeshot;
using devDept.Geometry;
using devDept.Graphics;

namespace hanee.ThreeD
{
    [Serializable]
    public class HDrawings : devDept.Eyeshot.Drawings
    {
        static public string dimensionLayerName = "dimension";

        // Current snapped point, which is one of the vertex from model
        private Point3D snapPoint { get; set; }

        // Flags to indicate current snapping mode
        // snap을 활성화 하려면 이 속성을 TRUE를 줘야한다.(프로그램 시작할때 주면 된다)
        public bool objectSnapEnabled { get; set; }

        public static Color drawingColor = Color.Black;
        public static Font drawingFont = new Font("Tahoma", 10.0f, FontStyle.Bold);

        public Snapping Snapping;

        // property grid를 지정하면 객체 선택시 property grid에 속성이 표시됨
        public PropertyGridHelper propertyGridHelper { get; set; }

        public HDrawings()
        {
            Snapping = new Snapping(this);
            Snapping.objectSnapEnabled = true;
            Snapping.SetActiveObjectSnap(Snapping.objectSnapType.End, true);

            Pan.MouseButton = new MouseButton(MouseButtons.Middle, modifierKeys.None);

            InitDefaultLayers();
            InitDefaultSheets();
        }

        /// <summary>
        /// 기본 레이어
        /// </summary>
        public void InitDefaultLayers()
        {
            if (!Layers.Contains(dimensionLayerName))
            {
                Layers.Add(new Layer(dimensionLayerName, Color.Blue));
            }
        }

        /// <summary>
        /// 기본 시트상태로 초기화 한다.
        /// </summary>
        public void InitDefaultSheets()
        {
            // 시트가 하나도 없는 상태라면 하나를 기본으로 만들어 준다.
            if (Sheets.Count == 0)
            {
                // 시트 추가
                var size = SheetHelper.GetFormatSize(linearUnitsType.Millimeters, SheetHelper.formatType.A0_ISO);
                Sheet newSheet = new Sheet(linearUnitsType.Millimeters, size.Item1, size.Item2, $"Sheet{Sheets.Count + 1}");
                Sheets.Add(newSheet);

                ActiveSheet = newSheet;
            }

        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (e.Button == MouseButtons.Middle)
            {
                ZoomFit();
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //base.OnMouseMove(e);

            ActionBase.MouseMoveHandler(this, e);

            // If ObjectSnap is ON, we need to find closest vertex (if any)
            if (ActionBase.IsNeedSnapping())
            {
                Snapping.OnMouseMoveForSnap(e);
            }

            // draw overlay가 호출된다.(back buffer에 그림)
            PaintBackBuffer();

            Invalidate();
        }

        public void RunPaintBackBuffer()
        {
            PaintBackBuffer();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (ActionBase.IsUserInputting())
            {
                ActionBase.MouseDownHandler(this, e);
            }
            else
            {
                // 객체 선택 해제
                Entities.ClearSelection();

                // 객체 선택
                var item = GetItemUnderMouseCursor(e.Location, true);
                if (item != null)
                {
                    item.Select(this, true);
                    UpdatePropertyGridControl(item.Item);
                }
                else
                {
                    UpdatePropertyGridControl(null);
                }


            }
        }

        /// <summary>
        /// 선택되어 있는 객체로 property grid를 갱신한다.
        /// </summary>
        private void UpdatePropertyGridControl(Object obj)
        {
            if (propertyGridHelper == null)
                return;

            propertyGridHelper.UpdatePropertyGridControl(obj);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (ActionBase.IsUserInputting())
            {
                ActionBase.KeyUpHandler(e);
            }
            else
            {
                base.OnKeyUp(e);

                if (e.KeyCode == Keys.Escape)
                {
                    Entities.ClearSelection();
                    UpdatePropertyGridControl(null);
                    Invalidate();
                }

            }
        }

        protected override void DrawOverlay(DrawSceneParams data)
        {
            if (ActionBase.IsUserInputting() == true)
            {
                // 사용자 입력중일때만 draw overlay
                Snapping.DrawOverlayForSnap();

                renderContext.EnableXOR(true);

                renderContext.SetState(depthStencilStateType.DepthTestOff);

                if (ActionBase.cursorText != null)
                    DrawMouseText(ActionBase.cursorText, ActionBase.CurrentMousePoint);

                // preview entity가 있다면 그걸 그린다.
                devDept.Eyeshot.Environment env = this;
                if (ActionBase.PreviewEntities != null)
                    env.DrawPreviewEntity();
                if (ActionBase.PreviewFaceEntities != null)
                    env.DrawPreviewEntity(false);


                renderContext.EnableXOR(false);
            }

            base.DrawOverlay(data);
        }

        // 마우스 옆에 따라 다니는 text를 그린다.
        public void DrawMouseText(string text, System.Drawing.Point location)
        {
            renderContext.EnableXOR(false);

            DrawText(location.X, (int)Size.Height - location.Y + 10,
                 text, drawingFont, drawingColor, ContentAlignment.BottomLeft);



            renderContext.EnableXOR(true);
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
