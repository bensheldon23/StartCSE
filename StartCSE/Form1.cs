using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using GoogleMaps.LocationServices;
using Microsoft.Vbe.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;


namespace StartCSE
{
    public partial class Form1 : Form
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["StartCSE.Properties.Settings.CommSalesPricingPlatformConnectionString"].ConnectionString;
        ProgressForm progress;

        public class Sites
        {
            private int Site_ID;
            private string site_name;
            private string site_address;
            private double site_lat;
            private double site_long;
            private string site_WeatherStation;
            private string site_WeatherStationNum;
            private string site_WeatherStationState;
            private string site_state;
            private int site_altitude;
            private double temp_max;
            private double temp_min;
            private string site_TMY3URL;
            private string site_snow_id;
            private string site_snow_station;

            public int SiteID
            {
                get { return Site_ID; }
                set { Site_ID=value; }
            }
            public string SiteName
            {
                get { return site_name; }
                set { site_name = value; }
            }

            public string SiteAddress
            {
                get { return site_address; }
                set { site_address = value; }
            }

            public double SiteLat
            {
                get { return site_lat; }
                set { site_lat = value; }
            }

            public double SiteLong
            {
                get { return site_long; }
                set { site_long = value; }
            }
            public string SiteState
            {
                get { return site_state; }
                set { site_state = value; }
            }
            public string WeatherStation
            {
                get { return site_WeatherStation; }
                set { site_WeatherStation = value; }
            }
            public string WeatherStationNum
            {
                get { return site_WeatherStationNum; }
                set { site_WeatherStationNum = value; }
            }
            public double TempMax
            {
                get { return temp_max; }
                set { temp_max = value; }
            }
            public double TempMin
            {
                get { return temp_min; }
                set { temp_min = value; }
            }
            public int Altitude
            {
                get { return site_altitude; }
                set { site_altitude = value; }
            }
            public string WeatherStationState
            {
                get { return site_WeatherStationState; }
                set { site_WeatherStationState = value; }
            }
            public string TMY3URL
            {
                get { return site_TMY3URL; }
                set { site_TMY3URL = value; }
            }
            public string SnowID
            {
                get { return site_snow_id; }
                set { site_snow_id = value; }
            }

            public string SnowStation
            {
                get { return site_snow_station; }
                set { site_snow_station = value; }
            }
        }
        
        public Form1()
        {
            InitializeComponent();

            GlobalV.multi_site = false;
            GlobalV.customers_dir = @"C:\COMMON\INSTALLS\CSE\CUSTOMERS\";
            GlobalV.CSE_path = @"C:\COMMON\INSTALLS\CSE\";
            GlobalV.version_path_local = @"c:\common\installs\cse\version.txt";
            
            List<string> version = new List<string>();
            version.AddRange(File.ReadAllLines(GlobalV.version_path_local));
            GlobalV.version_current = version[0];
            GlobalV.version_bos_current = version[1];
            label1.Text = GlobalV.version_current;
            label9.Text = "V" + GlobalV.version_bos_current;

            GlobalV.version_path = @"\\photon\groups\Sales\Commercial_Sales\SalesEngineering\StartCSE\release\version.txt";
            GlobalV.update_path = @"\\photon\groups\Sales\Commercial_Sales\SalesEngineering\StartCSE\UpdateCSECode.bat";
            if (File.Exists(GlobalV.version_path))
            {
                versioncheck();
            }
            
        }

        public class GlobalV
        {
            public static bool multi_site;
            public static string customers_dir;
            public static string CSE_path;
            public static string notes_path;
            public static string acad_path;
            public static string job_path;
            public static string version_path;
            public static string version_path_local;
            public static string version_current;
            public static string version_bos_current;
            public static string update_path;
            public static string project_name;
            public static int project_ID; //for SQL
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressLabel.Visible = true;
            progressLabel.Text = @"Initializing...";
            progressLabel.Refresh();
            dataGridView1.AllowUserToAddRows = false;
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            progressBar1.Maximum = dataGridView1.RowCount+1;
            

            int count = 0, count_ASH=0, count_TMY3=0;
            Sites[] Sites1 = new Sites[dataGridView1.RowCount];

