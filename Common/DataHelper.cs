using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common
{
    public class DataHelper
    {
        public static void SelfLoopObjects(object Source)
        {
            if (Source == null || Source as IEnumerable == null) return;
            IEnumerable list = (IEnumerable)Source;
            foreach (var item in list)
            {
                SelfLoopObject(item);
            }

        }

        public static void SelfLoopObject(object Source)
        {
            if (Source != null)
            {
                var notMap = Source.GetType().GetProperties().Where(o => o.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute), true) != null);

                foreach (PropertyInfo item in notMap)
                {
                    if (item.GetGetMethod() != null)
                        item.GetValue(Source, null);
                }

                var mapProprrty = Source.GetType().GetProperties();
                foreach (PropertyInfo item in mapProprrty)
                {
                    if (item.GetSetMethod() != null)
                        item.SetValue(Source, null, null);
                }
            }
        }
    }
}
