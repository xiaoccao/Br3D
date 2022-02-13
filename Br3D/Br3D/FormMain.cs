using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using hanee.Cad.Tool;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ToolBarButton = devDept.Eyeshot.ToolBarButton;

namespace Br3D
{
    public partial class FormMain : DevExpress.XtraEditors.XtraForm
    {
        devDept.Eyeshot.ToolBarButton toolBarButtonWireframe = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.wireframe_32x, "Wireframe", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButtonHiddenLine = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.hiddenline_32x, "HiddenLine", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButtonShaded = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.shaded_32x, "Shade", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButtonRendered = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.rendered_32x, "Render", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);

        List<Viewport> viewports = new List<Viewport>();

        private Memo lastMemo = null;
        Model model => o;
        Dictionary<NavElement, Action> functionByElement = new Dictionary<NavElement, Action>();
        public FormMain()
        {
            InitializeComponent();
            model.Unlock("US21-D8G5N-12J8F-5F65-RD3W");
            model.MouseDoubleClick += Model_MouseDoubleClick;
            model.WorkCompleted += Model_WorkCompleted;
            model.WorkFailed += Model_WorkFailed;
            model.MouseUp += Model_MouseUp;
            model.MouseMove += Model_MouseMove;
            
            foreach (Viewport vp in model.Viewports)
                viewports.Add(vp);

            Options.Instance.appName = "Br3D";
            Options.Instance.LoadOptions();

            LanguageHelper.Load(Options.Instance.language);
            InitGraphics();
            InitDisplayMode();

            InitSnapping();
            InitTileElementMethod();
            InitTileElementStatus();
            InitObjectTreeList();

            InitToolbar();
            Translate();
        }

