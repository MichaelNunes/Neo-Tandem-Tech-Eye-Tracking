using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections;
using PdfSharp.Drawing.Layout;

namespace StatsClass
{
    class Statistics
    {
        string datasource;

        public string Datasource
        {
            get { return datasource; }
            set { datasource = value; }
        }

        /// <summary>
        /// Represents the name of the model that results will be made for. 
        /// </summary>
        string ModelName;
        public string _modelName
        {
            get { return ModelName; }
            set { ModelName = value; }
        }

        /// <summary>
        /// List<float> containing all X axis co-ordinates.
        /// </summary>
        public List<float> px = new List<float>();
        public List<float> Px
        {
            get { return px; }
            set { px = value; }
        }

        /// <summary>
        /// List<float> containing all Y axis co-ordinates.
        /// </summary>
        public List<float> py = new List<float>();
        public List<float> Py
        {
            get { return py; }
            set { py = value; }
        }

        public Statistics(string source, string model)
        {
            datasource = source;
            ModelName = model;
        }

        public void readFile()
        {
            px = null;
            py = null;
            px = new List<float>();
            py = new List<float>();
            string[] lines = System.IO.File.ReadAllLines(this.datasource + "\\RecordedData_" + this.ModelName + ".txt");
            foreach (string item in lines)
            {
                px.Add((float)Convert.ToDouble(item.Substring(0, item.IndexOf(":"))));

                int temp1 = item.IndexOf(":") + 1;
                int temp2 = item.Length - temp1;
                py.Add((float)Convert.ToDouble(item.Substring(temp1, temp2)));
            }
        }

        public int getTimeofRecording()
        {
            if (py.Count > 0 || px.Count > 0)
            {
                if (py.Count == px.Count)
                {
                    int count = py.Count / 30;
                    return count;
                }
            }
            return 0;

        }

        public void getQuadrantpoints()
        {

        }

        public void createPDF()
        {
            PdfDocument document = new PdfDocument();

            document.Info.Title = "Created with PDFsharp";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("TimesRoman", 30, XFontStyle.Bold);
            gfx.DrawString("Statistical Report", font, XBrushes.Black,new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);

            page = document.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            font = new XFont("TimesRoman", 20, XFontStyle.Bold);
            XTextFormatter tf = new XTextFormatter(gfx);
            ArrayList content = new ArrayList();
            content.Add("Model name: " + ModelName);
            content.Add("Model type: ");
            content.Add("Model location: " + datasource);
            string sContent = string.Join("\n", content.ToArray());
            tf.DrawString(sContent, font, XBrushes.Black, new XRect(50, 50, page.Width, page.Height), XStringFormats.TopLeft);

            string filename = "Statistical Report_"+ModelName+".pdf";
            document.Save(datasource+filename);

        }

    }
}
