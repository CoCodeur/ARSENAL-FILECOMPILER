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


        private void createFolder_Click(object sender, EventArgs e)
        {

            string pathOfFolder = @"c:\FileCompiler";

            if (!Directory.Exists(pathOfFolder))
            {
                Directory.CreateDirectory(pathOfFolder);
            }

            else
            {

                MessageBox.Show("The type of the selected file is not supported by this application. You must select an XML file.",
                       "Invalid File Type",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
            }
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




                getId(dialog.FileName);



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




            }
            else { }
        }

        private void LunchButton_Click(object sender, EventArgs e)
        {

            TreeCreatorXML(textBox1.Text);


        }


        private void button2_Click(object sender, EventArgs e)
        {


            FolderBrowserDialog browserDialog = new FolderBrowserDialog();


            if (Directory.Exists(@"c:\FileCompiler"))
            {

                browserDialog.SelectedPath = @"c:\FileCompiler";
            }

            else
            {

                browserDialog.ShowDialog();


            }



            SetText(browserDialog.SelectedPath, PathTxt);




        }


        //Method for set text in a textbox
        private void SetText(string fileName, TextBox textBox)
        {
            textBox.Text = fileName;
        }



        //Method for get ID of the command
        private dynamic getId(string filename)
        {
            var str = XMLExplorer(filename);
            var id = str.FirstAttribute;

            string parentFileName = id.ToString();
            parentFileName = CleanerID(parentFileName);

            return parentFileName;




        }

        //Method for clean the current ID

        private string CleanerID(string parentFileName)
        {
            String CleanStr = parentFileName.Remove(0, 4);

            CleanStr = CleanStr.Remove(CleanStr.Length - 1, 1);

            return CleanStr;
        }

        //Method for Search XML File and extract in String 
        private XElement XMLExplorer(string filename)
        {
            var xmlFile = File.ReadAllText(filename);
            var xmlStr = XElement.Parse(xmlFile);

            return xmlStr;
        }

        private void TreeCreatorXML(string fileName)
        {
            //recuperation du XML et de Son ID 
            XElement fileXML = XMLExplorer(fileName);
            string idRacineFolder = getId(fileName);

            

                //verification si son ID est null ou pas 
                if (idRacineFolder != null)
                {
                    
                    //Creation du dossier avec l'id de commande
                    string pathToCreate = PathTxt.Text + "/" + idRacineFolder;
                    Directory.CreateDirectory(pathToCreate);

                    //verification si il existe bien un Dossier avec l'ID
                    if (Directory.Exists(pathToCreate))
                    {

                        //Creation d'une Liste avec les matieres des produits du XML 
                        List<XElement> xElements = fileXML.Descendants("produit")
                              .Elements("matiere").ToList();


                           //Pour toutes les matieres trouvés
                        foreach (XElement element in xElements)
                        {
                            
                        //Parse de la matiere de XElement en String 
                            string matiere = element.Value.ToString();
                        //Creation du Path Associé 
                            string matierePath = pathToCreate + "/" + matiere;

                        //Verification si le dossier existe deja 
                            if (!Directory.Exists(matierePath))
                            {
                                
                                //creation du dossier matiere correspondant
                                Directory.CreateDirectory(matierePath);





                            }
                            else {

                            MessageBox.Show("The type of the selected file is not supported by this application. You must select an XML file.",
                   "Invalid File Type",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);

                        }


                        }

                    }

                    else
                    {

                    MessageBox.Show("The type of the selected file is not supported by this application. You must select an XML file.",
                   "Invalid File Type",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);

                }




                }

                else {

                MessageBox.Show("The type of the selected file is not supported by this application. You must select an XML file.",
                       "Invalid File Type",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
            }


            }

          


        }




    }