            //progress = new ProgressForm();
            //progress.Show();
            //progress.ProgressMax = dataGridView1.RowCount;

            //For Ashrae
            List<string> Ashrae = new List<string>();
            Ashrae.AddRange(File.ReadAllLines(@"C:\Common\Installs\CSE\Data\Ashrae.cse"));

            //For TMY3
            List<string> TMY3 = new List<string>();
            TMY3.AddRange(File.ReadAllLines(@"C:\Common\Installs\CSE\Data\TMY3.cse"));

            //For SNOW
            List<string> SNOW = new List<string>();
            SNOW.AddRange(File.ReadAllLines(@"C:\Common\Installs\CSE\Data\SNOW.cse"));

            double min_dist, temp_dist;
         

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (count % 5 == 0)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                Sites1[count] = new Sites();
                Sites1[count].SiteName = row.Cells[0].Value.ToString();

                progressBar1.Value = progressBar1.Value + 1;
                progressLabel.Text = "Retrieving data for " + Sites1[count].SiteName + "...";
                ProgressgroupBox1.Visible = true;
                ProgressgroupBox1.Refresh();
                progressLabel.Refresh();

                if (row.Cells[1].Value != null)
                {
                    Sites1[count].SiteAddress = row.Cells[1].Value.ToString();              
                    try
                    {
                        Sites1[count].SiteLat = GetLat(Sites1[count].SiteAddress);
                        Sites1[count].SiteLong = GetLong(Sites1[count].SiteAddress);
                        Sites1[count].SiteState = GetState(Sites1[count].SiteLat, Sites1[count].SiteLong);
                    }
                    catch
                    {
                        MessageBox.Show("GoogleServices could not find correct airport for " + Sites1[count].SiteName.ToString());
                    }
                    
                }

                    //ASHRAE
                    count_ASH = 0;
                    min_dist = 10000;
                    while (count_ASH < Ashrae.Count)
                    {
                        string[] line_temp = Ashrae[count_ASH].Split(',');
                        temp_dist = Distance(Sites1[count].SiteLat, Sites1[count].SiteLong, Convert.ToDouble(line_temp[3]), Convert.ToDouble(line_temp[4]));

                        if (temp_dist < min_dist)
                        {
                            //Sites1[count].WeatherStation = line_temp[0] + ", " + line_temp[1];
                            //Sites1[count].WeatherStationNum = line_temp[2];
                            Sites1[count].Altitude = Convert.ToInt32(line_temp[5]);
                            Sites1[count].TempMax = Convert.ToDouble(line_temp[6]);
                            if (line_temp[7] == "N/A")
                            {
                                Sites1[count].TempMin = 0;
                            }
                            else
                            {
                                Sites1[count].TempMin = Convert.ToDouble(line_temp[7]);
                            }
                            min_dist = temp_dist;
                        }
                        count_ASH++;
                    }

                    //TMY3
                    count_TMY3 = 0;
                    min_dist = 10000;
                    while (count_TMY3 < TMY3.Count)
                    {
                        string[] line_temp_TMY3 = TMY3[count_TMY3].Split(',');
                        temp_dist = Distance(Sites1[count].SiteLat, Sites1[count].SiteLong, Convert.ToDouble(line_temp_TMY3[3]), Convert.ToDouble(line_temp_TMY3[4]));
                        if (temp_dist < min_dist)
                        {
                            Sites1[count].WeatherStation = line_temp_TMY3[1];
                            Sites1[count].WeatherStationNum = line_temp_TMY3[0];
                            Sites1[count].WeatherStationState = line_temp_TMY3[2];
                            Sites1[count].TMY3URL = line_temp_TMY3[8];
                            min_dist = temp_dist;
                        }
                        count_TMY3++;
                    }

