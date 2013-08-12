namespace HelpKsyu
{
	partial class ExitForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExitForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.sureTextBox = new System.Windows.Forms.TextBox();
			this.yesBtn = new System.Windows.Forms.Button();
			this.noBtn = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::HelpKsyu.Properties.Resources.CatImage;
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(299, 284);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// textBox1
			// 
			this.sureTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.sureTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.sureTextBox.ForeColor = System.Drawing.Color.Black;
			this.sureTextBox.Location = new System.Drawing.Point(169, 302);
			this.sureTextBox.Name = "textBox1";
			this.sureTextBox.ReadOnly = true;
			this.sureTextBox.ShortcutsEnabled = false;
			this.sureTextBox.Size = new System.Drawing.Size(87, 20);
			this.sureTextBox.TabIndex = 1;
			this.sureTextBox.TabStop = false;
			this.sureTextBox.Text = "Are you sure?";
			this.sureTextBox.WordWrap = false;
			// 
			// button1
			// 
			this.yesBtn.Location = new System.Drawing.Point(144, 332);
			this.yesBtn.Name = "button1";
			this.yesBtn.Size = new System.Drawing.Size(75, 23);
			this.yesBtn.TabIndex = 2;
			this.yesBtn.Text = "Yes";
			this.yesBtn.UseVisualStyleBackColor = true;
			this.yesBtn.Click += new System.EventHandler(this.yesBtn_Click);
			// 
			// button2
			// 
			this.noBtn.Location = new System.Drawing.Point(226, 332);
			this.noBtn.Name = "button2";
			this.noBtn.Size = new System.Drawing.Size(75, 23);
			this.noBtn.TabIndex = 3;
			this.noBtn.Text = "No";
			this.noBtn.UseVisualStyleBackColor = true;
			this.noBtn.Click += new System.EventHandler(this.noBtn_Click);
			// 
			// PE
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(323, 367);
			this.Controls.Add(this.noBtn);
			this.Controls.Add(this.yesBtn);
			this.Controls.Add(this.sureTextBox);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ExitForm";
			this.Text = "Exit";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox sureTextBox;
		private System.Windows.Forms.Button yesBtn;
		private System.Windows.Forms.Button noBtn;
	}
}