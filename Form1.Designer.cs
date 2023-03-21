namespace GK_CAD
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.relationButton = new System.Windows.Forms.Button();
            this.deletionButton = new System.Windows.Forms.Button();
            this.manipulationButton = new System.Windows.Forms.Button();
            this.drawingButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BresenhamButton = new System.Windows.Forms.RadioButton();
            this.GDIPlusButton = new System.Windows.Forms.RadioButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.utwórzWierzchołekWŚrodkuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ustalDługośćKrawędziToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.krzywaBezieraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(-2, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1183, 955);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(6, 16);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(208, 23);
            this.resetButton.TabIndex = 0;
            this.resetButton.Text = "Resetuj";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.relationButton);
            this.groupBox2.Controls.Add(this.resetButton);
            this.groupBox2.Controls.Add(this.deletionButton);
            this.groupBox2.Controls.Add(this.manipulationButton);
            this.groupBox2.Controls.Add(this.drawingButton);
            this.groupBox2.Location = new System.Drawing.Point(958, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(223, 270);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // relationButton
            // 
            this.relationButton.Location = new System.Drawing.Point(6, 213);
            this.relationButton.Name = "relationButton";
            this.relationButton.Size = new System.Drawing.Size(208, 50);
            this.relationButton.TabIndex = 3;
            this.relationButton.Text = "Dodaj relacje";
            this.relationButton.UseVisualStyleBackColor = true;
            this.relationButton.Click += new System.EventHandler(this.relationButton_Click);
            // 
            // deletionButton
            // 
            this.deletionButton.Location = new System.Drawing.Point(6, 157);
            this.deletionButton.Name = "deletionButton";
            this.deletionButton.Size = new System.Drawing.Size(208, 50);
            this.deletionButton.TabIndex = 2;
            this.deletionButton.Text = "Usuwanie";
            this.deletionButton.UseVisualStyleBackColor = true;
            this.deletionButton.Click += new System.EventHandler(this.deletionButton_Click);
            // 
            // manipulationButton
            // 
            this.manipulationButton.Location = new System.Drawing.Point(6, 101);
            this.manipulationButton.Name = "manipulationButton";
            this.manipulationButton.Size = new System.Drawing.Size(208, 50);
            this.manipulationButton.TabIndex = 1;
            this.manipulationButton.Text = "Manipulowanie";
            this.manipulationButton.UseVisualStyleBackColor = true;
            this.manipulationButton.Click += new System.EventHandler(this.manipulationButton_Click);
            // 
            // drawingButton
            // 
            this.drawingButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.drawingButton.Location = new System.Drawing.Point(6, 45);
            this.drawingButton.Name = "drawingButton";
            this.drawingButton.Size = new System.Drawing.Size(208, 50);
            this.drawingButton.TabIndex = 0;
            this.drawingButton.Text = "Rysowanie";
            this.drawingButton.UseVisualStyleBackColor = false;
            this.drawingButton.Click += new System.EventHandler(this.drawingButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BresenhamButton);
            this.groupBox1.Controls.Add(this.GDIPlusButton);
            this.groupBox1.Location = new System.Drawing.Point(958, 276);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 76);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algorytm rysowania";
            // 
            // BresenhamButton
            // 
            this.BresenhamButton.AutoSize = true;
            this.BresenhamButton.Location = new System.Drawing.Point(19, 47);
            this.BresenhamButton.Name = "BresenhamButton";
            this.BresenhamButton.Size = new System.Drawing.Size(143, 19);
            this.BresenhamButton.TabIndex = 1;
            this.BresenhamButton.TabStop = true;
            this.BresenhamButton.Text = "Algorytm Bresenhama";
            this.BresenhamButton.UseVisualStyleBackColor = true;
            this.BresenhamButton.CheckedChanged += new System.EventHandler(this.BresenhamButton_CheckedChanged);
            // 
            // GDIPlusButton
            // 
            this.GDIPlusButton.AutoSize = true;
            this.GDIPlusButton.Location = new System.Drawing.Point(19, 22);
            this.GDIPlusButton.Name = "GDIPlusButton";
            this.GDIPlusButton.Size = new System.Drawing.Size(189, 19);
            this.GDIPlusButton.TabIndex = 0;
            this.GDIPlusButton.TabStop = true;
            this.GDIPlusButton.Text = "Algorytm biblioteczny GDI Plus";
            this.GDIPlusButton.UseVisualStyleBackColor = true;
            this.GDIPlusButton.CheckedChanged += new System.EventHandler(this.GDIPlusButton_CheckedChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.utwórzWierzchołekWŚrodkuToolStripMenuItem,
            this.ustalDługośćKrawędziToolStripMenuItem,
            this.krzywaBezieraToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(228, 92);
            // 
            // utwórzWierzchołekWŚrodkuToolStripMenuItem
            // 
            this.utwórzWierzchołekWŚrodkuToolStripMenuItem.Name = "utwórzWierzchołekWŚrodkuToolStripMenuItem";
            this.utwórzWierzchołekWŚrodkuToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.utwórzWierzchołekWŚrodkuToolStripMenuItem.Text = "Utwórz wierzchołek w środku";
            this.utwórzWierzchołekWŚrodkuToolStripMenuItem.Click += new System.EventHandler(this.utwórzWierzchołekWŚrodkuToolStripMenuItem_Click);
            // 
            // ustalDługośćKrawędziToolStripMenuItem
            // 
            this.ustalDługośćKrawędziToolStripMenuItem.Name = "ustalDługośćKrawędziToolStripMenuItem";
            this.ustalDługośćKrawędziToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.ustalDługośćKrawędziToolStripMenuItem.Text = "Ustal długość krawędzi";
            this.ustalDługośćKrawędziToolStripMenuItem.Click += new System.EventHandler(this.ustalDługośćKrawędziToolStripMenuItem_Click);
            // 
            // krzywaBezieraToolStripMenuItem
            // 
            this.krzywaBezieraToolStripMenuItem.Name = "krzywaBezieraToolStripMenuItem";
            this.krzywaBezieraToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.krzywaBezieraToolStripMenuItem.Text = "Krzywa Beziera";
            this.krzywaBezieraToolStripMenuItem.Click += new System.EventHandler(this.krzywaBezieraToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 900);
            this.MinimumSize = new System.Drawing.Size(1200, 900);
            this.Name = "Form1";
            this.Text = "GK_CAD";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public PictureBox pictureBox1;
        private Button resetButton;
        private GroupBox groupBox2;
        private Button manipulationButton;
        private Button drawingButton;
        private Button deletionButton;
        private GroupBox groupBox1;
        private RadioButton BresenhamButton;
        private RadioButton GDIPlusButton;
        private ToolStripMenuItem utwórzWierzchołekWŚrodkuToolStripMenuItem;
        private ToolStripMenuItem ustalDługośćKrawędziToolStripMenuItem;
        public ContextMenuStrip contextMenuStrip1;
        private Button relationButton;
        private ToolStripMenuItem krzywaBezieraToolStripMenuItem;
    }
}