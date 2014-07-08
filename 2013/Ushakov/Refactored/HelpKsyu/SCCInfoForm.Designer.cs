namespace HelpKsyu
{
	partial class StrongConnectedComponentInfoForm
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
			this.SCCTable = new System.Windows.Forms.DataGridView();
			this.SCCNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SCCColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SCCVrtx = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.SCCTable)).BeginInit();
			this.SuspendLayout();
			// 
			// SCCTable
			// 
			this.SCCTable.AllowUserToAddRows = false;
			this.SCCTable.AllowUserToDeleteRows = false;
			this.SCCTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.SCCTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SCCNum,
            this.SCCColor,
            this.SCCVrtx});
			this.SCCTable.Location = new System.Drawing.Point(13, 13);
			this.SCCTable.Name = "SCCTable";
			this.SCCTable.ReadOnly = true;
			this.SCCTable.Size = new System.Drawing.Size(343, 237);
			this.SCCTable.TabIndex = 0;
			// 
			// SCCNum
			// 
			this.SCCNum.HeaderText = "№";
			this.SCCNum.Name = "SCCNum";
			this.SCCNum.ReadOnly = true;
			this.SCCNum.Width = 50;
			// 
			// SCCColor
			// 
			this.SCCColor.HeaderText = "Color";
			this.SCCColor.Name = "SCCColor";
			this.SCCColor.ReadOnly = true;
			this.SCCColor.Width = 50;
			// 
			// SCCVrtx
			// 
			this.SCCVrtx.HeaderText = "Vertices";
			this.SCCVrtx.Name = "SCCVrtx";
			this.SCCVrtx.ReadOnly = true;
			this.SCCVrtx.Width = 200;
			// 
			// SCCInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(368, 262);
			this.Controls.Add(this.SCCTable);
			this.Name = "SCCInfo";
			this.Text = "SCCInfo";
			this.Load += new System.EventHandler(this.SCCInfoFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.SCCTable)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView SCCTable;
		private System.Windows.Forms.DataGridViewTextBoxColumn SCCNum;
		private System.Windows.Forms.DataGridViewTextBoxColumn SCCColor;
		private System.Windows.Forms.DataGridViewTextBoxColumn SCCVrtx;
	}
}