using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWebApp.Tests
{
    public static class ScrapeHelper
    {
        public static int _value = 5;

        public static string FileName(string folder, string region)
        {
            string basePath = @"C:\Users\Public\TestFolder";
            string provinceName = "NU";
            string filePath = String.Concat(DateTime.Now.ToFileTime().ToString(), ".json");
            string combinedPath = Path.Combine(basePath, provinceName, filePath); 
            return "asdf";
        }
    }
}
