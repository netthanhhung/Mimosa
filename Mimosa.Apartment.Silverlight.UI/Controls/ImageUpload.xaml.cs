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
    public partial class ImageUpload : UserControl
    {
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        internal event EventHandler DataBound;
        internal event EventHandler SaveEmployeePhotoComplete;

        private static class UserMessages
        {
            internal const string FileTooLarge = "The size of this file is too big, please choose another one.";
        }


        private byte[] _byteArray;
        private byte[] _byteArraySmall;
        public List<common.Image> ImageList { get; set; }
        public common.ImageType ImageType { get; set; }
        public int ItemId { get; set; }

        public bool ShowSaveButtion
        {
            get { return gridBtnSave.Visibility == Visibility.Visible; }
            set
            {
                gridBtnSave.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public ImageUpload()
        {
            InitializeComponent();

            btnSave.Click += new RoutedEventHandler(btnSave_Click);
            btnAdd.Click += new RoutedEventHandler(btnAdd_Click);
            btnChooseImage.Click += new RoutedEventHandler(btnChooseImage_Click);
            btnUploadOK.Click += new RoutedEventHandler(btnUploadOK_Click);
            
        }

        #region Image List

        public void BeginRebind()
        {
            Globals.IsBusy = true;
            DataServiceHelper.ListImageAsync(null, this.ItemId, (int)this.ImageType, 2, ListImageCompleted);
        }

        private void ListImageCompleted(List<common.Image> imageList)
        {
            Globals.IsBusy = false;
            listImages.Items.Clear();
            
            foreach (common.Image item in imageList)
            {
                ImageList.Add(item);
                ImageItem imageItem = new ImageItem(item);
                imageItem.btnDelete.Click += new RoutedEventHandler(btnDelete_Click);
                listImages.Items.Add(imageItem);
            }

            if (imageList.Count > 0)
            {
                listImages.Visibility = System.Windows.Visibility.Collapsed;
                btnSave.IsEnabled = false;
            }
            else
            {
                listImages.Visibility = System.Windows.Visibility.Visible;
                btnSave.IsEnabled = true;
            }
        }


        void btnSave_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SaveComplete()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            BeginRebind();
        }

        void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btnDelete = (Button)sender;
            ImageItem imageItem = btnDelete.Parent as ImageItem;
            if (imageItem != null)
            {
                listImages.Items.Remove(imageItem);
            }
        }
        #endregion

        #region Upload Image
        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtFileName.Text = string.Empty;
            imgRoomFigure.Source = null;
            uiPopupUpload.ShowDialog();
        }

        void btnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png";
            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = openFileDialog.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == true && openFileDialog.File != null)
            {
                BitmapImage imageSource = new BitmapImage();
                try
                {
                    FileStream fs = openFileDialog.File.OpenRead();
                    if (fs.Length < int.MaxValue)
                    {
                        imageSource.SetSource(fs);
                        //imgRoomFigure.Source = imageSource;
                        WriteableBitmap resizeSource = UiHelper.ResizeImage(fs, 200); //max 200px

                        txtFileName.Text = openFileDialog.File.Name;
                        _byteArray = new byte[fs.Length];
                        fs.Position = 0;
                        fs.Read(_byteArray, 0, (int)fs.Length);
                        fs.Close();

                        //_byteArraySmall = UiHelper.ToByteArray(resizeSource);
                        
                        using (Stream source = UiHelper.EncodeWriteableBitmap(resizeSource, 100))
                        {
                            int bufferSize = Convert.ToInt32(source.Length);
                            _byteArraySmall = new byte[bufferSize];
                            source.Read(_byteArraySmall, 0, bufferSize);
                            source.Close();
                        }

                        imgRoomFigure.Source = UiHelper.ToBitmapImageFromBytes(_byteArraySmall);

                    }
                    else
                    {
                        MessageBox.Show(UserMessages.FileTooLarge, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Loading File : " + ex.Message, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                }
            }
        }


        void btnUploadOK_Click(object sender, RoutedEventArgs e)
        {
            common.Image newImage = new common.Image();
            newImage.IsChanged = true;
            newImage.ImageContent = _byteArray;
            newImage.ImageSmallContent = _byteArraySmall;
            newImage.CreatedBy = newImage.UpdatedBy = Globals.UserLogin.UserName;
            newImage.DisplayIndex = ImageList != null && ImageList.Count > 0 ? (ImageList.Max(i => i.DisplayIndex) + 1) : 1;
            newImage.ImageTypeId = (int) this.ImageType;
            newImage.ItemId = this.ItemId;

            ImageItem imageItem = new ImageItem(newImage);
            imageItem.btnDelete.Click += new RoutedEventHandler(btnDelete_Click);
            listImages.Items.Add(imageItem);

            uiPopupUpload.Close();
        }

        #endregion

        

    }
}
