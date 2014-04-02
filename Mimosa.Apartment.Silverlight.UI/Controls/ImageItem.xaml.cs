using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;
using common = Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class ImageItem : UserControl
    {
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        
        public common.Image ImageDataItem { get; set; }
        public common.ImageType ImageType { get; set; }
        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public ImageItem(common.Image imageItem)
        {
            InitializeComponent();
            ImageDataItem = imageItem;
            ImageDataItem.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ImageDataItem_PropertyChanged);
            DataBind();
        }

        void ImageDataItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                common.Image item = (common.Image)sender;
                item.IsChanged = true;
            }
        }

        /*  ======================================================================            
         *      PAGE FUNCTIONS
         *  ====================================================================== */


        private void DataBind()
        {
            this.DataContext = this.ImageDataItem;
            btnDelete.Tag = this.ImageDataItem;
            if (this.ImageDataItem != null)
            {
                IsEnabled = true;

                if (this.ImageDataItem.ImageContent != null)
                {
                    imgContent.Source = UiHelper.ToBitmapImageFromBytes(this.ImageDataItem.ImageContent);
                }
                else
                {
                    imgContent.Source = null;
                }
            }
            else
            {
                IsEnabled = false;
                imgContent.Source = null;
            }
        }


    }
}
