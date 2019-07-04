using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
// needed for http requests
using System.Net;
using System.IO;
using Newtonsoft.Json;
// needed for word mail merge
using CsvHelper;
using word = Microsoft.Office.Interop.Word; 


namespace Label_Printer
{


    public partial class Form1 : Form
    {
        // List that stores input PMG ID numbers
        List<string> idNums = new List<string> { };
        List<assetItem> csv_list = new List<assetItem>();

        List<string> records = new List<string>();

        // Keep track of how many labels are available
        int label_count = 0;


        public string getUserEntry()
        {
            // method to grab input ID from textBox1 
            string userInput = textBox1.Text;
            textBox1.Clear();
            return userInput;
        }
        
        public Form1()
        {
            // Forms Constructor Method

            InitializeComponent();

            // Binding event handler
            // TODO: Understand what the purpose of this is
            textBox1.KeyUp += TextBoxKeyUp;
            outputBox.TextChanged += outputBox_TextChanged;
            labelsAvailable.TextChanged += labelsAvailable_TextChanged;

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {
           
        }


        // Func to generate word Doc with labels
        private void generate_doc_Click(object sender, EventArgs e)
        {

            string path_temp = Path.GetTempPath();
            Console.WriteLine(path_temp);

            using (TextWriter writer = new StreamWriter(path_temp + @"\output.csv"))
            {
            var csv = new CsvWriter(writer);
                                
                csv.WriteField("ID");
                csv.WriteField("Asset Tag");
                csv.WriteField("Serial");
                csv.NextRecord();
                                
                for (int i = 0; i < records.Count; i++)
                {
                    Console.Write(records[i]);
                    csv.WriteField(records[i]);                                       

                    if (i != 0 && (i + 1) % 3 == 0)
                    {
                        csv.NextRecord();                        
                    }                       
                }                           
               
                csv.NextRecord();                
            }

                       
            
            word.Application oWord = new word.Application();
            word.Document oWrdDoc = new word.Document();
            Object oTemplatePath = @"\template.docx";
            oWrdDoc = oWord.Documents.Open(Directory.GetCurrentDirectory() + @"\template.docx");
            oWrdDoc.MailMerge.OpenDataSource(path_temp + @"\output.csv");
            oWrdDoc.MailMerge.Execute();
            oWrdDoc.SaveAs2(path_temp + @"\final.docx");
            oWrdDoc.Close();
            oWord.Quit();



        }


        private void outputBox_TextChanged(object sender, EventArgs e)
        {
            // Force log/Output box to autoscroll
            outputBox.SelectionStart = outputBox.Text.Length;
            outputBox.ScrollToCaret();
        }

        private void TextBoxKeyUp(object sender, KeyEventArgs e)
        {
            // Connects the pressing of Enter to click the search button
            if (e.KeyCode == Keys.Enter)
            {
                enterButton.PerformClick();
            }
        }

        public void enterButton_Click(object sender, EventArgs e)
        {
            // TODO Move all the logic to it's own method/func 

            // Load input into var and create regex pattern for validation
            string id2check = getUserEntry();
            var pmgPattern = new Regex(@"^((?i)PMG)\d\d\d\d\d(?-i)");


            // Add to list if valid ID, update labels available display and update log
            if (pmgPattern.IsMatch(id2check) && label_count < 47)
            {
                labelsAvailable.Text = String.Empty;
                idNums.Add(id2check);
                outputBox.SelectionColor = Color.SeaGreen;
                outputBox.AppendText("- PCID entered: " + id2check);

                string ID = "http://assets.pmgroup.com.au/hardware/" + ApiCall_id(id2check) + "/view";
                string Serial = ApiCall_serial(id2check);
                string Asset_Tag = id2check;


                outputBox.AppendText(Environment.NewLine);
                outputBox.AppendText("ID: " + ID);
                outputBox.AppendText(Environment.NewLine);
                outputBox.AppendText("Serial: " + Serial);
                outputBox.AppendText(Environment.NewLine);

                // Add info to dictionary or list here. This will then be used to load mail merge when 'generate doc.' function is called
                // MOVED THIS TO TOP OF CODE -- DELETE? 
                // Dictionary<string, string> linkList = new Dictionary<string, string>();
                //Dictionary<string, string> serialList = new Dictionary<string, string>();

                records.Add(ID);
                records.Add(Asset_Tag);
                records.Add(Serial);
                               
               
                //This is to test info is getting stored into dict correctly 
                // linkList.ToList().ForEach(x => Console.WriteLine(x.Value));
                // serialList.ToList().ForEach(x => Console.WriteLine(x.Value));

                label_count++;
                labelsAvailable.AppendText(label_count.ToString());

                
            }

            else
            {
                outputBox.SelectionColor = Color.Red;
                outputBox.AppendText("- PCID rejected: " + id2check + " is not valid");
                outputBox.AppendText(Environment.NewLine);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void labelsAvailable_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void headerLabel_Click(object sender, EventArgs e)
        {

        }

        private void loadCSV_Click(object sender, EventArgs e)
        {
            LoadNewFile();
            load_in_csv();


        }

        private void load_in_csv()
        {
            


        }

        private void LoadNewFile()
        {
            OpenFileDialog opDialog = new OpenFileDialog();
            opDialog.Title = "Select CSV for import";
            // TODO change to Downloads for users 
            opDialog.InitialDirectory = @"c:\";
            // TODO only allow csv here -- "All files (*.*)|*.*|All files (*.*)|*.*";
            opDialog.Filter = "CSV Files (*.csv)|*.csv";
            
            opDialog.FilterIndex = 2;
            opDialog.RestoreDirectory = true;

            if (opDialog.ShowDialog() == DialogResult.OK)
            {

                // test if csv loaded -- DEBUG LINE-
                outputBox.SelectionColor = Color.SeaGreen;
                outputBox.AppendText("- File loaded: " + opDialog.FileName);
                
            }

        }

        public string ApiCall_id(string asset_tag)
        {
            // 
            // This func takes the input tag and makes a GET request to the Snipe IT REST API for the ID. It also converts the JSON resonponse so it's consumable by us later
            //

            var request = (HttpWebRequest)WebRequest.Create("https://assets.pmgroup.com.au/api/v1/hardware/bytag/" + asset_tag);

            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";

            // Input Snipe IT API key here
            request.Headers["Authorization"] = "SNIPE-API-KEY-GOES-HERE";

            var response = (HttpWebResponse)request.GetResponse();
            string content = string.Empty;
            
            using (var stream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                    dynamic json_info = JsonConvert.DeserializeObject<dynamic>(content);

                    return json_info.id;
                                       
                }
            }
        }

        public string ApiCall_serial(string asset_tag)
        {
            // 
            // This func takes the input tag and makes a GET request to the Snipe IT REST API for the ID. It also converts the JSON resonponse so it's consumable by us later
            //

            var request = (HttpWebRequest)WebRequest.Create("https://assets.pmgroup.com.au/api/v1/hardware/bytag/" + asset_tag);

            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";

            // Input Snipe IT API key here
            request.Headers["Authorization"] = "SNIPE-API-KEY-GOES-HERE";

            var response = (HttpWebResponse)request.GetResponse();
            string content = string.Empty;

            using (var stream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                    dynamic json_info = JsonConvert.DeserializeObject<dynamic>(content);

                    return json_info.serial;

                }
            }
        }

        private void lableHeading_Click(object sender, EventArgs e)
        {

        }
    }


