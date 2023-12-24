using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiSimLoader.LogiSim
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class LSContentAttribute : Attribute
    {
        readonly string? elementName;
        readonly bool isAttribute;

        public LSContentAttribute(string? elementName, bool isAttribute = true)
        {
            this.elementName = elementName;
            this.isAttribute = isAttribute;
        }

        public string? ElementName
        {
            get { return elementName; }
        }

        public bool IsAttribute
        {
            get { return isAttribute; }
        }
    }
}
