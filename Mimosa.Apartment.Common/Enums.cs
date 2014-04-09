using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mimosa.Apartment.Common
{
    public static class Enum<T> where T : struct
    {
        public static T Parse(object value)
        {
            return Parse(value.ToString());
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, false);
        }
    }

    public static class Enums
    {
        public enum UtilityType
        {
            None,
            Electricity,
            Gas
        }

        public enum UtilityValueType
        {
            Usage,
            Cost
        }

        public enum SiteStructureLevel
        {
            None = 0,
            Site,
            Department,
            RosterCentre,
            Centre,
            SubCentre
        }

        public enum DistributionChannelType
        {
           DistributionChannel,
           DistributionChannelBalance,
           CompanyAgent,
           CompanyAgentBalance,
           Division,
           DivisionBalance,
           PnL
        }

        public enum MailTemplateType
        {
            Payroll = 1,
            EmployeeFeedback = 2,
            OvertimeNotification = 3
        }


        public enum ExchangeServiceResult
        {
            ServerConnectionFailed = 1,
            AuthenticationFailed = 2,
            AppointmentAdded = 5,
        }

        public enum PMSSystemType
        {
            Protel,
            CRS
        }
    }
}
