using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class Image : Record
    {
        #region Public Constructors

        public Image() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string ImageId = "ImageId";
            public const string ImageTypeId = "ImageTypeId";
            public const string ItemId = "ItemId";
            public const string FileName = "FileName";
            public const string ImageContent = "ImageContent";
            public const string ImageSmallContent = "ImageSmallContent";
            public const string DisplayIndex = "DisplayIndex";
            public const string Description = "Description";          
        }

        #endregion

        #region Properties

        private int _ImageId;
        [DataMember]
        public int ImageId { get { return _ImageId; } set { _ImageId = value; RaisePropertyChanged("ImageId"); } }

        private int _imageTypeId;
        [DataMember]
        public int ImageTypeId { get { return _imageTypeId; } set { if (!this.ImageTypeId.Equals(value)) { _imageTypeId = value; RaisePropertyChanged("ImageTypeId"); } } }

        private int _itemId;
        [DataMember]
        public int ItemId { get { return _itemId; } set { if (!this.ItemId.Equals(value)) { _itemId = value; RaisePropertyChanged("ItemId"); } } }

        private string _fileName;
        [DataMember]
        public string FileName { get { return _fileName; } set { if (!object.ReferenceEquals(this.FileName, value)) { _fileName = value; RaisePropertyChanged("FileName"); } } }

        private byte[] _imageContent;
        [DataMember]
        public byte[] ImageContent { get { return _imageContent; } set { if (!object.ReferenceEquals(this.ImageContent, value)) { _imageContent = value; RaisePropertyChanged("ImageContent"); } } }

        private byte[] _imageSmallContent;
        [DataMember]
        public byte[] ImageSmallContent { get { return _imageSmallContent; } set { if (!object.ReferenceEquals(this.ImageSmallContent, value)) { _imageContent = value; RaisePropertyChanged("ImageSmallContent"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        private int _displayIndex;
        [DataMember]
        public int DisplayIndex { get { return _displayIndex; } set { if (!this.DisplayIndex.Equals(value)) { _displayIndex = value; RaisePropertyChanged("DisplayIndex"); } } }

        #endregion
    }
}




