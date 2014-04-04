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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Charting;
using System.Windows.Data;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.Windows.Automation;
using FluxJpeg.Core.Encoder;
using FluxJpeg.Core;

namespace Mimosa.Apartment.Silverlight.UI
{
    internal static class UiHelper
    {
        public const long k_DoubleClickSpeed = 500;
        private const double k_MaxMoveDistance = 10;
        private static long _LastClickTicks = 0;
        private static Point _LastPosition;
        private static WeakReference _LastSender;
        internal static bool IsDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(null);
            return IsDoubleClick(sender, position);
        }

        internal static bool IsDoubleClick(object sender, ChartItemClickEventArgs e)
        {
            Point position = e.MouseData.GetPosition(null);
            return IsDoubleClick(sender, position);
        }

        private static bool IsDoubleClick(object sender, Point mousePosition)
        {
            long clickTicks = DateTime.Now.Ticks;
            long elapsedTicks = clickTicks - _LastClickTicks;
            long elapsedTime = elapsedTicks / TimeSpan.TicksPerMillisecond;
            bool quickClick = (elapsedTime <= k_DoubleClickSpeed);
            bool senderMatch = (_LastSender != null && sender.Equals(_LastSender.Target));

            if (senderMatch && quickClick && mousePosition.Distance(_LastPosition) <= k_MaxMoveDistance)
            {
                // Double click!
                _LastClickTicks = 0;
                _LastSender = null;
                return true;
            }

            // Not a double click
            _LastClickTicks = clickTicks;
            _LastPosition = mousePosition;
            if (!quickClick)
                _LastSender = new WeakReference(sender);
            return false;
        }

        private static double Distance(this Point pointA, Point pointB)
        {
            double x = pointA.X - pointB.X;
            double y = pointA.Y - pointB.Y;
            return Math.Sqrt(x * x + y * y);
        }
        internal static class ControlNames
        {
            internal const string InformationPane = "ip";
        }

        private static void ExportRadGridView(RadGridView gvwToExport, ExportFormat format,
            bool showColumnHeaders, bool showColumnFooters, SaveFileDialog dialog)
        {
            if (dialog.ShowDialog().Value)
            {
                using (Stream fileStream = dialog.OpenFile())
                {
                    gvwToExport.Export(fileStream, new GridViewExportOptions()
                    {
                        Format = format,
                        ShowColumnHeaders = showColumnHeaders,
                        ShowColumnFooters = showColumnFooters
                    });

                    fileStream.Close();
                }
            }
        }

        internal static void ExportRadGridViewToExcelML(RadGridView gvwToExport,
            bool showColumnHeaders, bool showColumnFooters)
        {
            ExportRadGridViewToExcelML(gvwToExport, showColumnHeaders, showColumnFooters, true);
        }

        internal static void ExportRadGridViewToExcelML(RadGridView gvwToExport,
            bool showColumnHeaders, bool showColumnFooters, bool pretendItIsExcel)
        {
            string extension = pretendItIsExcel ? "xls" : "xml";
            string fileType = pretendItIsExcel ? "Excel" : "XML";

            SaveFileDialog dialog = InitSaveDialog(extension, fileType);

            gvwToExport.ElementExporting += new EventHandler<GridViewElementExportingEventArgs>(gvwToExport_ElementExporting);
            ExportRadGridView(gvwToExport, ExportFormat.ExcelML, showColumnHeaders, showColumnHeaders, dialog);
            gvwToExport.ElementExporting -= gvwToExport_ElementExporting;
        }

        private static SaveFileDialog InitSaveDialog(string extension, string fileType)
        {
            SaveFileDialog result = new SaveFileDialog()
            {
                DefaultExt = extension,
                Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension, fileType),
                FilterIndex = 1
            };

