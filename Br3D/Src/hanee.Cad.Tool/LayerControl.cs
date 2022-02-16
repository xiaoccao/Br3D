using devDept.Eyeshot;
using DevExpress.XtraGrid;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.ComponentModel;

namespace hanee.Cad.Tool
{
    public partial class LayerControl : DevExpress.XtraEditors.XtraUserControl
    {
        enum Field
        {
            [Description("Locked")]
            locked,
            [Description("Exportable")]
            exportable,
            [Description("Name")]
            name,
            [Description("Color")]
            color,
            [Description("MaterialName")]
            materialName,
            [Description("Visible")]
            visible,
            [Description("LineWeight")]
            lineWeight,
            [Description("LineTypeName")]
            lineTypeName
        }

        internal void RefreshDataSource()
        {
            gridControl1.RefreshDataSource();
        }

        public LayerControl()
        {
            InitializeComponent();
        }

        
        public void SetDesign(Design design)
        {
            gridControl1.DataSource = design.Layers;
            

            int idx = 0;

            SetColumn(Field.visible.GetDescription(), idx++, LanguageHelper.Tr("Visible"));
            SetColumn(Field.name.GetDescription(), idx++, LanguageHelper.Tr("Name"));
            SetColumn(Field.color.GetDescription(), idx++, LanguageHelper.Tr("Color"));
            SetColumn(Field.locked.GetDescription(), idx++, LanguageHelper.Tr("Locked"));
            SetColumn(Field.lineTypeName.GetDescription(), idx++, LanguageHelper.Tr("Line Type Name"));
            SetColumn(Field.lineWeight.GetDescription(), idx++, LanguageHelper.Tr("Line Weight"));
            SetColumn(Field.materialName.GetDescription(), idx++, LanguageHelper.Tr("Material Name"));
        }


        private void SetColumn(string fieldName, int visibleIndex, string caption)
        {
            gridView1.Columns[fieldName].VisibleIndex = visibleIndex;
            gridView1.Columns[fieldName].Caption = caption;
        }
    }
}
