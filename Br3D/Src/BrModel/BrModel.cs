using devDept.Eyeshot;
using devDept.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrModel
{
    public partial class BrModel : UserControl
    {
        public Model Model => model1;
        public bool topViewOnly = false;

        public BrModel()
        {
            InitializeComponent();
            model1.Unlock("US21-D8G5N-12J8F-5F65-RD3W");

            model1.MouseDown += Model1_MouseDown;
        }

        private void Model1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                // 객체 선택 해제
                model1.Entities.ClearSelection();

                // 객체 선택
                var item = model1.GetItemUnderMouseCursor(e.Location, true);
                if (item != null)
                {
                    item.Select(model1, true);
                }
            }
        }

        

        // 2D view로 설정한다.
        public void Set2DView()
        {
            topViewOnly = true;
            model1.ActiveViewport.Camera.ProjectionMode = projectionType.Orthographic;
            model1.ActiveViewport.SetView(viewType.Top, true, true);
        }

        // 3D view로 설정한다.
        public void Set3DView()
        {
            topViewOnly = false;
            model1.ActiveViewport.Camera.ProjectionMode = projectionType.Perspective;
            model1.ActiveViewport.SetView(viewType.Isometric, true, true);
        }
    }
}