            return result;
        }

        static void gvwToExport_ElementExporting(object sender, GridViewElementExportingEventArgs e)
        {
            if (e.Value != null && e.Value.GetType() == typeof(string))
            {
                e.Value = (e.Value as string)
                    .Replace("&", "&amp;")
                    .Replace("<", "&lt;")
                    .Replace(">", "&gt;")
                    .Replace("\"", "&quot;")
                    .Replace("\'", "&apos;");
            }
        }

        internal static void ToggleShowHideInfoPane(Grid uiContainer, FrameworkElement pane, GridLength gridLength, int rowIndex)
        {
            if (pane.Visibility == Visibility.Visible)
            {
                pane.Visibility = Visibility.Collapsed;
                if (uiContainer.RowDefinitions.Count > rowIndex)
                {
                    uiContainer.RowDefinitions.RemoveAt(rowIndex);
                }
            }
            else
            {
                RowDefinition newRow = new RowDefinition();
                newRow.Height = gridLength;
                uiContainer.RowDefinitions.Add(newRow);

                UIElement result = uiContainer.FindName(pane.Name) as UIElement;
                if (result == null)
                {
                    pane.SetValue(Grid.RowProperty, rowIndex);
                    uiContainer.Children.Add(pane);
                }

                pane.Visibility = Visibility.Visible;
            }
        }

        internal static void ToggleShowHideInfoPane(Grid uiContainer, FrameworkElement pane, GridLength gridLength, int rowIndex, RadMenuItem item)
        {
            if (pane.Visibility == Visibility.Visible)
            {
                pane.Visibility = Visibility.Collapsed;
                if (uiContainer.RowDefinitions.Count > rowIndex)
                {
                    uiContainer.RowDefinitions.RemoveAt(rowIndex);
                }

                item.ToggleHideToShow();
            }
            else
            {
                RowDefinition newRow = new RowDefinition();
                newRow.Height = gridLength;
                uiContainer.RowDefinitions.Add(newRow);

                UIElement result = uiContainer.FindName(pane.Name) as UIElement;
                if (result == null)
                {
                    pane.SetValue(Grid.RowProperty, rowIndex);
                    uiContainer.Children.Add(pane);
                }

                pane.Visibility = Visibility.Visible;
                item.ToggleShowToHide();
            }
        }

        internal static void SetInfoPane(Grid uiLayoutRoot, FrameworkElement pane)
        {
            SetInfoPane(uiLayoutRoot, pane, new GridLength(1, GridUnitType.Star));
        }

        internal static void SetInfoPane(Grid uiLayoutRoot, FrameworkElement pane, GridLength gridLength)
        {
            if (uiLayoutRoot.RowDefinitions.Count > 1)
            {
                UIElement infoPane = GetInfoPane(uiLayoutRoot);
                if (infoPane != null)
                {
                    uiLayoutRoot.Children.Remove(infoPane);
                }
                uiLayoutRoot.RowDefinitions.RemoveAt(1);
            }

            if (pane != null)
            {
                if (uiLayoutRoot.RowDefinitions.Count == 1)
                {
                    RowDefinition newRow = new RowDefinition();
                    newRow.Height = gridLength;
                    uiLayoutRoot.RowDefinitions.Add(newRow);

                    pane.Name = ControlNames.InformationPane;
                    pane.SetValue(Grid.RowProperty, 1);
                    uiLayoutRoot.Children.Add(pane);
                }
            }
        }

        internal static UIElement GetInfoPane(Grid uiLayoutRoot)
        {
            UIElement result = uiLayoutRoot.FindName(ControlNames.InformationPane) as UIElement;
            return result;
        }

        internal static void ApplyOfficeBlackThemeForChart(RadChart radChart, ChartArea chartArea, ChartLegend chartLegend)
        {
            //Old Fills 
            //radChart.PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 0, 198, 255)));
            //radChart.PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 27, 255, 0)));
            //radChart.PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 255, 141, 0)));
            //radChart.PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 255, 35, 0)));
            //radChart.PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 169, 0, 255)));
            //radChart.PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 255, 0, 212)));
            //radChart.PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 55, 172, 179)));
            //radChart.PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 180, 191, 53)));
            //radChart.PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 54, 92, 161)));
            //radChart.PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb(255, 152, 115, 93)));

            //radChart.DefaultView.ChartArea.AxisX.Title = "AxisX";
            //radChart.DefaultView.ChartArea.AxisY.Title = "AxisY";
            //radChart.DefaultView.ChartTitle.Content = "Office Black";

            radChart.Style = Application.Current.Resources["CustomChart"] as Style;
            radChart.AxisElementBrush = new SolidColorBrush(Colors.White);
            radChart.AxisForeground = new SolidColorBrush(Colors.White);

            if (chartLegend != null)
            {
                chartLegend.Style = Application.Current.Resources["CustomLegend"] as Style;
                chartLegend.LegendItemStyle = Application.Current.Resources["CustomLegendItemTemplate"] as Style;
            }

            radChart.DefaultView.ChartTitle.Style = Application.Current.Resources["CustomTitle"] as Style;

            if (chartArea != null)
            {
                chartArea.AxisX.AxisStyles.AlternateStripLineStyle = Application.Current.Resources["HorizontalStripLineStyle"] as Style;
                chartArea.AxisY.AxisStyles.AlternateStripLineStyle = Application.Current.Resources["VerticalStripLineStyle"] as Style;
            }
        }

        internal static void ApplyMouseWheelScrollViewer(ScrollViewer scrollViewer)
        {
            Telerik.Windows.Input.Mouse.AddMouseWheelHandler(scrollViewer, new EventHandler<Telerik.Windows.Input.MouseWheelEventArgs>(OnMouseWheelHandler));
        }

        private static void OnMouseWheelHandler(object sender, Telerik.Windows.Input.MouseWheelEventArgs args)
        {
            if (args.Handled)
            {
                return;
            }
            ScrollViewer scrollViewer = sender as ScrollViewer;
            int scrollDelta = args.Delta / 3;
            if (scrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Visible)
            {
                scrollViewer.ScrollToVerticalOffset(
                    Math.Min(scrollViewer.ScrollableHeight,
                         scrollViewer.VerticalOffset - scrollDelta));
                args.Handled = true;
            }
            else if (scrollViewer.ComputedHorizontalScrollBarVisibility == Visibility.Visible)
            {
                scrollViewer.ScrollToHorizontalOffset(
                    Math.Min(scrollViewer.ScrollableWidth,
                         scrollViewer.HorizontalOffset - scrollDelta));
                args.Handled = true;
            }
        }

        /// <summary>
        /// find the control starting from the give control (container).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="targetType"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public static T FindControl<T>(UIElement parent, Type targetType, string ControlName) where T : FrameworkElement
        {

            if (parent == null) return null;

            if (parent.GetType() == targetType && ((T)parent).Name == ControlName)
            {
                return (T)parent;
            }
            T result = null;
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                UIElement child = (UIElement)VisualTreeHelper.GetChild(parent, i);

                if (FindControl<T>(child, targetType, ControlName) != null)
                {
                    result = FindControl<T>(child, targetType, ControlName);
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// find the parent control starting from the given control.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static T FindParent<T>(UIElement control) where T : UIElement
        {
            UIElement p = VisualTreeHelper.GetParent(control) as UIElement;
            if (p != null)
            {
                if (p is T)
                    return p as T;
                else
                    return UiHelper.FindParent<T>(p);
            }
            return null;
        }

        public static void Validate(FrameworkElement control, DependencyProperty dp)
        {
            BindingExpression be = control.GetBindingExpression(dp);
            be.UpdateSource();
        }

        public static bool CheckValidate(Panel dataForm)
        {
            foreach (UIElement ui in dataForm.Children)
            {
                if ((ui as FrameworkElement) != null)
                {
                    foreach (ValidationError error in Validation.GetErrors(ui))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        internal static void ToggleShowHideElement(FrameworkElement element)
        {
            if (element.Visibility == Visibility.Visible)
            {
                element.Visibility = Visibility.Collapsed;
            }
            else
            {
                element.Visibility = Visibility.Visible;
            }
        }

        public static BitmapImage ToBitmapImageFromBytes(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            BitmapImage b = new BitmapImage();
            b.SetSource(ms);

            return b;
        }

        public static WriteableBitmap ResizeImage(Stream stream, double maxWidth, double maxHeight)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.SetSource(stream);

            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            //img.Effect = new DropShadowEffect() { ShadowDepth = 0, BlurRadius = 0 };
            img.Source = bmp;

            double scaleX = 1;
            double scaleY = 1;

            if (bmp.PixelHeight > maxHeight)
                scaleY = maxHeight / bmp.PixelHeight;
            if (bmp.PixelWidth > maxWidth)
                scaleX = maxWidth / bmp.PixelWidth;

            // maintain aspect ratio by picking the most severe scale
            double scale = Math.Min(scaleY, scaleX);

            int newWidth = Convert.ToInt32(bmp.PixelWidth * scale);
            int newHeight = Convert.ToInt32(bmp.PixelHeight * scale);
            WriteableBitmap result = new WriteableBitmap(newWidth, newHeight);
            result.Render(img, new ScaleTransform() { ScaleX = scale, ScaleY = scale });
            result.Invalidate();
            return result;
        }

        public static WriteableBitmap ResizeImage(Stream stream, double targetSize)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.SetSource(stream);
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            img.Source = bmp;
            double scaleX = 1;
            double scaleY = 1;
            if (bmp.PixelHeight > targetSize)
                scaleY = targetSize / bmp.PixelHeight;
            if (bmp.PixelWidth > targetSize)
                scaleX = targetSize / bmp.PixelWidth;
            double scale = Math.Min(scaleY, scaleX);
            int newWidth = Convert.ToInt32(bmp.PixelWidth * scale);
            int newHeight = Convert.ToInt32(bmp.PixelHeight * scale);
            WriteableBitmap result = new WriteableBitmap(newWidth, newHeight);
            result.Render(img, new ScaleTransform() { ScaleX = scale, ScaleY = scale });
            result.Invalidate();
            return result;
        }

        public static Stream EncodeWriteableBitmap(WriteableBitmap bitmap, int quality)
        {
            //Convert the Image to pass into FJCore
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;
            int bands = 3;

            byte[][,] raster = new byte[bands][,];

            for (int i = 0; i < bands; i++)
            {
                raster[i] = new byte[width, height];
            }

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    int pixel = bitmap.Pixels[width * row + column];
                    raster[0][column, row] = (byte)(pixel >> 16);
                    raster[1][column, row] = (byte)(pixel >> 8);
                    raster[2][column, row] = (byte)pixel;
                }
            }

            ColorModel model = new ColorModel { colorspace = ColorSpace.RGB };

            FluxJpeg.Core.Image img = new FluxJpeg.Core.Image(model, raster);

            //Encode the Image as a JPEG
            MemoryStream stream = new MemoryStream();
            JpegEncoder encoder = new JpegEncoder(img, quality, stream);

            encoder.Encode();

            //Move back to the start of the stream
            stream.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }


        public static byte[] SaveToByteArray(WriteableBitmap writeableBitmap)
        {
            int length = writeableBitmap.Pixels.Length * 4;
            var buffer = new byte[length];
            Buffer.BlockCopy(writeableBitmap.Pixels, 0, buffer, 0, length);
            return buffer;
        }

        public static byte[] ToByteArray(WriteableBitmap bmp)
        {
            // Init buffer
            int w = bmp.PixelWidth;
            int h = bmp.PixelHeight;
            int[] p = bmp.Pixels;
            int len = p.Length;
            byte[] result = new byte[4 * w * h];

            // Copy pixels to buffer
            for (int i = 0, j = 0; i < len; i++, j += 4)
            {
                int color = p[i];
                result[j + 0] = (byte)(color >> 24); // A
                result[j + 1] = (byte)(color >> 16); // R
                result[j + 2] = (byte)(color >> 8);  // G
                result[j + 3] = (byte)(color);       // B
            }

            return result;
        }

		[System.Diagnostics.DebuggerStepThrough()]
		public static ToggleState ToCheckState(bool input)
		{
			return input ? ToggleState.On : ToggleState.Off;
		}

        public static void SetEmployeeSelectionVisible(CheckBox employeeCheckbox, ScrollViewer scrollViewerEmployee)
        {
            if (employeeCheckbox.IsChecked == true)
            {
                scrollViewerEmployee.Visibility = Visibility.Visible;
            }
            else
            {
                scrollViewerEmployee.Visibility = Visibility.Collapsed;
            }
        }

        public static void SetTemplateContentLength(CheckBox smsCheckbox, TextBox txtTemplateContent)
        {
            if (smsCheckbox.IsChecked == true)
            {
                txtTemplateContent.MaxLength = Globals.MaxSMSContent;
                string templateContent = txtTemplateContent.Text;
                if (templateContent.Length > Globals.MaxSMSContent)
                {
                    txtTemplateContent.Text = templateContent.Substring(0, Globals.MaxSMSContent);
                }
                else
                {
                    txtTemplateContent.Text = templateContent;
                }
            }
            else
            {
                txtTemplateContent.MaxLength = Globals.MaxEmailContent;
            }
        }

        public static void SetTemplateContentLength(bool isSMSChecked, TextBox txtTemplateContent)
        {
            if (isSMSChecked == true)
            {
                txtTemplateContent.MaxLength = Globals.MaxSMSContent;
            }
            else
            {
                txtTemplateContent.MaxLength = Globals.MaxEmailContent;
            }
        }


	}
}
