using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using System.ComponentModel;

namespace hanee.ThreeD
{
    public class PropertiesRasterView : PropertiesView
    {
        protected readonly RasterView rasterView;
        public PropertiesRasterView(RasterView rasterView, Drawings drawings) : base(rasterView, drawings)
        {
            this.rasterView = rasterView;
        }

        //[Description("Image DPI resolution.")]
        //public int Dpi
        //{
        //    get { return rasterView.Dpi; }
        //    set { rasterView.Dpi = value; }
        //}

        //[Description("Indicates whether the shadows must be shown or not.")]
        //public bool Shadow
        //{
        //    get { return rasterView.Shadow; }
        //    set { rasterView.Shadow = value; }
        //}

        //[Description("Indicates the displayType used during the creation of the Raster view.")]
        //public displayType DisplayMode
        //{
        //    get { return rasterView.DisplayMode; }
        //    set { rasterView.DisplayMode = value != displayType.Wireframe && value != displayType.HiddenLines ? value : rasterView.DisplayMode; }
        //}
    }
}
