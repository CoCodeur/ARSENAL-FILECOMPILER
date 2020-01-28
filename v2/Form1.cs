using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace v2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Dialog

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML Files (*.xml)|*.xml";
            dialog.FilterIndex = 0;
            dialog.DefaultExt = "xml";

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                SetText(dialog.FileName, textBox1);
                XmlCopileur(dialog.FileName);

                  

                  }
                if (!string.Equals(Path.GetExtension(dialog.FileName),
                        ".xml",
                        System.StringComparison.OrdinalIgnoreCase
                    ))
                {

                    MessageBox.Show("The type of the selected file is not supported by this application. You must select an XML file.",
                        "Invalid File Type",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                //TODO LOL 


                }
            else { }
        }


        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog pathDialog = new OpenFileDialog();
            SetText(pathDialog.FileName, PathTxt);
            

        }


        private void SetText(string fileName, TextBox textBox)
        {
            textBox.Text = fileName;
        }


        private void XmlCopileur(string filename)
        {
            var xmlStr = File.ReadAllText(filename);

            var str = XElement.Parse(xmlStr);
            var id = str.FirstAttribute;

            string paretnFileName = id.ToString();

            SetText(CleanerID(paretnFileName),PathTxt);
        }


        private string CleanerID(string parentFileName)
        {
               String CleanStr = parentFileName.Remove(0, 4);

               CleanStr = CleanStr.Remove(CleanStr.Length - 1, 1);

               return CleanStr;
        }

       
    }

     

     
    }