    // CLASSES FOR JSON DESERIALIZATION

    public class assetItem
    {
        public string serial { get; set; }
        public string id_nums { get; set; }
        public string asset_tag { get; set; }

        public assetItem(string _serial, string _id_nums, string _asset_tag)
        {
            serial = _serial;
            id_nums = _id_nums;
            asset_tag = _asset_tag;

        }
    }


    public class Model
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class StatusLabel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status_type { get; set; }
        public string status_meta { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Manufacturer
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class CreatedAt
    {
        public string datetime { get; set; }
        public string formatted { get; set; }
    }

    public class UpdatedAt
    {
        public string datetime { get; set; }
        public string formatted { get; set; }
    }

    public class PartNumber
    {
        public string field { get; set; }
        public string value { get; set; }
        public string field_format { get; set; }
    }

    public class GeneralLedger
    {
        public string field { get; set; }
        public string value { get; set; }
        public string field_format { get; set; }
    }


    public class AvailableActions
    {
        public bool checkout { get; set; }
        public bool checkin { get; set; }
        public bool clone { get; set; }
        public bool restore { get; set; }
        public bool update { get; set; }
        public bool delete { get; set; }
    }

    public class RootObject
    {
        public int id { get; set; }
        public string name { get; set; }
        public string asset_tag { get; set; }
        public string serial { get; set; }
        public Model model { get; set; }
        public string model_number { get; set; }
        public object eol { get; set; }
        public StatusLabel status_label { get; set; }
        public Category category { get; set; }
        public Manufacturer manufacturer { get; set; }
        public object supplier { get; set; }
        public object notes { get; set; }
        public string order_number { get; set; }
        public object company { get; set; }
        public object location { get; set; }
        public object rtd_location { get; set; }
        public object image { get; set; }
        public object assigned_to { get; set; }
        public object warranty_months { get; set; }
        public object warranty_expires { get; set; }
        public CreatedAt created_at { get; set; }
        public UpdatedAt updated_at { get; set; }
        public object last_audit_date { get; set; }
        public object next_audit_date { get; set; }
        public object deleted_at { get; set; }
        public object purchase_date { get; set; }
        public object last_checkout { get; set; }
        public object expected_checkin { get; set; }
        public object purchase_cost { get; set; }
        public int checkin_counter { get; set; }
        public int checkout_counter { get; set; }
        public int requests_counter { get; set; }
        public bool user_can_checkout { get; set; }
        public AvailableActions available_actions { get; set; }
    }





}
