namespace Tolentino_CoinsActivity
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            loadToolStripMenuItem = new ToolStripMenuItem();
            loadImageToolStripMenuItem = new ToolStripMenuItem();
            coinsPictureBox = new PictureBox();
            button1 = new Button();
            valueLabel = new Label();
            coinsTextBox = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)coinsPictureBox).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { loadToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadImageToolStripMenuItem });
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(56, 24);
            loadToolStripMenuItem.Text = "Load";
            // 
            // loadImageToolStripMenuItem
            // 
            loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            loadImageToolStripMenuItem.Size = new Size(171, 26);
            loadImageToolStripMenuItem.Text = "Load Image";
            loadImageToolStripMenuItem.Click += loadImageToolStripMenuItem_Click;
            // 
            // coinsPictureBox
            // 
            coinsPictureBox.BorderStyle = BorderStyle.Fixed3D;
            coinsPictureBox.Location = new Point(12, 47);
            coinsPictureBox.Name = "coinsPictureBox";
            coinsPictureBox.Size = new Size(379, 372);
            coinsPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            coinsPictureBox.TabIndex = 1;
            coinsPictureBox.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(440, 115);
            button1.Name = "button1";
            button1.Size = new Size(284, 54);
            button1.TabIndex = 2;
            button1.Text = "Calculate";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // valueLabel
            // 
            valueLabel.AutoSize = true;
            valueLabel.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            valueLabel.Location = new Point(440, 65);
            valueLabel.Name = "valueLabel";
            valueLabel.Size = new Size(84, 38);
            valueLabel.TabIndex = 3;
            valueLabel.Text = "Value";
            // 
            // coinsTextBox
            // 
            coinsTextBox.Location = new Point(442, 176);
            coinsTextBox.Multiline = true;
            coinsTextBox.Name = "coinsTextBox";
            coinsTextBox.Size = new Size(282, 224);
            coinsTextBox.TabIndex = 4;
            coinsTextBox.TextChanged += coinsTextBox_TextChanged;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.FileOk += openFileDialog1_FileOk;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(coinsTextBox);
            Controls.Add(valueLabel);
            Controls.Add(button1);
            Controls.Add(coinsPictureBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Tolentino Coins Activity";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)coinsPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem loadImageToolStripMenuItem;
        private PictureBox coinsPictureBox;
        private Button button1;
        private Label valueLabel;
        private TextBox coinsTextBox;
        private OpenFileDialog openFileDialog1;
    }
}
