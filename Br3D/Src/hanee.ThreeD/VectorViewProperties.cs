using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    public class PropertiesVectorView : PropertiesView
    {
        protected readonly VectorView vectorView;
        private Model model;

        public PropertiesVectorView(VectorView vectorView, Model model, Drawings drawings) : base(vectorView, drawings)
        {
            this.vectorView = vectorView;
            this.model = model;
        }

        [Description("Center lines extension amount.")]
        public double CenterlinesExtensionAmount
        {
            get { return vectorView.CenterlinesExtensionAmount; }
            set { vectorView.CenterlinesExtensionAmount = value; }
        }

        [Description("Hidden segments (silhouettes, edges, wires) visibility status.")]
        public bool HiddenSegments
        {
            get { return vectorView.HiddenSegments; }
            set
            {
                vectorView.HiddenSegments = value;
                vectorView.UpdateViewBlock(value, model, drawings);
                drawings.Entities.Regen();
            }
        }

        [Description("When true, treats the transparent entities as if they are opaque, so they will hide the geometry behind.")]
        public bool IgnoreTransparency
        {
            get { return vectorView.IgnoreTransparency; }
            set { vectorView.IgnoreTransparency = value; }
        }
    }
}
