namespace DoodleJump
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.RecordsButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RecordsButton
            // 
            this.RecordsButton.BackColor = System.Drawing.Color.Transparent;
            this.RecordsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.RecordsButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.RecordsButton.FlatAppearance.BorderSize = 0;
            this.RecordsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.RecordsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.RecordsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RecordsButton.Font = new System.Drawing.Font("Segoe Script", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RecordsButton.ForeColor = System.Drawing.Color.Crimson;
            this.RecordsButton.Image = global::DoodleJump.Properties.Resources.Кнопка5;
            this.RecordsButton.Location = new System.Drawing.Point(123, 236);
            this.RecordsButton.Name = "RecordsButton";
            this.RecordsButton.Size = new System.Drawing.Size(132, 43);
            this.RecordsButton.TabIndex = 42;
            this.RecordsButton.TabStop = false;
            this.RecordsButton.Text = "Records...";
            this.RecordsButton.UseVisualStyleBackColor = false;
            this.RecordsButton.Click += new System.EventHandler(this.RecordsButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.BackColor = System.Drawing.Color.Transparent;
            this.PlayButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PlayButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.PlayButton.FlatAppearance.BorderSize = 0;
            this.PlayButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.PlayButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.PlayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayButton.Font = new System.Drawing.Font("Segoe Script", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PlayButton.ForeColor = System.Drawing.Color.Crimson;
            this.PlayButton.Image = global::DoodleJump.Properties.Resources.Кнопка4;
            this.PlayButton.Location = new System.Drawing.Point(110, 157);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(101, 49);
            this.PlayButton.TabIndex = 43;
            this.PlayButton.TabStop = false;
            this.PlayButton.Text = "Play!";
            this.PlayButton.UseVisualStyleBackColor = false;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(265, 337);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.RecordsButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Menu";
            this.Text = "DoodleJump!";
            this.Activated += new System.EventHandler(this.Menu_Activated);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Menu_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RecordsButton;
        private System.Windows.Forms.Button PlayButton;
    }
}