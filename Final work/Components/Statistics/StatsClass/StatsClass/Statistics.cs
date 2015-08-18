using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Exceptions;
using System.Collections;

namespace StatsClass
{
    class Statistics
    {
        string datasource;
        /// <summary>
        /// Sets and gets the data source
        /// </summary>
        public string Datasource
        {
            get { return datasource; }
            set { datasource = value; }
        }

        List<int> pointCounter;

        public List<int> PointCounter
        {
            get { return pointCounter; }
            set { pointCounter = value; }
        }

        int height;
        /// <summary>
        /// gets or sets the height
        /// </summary>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        int width;
        /// <summary>
        /// gets and sets the width
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; }
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

        public Statistics(string source, string model,int w,int h)
        {
            datasource = source;
            ModelName = model;
            height = h;
            width = w;
            pointCounter = null;
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

        public void getgridsCount()
        {
            this.readFile();

        }

        public int getGridHeight()
        {
            return this.height / 3;
        }

        public int getGridWidth()
        {
            return this.width / 3;
        }

        public pdfTable createTable(ArrayList aList, int rows, int cols)
        {
            /*Table's creation*/
            pdfTable myTable = new pdfTable();
            //Set table's border
            myTable.borderSize = 1;
            myTable.borderColor = predefinedColor.csDarkBlue;
            /*Create table's header*/
            for (int i = 0; i < cols; i++)
            {
                myTable.tableHeader.addColumn(new pdfTableColumn(aList[i].ToString(), predefinedAlignment.csCenter, 70));
            }
            /*Create table's rows*/
            int counter = 0;
            for (int i = 0; i < rows; i++)
            {
                pdfTableRow myRow = myTable.createRow();
                for (int j = 0; j < cols; j++)
                {
                    myRow[j].columnValue = aList[cols + counter + j].ToString();
                }
                myTable.addRow(myRow);
                counter += cols;
            }
            /*Set Header's Style*/
            myTable.tableHeaderStyle = new pdfTableRowStyle(predefinedFont.csCourierBoldOblique, 12, predefinedColor.csBlack, predefinedColor.csLightCyan);
            /*Set Row's Style*/
            myTable.rowStyle = new pdfTableRowStyle(predefinedFont.csCourier, 8, predefinedColor.csBlack, predefinedColor.csWhite);
            /*Set Alternate Row's Style*/
            myTable.alternateRowStyle = new pdfTableRowStyle(predefinedFont.csCourier, 8, predefinedColor.csBlack, predefinedColor.csLightYellow);
            /*Set Cellpadding*/
            myTable.cellpadding = 20;
            return myTable;
        }

        public void createPDF()
        {
            pdfDocument myDoc = new pdfDocument("Statistics Report", "Created with PDFsharp");
            pdfPage CoverPage = myDoc.addPage();
            CoverPage.addText("Statistical Report", 200, 450, predefinedFont.csTimesBold, 20);

            pdfPage FirstPage = myDoc.addPage();
            ArrayList content = new ArrayList();
            content.Add("Model name: " + ModelName);
            content.Add("Model type: ");
            content.Add("Model location: " + datasource);
            string sContent = string.Join("\n", content.ToArray());
            FirstPage.addText(sContent, 200, 450, predefinedFont.csTimesBold, 20);

            pdfPage SecondPage = myDoc.addPage();
            ArrayList tableContents = new ArrayList();
            tableContents.Add("Col 1");
            tableContents.Add("Col 2");
            tableContents.Add("Col 3");
            tableContents.Add("1");
            tableContents.Add("2");
            tableContents.Add("3");
            tableContents.Add("4");
            tableContents.Add("5");
            tableContents.Add("6");
            pdfTable myTable = createTable(tableContents,2,3); // Its a 3x3 table, but the first row is the column headings
            SecondPage.addTable(myTable, 100, 600);

            string filename = "Statistical Report_" + ModelName + ".pdf";
            myDoc.createPDF(datasource + filename);

            CoverPage = null;
            FirstPage = null;
            myTable = null;
            SecondPage = null;
            myDoc = null; 

        }

    }
}