                    //SNOW
                    count_TMY3 = 0;
                    min_dist = 10000;
                    while (count_TMY3 < SNOW.Count)
                    {
                        string[] line_temp_snow = SNOW[count_TMY3].Split(',');
                        temp_dist = Distance(Sites1[count].SiteLat, Sites1[count].SiteLong, Convert.ToDouble(line_temp_snow[1]), Convert.ToDouble(line_temp_snow[2]));
                        if (temp_dist < min_dist)
                        {
                            Sites1[count].SnowID = line_temp_snow[0];
                            Sites1[count].SnowStation = line_temp_snow[4] + ", " + line_temp_snow[3];
                            min_dist = temp_dist;
                        }
                        count_TMY3++;
                    }
                
                    //MessageBox.Show("Name: " + Sites1[count].SiteName + "\nAddress: " + Sites1[count].SiteAddress + "\nWeatherStation: " + Sites1[count].WeatherStation + "\nStation #: " + Sites1[count].WeatherStationNum + "\nAltitude: " + Sites1[count].Altitude + "m\nMax Temp: " + Sites1[count].TempMax + "degrees C\nMin Temp: " + Sites1[count].TempMin + "degrees C");
                    count++;
                
            }
            progressBar1.Value = progressBar1.Value + 1;
            progressLabel.Text = "Creating Folder Structure and populating excel spreadsheets...";
            if (MStextBox1.Text.Length == 0)
            {
                CreateJobDirectory(Sites1[0].SiteName);
                GlobalV.job_path = GlobalV.customers_dir + Sites1[0].SiteName + @"\";
                GlobalV.project_name = Sites1[0].SiteName;
            }
            else
            {
                CreateJobDirectory(MStextBox1.Text);
                GlobalV.job_path = GlobalV.customers_dir + MStextBox1.Text + @"\";
                GlobalV.project_name = MStextBox1.Text;
            }

            //SQL Record Creation
            CreateProjectRecord(GlobalV.project_name);

