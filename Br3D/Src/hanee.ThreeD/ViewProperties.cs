using devDept.Eyeshot.Entities;
using System.ComponentModel;
using devDept.Eyeshot;
namespace hanee.ThreeD
{
    public abstract class PropertiesView
    {
        private readonly View view;
        protected Drawings drawings;
        protected PropertiesView(View view, Drawings drawings)
        {
            this.view = view;
            this.drawings = drawings;
        }


        [Description("The name of the block related to the view.")]
        public string BlockName
        {
            get { return view.BlockName; }
        }

        [Description("Position along the X axis.")]
        public double X
        {
            get { return view.X; }
            set
            {
                view.X = value;
                drawings.Entities.Regen();
            }
        }

        [Description("Position along the Y axis.")]
        public double Y
        {
            get { return view.Y; }
            set
            {
                view.Y = value;
                drawings.Entities.Regen();
            }
        }

        [Description("Scale factor among model and drawings units.")]
        public double Scale
        {
            get { return view.Scale; }
            set
            {
                view.Scale = value;
                drawings.Entities.Regen();
            }
        }

        [Description("Entity visibility status.")]
        public bool Visible
        {
            get { return view.Visible; }
            set { view.Visible = value; }
        }
    }
}
