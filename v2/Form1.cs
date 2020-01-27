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

                SetText(dialog.FileName);

                try
                {
                    var file = new StreamReader(dialog.FileName);

                }

                catch (SecurityException ex)
                {
                    MessageBox.Show($"Secutiry error. \n\n Error message: {ex.Message} \n\n" + $"Details: \n\n{ex.StackTrace}");
                }

                  

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
            
private void SetText(string fileName)
        {
            textBox1.Text = fileName;
        }
    }

     

     
    }

