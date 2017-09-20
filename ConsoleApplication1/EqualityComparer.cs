using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class EqualityComparer : IEqualityComparer<System.Object>
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(EqualityComparer));
        public new bool Equals(object x, object y)
        {
            //if different classes return false
            //if the same class get attributes values and check them for equals
            if (x == null || y == null || x.GetType() != y.GetType())
                return false;
         //   x.GetType().GetCustomAttributes(false);
            UseForEqualityCheck MyAttribute =
           (UseForEqualityCheck)Attribute.GetCustomAttribute(x.GetType(), typeof(UseForEqualityCheck));

            if (MyAttribute == null)
            {
                log.Error("The attribute was not found.");
            }
            else
            {
                foreach (string UseFieldForCheckEquals in MyAttribute.FieldsForEqualityCheck)
                {
                    FieldInfo[] fi = x.GetType().GetFields(BindingFlags.Public |
                                                 BindingFlags.NonPublic |
                                                 BindingFlags.Instance);
                    FieldInfo x_field = x.GetType().GetField(UseFieldForCheckEquals, BindingFlags.Public |
                                                 BindingFlags.NonPublic |
                                                 BindingFlags.Instance);
                    FieldInfo y_field = y.GetType().GetField(UseFieldForCheckEquals, BindingFlags.Public |
                                                  BindingFlags.NonPublic |
                                                  BindingFlags.Instance);
                    //        object ox = (ObjectType)Activator.CreateInstance(); oy = new object();
                    Type type = x_field.GetType();
                    object ox = x_field.GetValue(x);
                    object oy = y_field.GetValue(y);
                    if (!ox.Equals(oy))
                    {
                        log.Info("false");
                        return false;
                    }
                }

            }

            return true;
        }

        public int GetHashCode(object obj)
        {
            return 0;
            throw new NotImplementedException();
        }
    }
}
