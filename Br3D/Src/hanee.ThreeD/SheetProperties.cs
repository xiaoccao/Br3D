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
    public class PropertiesSheet
    {
        private readonly Sheet sheet;
        protected Drawings drawings;

        public PropertiesSheet(Sheet sheet, Drawings drawings)
        {
            this.sheet = sheet;
            this.drawings = drawings;
        }


        [Description("The name of the sheet.")]
        public string Name
        {
            get { return sheet.Name; }
            set{ sheet.Name = value; }
        }

        [Description("The width of the sheet.")]
        public string Width
        {
            get { return sheet.Width.ToString("0"); }
        }

        [Description("The height of the sheet.")]
        public string Height
        {
            get { return sheet.Height.ToString("0"); }
        }

        [Description("The units of the sheet.")]
        public string Units
        {
            get { return sheet.Units.ToString(); }
        }

        [Description("Vector views")]
        public string VectorViews
        {
            get 
            {
                int count = 0;
                foreach(var ent in sheet.Entities)
                {
                    if(ent is VectorView)
                    {
                        count++;
                    }
                }

                return count.ToString() + " ea";
                
            }
        }

        [Description("Raster views")]
        public string RasterViews
        {
            get
            {
                int count = 0;
                foreach (var ent in sheet.Entities)
                {
                    if (ent is RasterView)
                    {
                        count++;
                    }
                }

                return count.ToString() + " ea";

            }
        }
    }
}
