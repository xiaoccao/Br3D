﻿using System.Drawing;

namespace hanee.Cad.Tool
{
    public class Define
    {
        #region 상수정의
        static public string DefaultLayer = "0";
        static public string Only2DLayer = "Only2D";
        static public string Only3DLayer = "Only3D";
        static public float DefaultTextHeight = 12.0f;
        static private System.Drawing.Font defaultFont;
        static public System.Drawing.Font DefaultFont
        {
            get
            {
                if (defaultFont == null)
                    defaultFont = new Font("Arial", DefaultTextHeight);
                return defaultFont;
            }

            set
            {
                defaultFont = value;
            }
        }


        static public Color DefaultTextColor = Color.FromArgb(255, 0, 0, 0);
        static public int Slices = 20;
        static public int Stacks = 20;
        static public int Rings = 20;

        // section 만들때의 오차
        static public double SectionTol = 0.01;

        // extrude 할때의 오차
        static public double ExtrudeTol = 0;

        #endregion

        #region
        public enum pos
        {
            left,
            center,
            right
        }

        #endregion
    }
}