        private void Model_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.None)
                return;

            Memo memo = GetMemoUnderMouseCursor(e.Location);
            if (memo != null && lastMemo != memo)
            {
                toolTipController1.ShowHint(memo.OneLineText);
                lastMemo = memo;
            }
            else
            {
                lastMemo = memo;
            }

            if (lastMemo == null)
                toolTipController1.HideHint();
        }

        // 마우스 커서 아래에 있는 memo를 리턴한다.
        private Memo GetMemoUnderMouseCursor(System.Drawing.Point location)
        {
            int index = model.GetLabelUnderMouseCursor(location);
            if (index != -1)
            {
                //get the entity
                var label = model.ActiveViewport.Labels[index];
                return label as Memo;
            }
            return null;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ViewportSingle();
        }

        private void Translate()
        {
            // navButton
            navButtonHome.Caption = LanguageHelper.Tr("Home");

            // category
            tileNavCategoryOptions.Caption = LanguageHelper.Tr("Options");
            tileNavCategoryOsnap.Caption = LanguageHelper.Tr("Osnap");
            tileNavCategoryAnnotation.Caption = LanguageHelper.Tr("Annotation");
            tileNavCategoryViewport.Caption = LanguageHelper.Tr("Viewport");


            //tile
            SetTileText(tileNavItemOpen, LanguageHelper.Tr("Open"));
            SetTileText(tileNavItemSaveAs, LanguageHelper.Tr("Save As"));
            SetTileText(tileNavItemSaveImage, LanguageHelper.Tr("Save Image"));
            SetTileText(tileNavItemExit, LanguageHelper.Tr("Exit"));
            SetTileText(tileNavItemEnd, LanguageHelper.Tr("End Point"));
            SetTileText(tileNavItemIntersection, LanguageHelper.Tr("Intersection Point"));
            SetTileText(tileNavItemCenter, LanguageHelper.Tr("Center Point"));
            SetTileText(tileNavItemPoint, LanguageHelper.Tr("Point"));
            SetTileText(tileNavItemMiddle, LanguageHelper.Tr("Middle Point"));
            SetTileText(tileNavItemCoordinates, LanguageHelper.Tr("Coordinates"));
            SetTileText(tileNavItemDistance, LanguageHelper.Tr("Distance"));
            SetTileText(tileNavItemMemo, LanguageHelper.Tr("Memo"));
            SetTileText(tileNavItemLanguage, LanguageHelper.Tr("Language"));

            // sub tile

            // control
            dockPanelObjectTree.Text = LanguageHelper.Tr("Object Tree");
        }

        private void SetTileText(TileNavItem tileNavItem, string text)
        {
            tileNavItem.TileText = text;
            tileNavItem.Caption = text;
        }


        private void InitToolbar()
        {
            foreach (Viewport vp in model.Viewports)
            {
                if (vp.ToolBars.Length > 1)
                {
                    var displayModelToolbar = vp.ToolBars[1];
                    displayModelToolbar.Position = devDept.Eyeshot.ToolBar.positionType.VerticalTopLeft;
                    displayModelToolbar.Buttons.Clear();
                    displayModelToolbar.Buttons.Add(toolBarButtonWireframe);
                    displayModelToolbar.Buttons.Add(toolBarButtonHiddenLine);
                    displayModelToolbar.Buttons.Add(toolBarButtonShaded);
                    displayModelToolbar.Buttons.Add(toolBarButtonRendered);
                }

                vp.Rotate.MouseButton = new MouseButton(MouseButtons.Middle, modifierKeys.Ctrl);
                vp.Rotate.RotationMode = rotationType.Turntable;

                vp.Pan.MouseButton = new MouseButton(MouseButtons.Middle, modifierKeys.None);
            }

            toolBarButtonWireframe.Click += ToolBarButtonDisplayMode_Click;
            toolBarButtonHiddenLine.Click += ToolBarButtonDisplayMode_Click;
            toolBarButtonShaded.Click += ToolBarButtonDisplayMode_Click;
            toolBarButtonRendered.Click += ToolBarButtonDisplayMode_Click;
        }

        private void ToolBarButtonDisplayMode_Click(object sender, EventArgs e)
        {

            var toolBar = sender as ToolBarButton;
            if (toolBar == toolBarButtonWireframe)
                model.ActiveViewport.DisplayMode = displayType.Wireframe;
            else if (toolBar == toolBarButtonHiddenLine)
                model.ActiveViewport.DisplayMode = displayType.HiddenLines;
            else if (toolBar == toolBarButtonShaded)
                model.ActiveViewport.DisplayMode = displayType.Shaded;
            else if (toolBar == toolBarButtonRendered)
                model.ActiveViewport.DisplayMode = displayType.Rendered;

            model.Invalidate();
        }

        private void InitGraphics()
        {
            model.AntiAliasing = true;
            model.AntiAliasingSamples = devDept.Graphics.antialiasingSamplesNumberType.x4;
            model.AskForAntiAliasing = true;
        }

        private void InitDisplayMode()
        {
            model.Shaded.ShowInternalWires = false;
            model.Shaded.EdgeColorMethod = edgeColorMethodType.SingleColor;
            model.Shaded.EdgeThickness = 1;

            model.Rendered.SilhouettesDrawingMode = silhouettesDrawingType.Never;
            model.Rendered.ShadowMode = devDept.Graphics.shadowType.None;
        }

        private void Model_MouseUp(object sender, MouseEventArgs e)
        {
            // tree에서 선택
            if (!treeListObject.Visible)
                return;
            if (model.ActionMode != actionType.None)
                return;



            if (e.Button == MouseButtons.Left)
            {
                treeListObject.ClearSelection();
                var item = model.GetItemUnderMouseCursor(e.Location);
                if (item == null)
                    return;

                var node = treeListObject.FindNode(x => x.Tag == item.Item);
                if (node == null)
                    return;
                if (node.ParentNode != null)
                    node.ParentNode.Expand();
                treeListObject.SelectNode(node);
                treeListObject.TopVisibleNodeIndex = node.Id;

            }
        }

        private void InitObjectTreeList()
        {
            treeListObject.FocusedNodeChanged += TreeListObject_FocusedNodeChanged;
            treeListObject.AfterCheckNode += TreeListObject_AfterCheckNode;
        }

        // check 변경시
        private void TreeListObject_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            var node = e.Node;
            if (node == null)
                return;

            AfterCheckNode(node);
            model.Invalidate();
        }

        private void AfterCheckNode(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            var ent = node.Tag as Entity;
            if (ent != null)
            {
                ent.Visible = node.Checked;
            }


            if (node.Nodes == null)
                return;

            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode childNode in node.Nodes)
            {
                AfterCheckNode(childNode);
            }
        }

        private void TreeListObject_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var entities = GetAllEntitiesByNode(e.Node, true);
            if (entities == null)
                return;

            model.Entities.ClearSelection();
            entities.ForEach(x => x.Selected = true);
            model.Invalidate();
        }

        private List<Entity> GetAllEntitiesByNode(DevExpress.XtraTreeList.Nodes.TreeListNode node, bool subEntities)
        {
            List<Entity> entities = new List<Entity>();
            var subNodes = node.GetAllSubNodes();
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode subNode in subNodes)
            {
                var ent = subNode.Tag as Entity;
                if (ent == null)
                    continue;

                entities.Add(ent);
            }

            return entities;
        }


        void InitTileElementStatus()
        {
            tileNavSubItemKorean.Tile.Checked = false;
            tileNavSubItemEnglish.Tile.Checked = false;
            if (Options.Instance.language == "ko-KR")
                tileNavSubItemKorean.Tile.Checked = true;
            else if(Options.Instance.language == "en-US")
                tileNavSubItemEnglish.Tile.Checked = true;
        }

        // element별 method 목록 초기화
        private void InitTileElementMethod()
        {
            functionByElement.Add(tileNavItemOpen, Open);
            functionByElement.Add(tileNavItemSaveAs, SaveAs);
            functionByElement.Add(tileNavItemSaveImage, SaveImage);
            functionByElement.Add(tileNavItemExit, Close);
            functionByElement.Add(tileNavItemCoordinates, Coorindates);
            functionByElement.Add(tileNavItemDistance, Distance);
            functionByElement.Add(tileNavItemMemo, Memo);
            functionByElement.Add(tileNavItemClearAnnotations, ClearAnnotations);
            functionByElement.Add(tileNavItemEnd, End);
            functionByElement.Add(tileNavItemIntersection, Intersection);
            functionByElement.Add(tileNavItemMiddle, Middle);
            functionByElement.Add(tileNavItemCenter, Center);
            functionByElement.Add(tileNavItemPoint, Point);

            functionByElement.Add(tileNavItemViewportsingle, ViewportSingle);
            functionByElement.Add(tileNavItemViewport1x1, Viewport1x1);
            functionByElement.Add(tileNavItemViewport1x2, Viewport1x2);
            functionByElement.Add(tileNavItemViewport2x2, Viewport2x2);

            functionByElement.Add(tileNavItemLanguage, Language);
            functionByElement.Add(tileNavSubItemKorean, Korean);
            functionByElement.Add(tileNavSubItemEnglish, English);

            functionByElement.Add(tileNavItemHomePage, Homepage);
            functionByElement.Add(tileNavItemCheckForUpdate, CheckForUpdate);
            functionByElement.Add(tileNavItemAbout, About);
        }

        void About()
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();

        }
        void CheckForUpdate()
        {
            // 업데이트 체크
            var filePath = Path.Combine(hanee.ThreeD.Util.GetExePath(), "wyUpdate.exe");
            if (File.Exists(filePath))
            {
                System.Diagnostics.Process.Start(filePath);
            }
            else
                XtraMessageBox.Show(LanguageHelper.Tr("Update check failed! - wyUpdate.exe not found!"));
        }

        void Homepage()
        {
            System.Diagnostics.Process.Start("http://hileejaeho.cafe24.com/kr-br3d/");
        }

        void Language()
        {
            tileNavItemLanguage.Tile.ShowDropDown();
        }

        void Korean()
        {
            Options.Instance.language = "ko-KR";
            InitTileElementStatus();
            LanguageHelper.Load(Options.Instance.language);
            Translate();
        }

        void English()
        {
            Options.Instance.language = "en-US";
            InitTileElementStatus();
            LanguageHelper.Load(Options.Instance.language);
            Translate();
        }
        private void InitSnapping()
        {
            if (model is HModel)
            {
                HModel vp = (HModel)model;
                vp.Snapping.SetActiveObjectSnap(Snapping.objectSnapType.None, true);
                vp.Snapping.objectSnapEnabled = true;
            }
        }

        private void Model_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                model.ZoomFit();
                return;
            }

            // zoom all
            if (e.Button == MouseButtons.Middle)
            {
                model.ZoomFit();
                return;
            }

            if (ActionBase.IsUserInputting())
            {
                return;
            }

            if (model.ObjectManipulator.Visible == false)
            {
                devDept.Geometry.Transformation trans = new devDept.Geometry.Transformation();
                IList<Entity> selectedEntities = ((HModel)model).GetAllSelectedEntities();
                trans.Identity();
                model.ObjectManipulator.Enable(trans, true, selectedEntities);
            }
            else
            {
                model.ObjectManipulator.Apply();
                model.Entities.Regen();
            }
        }

        private void Model_WorkFailed(object sender, WorkFailedEventArgs e)
        {
            MessageBox.Show(e.Error);

        }

        private void Model_WorkCompleted(object sender, devDept.Eyeshot.WorkCompletedEventArgs e)
        {
            if (e.WorkUnit is ReadFileAsync)
            {
                ReadFileAsync rfa = (ReadFileAsync)e.WorkUnit;

                // viewport에 추가한다.
                rfa.AddToScene(model);

                // zoom fit
                foreach (Viewport v in model.Viewports)
                    v.ZoomFit();

                // object tree 갱신
                ObjectTreeListHelper.Regen(treeListObject, model);
            }
        }
        private void navButtonMain_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {

        }

        private void tileNavPane1_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            if (!e.IsTile)
                return;

            if (functionByElement.TryGetValue(e.Element, out Action act))
            {
                act();
            }
            else
            {
#if DEBUG
                MessageBox.Show("undefined function");
#endif
            }
        }

        // 
        void FlagOsnap(DevExpress.XtraBars.Navigation.TileNavItem tile, Snapping.objectSnapType snapType)
        {
            HModel hModel = model as HModel;
            if (hModel == null)
                return;

            hModel.Snapping.FlagActiveObjectSnap(snapType);
            tile.Tile.Checked = hModel.Snapping.IsActiveObjectSnap(snapType);
        }

        void ViewportSingle()
        {
            if (model.Viewports.Contains(viewports[1]))
                model.Viewports.Remove(viewports[1]);
            if (model.Viewports.Contains(viewports[2]))
                model.Viewports.Remove(viewports[2]);
            if (model.Viewports.Contains(viewports[3]))
                model.Viewports.Remove(viewports[3]);
            model.Invalidate();
        }

        void Viewport1x1()
        {
            if (!model.Viewports.Contains(viewports[1]))
                model.Viewports.Add(viewports[1]);
            if (model.Viewports.Contains(viewports[2]))
                model.Viewports.Remove(viewports[2]);
            if (model.Viewports.Contains(viewports[3]))
                model.Viewports.Remove(viewports[3]);
            model.Invalidate();
        }
        void Viewport1x2()
        {
            if (!model.Viewports.Contains(viewports[1]))
                model.Viewports.Add(viewports[1]);
            if (!model.Viewports.Contains(viewports[2]))
                model.Viewports.Add(viewports[2]);
            if (model.Viewports.Contains(viewports[3]))
                model.Viewports.Remove(viewports[3]);
            model.Invalidate();
        }
        void Viewport2x2()
        {
            if (!model.Viewports.Contains(viewports[1]))
                model.Viewports.Add(viewports[1]);
            if (!model.Viewports.Contains(viewports[2]))
                model.Viewports.Add(viewports[2]);
            if (!model.Viewports.Contains(viewports[3]))
                model.Viewports.Add(viewports[3]);
            model.Invalidate();
        }

        void End() => FlagOsnap(tileNavItemEnd, Snapping.objectSnapType.End);
        void Middle() => FlagOsnap(tileNavItemMiddle, Snapping.objectSnapType.Mid);
        void Point() => FlagOsnap(tileNavItemPoint, Snapping.objectSnapType.Point);
        void Intersection() => FlagOsnap(tileNavItemIntersection, Snapping.objectSnapType.Intersect);
        void Center() => FlagOsnap(tileNavItemCenter, Snapping.objectSnapType.Center);

        async void Coorindates()
        {
            ActionID ac = new ActionID(model, ActionID.ShowResult.label);
            await ac.RunAsync();
        }

        async void Distance()
        {
            ActionDist ac = new ActionDist(model, ActionDist.ShowResult.label);
            await ac.RunAsync();
        }

        async void Memo()
        {
            ActionMemo ac = new ActionMemo(model);
            await ac.RunAsync();
        }

        void ClearAnnotations()
        {
            model.ActiveViewport.Labels.Clear();
            model.Invalidate();
        }

        private void SaveImage()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Bitmap (*.bmp)|*.bmp|" +
                "Portable Network Graphics (*.png)|*.png|" +
                "Windows metafile (*.wmf)|*.wmf|" +
                "Enhanced Windows Metafile (*.emf)|*.emf";

            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                switch (dlg.FilterIndex)
                {

                    case 1:
                        model.WriteToFileRaster(2, dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 2:
                        model.WriteToFileRaster(2, dlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case 3:
                        model.WriteToFileRaster(2, dlg.FileName, System.Drawing.Imaging.ImageFormat.Wmf);
                        break;
                    case 4:
                        model.WriteToFileRaster(2, dlg.FileName, System.Drawing.Imaging.ImageFormat.Emf);
                        break;

                }

            }
        }

        void Open()
        {
            // 파일 선택
            OpenFileDialog openFile = new OpenFileDialog();


            Dictionary<string, string> additionalSupportFormats = new Dictionary<string, string>();
            //additionalSupportFormats.Add("Br3D(model, drawings)", "*.br3");
            openFile.Filter = FileHelper.FilterForOpenDialog(additionalSupportFormats);
            openFile.FilterIndex = 0;
            openFile.AddExtension = true;
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            NewFile();

            Import(openFile.FileName);
        }

        void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = FileHelper.FilterForSaveDialog();
            dlg.DefaultExt = "dwg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Export(dlg.FileName);
            }
        }

        // ribbon - import
        // iges, igs, stl, step, stp, obj, las, dwg, dxf, ifc, ifczip, 3ds, lus
        void Import(string pathFileName)
        {
            try
            {
                devDept.Eyeshot.Translators.ReadFileAsync rf = FileHelper.GetReadFileAsync(pathFileName);
                if (rf == null)
                    return;

                model.StartWork(rf);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);

            }

        }

        void Export(string pathFileName)
        {
            try
            {
                devDept.Eyeshot.Translators.WriteFileAsync wf = FileHelper.GetWriteFileAsync(model, null, pathFileName);
                if (wf == null)
                    return;

                model.StartWork(wf);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }

        void NewFile()
        {
            model.Clear();
            model.Invalidate();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Options.Instance.SaveOptions();
        }


    }
}