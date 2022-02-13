using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Linq;
using hanee.Geometry;

namespace hanee.ThreeD
{
    public class SheetHelper
    {
        public enum formatType
        {
            A0_ISO, A1_ISO, A2_ISO, A3_ISO, A4_ISO, A4_LANDSCAPE_ISO, A_ANSI, A_LANDSCAPE_ANSI, B_ANSI, C_ANSI, D_ANSI, E_ANSI
        }

        static SheetHelper()
        {
            // sets the display name that will be used in the title block
            formatType.A0_ISO.SetDisplayName("A0");
            formatType.A1_ISO.SetDisplayName("A1");
            formatType.A2_ISO.SetDisplayName("A2");
            formatType.A3_ISO.SetDisplayName("A3");
            formatType.A4_ISO.SetDisplayName("A4");
            formatType.A4_LANDSCAPE_ISO.SetDisplayName("A4");
            formatType.A_ANSI.SetDisplayName("A");
            formatType.A_LANDSCAPE_ANSI.SetDisplayName("A");
            formatType.B_ANSI.SetDisplayName("B");
            formatType.C_ANSI.SetDisplayName("C");
            formatType.D_ANSI.SetDisplayName("D");
            formatType.E_ANSI.SetDisplayName("E");
        }

        /// <summary>
        /// 단위 기호
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        static public string UnitsSymbol(Sheet sheet)
        {
            string symbol = "mm";
            switch (sheet.Units)
            {
                case linearUnitsType.Inches:
                    symbol = "in";
                    break;
                case linearUnitsType.Feet:
                    symbol = "ft";
                    break;
                case linearUnitsType.Miles:
                    symbol = "mile";
                    break;

                case linearUnitsType.Millimeters:
                    symbol = "mm";
                    break;
                case linearUnitsType.Centimeters:
                    symbol = "cm";
                    break;
                case linearUnitsType.Meters:
                    symbol = "m";
                    break;
                case linearUnitsType.Kilometers:
                    symbol = "km";
                    break;
                default:
                    symbol = sheet.Units.ToString();
                    break;
            }

            return symbol;
        }
        public static string GetBlockNameForSheetFormatType(Sheet sheet)
        {
            return $"{sheet.Name}_{sheet.Width.ToString("0.000")}_{sheet.Height.ToString("0.000")}_{sheet.Units.ToString()})";
        }

        public static SheetHelper.formatType GetFormatType(Sheet sheet)
        {
            if (sheet == null)
                return formatType.A0_ISO;

            var formatTypes = Enum.GetValues(typeof(SheetHelper.formatType)).Cast<SheetHelper.formatType>();
            foreach (var ft in formatTypes)
            {
                if (GetUnitsByFormatType(ft) != sheet.Units)
                    continue;

                Tuple<double, double> size = GetFormatSize(sheet.Units, ft);

                if (!sheet.Width.Equals(size.Item1, 0.001))
                    continue;
                if (!sheet.Height.Equals(size.Item2, 0.001))
                    continue;

                return ft;
            }

            return formatType.A0_ISO;
        }

        // mllimeter를 sheet의 단위에 맞게 편집
        static public double GetUnitsConversionFactor(Sheet sheet)
        {
            if (sheet == null)
                return 1;

            return Utility.GetLinearUnitsConversionFactor(linearUnitsType.Millimeters, sheet.Units);
        }

        public static BlockReference CreateFormatBlock(Sheet sheet, formatType formatType, out Block block)
        {
            string blockName = GetBlockNameForSheetFormatType(sheet);

            block = null;
            BlockReference br = null;
            switch (formatType)
            {
                case formatType.A0_ISO:
                    br = sheet.BuildA0ISO(out block, blockName);
                    break;
                case formatType.A1_ISO:
                    br = sheet.BuildA1ISO(out block, blockName);
                    break;
                case formatType.A2_ISO:
                    br = sheet.BuildA2ISO(out block, blockName);
                    break;
                case formatType.A3_ISO:
                    br = sheet.BuildA3ISO(out block, blockName);
                    break;
                case formatType.A4_ISO:
                    br = sheet.BuildA4ISO(out block, blockName);
                    break;
                case formatType.A4_LANDSCAPE_ISO:
                    br = sheet.BuildA4LANDSCAPEISO(out block, blockName);
                    break;
                case formatType.A_ANSI:
                    br = sheet.BuildAANSI(out block, blockName);
                    break;
                case formatType.B_ANSI:
                    br = sheet.BuildBANSI(out block, blockName);
                    break;
                case formatType.C_ANSI:
                    br = sheet.BuildCANSI(out block, blockName);
                    break;
                case formatType.D_ANSI:
                    br = sheet.BuildDANSI(out block, blockName);
                    break;
                case formatType.E_ANSI:
                    br = sheet.BuildEANSI(out block, blockName);
                    break;

            }


            return br;
        }


