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
    public class Statistics
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

        string output;

        public string Outputlocation
        {
            get { return output; }
            set { output = value; }
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

        int excludedpoints;

        public int Excludedpoints
        {
            get { return excludedpoints; }
            set { excludedpoints = value; }
        }

        string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        string PName;

        public string pName
        {
            get { return PName; }
            set { PName = value; }
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

        public Statistics(string source,string outter, string model,int w,int h,string t, string pname)
        {
            datasource = source;
            output = outter;
            ModelName = model;
            height = h;
            width = w;
            pointCounter = null;
            excludedpoints = 0;
            this.type = t;
            pName = pname;
        }

        public void readFile()
        {
            px = null;
            py = null;
            px = new List<float>();
            py = new List<float>();
            int count = 1;
            string[] lines = System.IO.File.ReadAllLines(this.datasource + /*"\\" + pName +*/ "\\RecordedData_" + this.ModelName + ".txt");
            foreach (string item in lines)
            {
                if (count == 1)
                {
                   // float b = (float)Convert.ToDouble(item.Substring(0, item.IndexOf("x")));
                    count++;
                }
                else
                {
                    px.Add((float)Convert.ToDouble(item.Substring(0, item.IndexOf(":"))));
                    int temp1 = item.IndexOf(":") + 1;
                    int temp2 = item.Length - temp1;
                    py.Add((float)Convert.ToDouble(item.Substring(temp1, temp2)));
                }
            }
        }

        public double getTimeofRecording()
        {
            if (py.Count > 0 || px.Count > 0)
            {
                if (py.Count == px.Count)
                {
                    
                    double count = (double)py.Count / 30.000;
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
                myTable.tableHeader.addColumn(new pdfTableColumn(aList[i].ToString(), predefinedAlignment.csCenter, 150));
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
            myTable.tableHeaderStyle = new pdfTableRowStyle(predefinedFont.csCourierBoldOblique, 8, predefinedColor.csBlack, predefinedColor.csWhite);
            /*Set Row's Style*/
            myTable.rowStyle = new pdfTableRowStyle(predefinedFont.csCourierBoldOblique, 8, predefinedColor.csBlack, predefinedColor.csWhite);
            /*Set Alternate Row's Style*/
            myTable.alternateRowStyle = new pdfTableRowStyle(predefinedFont.csCourierBoldOblique, 8, predefinedColor.csBlack, predefinedColor.csWhite);
            /*Set Cellpadding*/
            myTable.cellpadding = 20;
            return myTable;
        }

        public List<int> getpoints()
        {
            List<int> points = new List<int>();
            getgridsCount();
            int count = px.Count;
            int height = getGridHeight();
            int width = getGridWidth();
            int one = 0;
            int two = 0;
            int three = 0;
            int four = 0;
            int five = 0;
            int six = 0;
            int seven = 0;
            int eight = 0;
            int nine = 0;

            for (int i = 0; i < count; i++)
            {
                if(px.ElementAt<float>(i) < width && py.ElementAt<float>(i) < height )
                {
                    one++;
                }
                else if (px.ElementAt<float>(i) < (2*width) && py.ElementAt<float>(i) < height)
                {
                    two++;
                }
                else if (px.ElementAt<float>(i) < (3 * width) && py.ElementAt<float>(i) < height)
                {
                    three++;
                }
                else if (px.ElementAt<float>(i) < width && py.ElementAt<float>(i) < (2*height))
                {
                    four++;
                }
                else if (px.ElementAt<float>(i) < (2 * width) && py.ElementAt<float>(i) < (2 * height))
                {
                    five++;
                }
                else if (px.ElementAt<float>(i) < (3 * width) && py.ElementAt<float>(i) < (2 * height))
                {
                    six++;
                }
                else if (px.ElementAt<float>(i) < width && py.ElementAt<float>(i) < (3 * height))
                {
                    seven++;
                }
                else if (px.ElementAt<float>(i) < (2 * width) && py.ElementAt<float>(i) < (3 * height))
                {
                    eight++;
                }
                else if (px.ElementAt<float>(i) < (3 * width) && py.ElementAt<float>(i) < (3 * height))
                {
                    nine++;
                }
                else if(px.ElementAt<float>(i) <= 0 && py.ElementAt<float>(i) <= 0 )
                {
                    this.excludedpoints++;
                }
                else if (px.ElementAt<float>(i) >= this.width && py.ElementAt<float>(i) >= this.height)
                {
                    this.excludedpoints++;
                }

            }

            points.Add(one);
            points.Add(two); 
            points.Add(three);
            points.Add(four);
            points.Add(five);
            points.Add(six);
            points.Add(seven);
            points.Add(eight);
            points.Add(nine);

                return points;
        }

        public string getFocusPoint(List<int> m)
        {
            int max = 1;
            int num = -1;

            for(int i =0;i<m.Count;i++)
            {
                if(m.ElementAt<int>(i) >= max)
                {
                    max = m.ElementAt<int>(i);

                    num=i;
                }
            }

            string outmsg = "";

            
            if(num == 0)
            {
                return outmsg += "Top left with " + m.ElementAt<int>(0) +" points";
            }
            else if (num == 1)
            {
                return outmsg += "Top center with " + m.ElementAt<int>(1) + " points";
            }
            else if (num == 2)
            {
                return outmsg += "Top right with " + m.ElementAt<int>(2) + " points";
            }
            else if (num == 3)
            {
                return outmsg += "Middle left with " + m.ElementAt<int>(3) + " points";
            }
            else if (num == 4)
            {
                return outmsg += "Middle center with " + m.ElementAt<int>(4) + " points";
            }
            else if (num == 5)
            {
                return outmsg += "Middle right with " + m.ElementAt<int>(5) + " points";
            }
            else if (num == 6)
            {
                return outmsg += "Bottom left with " + m.ElementAt<int>(6) + " points";
            }
            else if (num == 7)
            {
                return outmsg += "Bottom center with " + m.ElementAt<int>(7) + " points";
            }
            else if (num == 8)
            {
                return outmsg += "Bottom right with " + m.ElementAt<int>(8) + " points";
            }
            else
            {
                return "No focus point available";
            }
        }

        public void createPDF()
        {
            int lengthstr = ModelName.Length;
            int fontsize = 35;
            if (lengthstr >= 35)
            {
                fontsize = 30;
            }

            pdfDocument myDoc = new pdfDocument("Statistics Report", "Created with PDFsharp");
            pdfPage CoverPage = myDoc.addPage();
            int distance = (CoverPage.width / 2) - lengthstr * 8 + 20;
            CoverPage.addText("Statistical Report", 200, 650, predefinedFont.csTimesBold, 35);
            CoverPage.addText("for", 300, 600, predefinedFont.csTimesBold, 35);
            CoverPage.addText(ModelName, distance, 550, predefinedFont.csTimesBold, fontsize);
            CoverPage.addText("created :" + DateTime.UtcNow, 200, 500, predefinedFont.csTimes, 20);
            pdfPage FirstPage = myDoc.addPage();
            ArrayList content = new ArrayList();
            content.Add("Model name: " + ModelName);
            content.Add("Model type: " + type);
            content.Add("Model location: " + datasource.Replace(@"\","/"));
            content.Add("Model recorded  height: " + height);
            content.Add("Model recorded width:" + width);
            FirstPage.addText("Recorded media  Details", 150, 700, predefinedFont.csTimesBold, 30);

            int c = 630;
            int counter = 1;
            foreach (string s in content)
            {
                if (counter == 1)
                {
                    FirstPage.addText(s, 50, c, predefinedFont.csTimes, fontsize - 10);
                }
                else
                {
                    FirstPage.addText(s, 50, c, predefinedFont.csTimes, 25);
                }
                c = c - 30;
            }
            pdfPage SecondPage = myDoc.addPage();
            ArrayList tableContents = new ArrayList();


            //111111111111111111111111111111111111111111111111111111111111111111
            List<int> m = getpoints();

            tableContents.Add(m.ElementAt(0));
            tableContents.Add(m.ElementAt(1));
            tableContents.Add(m.ElementAt(2));
            
            tableContents.Add(m.ElementAt(3));
            tableContents.Add(m.ElementAt(4));
            tableContents.Add(m.ElementAt(5));

            tableContents.Add(m.ElementAt(6));
            tableContents.Add(m.ElementAt(7));
            tableContents.Add(m.ElementAt(8));
            
            pdfTable myTable = createTable(tableContents,2,3); // Its a 3x3 table, but the first row is the column headings

            SecondPage.addText("Recorded Data Summary", 150, 700, predefinedFont.csTimesBold, 30);
            SecondPage.addText("The following will show the data that is recorded.", 50, 600, predefinedFont.csTimes, 20);
            SecondPage.addText("Total time of recording: " + this.getTimeofRecording().ToString("#.##") + " seconds", 50, 516, predefinedFont.csTimes, 20);
            SecondPage.addText("Total points: " + this.px.Count, 50, 558, predefinedFont.csTimes, 20);
            SecondPage.addText("Total invalid points: " + this.excludedpoints, 50, 537, predefinedFont.csTimes, 20);
            SecondPage.addTable(myTable, 100, 470);
            SecondPage.addText("Point of focus: " + this.getFocusPoint(m), 50, 300, predefinedFont.csTimes, 20);

            
            string filename = "Statistical Report_" + ModelName + ".pdf";
            myDoc.createPDF(this.output + filename);

            CoverPage = null;
            FirstPage = null;
            myTable = null;
            SecondPage = null;
            myDoc = null; 

        }
        //**************************************************************************
        public void createPDF3d()
        {
            int lengthstr = ModelName.Length;
            int fontsize = 35;
            if(lengthstr >= 35)
            {
                fontsize = 30;
            }
           
            pdfDocument myDoc = new pdfDocument("Statistics Report", "Created with PDFsharp");
            pdfPage CoverPage = myDoc.addPage();
            int distance = (CoverPage.width / 2) - lengthstr*8+20;
            CoverPage.addText("Statistical Report", 200, 650, predefinedFont.csTimesBold, 35);
            CoverPage.addText("for", 300, 600, predefinedFont.csTimesBold, 35);
            CoverPage.addText(ModelName, distance, 550, predefinedFont.csTimesBold, fontsize);
            CoverPage.addText("created :" + DateTime.UtcNow, 200, 500, predefinedFont.csTimes, 20);
            pdfPage FirstPage = myDoc.addPage();
            ArrayList content = new ArrayList();
            content.Add("Model name: " + ModelName);
            content.Add("Model type: 3D");
            content.Add("Model location: " + datasource.Replace(@"\", "/"));
            content.Add("Model recorded  height: " + height);
            content.Add("Model recorded width:" + width);
            FirstPage.addText("Recorded media  Details", 150, 700, predefinedFont.csTimesBold, 30);

            int c = 630;
            int counter = 1;
            foreach (string s in content)
            {
                if (counter == 1)
                {
                    FirstPage.addText(s, 50, c, predefinedFont.csTimes, fontsize-10);
                }
                else
                {
                    FirstPage.addText(s, 50, c, predefinedFont.csTimes, 25);
                }
                c = c - 30;
            }
            string name = this.ModelName;
            List<string> views = new List<string>(9);
            views.Add("Front");
            views.Add("Front left");
            views.Add("Left-side");
            views.Add("Back left");
            views.Add("Back");
            views.Add("Back right");
            views.Add("Right-side");
            views.Add("Front right");
            views.Add("Top");
            for (int i = 0; i < 9; i++)
            {
                pdfPage SecondPage = myDoc.addPage();
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                ArrayList tableContents = new ArrayList();
                this.ModelName = "view" + i;
                List<int> m = getpoints();

                tableContents.Add(m.ElementAt(0));
                tableContents.Add(m.ElementAt(1));
                tableContents.Add(m.ElementAt(2));

                tableContents.Add(m.ElementAt(3));
                tableContents.Add(m.ElementAt(4));
                tableContents.Add(m.ElementAt(5));

                tableContents.Add(m.ElementAt(6));
                tableContents.Add(m.ElementAt(7));
                tableContents.Add(m.ElementAt(8));

                pdfTable myTable = createTable(tableContents, 2, 3); // Its a 3x3 table, but the first row is the column headings

                SecondPage.addText("Veiw type: "+ views[i], 150, 700, predefinedFont.csTimesBold, 30);
                SecondPage.addText("The following will show the data that is recorded.", 50, 600, predefinedFont.csTimes, 20);
                SecondPage.addText("Total time of recording: " + this.getTimeofRecording().ToString("#.##") + " seconds", 50, 516, predefinedFont.csTimes, 20);
                SecondPage.addText("Total points: " + this.px.Count, 50, 558, predefinedFont.csTimes, 20);
                SecondPage.addText("Total invalid points: " + this.excludedpoints, 50, 537, predefinedFont.csTimes, 20);
                SecondPage.addTable(myTable, 100, 470);
                SecondPage.addText("Point of focus: " + this.getFocusPoint(m), 50, 300, predefinedFont.csTimes, 20);
            }
            this.ModelName = name;
            ModelName = ModelName.Replace(" ", "_");
            //))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            string filename = "Statistical Report_" + ModelName + ".pdf";
            myDoc.createPDF(this.output + filename);

            CoverPage = null;
            FirstPage = null;
            myDoc = null;

        }

    }
}
