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
    public interface IParam
    {
        // Property declaration:
        string DisplayString { get; set; }
        event EventHandler InitComplete;
        void Init();
    }
}
