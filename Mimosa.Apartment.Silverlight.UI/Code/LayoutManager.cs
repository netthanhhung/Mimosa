using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mimosa.Apartment.Silverlight.UI
{
    public class LayoutManager
    {
        private int _layoutLevel;
        public int LayoutLevel {
            get { return _layoutLevel; }
            set
            {
                if (_layoutLevel != value)
                {
                    _layoutLevel = value;
                    SetLayoutFactors();
                }
            }
        }

        public double WidthFactor { get; private set; }

        private void SetLayoutFactors()
        {
            WidthFactor = _layoutLevel * 0.5;
            if (WidthFactor == 0)
            {
                WidthFactor = 1;
            }
            //switch (_layoutLevel)
            //{
            //    case 1:
            //        WidthFactor = 1;
            //        break;
            //    case 10:
            //        WidthFactor = 2.5;
            //        break;
            //    default:
            //        WidthFactor = 1;
            //        break;
            //}
        }

        public int GetLayoutWidth(double width)
        {
            return Convert.ToInt32(Math.Round(width * WidthFactor));
        }

        public int GetLayoutWidthNoFactor(double width)
        {
            return Convert.ToInt32(Math.Round(width));
        }
    }
}
