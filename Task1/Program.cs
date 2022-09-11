using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Task1
{
    class Point
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Point()
        {

        }
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
        public string ToString()
        {
            return $"{X},{Y}";
        }
    }
    class Program
    {
        private static List<Point> MinizePoint(List<Point> A)
        {
            var MinY = A.Select(c => c.Y).Min() - 5;
            var MinX = A.Select(c => c.X).Min() - 5;
            return A.Select(c => new Point(c.X - MinX, c.Y - MinY)).ToList();
        }
        private static void ExportSVG(string filepath, List<Point> finalPoints)
        {
            // Initialize an SVG document from a string content
            try
            {
                var Mpoints = MinizePoint(finalPoints);
                var Content = System.IO.File.ReadAllText(@"SVGFormatXml.xml");
                var Height = Mpoints.Select(c => c.Y).Max();
                var Width = Mpoints.Select(c => c.X).Max();
                var SvgContent = string.Format(Content, Height, Width, string.Join(" ", Mpoints.Select(c => $"{c.X},{c.Y}")), @"fill:#044B94;fill-opacity:0.0;stroke:blue");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(SvgContent);
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                // Save the document to a file and auto-indent the output.
                XmlWriter writer = XmlWriter.Create(filepath, settings);
                doc.Save(writer);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        static void Main(string[] args)
        {
            var points = new List<Point>() { new Point(5, 105), new Point(100, 5), new Point(235, 22), new Point(199, 198) };
            Console.WriteLine("Input Scale size:");
            double scale = double.Parse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture);
            var scaleVale = Convert.ToSingle(scale);
            var scalePoints = points.Select(c => new Point() { X = c.X * scaleVale, Y = c.Y * scaleVale }).ToList();
            ExportSVG("Root.svg", points);
            ExportSVG("Scale.svg", scalePoints);
        }
    }
}
