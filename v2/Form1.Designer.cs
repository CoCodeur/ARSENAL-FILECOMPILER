﻿using System.IO;
using System.Windows.Forms;

namespace v2
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.PathButon = new System.Windows.Forms.Button();
            this.PathTxt = new System.Windows.Forms.TextBox();
            this.LunchButton = new System.Windows.Forms.Button();
            this.createFolder = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.button1.Location = new System.Drawing.Point(74, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "Veuillez choisir votre ficher XML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(74, 225);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(218, 20);
            this.textBox1.TabIndex = 1;
            // 
            // PathButon
            // 
            this.PathButon.Location = new System.Drawing.Point(419, 75);
            this.PathButon.Name = "PathButon";
            this.PathButon.Size = new System.Drawing.Size(222, 50);
            this.PathButon.TabIndex = 2;
            this.PathButon.Text = "Veuillez Choisir L\'emplacement cible";
            this.PathButon.UseVisualStyleBackColor = true;
            this.PathButon.Click += new System.EventHandler(this.button2_Click);
            // 
            // PathTxt
            // 
            this.PathTxt.Location = new System.Drawing.Point(419, 225);
            this.PathTxt.Name = "PathTxt";
            this.PathTxt.ReadOnly = true;
            this.PathTxt.Size = new System.Drawing.Size(222, 20);
            this.PathTxt.TabIndex = 3;
            // 
            // LunchButton
            // 
            this.LunchButton.Location = new System.Drawing.Point(287, 283);
            this.LunchButton.Name = "LunchButton";
            this.LunchButton.Size = new System.Drawing.Size(155, 59);
            this.LunchButton.TabIndex = 4;
            this.LunchButton.Text = "Lancer la Compilation";
            this.LunchButton.UseVisualStyleBackColor = true;
            this.LunchButton.Click += new System.EventHandler(this.LunchButton_Click);
            // 
            // createFolder
            // 
            this.createFolder.Location = new System.Drawing.Point(419, 149);
            this.createFolder.Name = "createFolder";
            this.createFolder.Size = new System.Drawing.Size(222, 50);
            this.createFolder.TabIndex = 5;
            this.createFolder.Text = "Créer un dossier automatiquement dans le Disque C:/\r\n";
            this.createFolder.UseVisualStyleBackColor = true;
            this.createFolder.Click += new System.EventHandler(this.createFolder_Click);
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(103, 370);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(523, 20);
            this.logTextBox.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.createFolder);
            this.Controls.Add(this.LunchButton);
            this.Controls.Add(this.PathTxt);
            this.Controls.Add(this.PathButon);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }











        #endregion

        private Button button1;
        private TextBox textBox1;
        private Button PathButon;
        private TextBox PathTxt;
        private Button LunchButton;
        private Button createFolder;
        private TextBox logTextBox;
    }
}