        /// <summary>
        /// format type별 단위 리턴
        /// </summary>
        /// <param name="formatType"></param>
        /// <returns></returns>
        public static linearUnitsType GetUnitsByFormatType(formatType formatType)
        {
            linearUnitsType units = linearUnitsType.Millimeters;
            switch (formatType)
            {
                case SheetHelper.formatType.A0_ISO:
                case SheetHelper.formatType.A1_ISO:
                case SheetHelper.formatType.A2_ISO:
                case SheetHelper.formatType.A3_ISO:
                case SheetHelper.formatType.A4_ISO:
                case SheetHelper.formatType.A4_LANDSCAPE_ISO:
                    units = linearUnitsType.Millimeters;
                    break;
                case SheetHelper.formatType.A_ANSI:
                case SheetHelper.formatType.A_LANDSCAPE_ANSI:
                case SheetHelper.formatType.B_ANSI:
                case SheetHelper.formatType.C_ANSI:
                case SheetHelper.formatType.D_ANSI:
                case SheetHelper.formatType.E_ANSI:
                    units = linearUnitsType.Inches;
                    break;
            }

            return units;
        }
        /// <summary>
        /// Gets the paper size of the specified format type.
        /// </summary>
        /// <param name="units">The linear units.</param>
        /// <param name="formatType">The format type.</param>
        /// <returns>The width and height of the paper.</returns>
        public static Tuple<double, double> GetFormatSize(linearUnitsType units, formatType formatType)
        {
            // values on this method are millimeters so it uses this factor to get converted values according to the current units.
            double conversionFactor = Utility.GetLinearUnitsConversionFactor(linearUnitsType.Millimeters, units);

            switch (formatType)
            {
                case formatType.A0_ISO:
                    return new Tuple<double, double>(1189 * conversionFactor, 841 * conversionFactor);
                case formatType.A1_ISO:
                    return new Tuple<double, double>(841 * conversionFactor, 594 * conversionFactor);
                case formatType.A2_ISO:
                    return new Tuple<double, double>(594 * conversionFactor, 420 * conversionFactor);
                case formatType.A3_ISO:
                    return new Tuple<double, double>(420 * conversionFactor, 297 * conversionFactor);
                case formatType.A4_ISO:
                    return new Tuple<double, double>(210 * conversionFactor, 297 * conversionFactor);
                case formatType.A4_LANDSCAPE_ISO:
                    return new Tuple<double, double>(297 * conversionFactor, 210 * conversionFactor);
                case formatType.A_ANSI:
                    return new Tuple<double, double>(215.9 * conversionFactor, 279.4 * conversionFactor);
                case formatType.A_LANDSCAPE_ANSI:
                    return new Tuple<double, double>(279.4 * conversionFactor, 215.9 * conversionFactor);
                case formatType.B_ANSI:
                    return new Tuple<double, double>(431.8 * conversionFactor, 279.4 * conversionFactor);
                case formatType.C_ANSI:
                    return new Tuple<double, double>(558.8 * conversionFactor, 431.8 * conversionFactor);
                case formatType.D_ANSI:
                    return new Tuple<double, double>(863.6 * conversionFactor, 558.8 * conversionFactor);
                case formatType.E_ANSI:
                    return new Tuple<double, double>(1117.6 * conversionFactor, 863.6 * conversionFactor);
                default:
                    return new Tuple<double, double>(210 * conversionFactor, 297 * conversionFactor);
            }
        }
    }

}