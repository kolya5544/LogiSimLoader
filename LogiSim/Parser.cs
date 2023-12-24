using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Xml;

namespace LogiSimLoader.LogiSim
{
    internal class Parser
    {
        internal static object? ParseRecursively(object? currentObj, XmlNode currentNode, Type type = null)
        {
            object? mostValuable = currentObj;

            if (currentObj != null && type is null) type = currentObj.GetType();

            var children = currentNode.ChildNodes;
            foreach (object child in children)
            {
                var childType = child.GetType();

                if (childType == typeof(System.Xml.XmlText))
                {
                    var transformed = (XmlText)child;

                    //Console.WriteLine(transformed.Name);

                    // look for an appropriate field...
                    var field = GetAppropriateField("text", type);

                    if (field is null) continue;
                    field.SetValue(currentObj, transformed.InnerText);
                }
                else if (childType == typeof(System.Xml.XmlElement))
                {
                    var transformed = (XmlElement)child;

                    //Console.WriteLine(transformed.Name);

                    var field = GetAppropriateField("element", type, transformed.Name);

                    if (field is null)
                    {
                        // handling some edge cases for BEST effect <- important
                        if (transformed.Name == "sep") // we only need to REMEMBER the position of sep, not its value (which is always empty)
                        {
                            var e = ((LSToolbar)currentObj);
                            e.sepIndex = e.tool.Count;
                        }
                        continue;
                    }

                    if (field.FieldType.Namespace == "System.Collections.Generic") // is a list
                    {
                        var nType = field.FieldType.GetGenericArguments()[0];
                        var newObj = Activator.CreateInstance(nType);

                        // do it RECURSIVELY
                        var actualObject = ParseRecursively(newObj, (XmlNode)child, nType);
                        var lObj = field.GetValue(currentObj);
                        if (lObj is null)
                        {
                            lObj = Activator.CreateInstance(field.FieldType);
                            field.SetValue(currentObj, lObj);
                        }
                        if (actualObject != null)
                        {
                            var genericAdd = field.FieldType.GetMethod("Add");
                            genericAdd.Invoke(lObj, new object[] { actualObject });
                            //list.Add(actualObject);
                        }
                    }
                    else // is an object of some kind?
                    {
                        var newObj = Activator.CreateInstance(field.FieldType);

                        var actualObject = ParseRecursively(newObj, (XmlNode)child, field.FieldType);

                        field.SetValue(currentObj, actualObject);
                        //ParseRecursively();
                    }
                }
                //Console.WriteLine(child);
            }

            foreach (var attr in currentNode.Attributes)
            {
                // yep we do the same thing here :)
                var actAttr = (XmlAttribute)attr;

                //Console.WriteLine(actAttr.Name);

                var field = GetAppropriateField("attribute", type, actAttr.Name);

                var l = field.FieldType.GetGenericArguments();
                if (field.FieldType == typeof(Vector2) || (l.Length == 1 && l[0] == typeof(Vector2)))
                {
                    // do some parsing (1,5)
                    var vals = actAttr.Value.Trim(new char[] { '(', ')' }).Split(',');
                    var newVector = new Vector2(int.Parse(vals[0]), int.Parse(vals[1]));
                    field.SetValue(currentObj, newVector);
                    continue;
                }
                else if (field.FieldType == typeof(int) || (l.Length == 1 && l[0] == typeof(int)))
                {
                    var intVal = int.Parse(actAttr.Value);
                    field.SetValue(currentObj, intVal);
                    continue;
                }
                field.SetValue(currentObj, actAttr.Value);
            }

            return mostValuable;
        }

        private static FieldInfo? GetAppropriateField(string criteria, Type type, string name = null)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            switch (criteria)
            {
                case "text":
                    return fields.FirstOrDefault((z) =>
                    {
                        object[] attributes = z.GetCustomAttributes(typeof(LSContentAttribute), false);
                        if (attributes.Length == 0) return false;
                        var attr = (LSContentAttribute)attributes[0];

                        return attr.ElementName is null || attr.ElementName == "_";
                    });
                case "element":
                    return fields.FirstOrDefault((z) =>
                    {
                        object[] attributes = z.GetCustomAttributes(typeof(LSContentAttribute), false);
                        if (attributes.Length == 0) return false;
                        var attr = (LSContentAttribute)attributes[0];

                        return !string.IsNullOrEmpty(attr.ElementName) && attr.ElementName.Equals(name, StringComparison.OrdinalIgnoreCase) && !attr.IsAttribute;
                    });
                case "attribute":
                    return fields.FirstOrDefault((z) =>
                    {
                        object[] attributes = z.GetCustomAttributes(typeof(LSContentAttribute), false);
                        if (attributes.Length == 0) return false;
                        var attr = (LSContentAttribute)attributes[0];

                        return !string.IsNullOrEmpty(attr.ElementName) && attr.ElementName.Equals(name, StringComparison.OrdinalIgnoreCase) && attr.IsAttribute;
                    });
            }

            return null;
        }
    }
}
