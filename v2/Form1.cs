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
using Utilities.Network;
using System.Configuration;

namespace v2
{
    public partial class FileCompiler : Form
    {
        public FileCompiler()
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
            logTextBox.Text = null;
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

        private List<string> SearchTag(string tagName, string fileName)
        {
            XElement fileXML = XMLExplorer(fileName);
            List<XElement> elementBalise = fileXML.Descendants("produit")
                .Elements(tagName).ToList();

            List<string> elementBaliseStr = new List<string> { };

            foreach (XElement element in elementBalise)
            {
                elementBaliseStr.Add(element.Value.ToString());


            }

            return elementBaliseStr;

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
                string pathToCreate = Path.Combine(PathTxt.Text, idRacineFolder);

                if (!Directory.Exists(pathToCreate))
                {
                    Directory.CreateDirectory(pathToCreate);

                    //verification si il existe bien un Dossier avec l'ID
                    if (Directory.Exists(pathToCreate))
                    {

                        List<string> matiere = SearchTag("matiere", fileName);


                        foreach (string matiereInList in matiere)
                        {

                            string matierePath = Path.Combine(pathToCreate, matiereInList);





                            //Verification si le dossier existe deja 
                            if (!Directory.Exists(matierePath))
                            {

                                //creation du dossier matiere correspondant
                                Directory.CreateDirectory(matierePath);
                                

                            }
                            else { }
                        }

                                AddPDF(fileName, pathToCreate);
                    }
                    else { }
                }
                else {
                    DialogResult fenetreFichierExistant = MessageBox.Show("Il existe déjà un dossier du même nom, le supprimer et remplacer ?",
                        "The Question",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (fenetreFichierExistant == DialogResult.Yes) {

                        DeleteDirectory(pathToCreate);

                        Directory.CreateDirectory(pathToCreate);

                        //verification si il existe bien un Dossier avec l'ID
                        if (Directory.Exists(pathToCreate))
                        {

                            List<string> matiere = SearchTag("matiere", fileName);


                            foreach (string matiereInList in matiere)
                            {


                                string matierePath = Path.Combine(pathToCreate, matiereInList);





                                //Verification si le dossier existe deja 
                                if (!Directory.Exists(matierePath))
                                {

                                    //creation du dossier matiere correspondant
                                    Directory.CreateDirectory(matierePath);
                                   

                                }
                                else { }
                            }

                            AddPDF(fileName, pathToCreate);
                        }
                        else { }


                    }


                    else if(fenetreFichierExistant == DialogResult.No) {
                    
                    
                    
                    }

                    

                }
            } else {

                Console.WriteLine("pas d'ID détécté");
            
            }

            }

        private void AddPDF(string filename, string pathToTarget)
        {
            //connect to NAS
            Utilities.Network.NetworkDrive network = new Utilities.Network.NetworkDrive();

            network.MapNetworkDrive(@"\\" + ConfigurationManager.AppSettings["server"] + @"\" + ConfigurationManager.AppSettings["path"],
                ConfigurationManager.AppSettings["drive"],
                ConfigurationManager.AppSettings["username"],
                ConfigurationManager.AppSettings["password"]);

            List<XElement> xElements = XMLExplorer(filename).Elements("produit").ToList();

            string IdCommande = CleanerID(getId(filename));

            int indexFileFound = 0;
            int indexFolderFound = 0;
            int indexFolderNotFound = 0;
            int indexFileFoundAndCopy = 0;

            


            foreach (XElement element in xElements)
            {



                string targetToCopy = ConfigurationManager.AppSettings["drive"] + element.Element("categorie").Value.ToString().Replace(" / ", @"\");
                DirectoryInfo directoryToCopy = new DirectoryInfo(targetToCopy);

               

                if (!Directory.Exists(targetToCopy))
                {

                    indexFolderNotFound++;
                }

                else
                {

                    string nomFichier = element.Element("reference").Value.ToString();



                    //foreach (var file in directoryToCopy.GetFiles("*" + nomFichier + "*"))
                    //{

                    //    indexFileFound++;




                    //    File.Copy(Path.Combine(directoryToCopy.ToString(), file.Name), Path.Combine(pathToTarget, element.Element("matiere").Value.ToString(), file.Name));




                    //    indexFileFoundAndCopy++;
                    //}









                    indexFolderFound++;

                   // Console.WriteLine(targetToCopy);
                }







            }

            

            logTextBox.Text += indexFolderFound.ToString() + " Dossier(s) trouvé(s)  dont :  ";
            logTextBox.Text += indexFileFound.ToString() + " fichier(s) trouvé(s)  dont :   ";
            logTextBox.Text += indexFileFoundAndCopy.ToString() + " fichier(s) copié(s) ";
            logTextBox.Text += xElements.Count - indexFileFound + " fichier(s) non trouvé(s) ";
            logTextBox.Text += indexFolderNotFound.ToString() + " dossier(s) non trouvé(s)   ";
         
        }


           


            private void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }



        private void Clear_Click(object sender, EventArgs e)
        {

            string dirToClear = PathTxt.Text;
            System.IO.DirectoryInfo directoryInfo = new DirectoryInfo(dirToClear);

            if(String.IsNullOrEmpty(dirToClear))
            {

                MessageBox.Show("Vous avez selectionner aucun repertoire à vider",
                    "Error",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);

            }

            else
            {

                DialogResult confirmation = MessageBox.Show("Etes vous sûr de vouloir vider ce dossier ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

                if(confirmation == DialogResult.Yes)
                {
                        
                    foreach(FileInfo file in directoryInfo.GetFiles())
                    {
                        file.Delete();
                    }

                    foreach(DirectoryInfo directory in directoryInfo.GetDirectories())
                    {

                        directory.Delete(true);
                    }
}

                else
                {

                }
               
                
            }

        }
    }



    }

    


    


