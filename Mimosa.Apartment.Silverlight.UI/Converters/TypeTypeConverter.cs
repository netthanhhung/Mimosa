// TODO: EJM - Come back to EnumViewModel/TypeTypeConverter and find the camel or decoration as text.
// http://www.telerik.com/help/silverlight/radcombobox-howto-bind-enum-values.html

//using System;
//using System.Net;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Ink;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;
//using System.ComponentModel;

//namespace Mimosa.Apartment.Silverlight.UI
//{
//    public class TypeTypeConverter : TypeConverter
//    {
//        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
//        {
//            return sourceType.IsAssignableFrom(typeof(string));
//        }

//        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
//        {
//            // Try to load the type from the current assembly (EnumValuesInCombo.dll)
//            Type t = Type.GetType((string)value, false);
//            // If the type is from a different known assembly, try to load it from there
//            if (t == null)
//            {
//                // Try to load the type from Telerik.Windows.Controls.dll
//                t = this.GetTypeFromAssembly(value.ToString(), typeof(Telerik.Windows.Controls.ItemsControl));
//            }
//            if (t == null)
//            {
//                // Try to load the type from System.Windows.dll
//                t = this.GetTypeFromAssembly(value.ToString(), typeof(System.Windows.Controls.ItemsControl));
//            }
//            // You can also try with other known assemblies.
//            //if (t == null)
//            //{
//            //    t = GetTypeFromAssembly(value.ToString(), typeof(a type that is in the assembly, containing the enum));
//            //}
//            return t;
//        }

//        private Type GetTypeFromAssembly(string typeName, Type knownType)
//        {
//            string assemblyName = knownType.AssemblyQualifiedName;
//            return Type.GetType(assemblyName.Replace(knownType.FullName, typeName), false);
//        }
//    }
//}
