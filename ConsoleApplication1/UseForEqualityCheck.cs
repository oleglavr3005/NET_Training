using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct)
    ]
    public class UseForEqualityCheck:System.Attribute
    {
     public   String[] FieldsForEqualityCheck;

        public UseForEqualityCheck(params String[] fields)
        {
            FieldsForEqualityCheck = fields;
        }
    }
}
