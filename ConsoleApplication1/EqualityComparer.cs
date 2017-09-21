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

        private List<FieldInfo> GetFieldsForEqualityCheck(object x)
        {
            List<FieldInfo> listOfFields = new List<FieldInfo>();
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
                    listOfFields.Add(x.GetType().GetField(UseFieldForCheckEquals, BindingFlags.Public |
                                                 BindingFlags.NonPublic |
                                                 BindingFlags.Instance));
                }
            }
            return listOfFields;
        }
        public new bool Equals(object x, object y)
        {
            //if different classes return false
            //if the same class get attributes values and check them for equals
            if (x == null || y == null || x.GetType() != y.GetType())
                return false;
            //   x.GetType().GetCustomAttributes(false);
            foreach (FieldInfo checkedField in GetFieldsForEqualityCheck(x))
            {
                object ox = checkedField.GetValue(x);
                object oy = checkedField.GetValue(y);
                if (!ox.Equals(oy))
                {
                    log.Info("false");
                    return false;
                }
            }


            return true;
        }

        public int GetHashCode(object x)
        {
            int hash = 0;
            foreach (FieldInfo checkedField in GetFieldsForEqualityCheck(x))
            {
                object ox = checkedField.GetValue(x);
                hash += ox.GetType().GetHashCode();
            }
            return hash;
        }
    }
}