            count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Sites1[count].SiteID = CreateSiteRecord(Sites1[count].SiteName, Sites1[count].SiteAddress, Sites1[count].SiteState);
                CreateBOSTool(Sites1[count].SiteName, Sites1[count].TempMax, Sites1[count].TempMin, Sites1[count].SiteName, Sites1[count].SiteState,Sites1[count].SiteID);
                count++;    
            }
            
            
            int row_index;

            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(GlobalV.notes_path,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);
            Excel.Sheets excelSheets = excelWorkbook.Worksheets;
            string currentSheet = "Notes";
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(currentSheet);

            count = 0;
            excelApp.Cells[1, 1].Value2 = GlobalV.project_name;
            excelApp.Cells[1,2].Value2 = GlobalV.version_bos_current;

            while (count < Sites1.Length)
            {
                row_index = count + 3;
                excelApp.Cells[row_index,1].Value2 = Sites1[count].SiteName;
                excelApp.Cells[row_index, 2].Value2 = Sites1[count].SiteAddress;
                excelApp.Cells[row_index, 31].Value2 = Sites1[count].WeatherStation;
                excelApp.Cells[row_index, 68].Value2 = Sites1[count].Altitude;
                excelApp.Cells[row_index, 69].Value2 = Sites1[count].TempMax;
                excelApp.Cells[row_index, 70].Value2 = Sites1[count].TempMin;
                excelApp.Cells[row_index, 32].Value2 = Sites1[count].TMY3URL;
                excelApp.Cells[row_index, 39].Value2 = Sites1[count].SnowID;
                excelApp.Cells[row_index, 40].Value2 = Sites1[count].SnowStation;
                excelApp.Cells[row_index, 73].Value2 = Sites1[count].SiteLat;
                excelApp.Cells[row_index, 74].Value2 = Sites1[count].SiteLong;
                excelApp.Cells[row_index, 75].Value2 = Sites1[count].SiteState;
                excelApp.Cells[row_index, 55].Value2 = "='"+GlobalV.job_path+@"costing\["+Sites1[count].SiteName+@"_BOS_"+GlobalV.version_bos_current+ "_R_0.1.xlsm]Summary'!$C$14";
                excelApp.Cells[row_index, 56].Value2 = "='" + GlobalV.job_path + @"costing\[" + Sites1[count].SiteName + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm]Summary'!$H$15";
                excelApp.Cells[row_index, 60].Value2 = "='" + GlobalV.job_path + @"costing\[" + Sites1[count].SiteName + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm]Summary'!$H$16";
                excelApp.Cells[row_index, 62].Value2 = "='" + GlobalV.job_path + @"costing\[" + Sites1[count].SiteName + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm]Summary'!$C$16";
                excelApp.Cells[row_index, 66].Value2 = "='" + GlobalV.job_path + @"costing\[" + Sites1[count].SiteName + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm]Summary'!$R$5";
                excelApp.Cells[row_index, 3].Value2 = excelApp.Cells[3, 3].Value2;
                excelApp.Cells[row_index, 4].Value2 = excelApp.Cells[3, 4].Value2;
                excelApp.Cells[row_index, 12].Value2 = excelApp.Cells[3, 12].Value2;
                excelApp.Cells[row_index, 13].Value2 = excelApp.Cells[3, 13].Value2;
                count = count + 1;
            }
                   
            excelWorkbook.Close(true);
            dataGridView1.AllowUserToAddRows = true;
            System.Diagnostics.Process.Start(GlobalV.job_path);
            progressBar1.Visible = false;
            progressLabel.Visible = false;
            ProgressgroupBox1.Visible = false;
            
            MessageBox.Show("Job Created");
        }

        public double GetLat(string address)
        {
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);
            var latitude = point.Latitude;
            return latitude;
        }

        public double GetLong(string address)
        {
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);
            var longitude = point.Longitude;
            return longitude;
        }

        public string GetState(double lat, double lon)
        {
            var locationService = new GoogleLocationService();
            var result = locationService.GetRegionFromLatLong(lat,lon);
            string state = result.ShortCode;
            return state;
        }

        public double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double dist;
            dist = Math.Sqrt(Math.Pow(lat2 - lat1, 2) + Math.Pow(lon2-lon1,2));
            return dist;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void MScheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MScheckBox.Checked == true)
            {
                MSlabel1.Visible = true;
                MStextBox1.Visible = true;
                GlobalV.multi_site = true;
                dataGridView1.AllowUserToAddRows = true;
                
            }
            if (MScheckBox.Checked == false)
            {
                MSlabel1.Visible = false;
                MStextBox1.Visible = false;
                GlobalV.multi_site = false;
                if (dataGridView1.Rows.Count > 1)
                {
                dataGridView1.AllowUserToAddRows = false;
                }
            }
        }

        public void CreateJobDirectory(string jobname)
        {
            if(!Directory.Exists(GlobalV.customers_dir + jobname))
            {
            Directory.CreateDirectory(GlobalV.customers_dir + jobname);
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Costing");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Costing\Old");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Production_Estimates");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Production_Estimates\Old");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Component_Cut_Sheets");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Component_Warranties");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Project_Schedule");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Layout");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Layout\Old");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Layout\Captures");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Single_Lines");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\SOW");
            Directory.CreateDirectory(GlobalV.customers_dir + jobname + @"\Site_Photos_and_Information");


            GlobalV.notes_path = GlobalV.customers_dir + jobname + @"\" + jobname + "_notes.xlsm";
            GlobalV.acad_path = GlobalV.customers_dir + jobname + @"\Layout\" + jobname + "_V0.1.dwg";
            File.Copy(GlobalV.CSE_path + @"\Notes_Template.xlsm", GlobalV.notes_path);
            File.Copy(GlobalV.CSE_path + @"\CSEdrawing_V0.1.dwg", GlobalV.acad_path);
            }
            else
            {
                MessageBox.Show("Folder Already Exists!");
            }
        }

        public void CreateBOSTool(string sitename, double tmax, double tmin, string sname, string sstate,int siteid)
        {
        File.Copy(GlobalV.CSE_path + @"\BOS_Costing_Tool.xlsm", GlobalV.job_path + @"Costing\" + sitename + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm");
        
        Excel.Application excelApp = new Excel.Application();
        excelApp.Visible = false;
        Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(GlobalV.job_path + @"Costing\" + sitename + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm",
                0, false, 5, "porterhouse", "", false, Excel.XlPlatform.xlWindows, "",
                true, false, 0, true, false, false);

        Excel.Sheets excelSheets = excelWorkbook.Worksheets;
        string currentSheet = "INPUTS";
        Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.Application.Worksheets[currentSheet];
        excelWorksheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
        excelWorksheet.Select(true);
        excelApp.Cells[13, 2].Value2 = tmin;
        excelApp.Cells[14, 2].Value2 = tmax;
        //excelApp.Cells[1, 10].Value2 = siteid;  SQL
        excelApp.Cells[9, 2].Value2 = sname;
        excelApp.Cells[26, 2].Value2 = sstate;
        excelWorksheet.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
        excelWorkbook.Close(true);
        }
        public void versioncheck()
        {
            
            List<string> version = new List<string>();
            version.AddRange(File.ReadAllLines(GlobalV.version_path));
            label2.Text = version[0].ToString();
            label7.Text = version[1].ToString();
            if (label1.Text != version[0].ToString())
            {
                label1.ForeColor = System.Drawing.Color.Red;
                label1.Text = GlobalV.version_current + "  UPDATE CODE!";
            }
            else
            {
                label1.ForeColor = System.Drawing.Color.Green;
            }
            if (label9.Text.Substring(1, label9.Text.Length - 1) != version[1].ToString())
            {
                label9.ForeColor = System.Drawing.Color.Red;
                label9.Text = "V"+GlobalV.version_bos_current + "  UPDATE CODE!";
            }
            else
            {
                label9.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PasteSiteInfo(DataGridView dgv)
        {
            string[] clipboardRows;
            string[] clipboardValues;
            DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();

            clipboardRows = Clipboard.GetText().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            string[,] clipboardCells = new string[clipboardRows.Length, 2];
            for (int i = 0; i < clipboardRows.Length - 1; i++)
            {
                clipboardValues = clipboardRows[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                if (clipboardValues.Length != 2)
                {
                    MessageBox.Show("Error - There must be 2 columns in each Clipboard Row");
                    return;
                }
                clipboardCells[i, 0] = clipboardValues[0];
                clipboardCells[i, 1] = clipboardValues[1];
            }
            for (int i = 0; i < clipboardRows.Length - 1; i++)
            {

                row.Cells[0].Value = clipboardCells[i, 0];
                row.Cells[1].Value = clipboardCells[i, 1];
                dgv.Rows.Add(row.Cells[0].Value, row.Cells[1].Value);
            } 
        }

        private void ClearSiteInfo(DataGridView dgv)
        {
            do
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    try
                    {
                        dgv.Rows.Remove(row);
                    }
                    catch (Exception) { }
                }
            } while (dgv.Rows.Count > 1);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo stratInfo = new System.Diagnostics.ProcessStartInfo();
            
            stratInfo.FileName = GlobalV.update_path;
            process.StartInfo = stratInfo;
            process.Start();
            System.Windows.Forms.Application.Exit();
            if (File.Exists(GlobalV.version_path))
            {
                versioncheck();
            }
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            PasteSiteInfo(dataGridView1);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearSiteInfo(dataGridView1);
        }
        
        private void CreateProjectRecord(string ProjectName)
        {
            string insertSQL;
            insertSQL = "INSERT INTO Projects (ProjectName,PDM) VALUES ('" + ProjectName + "','"+comboBoxPDM.SelectedItem.ToString()+"'); SELECT CAST(scope_identity() AS int)";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            try
            {
                con.Open();
                GlobalV.project_ID = (int)cmd.ExecuteScalar();
                cmd.Dispose();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private int CreateSiteRecord(string SiteName,string SiteAddress,string SiteState)
        {
            int SiteID;
            string insertSQL;
            insertSQL = "INSERT INTO Sites (SiteName,SiteAddress,State,ProjectID) VALUES ('" + SiteName + "','"+SiteAddress+"','"+SiteState+"','"+GlobalV.project_ID+"'); SELECT CAST(scope_identity() AS int)";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            
            try
            {
                con.Open();
                SiteID = (int)cmd.ExecuteScalar();
                cmd.Dispose();
            }
            catch (Exception err)
            {
                SiteID = -1;
                MessageBox.Show(err.Message);
            }
            finally
            {
                con.Close();
            }
            return SiteID;
        }
    }
}
