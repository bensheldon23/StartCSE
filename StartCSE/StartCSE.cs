using System;
using System.Collections;
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

using System.Xml;
using System.Xml.Linq;

using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace StartCSE
{
    public partial class StartCSE : Form
    {
        new ProgressForm progressform = new ProgressForm();
        GeneralFunctions GF = new GeneralFunctions();

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["StartCSE.Properties.Settings.CommSalesPricingPlatformConnectionString"].ConnectionString;
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
        
        public StartCSE()
        {
            InitializeComponent();

            if (Environment.UserName == "bsheldon" || Environment.UserName == "pmorini" || Environment.UserName == "jfiorelli" || Environment.UserName == "dsmith")
            {
                label11.Visible = true;
                comboBoxPDM.Visible = true;
                OfflinecheckBox.Enabled = true;
                OfflinecheckBox.Checked = false;
            }

            //Loading PMs from AD
            comboBoxPDM.DataSource = FindPDMs();

            //Loading Version Info
            List<string> version = new List<string>();
            version.AddRange(File.ReadAllLines(GlobalV.version_path_local));
            GlobalV.version_current = version[0];
            GlobalV.version_bos_current = version[1];
            label1.Text = GlobalV.version_current;
            label9.Text = "V" + GlobalV.version_bos_current;

            if (File.Exists(GlobalV.version_path))
            {
                versioncheck();
            }           
        }
        public class GlobalV
        {
            public static bool multi_site = false;
            public static string customers_dir = @"C:\COMMON\INSTALLS\CSE\CUSTOMERS\";
            public static string CSE_path = @"C:\COMMON\INSTALLS\CSE\";
            public static string notes_path;
            public static string acad_path;
            public static string job_path;
            public static string version_path = @"\\photon\groups\Sales\Commercial_Sales\SalesEngineering\StartCSE\release\version.txt";
            public static string version_path_local = @"c:\common\installs\cse\version.txt";
            public static string version_current;
            public static string version_bos_current;
            public static string update_path = @"\\photon\groups\Sales\Commercial_Sales\SalesEngineering\StartCSE\release\version.txt";
            public static string project_name;
            public static int project_ID; //for SQL

            //Resources in Data Folder
            public static string ASHRAE_path = @"C:\Common\Installs\CSE\Data\Ashrae.cse";
            public static string TMY3_path = @"C:\Common\Installs\CSE\Data\TMY3.cse";
            public static string SNOW_path = @"C:\Common\Installs\CSE\Data\SNOW.cse";
        }

        private List<string> FindPDMs()
        {
            List<string> PDMs = new List<string>();
            
            try  
            {
                //las5dmc00.solarcity.local
                PrincipalContext AD = new PrincipalContext(ContextType.Domain, "slc5dmc00.solarcity.local"); 
                UserPrincipal     u      = new UserPrincipal(AD);  
                PrincipalSearcher search = new PrincipalSearcher(u);
                u.Description = "*Project Development Manager*";

                foreach (UserPrincipal result in search.FindAll())
                {
                    PDMs.Add(result.DisplayName);
                }
                search.Dispose();                
            }  
  
            catch (Exception e)  
           {  
                MessageBox.Show("PDMs not loaded.  Try reconnecting to VPN."+System.Environment.NewLine+"Error: " + e.Message);  
           }
           return PDMs;

        }

        private void AddSitesButton_Click(object sender, EventArgs e)
        {
            if (IsServerConnected())
            {
                string project_id;
                string job_path, notes_path;

                //Add check to confirm all files are closed

                ASTdataGridView.AllowUserToAddRows = false;

                Sites[] Sites = new Sites[ASTdataGridView.RowCount];

                Sites = getGeoData(Sites, ASTdataGridView);

                job_path = GlobalV.customers_dir + ASTProjectsListBox.SelectedValue + @"\";
                notes_path = GlobalV.customers_dir + ASTProjectsListBox.SelectedValue + @"\" + ASTProjectsListBox.SelectedValue + "_notes.xlsm";

                project_id = getProjectID(ASTProjectsListBox.SelectedValue.ToString());

                foreach (Sites Site in Sites)
                {
                    Site.SiteID = CreateSiteRecord(Site.SiteName, Site.SiteAddress, Site.SiteState, project_id);
                    CreateBOSTool(Site.SiteName, Site.TempMax, Site.TempMin, Site.SiteName, Site.SiteState, Site.SiteID, job_path);
                }

                UpdateNotesTool(Sites, notes_path, job_path);
                AddSitesXML(Sites, ASTProjectsListBox.SelectedValue.ToString());

                MessageBox.Show("Sites Added");
                dataGridView1.AllowUserToAddRows = true;
            }
            else
            {
                MessageBox.Show("Cannot connect to CommSales database.  Try reconnecting to VPN.");
            }

        }
        public bool IsServerConnected()
        {
            using (var l_oConnection = new SqlConnection(connectionString))
            {
                try
                {
                    l_oConnection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        private string getProjectID(string project_name)
        {
            string project_id="";
            using (XmlReader xmlReader = XmlReader.Create(GlobalV.customers_dir + project_name +@"\project.xml"))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.IsStartElement())
                    {
                        switch (xmlReader.Name.ToString())
                        {
                            case "ProjectID":
                            project_id = xmlReader.ReadString();
                            break;
                        }
                    }
                }
            }
            return project_id;
        }

        private Sites[] getGeoData(Sites[] Sites,DataGridView dataGridView)
        {
            //For Ashrae
            List<string> Ashrae = new List<string>();
            Ashrae.AddRange(File.ReadAllLines(GlobalV.ASHRAE_path));

            //For TMY3
            List<string> TMY3 = new List<string>();
            TMY3.AddRange(File.ReadAllLines(GlobalV.TMY3_path));

            //For SNOW
            List<string> SNOW = new List<string>();
            SNOW.AddRange(File.ReadAllLines(GlobalV.SNOW_path));

            double min_dist, temp_dist;
            int count = 0, count_ASH = 0, count_TMY3 = 0;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                //Pause so as not to exceed PVWatts ratio
                if (count % 5 == 0)
                {
                    System.Threading.Thread.Sleep(1000);
                }

                //Creates Site in for Sites row
                Sites[count] = new Sites();

                //Sets Site Value
                Sites[count].SiteName = row.Cells[0].Value.ToString();

                //Fills Address and Latitude, Long
                if (row.Cells[1].Value != null)
                {
                    Sites[count].SiteAddress = row.Cells[1].Value.ToString();
                    try
                    {
                        Sites[count].SiteLat = GetLat(Sites[count].SiteAddress);
                        Sites[count].SiteLong = GetLong(Sites[count].SiteAddress);
                        Sites[count].SiteState = GetState(Sites[count].SiteLat, Sites[count].SiteLong);
                    }
                    catch
                    {
                        MessageBox.Show("GoogleServices could not find correct airport for " + Sites[count].SiteName.ToString());
                    }

                }

                //ASHRAE
                count_ASH = 0;
                min_dist = 10000;
                while (count_ASH < Ashrae.Count)
                {
                    string[] line_temp = Ashrae[count_ASH].Split(',');
                    temp_dist = Distance(Sites[count].SiteLat, Sites[count].SiteLong, Convert.ToDouble(line_temp[3]), Convert.ToDouble(line_temp[4]));

                    if (temp_dist < min_dist)
                    {
                        Sites[count].Altitude = Convert.ToInt32(line_temp[5]);
                        Sites[count].TempMax = Convert.ToDouble(line_temp[6]);
                        if (line_temp[7] == "N/A")
                        {
                            Sites[count].TempMin = 0;
                        }
                        else
                        {
                            Sites[count].TempMin = Convert.ToDouble(line_temp[7]);
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
                    temp_dist = Distance(Sites[count].SiteLat, Sites[count].SiteLong, Convert.ToDouble(line_temp_TMY3[3]), Convert.ToDouble(line_temp_TMY3[4]));
                    if (temp_dist < min_dist)
                    {
                        Sites[count].WeatherStation = line_temp_TMY3[1];
                        Sites[count].WeatherStationNum = line_temp_TMY3[0];
                        Sites[count].WeatherStationState = line_temp_TMY3[2];
                        Sites[count].TMY3URL = line_temp_TMY3[8];
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
                    temp_dist = Distance(Sites[count].SiteLat, Sites[count].SiteLong, Convert.ToDouble(line_temp_snow[1]), Convert.ToDouble(line_temp_snow[2]));
                    if (temp_dist < min_dist)
                    {
                        Sites[count].SnowID = line_temp_snow[0];
                        Sites[count].SnowStation = line_temp_snow[4] + ", " + line_temp_snow[3];
                        min_dist = temp_dist;
                    }
                    count_TMY3++;
                }

                //MessageBox.Show("Name: " + Sites1[count].SiteName + "\nAddress: " + Sites1[count].SiteAddress + "\nWeatherStation: " + Sites1[count].WeatherStation + "\nStation #: " + Sites1[count].WeatherStationNum + "\nAltitude: " + Sites1[count].Altitude + "m\nMax Temp: " + Sites1[count].TempMax + "degrees C\nMin Temp: " + Sites1[count].TempMin + "degrees C");
                count++;

            }  //End of sorting through rows
            return Sites;
        }

        private void UpdateNotesTool(Sites[] Sites,string notes_path, string job_path)
        {
            int row_index;
            int count;

            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(notes_path,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);
            Excel.Sheets excelSheets = excelWorkbook.Worksheets;
            string currentSheet = "Notes";
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(currentSheet);

            count = 0;
            excelApp.Cells[1, 1].Value2 = GlobalV.project_name;
            excelApp.Cells[1, 2].Value2 = GlobalV.version_bos_current;

            if (GlobalV.project_ID.ToString()!="")
            {
                excelApp.Cells[1, 3].Value2 = "ProjectID";
                excelApp.Cells[1, 4].Value2 = GlobalV.project_ID;
            }

            row_index = count+3;
            while (excelApp.Cells[row_index,1].Value2 != null)
            {
                row_index++;
            }

            while (count < Sites.Length)
            {
                excelApp.Cells[row_index, 1].Value2 = Sites[count].SiteName;
                excelApp.Cells[row_index, 2].Value2 = Sites[count].SiteAddress;
                excelApp.Cells[row_index, 31].Value2 = Sites[count].WeatherStation;
                excelApp.Cells[row_index, 68].Value2 = Sites[count].Altitude;
                excelApp.Cells[row_index, 69].Value2 = Sites[count].TempMax;
                excelApp.Cells[row_index, 70].Value2 = Sites[count].TempMin;
                excelApp.Cells[row_index, 32].Value2 = Sites[count].TMY3URL;
                excelApp.Cells[row_index, 39].Value2 = Sites[count].SnowID;
                excelApp.Cells[row_index, 40].Value2 = Sites[count].SnowStation;
                excelApp.Cells[row_index, 73].Value2 = Sites[count].SiteLat;
                excelApp.Cells[row_index, 74].Value2 = Sites[count].SiteLong;
                excelApp.Cells[row_index, 75].Value2 = Sites[count].SiteState;
                excelApp.Cells[row_index, 55].Value2 = "='" + job_path + @"costing\[" + Sites[count].SiteName + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm]Summary'!$C$14";
                excelApp.Cells[row_index, 56].Value2 = "='" + job_path + @"costing\[" + Sites[count].SiteName + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm]Summary'!$H$15";
                excelApp.Cells[row_index, 60].Value2 = "='" + job_path + @"costing\[" + Sites[count].SiteName + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm]Summary'!$H$16";
                excelApp.Cells[row_index, 62].Value2 = "='" + job_path + @"costing\[" + Sites[count].SiteName + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm]Summary'!$C$16";
                excelApp.Cells[row_index, 66].Value2 = "='" + job_path + @"costing\[" + Sites[count].SiteName + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm]Summary'!$R$5";
                excelApp.Cells[row_index, 3].Value2 = excelApp.Cells[3, 3].Value2;
                excelApp.Cells[row_index, 4].Value2 = excelApp.Cells[3, 4].Value2;
                excelApp.Cells[row_index, 12].Value2 = excelApp.Cells[3, 12].Value2;
                excelApp.Cells[row_index, 13].Value2 = excelApp.Cells[3, 13].Value2;
                count = count + 1;
                row_index++;
            }

            excelWorkbook.Close(true);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (IsServerConnected() || OfflinecheckBox.Checked==true)
            {

                dataGridView1.AllowUserToAddRows = false;

                Sites[] Sites1 = new Sites[dataGridView1.RowCount];

                Sites1 = getGeoData(Sites1, dataGridView1);

                int count = 0;

                //Creates Job Folder for One or Many Sites
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

                //SQL Project Record Creation
                if (!OfflinecheckBox.Checked)
                {
                    CreateProjectRecord(GlobalV.project_name);
                }

                count = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!OfflinecheckBox.Checked)
                    {
                        Sites1[count].SiteID = CreateSiteRecord(Sites1[count].SiteName, Sites1[count].SiteAddress, Sites1[count].SiteState, GlobalV.project_ID.ToString());
                    }

                    //SQL Site Record Creation
                    CreateBOSTool(Sites1[count].SiteName, Sites1[count].TempMax, Sites1[count].TempMin, Sites1[count].SiteName, Sites1[count].SiteState, Sites1[count].SiteID, GlobalV.job_path);

                    count++;
                }

                UpdateNotesTool(Sites1, GlobalV.notes_path, GlobalV.job_path);

                CreateProjectXML(Sites1, GlobalV.project_name);

                dataGridView1.AllowUserToAddRows = true;
                System.Diagnostics.Process.Start(GlobalV.job_path);
                MessageBox.Show("Job Created");
            }
            else
            {
                MessageBox.Show("Cannot connect to CommSales database.  Try reconnecting to VPN.");
            }
        }

        private void AddSitesXML(Sites[] Sites,string project_name)
        {
            foreach (Sites Site in Sites)
            {
            XDocument xDocument = XDocument.Load(GlobalV.customers_dir + project_name + @"\Layout\project.xml");
            XElement root = xDocument.Element("Project");
            root.Add(
                new XElement("Site",
                new XElement("SiteName",Site.SiteName),
                new XElement("SiteAddress",Site.SiteAddress),
                new XElement("SiteLat",Site.SiteLat),
                new XElement("SiteLong",Site.SiteLong),
                new XElement("SiteID",Site.SiteID)));
            xDocument.Save(GlobalV.customers_dir + project_name + @"\project.xml");
            }
        }


        private void CreateProjectXML(Sites[] Sites,string project_name)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(@"c:\common\installs\cse\customers\"+project_name+@"\Layout\project.xml"))
            {
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Project");
            xmlWriter.WriteAttributeString("Application", "SCExchange");
            xmlWriter.WriteAttributeString("Version", "1.0.0.5");
            xmlWriter.WriteAttributeString("Units", "IP");
            
            //Add SQL ProjectID to XML
            xmlWriter.WriteElementString("ProjectName", project_name);
            xmlWriter.WriteElementString("ProjectID", GlobalV.project_ID.ToString());

            foreach (Sites Site in Sites)
            {
                xmlWriter.WriteStartElement("Site");
                xmlWriter.WriteElementString("SiteName", Site.SiteName);
                xmlWriter.WriteElementString("SiteAddress", Site.SiteAddress);
                xmlWriter.WriteElementString("SiteLat", Site.SiteLat.ToString());
                xmlWriter.WriteElementString("SiteLong", Site.SiteLong.ToString());
                xmlWriter.WriteElementString("SiteID", Site.SiteID.ToString());
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement(); //End Project

            }
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

        public void CreateBOSTool(string sitename, double tmax, double tmin, string sname, string sstate,int siteid, string job_path)
        {
        File.Copy(GlobalV.CSE_path + @"\BOS_Costing_Tool.xlsm", job_path + @"Costing\" + sitename + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm");
        
        Excel.Application excelApp = new Excel.Application();
        excelApp.Visible = false;
        Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(job_path + @"Costing\" + sitename + @"_BOS_" + GlobalV.version_bos_current + "_R_0.1.xlsm",
                0, false, 5, "porterhouse", "", false, Excel.XlPlatform.xlWindows, "",
                true, false, 0, true, false, false);

        Excel.Sheets excelSheets = excelWorkbook.Worksheets;
        string currentSheet = "INPUTS";
        Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.Application.Worksheets[currentSheet];
        excelWorksheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
        excelWorksheet.Select(true);
        excelApp.Cells[13, 2].Value2 = tmin;
        excelApp.Cells[14, 2].Value2 = tmax;
        
        if (siteid.ToString() != "")
        {
        excelApp.Cells[1, 10].Value2 = siteid;  //SQL
        }

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
            GF.PasteSiteInfo(dataGridView1);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            GF.ClearSiteInfo(dataGridView1);
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
        private int CreateSiteRecord(string SiteName,string SiteAddress,string SiteState,string project_ID)
        {
            int SiteID;
            string insertSQL;
            insertSQL = "INSERT INTO Sites (SiteName,SiteAddress,State,ProjectID) VALUES ('" + SiteName + "','"+SiteAddress+"','"+SiteState+"','"+project_ID+"'); SELECT CAST(scope_identity() AS int)";
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

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            string[] projects = Directory.GetDirectories(GlobalV.customers_dir);
            for (int i = 0; i < projects.Count();i++ )
            {
                projects[i] = projects[i].Replace(GlobalV.customers_dir, string.Empty);
            }
            ASTProjectsListBox.DataSource = projects;
        }

        private void ASTPasteButton_Click(object sender, EventArgs e)
        {
            GF.PasteSiteInfo(ASTdataGridView);
        }

        private void ASTClearButton_Click(object sender, EventArgs e)
        {
            GF.ClearSiteInfo(ASTdataGridView);
        }


    }
}
