using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CSVCombiner
{

    public partial class Form1 : Form
    {
        double maxDeltaTime = 2;
        OpenFileDialog openFileDialog1;
        SaveFileDialog saveFileDialog1;
        Dictionary<String, DateTime> fileDictonary;
        List<KeyValuePair<List<KeyValuePair<String, DateTime>>,DateTime>> sortedList;
        
        public Form1()
        {
            fileDictonary = new Dictionary<String,DateTime>();
            sortedList = new List<KeyValuePair<List<KeyValuePair<String, DateTime>>, DateTime>>();
            InitializeComponent();
            InitializeOpenFileDialog();
        }

        private void InitializeOpenFileDialog()
        {
            openFileDialog1 = new OpenFileDialog();
            // Set the file dialog to filter for graphics files.
            openFileDialog1.Filter = "Files (*.csv;*.CSV;)|*.csv;*.CSV;";

            // Allow the user to select multiple images.
            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "My CSV file Browser";
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            string[] formats = { "yyyy-MM-dd_HHmmss" };
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                lblFilesSelected.Text = openFileDialog1.FileNames.Length.ToString() + " Files selected";
                // Read the files
                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        String[] values = System.IO.Path.ChangeExtension(file, null).Split('_');
                        String timepStamp = values[values.Length-2] + "_" + values[values.Length-1];
                        DateTime parsedTimeStamp;
                        DateTime.TryParseExact(timepStamp, formats, null, 
                                    DateTimeStyles.AllowWhiteSpaces |
                                    DateTimeStyles.AdjustToUniversal,
                                    out parsedTimeStamp);

                        fileDictonary.Add(file, parsedTimeStamp);
                    }
                    catch (SecurityException ex)
                    {

                    }
                    catch (Exception ex)
                    {

                    }
                }
                List<KeyValuePair<String, DateTime>> myList = fileDictonary.ToList();
                myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

                DateTime startOflogging = new DateTime(0);
                int tempCount = 0;
                foreach (KeyValuePair<String, DateTime> pair in myList)
                {
                    if (/*pair.Value.Subtract(startOflogging).Seconds >= maxDeltaTime*/tempCount == 0 || tempCount%2==0)
                    {
                        startOflogging = pair.Value;
                        sortedList.Add(new KeyValuePair<List<KeyValuePair<String,DateTime>>,DateTime>(new List<KeyValuePair<String,DateTime>>(),startOflogging));
                    }
                    sortedList.Last().Key.Add(pair);
                    tempCount++;
                }
                lblTimeStamps.Text = sortedList.Count.ToString() + " Timestamps detected";
            }
        }

        private void convertListToCsv(List<KeyValuePair<List<KeyValuePair<String, DateTime>>,DateTime>> input){
            double timeOffset = 0;
            double lastTime = 0;
            double writeTime = 0;
            //create headers
            HashSet<String> headers = new HashSet<string>();
            foreach (KeyValuePair<List<KeyValuePair<String, DateTime>>, DateTime> subList in input)
            {
                foreach (KeyValuePair<String, DateTime> timedFiles in subList.Key)
                {
                    // Get prefix 
                    String prefix = timedFiles.Key.Split('\\').Last().Split('_').First();
                    foreach (String item in File.ReadLines(timedFiles.Key).First().ToString().Split(','))
                    {
                        if (!prefix.Contains("ApplLog"))
                        {
                            if(item.Contains("Time")){
                                headers.Add(item.Replace("\\","").Replace("\"",""));
                            }else{
                                headers.Add((prefix + "_" + item).Replace("\\","").Replace("\"",""));
                            }
                        }         
                    }
                }
            }

            //Create dictonary with headers
            Dictionary<String, String> values = new Dictionary<String, String>();
            foreach (String header in headers)
            {
                values.Add(header,"0");
            }

            // Create csv file
            Stream myStream;
            saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "CSV files (*.CSV)|*.CSV";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    using (System.IO.StreamWriter file =   new System.IO.StreamWriter(myStream))
                    {
                        // Write headers
                        String headerString = "";
                        foreach (KeyValuePair<String, String> data in values)
                        {
                            headerString += data.Key + ",";
                        }
                        file.WriteLine(headerString.Remove(headerString.Length - 1));

                        List<String> tempHeaders = new List<string>();
                        int currentLine = 1;
                        Boolean readNextLine = true;
                        foreach (KeyValuePair<List<KeyValuePair<String, DateTime>>, DateTime> subList in input)
                        {
                            // clear values
                            for (int k = 0; k < values.Count; k++)
                                values[values.ElementAt(k).Key] = "0";

                            // Create data string
                            String dataLine = "";
                            foreach (KeyValuePair<String, String> data in values)
                            {
                                if (data.Key == values.First().Key)
                                {
                                    dataLine = (writeTime+timeOffset).ToString() + ",";
                                }
                                else
                                {
                                    dataLine += "0,";
                                }

                            }
                            file.WriteLine(dataLine.Remove(dataLine.Length - 1));

                            readNextLine = true;
                            while (readNextLine)
                            {
                                // clear values
                                for (int k = 0; k < values.Count; k++)
                                    values[values.ElementAt(k).Key] = "0";

                                foreach (KeyValuePair<String, DateTime> timedFiles in subList.Key)
                                {
                                    String prefix = timedFiles.Key.Split('\\').Last().Split('_').First();
                                    if (prefix.Contains("ApplLog"))
                                        continue;
                                    // Create headers
                                    tempHeaders.Clear();
                                    if (true)
                                    {
                                        foreach (String item in File.ReadLines(timedFiles.Key).First().ToString().Split(','))
                                        {
                                            if (item.Contains("Time"))
                                            {
                                                tempHeaders.Add(item.Replace("\\", "").Replace("\"", ""));
                                            }
                                            else
                                            {
                                                tempHeaders.Add((prefix + "_" + item).Replace("\\", "").Replace("\"", ""));
                                            }
                                        }
                                    }
                                    String actualLine;
                                    try{
                                        actualLine = File.ReadLines(timedFiles.Key).ElementAt(currentLine);
                                        String[] actualValues = actualLine.Split(',');
                                        for (int i = 0; i < actualValues.Length-1; i++)
                                        {
                                            values[tempHeaders[i]] = actualValues[i];
                                        }
                                       
                                    }catch(Exception e){
                                        timeOffset = 3;
                                        readNextLine = false;
                                        currentLine = 1;
                                        Console.WriteLine(e.Message);
                                    }                        
                                }
                                // Create data string
                                dataLine = "";
                                if (readNextLine)
                                {
                                    foreach (KeyValuePair<String, String> data in values)
                                    {
                                        if (data.Key == values.First().Key)
                                        {
                                            writeTime = double.Parse(data.Value) + lastTime + timeOffset;
                                            dataLine = writeTime.ToString() + ",";
                                        }
                                        else
                                        {
                                            dataLine += data.Value + ",";
                                        }

                                    }
                                    file.WriteLine(dataLine.Remove(dataLine.Length - 1));
                                }
                                else
                                {
                                    foreach (KeyValuePair<String, String> data in values)
                                    {
                                        if (data.Key == values.First().Key)
                                        {
                                            dataLine = writeTime.ToString() + ",";
                                        }
                                        else
                                        {
                                            dataLine += "0,";
                                        }

                                    }
                                    file.WriteLine(dataLine.Remove(dataLine.Length - 1));
                                }
                                currentLine++;
                            }
                            
                            lastTime = writeTime;
                        }
                    }  
                    myStream.Close();
                }
            }

        }

        private void btnCombine_Click(object sender, EventArgs e)
        {
            convertListToCsv(sortedList);
        }

    }
}
