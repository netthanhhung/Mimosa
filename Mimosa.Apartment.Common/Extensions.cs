using System;
using System.Data;

namespace Mimosa.Apartment.Common
{
    internal static class Extensions
    {
        /// <summary>
        /// Checks if a column exists in the DataReader
        /// </summary>
        /// <param name="dr">DataReader</param>
        /// <param name="ColumnName">Name of the column to find</param>
        /// <returns>Returns true if the column exists in the DataReader, else returns false</returns>
        public static Boolean ColumnExists(this IDataReader dr, String ColumnName)
        {
            for (Int32 i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(ColumnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static DateTime StartOfWeek(this DateTime refdate, DayOfWeek weekStartDay)
        {
            DateTime result = refdate;

            for (int i = 0; i > -6; i--)
            {
                if (result.DayOfWeek == weekStartDay)
                {
                    break;
                }

                result = result.AddDays(-1);
            }

            return result;
        }

        public static DateTime EndOfMonth(this DateTime refDate)
        {
            return new DateTime(refDate.Year, refDate.Month + 1, 1).AddDays(-1);
        }
    }
}
