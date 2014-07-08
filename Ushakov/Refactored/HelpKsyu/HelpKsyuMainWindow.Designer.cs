namespace HelpKsyu
{
	partial class HelpKsyuMainWindow
	{
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpKsyuMainWindow));
			this.DisplayForGraph = new Tao.Platform.Windows.SimpleOpenGlControl();
			this.ShowGraphs = new System.Windows.Forms.Button();
			this.openInputTxt = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fullSCCsInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// DsplForGraph
			// 
			this.DisplayForGraph.AccumBits = ((byte)(0));
			this.DisplayForGraph.AutoCheckErrors = false;
			this.DisplayForGraph.AutoFinish = false;
			this.DisplayForGraph.AutoMakeCurrent = true;
			this.DisplayForGraph.AutoSwapBuffers = true;
			this.DisplayForGraph.BackColor = System.Drawing.Color.Black;
			this.DisplayForGraph.ColorBits = ((byte)(32));
			this.DisplayForGraph.DepthBits = ((byte)(16));
			this.DisplayForGraph.Location = new System.Drawing.Point(12, 65);
			this.DisplayForGraph.Name = "DsplForGraph";
			this.DisplayForGraph.Size = new System.Drawing.Size(760, 760);
			this.DisplayForGraph.StencilBits = ((byte)(0));
			this.DisplayForGraph.TabIndex = 0;
			// 
			// ShowGraphs
			// 
			this.ShowGraphs.BackColor = System.Drawing.Color.Red;
			this.ShowGraphs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ShowGraphs.Location = new System.Drawing.Point(12, 28);
			this.ShowGraphs.Name = "ShowGraphs";
			this.ShowGraphs.Size = new System.Drawing.Size(760, 31);
			this.ShowGraphs.TabIndex = 3;
			this.ShowGraphs.Text = "Show magic with graph";
			this.ShowGraphs.UseVisualStyleBackColor = false;
			this.ShowGraphs.Click += new System.EventHandler(this.ShowGraphs_Click);
			// 
			// openInputTxt
			// 
			this.openInputTxt.FileName = "openInputTxt";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.graphToolStripMenuItem,
            this.showToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(784, 24);
			this.menuStrip1.TabIndex = 4;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// graphToolStripMenuItem
			// 
			this.graphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enterToolStripMenuItem,
            this.openFromToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
			this.graphToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			this.graphToolStripMenuItem.Text = "Graph";
			// 
			// enterToolStripMenuItem
			// 
			this.enterToolStripMenuItem.Name = "enterToolStripMenuItem";
			this.enterToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.enterToolStripMenuItem.Text = "Enter in the console";
			this.enterToolStripMenuItem.Click += new System.EventHandler(this.ReadGraphFromAConsole_Click);
			// 
			// openFromToolStripMenuItem
			// 
			this.openFromToolStripMenuItem.Name = "openFromToolStripMenuItem";
			this.openFromToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.openFromToolStripMenuItem.Text = "Open from a text file";
			this.openFromToolStripMenuItem.Click += new System.EventHandler(this.ReadGraphFromATextFile_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// showToolStripMenuItem
			// 
			this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullSCCsInfoToolStripMenuItem});
			this.showToolStripMenuItem.Name = "showToolStripMenuItem";
			this.showToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.showToolStripMenuItem.Text = "Show";
			// 
			// fullSCCsInfoToolStripMenuItem
			// 
			this.fullSCCsInfoToolStripMenuItem.Name = "fullSCCsInfoToolStripMenuItem";
			this.fullSCCsInfoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.fullSCCsInfoToolStripMenuItem.Text = "Full SCC\'s info";
			this.fullSCCsInfoToolStripMenuItem.Click += new System.EventHandler(this.fullSCCsInfoToolStripMenuItem_Click);
			// 
			// HelpKsyuMainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(784, 837);
			this.Controls.Add(this.ShowGraphs);
			this.Controls.Add(this.DisplayForGraph);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "HelpKsyuMainWindow";
			this.Text = "HelpKsyu";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Tao.Platform.Windows.SimpleOpenGlControl DisplayForGraph;
		private System.Windows.Forms.Button ShowGraphs;
		private System.Windows.Forms.OpenFileDialog openInputTxt;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem enterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openFromToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fullSCCsInfoToolStripMenuItem;
	}
}

