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
using System.Collections.Generic;
using System.Threading;

namespace Mimosa.Apartment.Silverlight.UI
{
	internal class DateHelper
	{
		internal DateTime StartOfMonth { get; set; }
		internal DateTime EndOfMonth { get; set; }
		internal int DaysInMonth { get; set; }

		internal DateTime StartOfLastMonth { get; set; }
		internal DateTime EndOfLastMonth { get; set; }
		internal int DaysInLastMonth { get; set; }

		// Shows the start of last month if today is the first.
		internal DateTime StartOfMonthOrLast { get; set; }

		internal DateHelper(DateTime sourceDate)
		{
			StartOfMonth = new DateTime(sourceDate.Year, sourceDate.Month, 1);
			EndOfMonth = (StartOfMonth.AddMonths(1)).AddDays(-1);
			DaysInMonth = EndOfMonth.Day;

			DateTime lastMonth = sourceDate.AddMonths(-1);
			StartOfLastMonth = new DateTime(lastMonth.Year, lastMonth.Month, 1);
			EndOfLastMonth = (StartOfLastMonth.AddMonths(1)).AddDays(-1);
			DaysInLastMonth = EndOfLastMonth.Day;

			StartOfMonthOrLast = sourceDate.Day == 1 ? StartOfLastMonth : StartOfMonth;
		}

        internal DateHelper() : this(Globals.Today) { }

        internal static IDictionary<int, string> MonthList
        {
            get
            {
                IDictionary<int, string> monthList = new Dictionary<int, string>();
                for (int i = 1; i <= 12; i++)
                {
                    monthList.Add(i, Thread.CurrentThread.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[i - 1]);
                }

                return monthList;
            }
        }

        internal static string[] AbbreviatedMonthNames
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
            }
        }

        public static string DateToString(DateTime date, string format)
        {
            return date.ToString(format);
        }

        public static string DateToString(DateTime date)
        {
            return date.ToString("dd-MMM-yyyy");
        }
    }
}
