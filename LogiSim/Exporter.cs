using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Xml;

namespace LogiSimLoader.LogiSim
{
    internal class Exporter
    {
        internal static void ExportRecursively(object? currentObject, XmlWriter xw)
        {
            if (currentObject == null) { return; }
            var type = currentObject.GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            // get this object's ElementName
            var elName = GetElementName(currentObject);

            xw.WriteStartElement(elName);

            // sort fields so simple attributes come FIRST, then ELEMENT CONTENTS (important, XmlWriter CANNOT handle this on its own)
            Dictionary<FieldInfo, LSContentAttribute> z = new Dictionary<FieldInfo, LSContentAttribute>();
            foreach (var item in fields)
            {
                z.Add(item, GetAttr(item));
            }
            var ordered = z.OrderBy((x) => x.Value is null ? -1 : (x.Value.IsAttribute ? 0 : 1)).Select(pair => pair.Key).ToList();
            var sepVal = ordered.FirstOrDefault((x) => x.Name == "sepIndex");
            int? sepIndex = null;
            if (sepVal != null)
            {
                sepIndex = (int?)sepVal.GetValue(currentObject);
            }

            // iterate over what is to be written to this element
            for (int x = 0; x < ordered.Count; x++)
            {
                var field = ordered[x];

                var attr = GetAttr(field);
                var val = field.GetValue(currentObject);

                if (val is null) continue; // sure
                if (attr is null) continue; // handling for sepIndex is coming... on another line
                if (attr.IsAttribute)
                {
                    var valType = val.GetType();

                    if (valType == typeof(Vector2))
                    {
                        var vect = (Vector2)val;
                        xw.WriteAttributeString(attr.ElementName, $"({vect.X},{vect.Y})");
                        continue;
                    }

                    xw.WriteAttributeString(attr.ElementName, val.ToString());
                }
                else if (attr.ElementName is null || attr.ElementName == "_")
                {
                    xw.WriteValue(val);
                }
                else
                {
                    // that's uh a COMPLEX object then

                    // lists are handled slightly differently
                    var valType = val.GetType();
                    if (valType.Namespace == "System.Collections.Generic")
                    {
                        // we need to run ExportRecursively for every element of the list

                        var nType = valType.GetGenericArguments()[0]; // the underlying object (List<OBJECT>)
                        var itemProperty = valType.GetProperty("Item");
                        var countProperty = valType.GetProperty("Count");

                        var count = (int)countProperty.GetValue(val);
                        for (int i = 0; i < count; i++)
                        {
                            if (sepIndex != null && i == sepIndex)
                            {
                                xw.WriteStartElement("sep");
                                xw.WriteEndElement();
                                continue;
                            }

                            var iObj = itemProperty.GetValue(val, new object[] { i }); // expected to be an element of List

                            ExportRecursively(iObj, xw);
                        }
                        continue;
                    }

                    ExportRecursively(val, xw);
                }
            }

            xw.WriteEndElement();
        }

        internal static string? GetElementName(object? currentObject)
        {
            if (currentObject == null) return null;
            Type z = currentObject.GetType();
            object[] attributes = z.GetCustomAttributes(typeof(LSContentAttribute), false);
            if (attributes.Length == 0) return null;
            var attr = (LSContentAttribute)attributes[0];
            return attr.ElementName;
        }

        internal static LSContentAttribute? GetAttr(FieldInfo fi)
        {
            var a = fi.GetCustomAttributes(typeof(LSContentAttribute), false);
            if (a.Length == 0) return null;
            return (LSContentAttribute)a[0];
        }
    }
}
