namespace ImageShuffle
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
            this.components = new System.ComponentModel.Container();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.imageBoxShuffled = new Emgu.CV.UI.ImageBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.imageBoxRestored = new Emgu.CV.UI.ImageBox();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelCurrentTest = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.runAll = new System.Windows.Forms.Button();
            this.checkTest = new System.Windows.Forms.Button();
            this.testSelector = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxShuffled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxRestored)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // imageBoxShuffled
            // 
            this.imageBoxShuffled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxShuffled.Location = new System.Drawing.Point(25, 44);
            this.imageBoxShuffled.Name = "imageBoxShuffled";
            this.imageBoxShuffled.Size = new System.Drawing.Size(512, 512);
            this.imageBoxShuffled.TabIndex = 2;
            this.imageBoxShuffled.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(436, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "load file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(517, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "shuffle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(598, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "restore";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(249, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Shuffled";
            // 
            // imageBoxRestored
            // 
            this.imageBoxRestored.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxRestored.Location = new System.Drawing.Point(564, 44);
            this.imageBoxRestored.Name = "imageBoxRestored";
            this.imageBoxRestored.Size = new System.Drawing.Size(512, 512);
            this.imageBoxRestored.TabIndex = 7;
            this.imageBoxRestored.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(809, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Restored";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(25, 565);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(316, 159);
            this.richTextBox1.TabIndex = 14;
            this.richTextBox1.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelCurrentTest);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.runAll);
            this.groupBox1.Controls.Add(this.checkTest);
            this.groupBox1.Controls.Add(this.testSelector);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(358, 565);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(718, 159);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tests";
            // 
            // labelCurrentTest
            // 
            this.labelCurrentTest.AutoSize = true;
            this.labelCurrentTest.Location = new System.Drawing.Point(194, 131);
            this.labelCurrentTest.Name = "labelCurrentTest";
            this.labelCurrentTest.Size = new System.Drawing.Size(0, 13);
            this.labelCurrentTest.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "current test: ";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(125, 105);
            this.progressBar1.Maximum = 9;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(577, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 25;
            // 
            // runAll
            // 
            this.runAll.Location = new System.Drawing.Point(28, 105);
            this.runAll.Name = "runAll";
            this.runAll.Size = new System.Drawing.Size(75, 23);
            this.runAll.TabIndex = 24;
            this.runAll.Text = "Run All";
            this.runAll.UseVisualStyleBackColor = true;
            this.runAll.Click += new System.EventHandler(this.runAll_Click);
            // 
            // checkTest
            // 
            this.checkTest.Enabled = false;
            this.checkTest.Location = new System.Drawing.Point(231, 43);
            this.checkTest.Name = "checkTest";
            this.checkTest.Size = new System.Drawing.Size(75, 23);
            this.checkTest.TabIndex = 23;
            this.checkTest.Text = "Check";
            this.checkTest.UseVisualStyleBackColor = true;
            this.checkTest.Click += new System.EventHandler(this.checkTest_Click);
            // 
            // testSelector
            // 
            this.testSelector.FormattingEnabled = true;
            this.testSelector.Items.AddRange(new object[] {
            "a2_1",
            "a2_2",
            "a2_3",
            "a4_1",
            "a4_2",
            "a4_3",
            "a8_1",
            "a8_2",
            "a8_3"});
            this.testSelector.Location = new System.Drawing.Point(88, 43);
            this.testSelector.Name = "testSelector";
            this.testSelector.Size = new System.Drawing.Size(137, 21);
            this.testSelector.TabIndex = 1;
            this.testSelector.SelectedIndexChanged += new System.EventHandler(this.testSelector_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select test";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 736);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.imageBoxRestored);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.imageBoxShuffled);
            this.Name = "Form1";
            this.Text = "Brimit Challenge #3";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxShuffled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxRestored)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Emgu.CV.UI.ImageBox imageBoxShuffled;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private Emgu.CV.UI.ImageBox imageBoxRestored;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button checkTest;
        private System.Windows.Forms.ComboBox testSelector;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelCurrentTest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button runAll;
    }
}

