using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LogiSimLoader.LogiSim
{
    [LSContent("project", false)]
    public class LSFile
    {
        [LSContent("source")]
        public string sourceVersion = "2.7.1";
        [LSContent("version")]
        public string version = "1.0";
        [LSContent(null, false)]
        public string text = "This file is intended to be loaded by Logisim (http://www.cburch.com/logisim/).";

        [LSContent("lib", false)]
        public List<LSLib> libraries = new List<LSLib>();
        [LSContent("main", false)]
        public LSMain main = new LSMain();
        [LSContent("options", false)]
        public LSOptions options = new LSOptions();
        //public List<LSAttribute> options = new();
        [LSContent("mappings", false)]
        public LSMapping mappings = new LSMapping();
        [LSContent("toolbar", false)]
        public LSToolbar toolbar = new LSToolbar();

        [LSContent("circuit", false)]
        public List<LSCircuit> circuits = new List<LSCircuit>();

        public void Parse(string content)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);

            // project element
            var projectNode = doc.DocumentElement.SelectSingleNode("/project");

            // parse recursively
            Parser.ParseRecursively(this, projectNode);
        }

        public string Export()
        {
            using (var sw = new StringWriter())
            using (var xw = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true }))
            {
                xw.WriteStartDocument(false); // should be false for LogiSim files

                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);

                /*var xmlSerializer = new XmlSerializer(typeof(data));
                xmlSerializer.Serialize(xw, data);*/

                Exporter.ExportRecursively(this, xw);

                xw.Flush();

                return sw.ToString().Replace("utf-16", "UTF-8"); // lol (this *is* necessary)
            }
        }
    }
}
