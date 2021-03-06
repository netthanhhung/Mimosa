﻿using System;
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
using System.Windows.Data;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class UserAccountPage : Page
    {
        
        public UserAccountPage()
        {            
            InitializeComponent();

            uiTitle.Text = ResourceHelper.GetReourceValue("UserAccountPage_uiTitle");
            ucUserAccount.IsEditable = true;
            ucUserAccount.RebindData();
        }

    }
}
