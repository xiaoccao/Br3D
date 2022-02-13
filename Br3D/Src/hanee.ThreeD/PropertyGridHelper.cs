using System;
using System.Windows.Forms;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using Environment = devDept.Eyeshot.Environment;

namespace hanee.ThreeD
{
    // Environment의 property grid helper
    public class PropertyGridHelper
    {
        Environment environment { get; set; }

        Model model { get; set; }
        Drawings drawings { get; set; }

        private PropertyGrid propertyGrid { get; set; }

        public PropertyGridHelper(Model model, PropertyGrid propertyGrid)
        {
            this.environment = model;
            this.model = model;
            this.drawings = null;
            this.propertyGrid = propertyGrid;

            this.propertyGrid.PropertyValueChanged += PropertyGrid_PropertyValueChanged;
        }

        public PropertyGridHelper(Drawings drawings, Model model, PropertyGrid propertyGrid)
        {
            this.environment = drawings;
            this.model = model;
            this.drawings = drawings;

            this.propertyGrid = propertyGrid;

            this.propertyGrid.PropertyValueChanged += PropertyGrid_PropertyValueChanged;
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (propertyGrid.SelectedObject is Entity || propertyGrid.SelectedObject is PropertiesView)
            {
                environment.Entities.Regen();

                propertyGrid.Refresh();

                environment.Invalidate();
            }
        }

        public void UpdatePropertyGridControl(Object obj)
        {
            if (propertyGrid == null)
                return;

            if (obj is VectorView)
            {
                propertyGrid.SelectedObject = new PropertiesVectorView(obj as VectorView, model, drawings);
            }
            else if (obj is RasterView)
            {
                propertyGrid.SelectedObject = new PropertiesRasterView(obj as RasterView, drawings);
            }
            else
            {
                propertyGrid.SelectedObject = obj;
            }

            propertyGrid.Refresh();
        }
    }
}
