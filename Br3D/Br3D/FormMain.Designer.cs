
namespace Br3D
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement10 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement5 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement6 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement7 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement8 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement9 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement11 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement12 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement13 = new DevExpress.XtraEditors.TileItemElement();
            devDept.Eyeshot.CancelToolBarButton cancelToolBarButton1 = new devDept.Eyeshot.CancelToolBarButton("Cancel", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ProgressBar progressBar1 = new devDept.Eyeshot.ProgressBar(devDept.Eyeshot.ProgressBar.styleType.Circular, 0, "Idle", System.Drawing.Color.Black, System.Drawing.Color.Transparent, System.Drawing.Color.Green, 1D, true, cancelToolBarButton1, false, 0.1D, 0.333D, true);
            devDept.Graphics.BackgroundSettings backgroundSettings1 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.LinearGradient, System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(82)))), ((int)(((byte)(103))))), System.Drawing.Color.DodgerBlue, System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(32)))), ((int)(((byte)(41))))), 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.33D);
            devDept.Eyeshot.Camera camera1 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(0D, 0D, 45D), 380D, new devDept.Geometry.Quaternion(0.018434349666532526D, 0.039532590434972079D, 0.42221602280006187D, 0.90544518284475428D), devDept.Graphics.projectionType.Perspective, 40D, 6.4499992392052707D, false, 0.001D);
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton1 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton1 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton1 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton1 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton1 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton1 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton1 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar1 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton1))});
            devDept.Eyeshot.ToolBarButton toolBarButton1 = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.wireframe_32x, "Wireframe", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
            devDept.Eyeshot.ToolBarButton toolBarButton2 = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.hiddenline_32x, "HiddenLine", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
            devDept.Eyeshot.ToolBarButton toolBarButton3 = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.shaded_32x, "Shade", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
            devDept.Eyeshot.ToolBarButton toolBarButton4 = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.rendered_32x, "Render", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
            devDept.Eyeshot.ToolBar toolBar2 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.VerticalTopLeft, true, new devDept.Eyeshot.ToolBarButton[] {
            toolBarButton1,
            toolBarButton2,
            toolBarButton3,
            toolBarButton4});
            devDept.Eyeshot.Grid grid1 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point2D(-100D, -100D), new devDept.Geometry.Point2D(100D, 100D), 10D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), false, true, false, false, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));
            devDept.Eyeshot.OriginSymbol originSymbol1 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", true, null, false);
            devDept.Eyeshot.RotateSettings rotateSettings1 = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 10D, true, 1D, devDept.Eyeshot.rotationType.Turntable, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.ZoomSettings zoomSettings1 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10, true);
            devDept.Eyeshot.PanSettings panSettings1 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings1 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon1 = new devDept.Eyeshot.CoordinateSystemIcon(new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, null, false);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon1 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, true, System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(60))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, null, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false, new devDept.Geometry.Quaternion(0D, 0D, 0D, 1D));
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager1 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
            devDept.Eyeshot.Viewport viewport1 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(820, 645), backgroundSettings1, camera1, new devDept.Eyeshot.ToolBar[] {
            toolBar1,
            toolBar2}, new devDept.Eyeshot.Legend[0], devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            grid1}, new devDept.Eyeshot.OriginSymbol[] {
            originSymbol1}, false, rotateSettings1, zoomSettings1, panSettings1, navigationSettings1, coordinateSystemIcon1, viewCubeIcon1, savedViewsManager1, devDept.Eyeshot.viewType.Trimetric);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tileNavPane1 = new DevExpress.XtraBars.Navigation.TileNavPane();
            this.navButtonMain = new DevExpress.XtraBars.Navigation.NavButton();
            this.tileNavCategoryAnnotation = new DevExpress.XtraBars.Navigation.TileNavCategory();
            this.tileNavItemCoordinates = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItemDistance = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItemClearAnnotations = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavCategoryOsnap = new DevExpress.XtraBars.Navigation.TileNavCategory();
            this.tileNavItemEnd = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItemIntersection = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItemMiddle = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItemCenter = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItemPoint = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItemOpen = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItemSaveAs = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItemSaveImage = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.hModel1 = new hanee.ThreeD.HModel();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanelObjectTree = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.treeListObject = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.tileNavPane1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hModel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanelObjectTree.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListObject)).BeginInit();
            this.SuspendLayout();
            // 
            // tileNavPane1
            // 
            this.tileNavPane1.Buttons.Add(this.navButtonMain);
            this.tileNavPane1.Buttons.Add(this.tileNavCategoryAnnotation);
            this.tileNavPane1.Buttons.Add(this.tileNavCategoryOsnap);
            // 
            // tileNavCategory1
            // 
            this.tileNavPane1.DefaultCategory.Items.AddRange(new DevExpress.XtraBars.Navigation.TileNavItem[] {
            this.tileNavItemOpen,
            this.tileNavItemSaveAs,
            this.tileNavItemSaveImage});
            this.tileNavPane1.DefaultCategory.Name = "tileNavCategory1";
            // 
            // 
            // 
            this.tileNavPane1.DefaultCategory.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            this.tileNavPane1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tileNavPane1.Location = new System.Drawing.Point(0, 0);
            this.tileNavPane1.Name = "tileNavPane1";
            this.tileNavPane1.Size = new System.Drawing.Size(1044, 43);
            this.tileNavPane1.TabIndex = 2;
            this.tileNavPane1.Text = "tileNavPane1";
            this.tileNavPane1.ElementClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.tileNavPane1_ElementClick);
            // 
            // navButtonMain
            // 
            this.navButtonMain.Caption = "Main Menu";
            this.navButtonMain.IsMain = true;
            this.navButtonMain.Name = "navButtonMain";
            this.navButtonMain.ElementClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.navButtonMain_ElementClick);
            // 
            // tileNavCategoryAnnotation
            // 
            this.tileNavCategoryAnnotation.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.tileNavCategoryAnnotation.Caption = "Annotation";
            this.tileNavCategoryAnnotation.Items.AddRange(new DevExpress.XtraBars.Navigation.TileNavItem[] {
            this.tileNavItemCoordinates,
            this.tileNavItemDistance,
            this.tileNavItemClearAnnotations});
            this.tileNavCategoryAnnotation.Name = "tileNavCategoryAnnotation";
            // 
            // 
            // 
            this.tileNavCategoryAnnotation.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement4.Text = "Annotation";
            this.tileNavCategoryAnnotation.Tile.Elements.Add(tileItemElement4);
            // 
            // tileNavItemCoordinates
            // 
            this.tileNavItemCoordinates.Caption = "Coordinates";
            this.tileNavItemCoordinates.Name = "tileNavItemCoordinates";
            // 
            // 
            // 
            this.tileNavItemCoordinates.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement1.Text = "Coordinates";
            this.tileNavItemCoordinates.Tile.Elements.Add(tileItemElement1);
            this.tileNavItemCoordinates.Tile.Name = "tileBarItem1";
            // 
            // tileNavItemDistance
            // 
            this.tileNavItemDistance.Caption = "Distance";
            this.tileNavItemDistance.Name = "tileNavItemDistance";
            // 
            // 
            // 
            this.tileNavItemDistance.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement2.Text = "Distance";
            this.tileNavItemDistance.Tile.Elements.Add(tileItemElement2);
            this.tileNavItemDistance.Tile.Name = "tileBarItem2";
            // 
            // tileNavItemClearAnnotations
            // 
            this.tileNavItemClearAnnotations.Caption = "Clear Annotations";
            this.tileNavItemClearAnnotations.Name = "tileNavItemClearAnnotations";
            // 
            // 
            // 
            this.tileNavItemClearAnnotations.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement3.Text = "Clear Annotations";
            this.tileNavItemClearAnnotations.Tile.Elements.Add(tileItemElement3);
            this.tileNavItemClearAnnotations.Tile.Name = "tileBarItem3";
            // 
            // tileNavCategoryOsnap
            // 
            this.tileNavCategoryOsnap.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.tileNavCategoryOsnap.Caption = "Osnap";
            this.tileNavCategoryOsnap.GroupName = "";
            this.tileNavCategoryOsnap.Items.AddRange(new DevExpress.XtraBars.Navigation.TileNavItem[] {
            this.tileNavItemEnd,
            this.tileNavItemIntersection,
            this.tileNavItemMiddle,
            this.tileNavItemCenter,
            this.tileNavItemPoint});
            this.tileNavCategoryOsnap.Name = "tileNavCategoryOsnap";
            // 
            // 
            // 
            this.tileNavCategoryOsnap.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement10.Text = "Osnap";
            this.tileNavCategoryOsnap.Tile.Elements.Add(tileItemElement10);
            // 
            // tileNavItemEnd
            // 
            this.tileNavItemEnd.Caption = "End";
            this.tileNavItemEnd.Name = "tileNavItemEnd";
            // 
            // 
            // 
            this.tileNavItemEnd.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement5.Text = "End";
            this.tileNavItemEnd.Tile.Elements.Add(tileItemElement5);
            this.tileNavItemEnd.Tile.Name = "tileBarItem1";
            // 
            // tileNavItemIntersection
            // 
            this.tileNavItemIntersection.Caption = "Intersection";
            this.tileNavItemIntersection.Name = "tileNavItemIntersection";
            // 
            // 
            // 
            this.tileNavItemIntersection.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement6.Text = "Intersection";
            this.tileNavItemIntersection.Tile.Elements.Add(tileItemElement6);
            this.tileNavItemIntersection.Tile.Name = "tileBarItem5";
            // 
            // tileNavItemMiddle
            // 
            this.tileNavItemMiddle.Caption = "Middle";
            this.tileNavItemMiddle.Name = "tileNavItemMiddle";
            // 
            // 
            // 
            this.tileNavItemMiddle.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement7.Text = "Middle";
            this.tileNavItemMiddle.Tile.Elements.Add(tileItemElement7);
            this.tileNavItemMiddle.Tile.Name = "tileBarItem2";
            // 
            // tileNavItemCenter
            // 
            this.tileNavItemCenter.Caption = "Center";
            this.tileNavItemCenter.Name = "tileNavItemCenter";
            // 
            // 
            // 
            this.tileNavItemCenter.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement8.Text = "Center";
            this.tileNavItemCenter.Tile.Elements.Add(tileItemElement8);
            this.tileNavItemCenter.Tile.Name = "tileBarItem3";
            // 
            // tileNavItemPoint
            // 
            this.tileNavItemPoint.Caption = "Point";
            this.tileNavItemPoint.Name = "tileNavItemPoint";
            // 
            // 
            // 
            this.tileNavItemPoint.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement9.Text = "Point";
            this.tileNavItemPoint.Tile.Elements.Add(tileItemElement9);
            this.tileNavItemPoint.Tile.Name = "tileBarItem4";
            // 
            // tileNavItemOpen
            // 
            this.tileNavItemOpen.Caption = "Open";
            this.tileNavItemOpen.Name = "tileNavItemOpen";
            // 
            // 
            // 
            this.tileNavItemOpen.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement11.Text = "Open";
            this.tileNavItemOpen.Tile.Elements.Add(tileItemElement11);
            this.tileNavItemOpen.Tile.Name = "tileBarItem4";
            // 
            // tileNavItemSaveAs
            // 
            this.tileNavItemSaveAs.Caption = "Save As";
            this.tileNavItemSaveAs.Name = "tileNavItemSaveAs";
            // 
            // 
            // 
            this.tileNavItemSaveAs.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement12.Text = "Save As";
            this.tileNavItemSaveAs.Tile.Elements.Add(tileItemElement12);
            this.tileNavItemSaveAs.Tile.Name = "tileBarItem5";
            // 
            // tileNavItemSaveImage
            // 
            this.tileNavItemSaveImage.Caption = "Save Image";
            this.tileNavItemSaveImage.Name = "tileNavItemSaveImage";
            // 
            // 
            // 
            this.tileNavItemSaveImage.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement13.Text = "Save Image";
            this.tileNavItemSaveImage.Tile.Elements.Add(tileItemElement13);
            this.tileNavItemSaveImage.Tile.Name = "tileBarItem1";
            // 
            // hModel1
            // 
            this.hModel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.hModel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hModel1.Location = new System.Drawing.Point(224, 43);
            this.hModel1.Name = "hModel1";
            this.hModel1.ProgressBar = progressBar1;
            this.hModel1.propertyGridHelper = null;
            this.hModel1.Size = new System.Drawing.Size(820, 645);
            this.hModel1.TabIndex = 3;
            this.hModel1.Text = "hModel1";
            this.hModel1.Transparency = hanee.ThreeD.HModel.TranparencyMode.untransparency;
            this.hModel1.Viewports.Add(viewport1);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelObjectTree});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // dockPanelObjectTree
            // 
            this.dockPanelObjectTree.Controls.Add(this.dockPanel3_Container);
            this.dockPanelObjectTree.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelObjectTree.ID = new System.Guid("c306bba9-1d83-4cca-b897-1e005975cc6f");
            this.dockPanelObjectTree.Location = new System.Drawing.Point(0, 43);
            this.dockPanelObjectTree.Name = "dockPanelObjectTree";
            this.dockPanelObjectTree.OriginalSize = new System.Drawing.Size(224, 200);
            this.dockPanelObjectTree.Size = new System.Drawing.Size(224, 645);
            this.dockPanelObjectTree.Text = "Object Tree";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.treeListObject);
            this.dockPanel3_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(217, 616);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // treeListObject
            // 
            this.treeListObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListObject.Location = new System.Drawing.Point(0, 0);
            this.treeListObject.Name = "treeListObject";
            this.treeListObject.Size = new System.Drawing.Size(217, 616);
            this.treeListObject.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 688);
            this.Controls.Add(this.hModel1);
            this.Controls.Add(this.dockPanelObjectTree);
            this.Controls.Add(this.tileNavPane1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("FormMain.IconOptions.SvgImage")));
            this.Name = "FormMain";
            this.Text = "Br3D";
            ((System.ComponentModel.ISupportInitialize)(this.tileNavPane1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hModel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanelObjectTree.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListObject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.TileNavPane tileNavPane1;
        private DevExpress.XtraBars.Navigation.NavButton navButtonMain;
        private DevExpress.XtraBars.Navigation.TileNavCategory tileNavCategoryAnnotation;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItemCoordinates;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItemDistance;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItemClearAnnotations;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItemOpen;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItemSaveAs;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItemSaveImage;
        private hanee.ThreeD.HModel hModel1;
        private DevExpress.XtraBars.Navigation.TileNavCategory tileNavCategoryOsnap;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItemEnd;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItemIntersection;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItemMiddle;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItemCenter;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItemPoint;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelObjectTree;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraTreeList.TreeList treeListObject;
    }
}