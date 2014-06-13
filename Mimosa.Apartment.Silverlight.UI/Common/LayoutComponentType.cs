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
    public enum LayoutComponentType : int
    {
        None = 0,
        RoleAdmin = 1,
        RoleComponentPermission = 2,
        UserRoleAuthorisation = 3,
        SiteAdmin = 4,
        SiteGroupAdmin = 5,
        EquipmentAdmin = 6,
        ServiceAdmin = 7,
        RoomAdmin = 8,

        BookingAdmin = 101,

        CustomerAdmin = 201,
        MonthlyPayment = 202,
    }
}
