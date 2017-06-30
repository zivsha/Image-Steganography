namespace HideInImage
{
    partial class HideInImageForm
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
            this.buttonLoadImage = new System.Windows.Forms.Button();
            this.buttonHide = new System.Windows.Forms.Button();
            this.buttonReveal = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.richTextBoxInputText = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonEncoding = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonDecoding = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLoadImage
            // 
            this.buttonLoadImage.Location = new System.Drawing.Point(6, 19);
            this.buttonLoadImage.Name = "buttonLoadImage";
            this.buttonLoadImage.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadImage.TabIndex = 0;
            this.buttonLoadImage.Text = "Browse...";
            this.buttonLoadImage.UseVisualStyleBackColor = true;
            this.buttonLoadImage.Click += new System.EventHandler(this.buttonLoadImage_Click);
            // 
            // buttonHide
            // 
            this.buttonHide.Location = new System.Drawing.Point(187, 19);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(133, 23);
            this.buttonHide.TabIndex = 1;
            this.buttonHide.Text = "Encode (Hide)";
            this.buttonHide.UseVisualStyleBackColor = true;
            this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
            // 
            // buttonReveal
            // 
            this.buttonReveal.Location = new System.Drawing.Point(65, 125);
            this.buttonReveal.Name = "buttonReveal";
            this.buttonReveal.Size = new System.Drawing.Size(133, 23);
            this.buttonReveal.TabIndex = 2;
            this.buttonReveal.Text = "Decode...";
            this.buttonReveal.UseVisualStyleBackColor = true;
            this.buttonReveal.Click += new System.EventHandler(this.buttonReveal_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxFileName);
            this.groupBox1.Controls.Add(this.buttonLoadImage);
            this.groupBox1.Location = new System.Drawing.Point(6, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 61);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Step 1: Choose an Image";
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(93, 22);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.ReadOnly = true;
            this.textBoxFileName.Size = new System.Drawing.Size(227, 20);
            this.textBoxFileName.TabIndex = 1;
            // 
            // richTextBoxInputText
            // 
            this.richTextBoxInputText.Location = new System.Drawing.Point(12, 183);
            this.richTextBoxInputText.Name = "richTextBoxInputText";
            this.richTextBoxInputText.Size = new System.Drawing.Size(600, 162);
            this.richTextBoxInputText.TabIndex = 5;
            this.richTextBoxInputText.Text = "";
            this.richTextBoxInputText.TextChanged += new System.EventHandler(this.richTextBoxInputText_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonHide);
            this.groupBox2.Location = new System.Drawing.Point(6, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 53);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Step 2: Write some text below and click \'Encode\'";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 961);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(627, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonEncoding);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(345, 165);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // radioButtonEncoding
            // 
            this.radioButtonEncoding.AutoSize = true;
            this.radioButtonEncoding.Location = new System.Drawing.Point(6, 9);
            this.radioButtonEncoding.Name = "radioButtonEncoding";
            this.radioButtonEncoding.Size = new System.Drawing.Size(70, 17);
            this.radioButtonEncoding.TabIndex = 10;
            this.radioButtonEncoding.Text = "Encoding";
            this.radioButtonEncoding.UseVisualStyleBackColor = true;
            this.radioButtonEncoding.CheckedChanged += new System.EventHandler(this.radioButtonEncoding_CheckedChanged);
            this.radioButtonEncoding.Click += new System.EventHandler(this.radioButtonEncoding_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButtonDecoding);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.buttonReveal);
            this.groupBox4.Location = new System.Drawing.Point(363, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(249, 165);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            // 
            // radioButtonDecoding
            // 
            this.radioButtonDecoding.AutoSize = true;
            this.radioButtonDecoding.Location = new System.Drawing.Point(6, 9);
            this.radioButtonDecoding.Name = "radioButtonDecoding";
            this.radioButtonDecoding.Size = new System.Drawing.Size(71, 17);
            this.radioButtonDecoding.TabIndex = 11;
            this.radioButtonDecoding.TabStop = true;
            this.radioButtonDecoding.Text = "Decoding";
            this.radioButtonDecoding.UseVisualStyleBackColor = true;
            this.radioButtonDecoding.CheckedChanged += new System.EventHandler(this.radioButtonDecoding_CheckedChanged);
            this.radioButtonDecoding.Click += new System.EventHandler(this.radioButtonDecoding_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Click to choose an image with secret text";
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Location = new System.Drawing.Point(12, 351);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(600, 600);
            this.pictureBoxImage.TabIndex = 4;
            this.pictureBoxImage.TabStop = false;
            // 
            // HideInImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 983);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.richTextBoxInputText);
            this.Controls.Add(this.pictureBoxImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "HideInImageForm";
            this.Text = "Steganography - Hidden Text in Image (Ziv Shahaf © 2017)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadImage;
        private System.Windows.Forms.Button buttonHide;
        private System.Windows.Forms.Button buttonReveal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.RichTextBox richTextBoxInputText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonEncoding;
        private System.Windows.Forms.RadioButton radioButtonDecoding;
    }
}

