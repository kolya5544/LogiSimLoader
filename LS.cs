using LogiSimLoader.LogiSim;
using System;
using System.IO;

namespace LogiSimLoader
{
    public class LS
    {
        public static LSFile LoadFromFile(string filename) => Loader.LoadFromFile(filename);
        public static LSFile Load(string content) => Loader.Load(content);

        public static void ExportToFile(LSFile file, string filename) => File.WriteAllText(filename, file.Export());
        public static string Export(LSFile file) => file.Export();
    }
}
