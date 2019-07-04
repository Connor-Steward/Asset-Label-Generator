namespace Label_Printer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.generate_doc = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.enterButton = new System.Windows.Forms.Button();
            this.headerLabel = new System.Windows.Forms.Label();
            this.labelsAvailable = new System.Windows.Forms.RichTextBox();
            this.seperatorLine1 = new System.Windows.Forms.Label();
            this.lableHeading = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(54, 50);
            this.textBox1.MaxLength = 8;
            this.textBox1.Name = "textBox1";
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // generate_doc
            // 
            this.generate_doc.Location = new System.Drawing.Point(117, 224);
            this.generate_doc.Name = "generate_doc";
            this.generate_doc.Size = new System.Drawing.Size(217, 62);
            this.generate_doc.TabIndex = 49;
            this.generate_doc.Text = "Generate .DOC";
            this.generate_doc.UseVisualStyleBackColor = true;
            this.generate_doc.Click += new System.EventHandler(this.generate_doc_Click);
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(12, 86);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(322, 79);
            this.outputBox.TabIndex = 50;
            this.outputBox.Text = "";
            this.outputBox.TextChanged += new System.EventHandler(this.outputBox_TextChanged);
            // 
            // enterButton
            // 
            this.enterButton.Location = new System.Drawing.Point(181, 50);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(107, 20);
            this.enterButton.TabIndex = 51;
            this.enterButton.Text = "enter";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.Location = new System.Drawing.Point(50, 9);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(238, 24);
            this.headerLabel.TabIndex = 52;
            this.headerLabel.Text = "Input asset tag (PMGxxxxx)";
            this.headerLabel.Click += new System.EventHandler(this.headerLabel_Click);
            // 
            // labelsAvailable
            // 
            this.labelsAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelsAvailable.Location = new System.Drawing.Point(12, 224);
            this.labelsAvailable.MaxLength = 48;
            this.labelsAvailable.Name = "labelsAvailable";
            this.labelsAvailable.ReadOnly = true;
            this.labelsAvailable.Size = new System.Drawing.Size(69, 62);
            this.labelsAvailable.TabIndex = 53;
            this.labelsAvailable.Text = "";
            this.labelsAvailable.TextChanged += new System.EventHandler(this.labelsAvailable_TextChanged);
            // 
            // seperatorLine1
            // 
            this.seperatorLine1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.seperatorLine1.Location = new System.Drawing.Point(-53, 178);
            this.seperatorLine1.Name = "seperatorLine1";
            this.seperatorLine1.Size = new System.Drawing.Size(403, 2);
            this.seperatorLine1.TabIndex = 54;
            this.seperatorLine1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lableHeading
            // 
            this.lableHeading.AutoSize = true;
            this.lableHeading.Location = new System.Drawing.Point(9, 208);
            this.lableHeading.Name = "lableHeading";
            this.lableHeading.Size = new System.Drawing.Size(72, 13);
            this.lableHeading.TabIndex = 55;
            this.lableHeading.Text = "No. Of Labels";
            this.lableHeading.Click += new System.EventHandler(this.lableHeading_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 329);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Published by Connor Steward V1.1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 351);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lableHeading);
            this.Controls.Add(this.seperatorLine1);
            this.Controls.Add(this.labelsAvailable);
            this.Controls.Add(this.headerLabel);
            this.Controls.Add(this.enterButton);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.generate_doc);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PMG Label Printer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button generate_doc;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.Button enterButton;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.RichTextBox labelsAvailable;
        private System.Windows.Forms.Label seperatorLine1;
        private System.Windows.Forms.Label lableHeading;
        private System.Windows.Forms.Label label1;
    }
}

