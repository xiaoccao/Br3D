using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using hanee.Cad.Tool;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Br3D
{
    public partial class FormMain : DevExpress.XtraEditors.XtraForm
    {

        Model model => hModel1;
        Dictionary<NavElement, Action> functionByElement = new Dictionary<NavElement, Action>();
        public FormMain()
        {
            InitializeComponent();
            model.Unlock("US21-D8G5N-12J8F-5F65-RD3W");
            model.MouseDoubleClick += Model_MouseDoubleClick;
            model.WorkCompleted += Model_WorkCompleted;
            model.WorkFailed += Model_WorkFailed;
            model.MouseUp += Model_MouseUp;

       

            InitGraphics();
            InitDisplayMode();
            
            InitSnapping();
            InitElementMethod();
            InitObjectTreeList();

            InitToolbar();
        }

        private void InitToolbar()
        {
            
        }

        private void InitGraphics()
        {
            model.AntiAliasing = true;
            model.AntiAliasingSamples = devDept.Graphics.antialiasingSamplesNumberType.x4;
            model.AskForAntiAliasing = true;
        }

        private void InitDisplayMode()
        {
            model.Rendered.SilhouettesDrawingMode = silhouettesDrawingType.Never;
            model.Rendered.ShadowMode = devDept.Graphics.shadowType.None;
        }

        private void Model_MouseUp(object sender, MouseEventArgs e)
        {
            // tree에서 선택
            if (!treeListObject.Visible)
                return;

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


        // element별 method 목록 초기화
        private void InitElementMethod()
        {
            functionByElement.Add(tileNavItemOpen, Open);
            functionByElement.Add(tileNavItemSaveAs, SaveAs);
            functionByElement.Add(tileNavItemSaveImage, SaveImage);
            functionByElement.Add(tileNavItemCoordinates, Coorindates);
            functionByElement.Add(tileNavItemDistance, Distance);
            functionByElement.Add(tileNavItemClearAnnotations, ClearAnnotations);
            functionByElement.Add(tileNavItemEnd, End);
            functionByElement.Add(tileNavItemIntersection, Intersection);
            functionByElement.Add(tileNavItemMiddle, Middle);
            functionByElement.Add(tileNavItemCenter, Center);
            functionByElement.Add(tileNavItemPoint, Point);
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


    }
}