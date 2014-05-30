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
        public enum Gender
        {
            Male = 1,
            Female = 0            
        }
    }
}
