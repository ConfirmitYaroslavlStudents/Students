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
            this.label5 = new System.Windows.Forms.Label();
            this.Doodle2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(28, 294);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 10);
            this.label5.TabIndex = 39;
            // 
            // Doodle2
            // 
            this.Doodle2.AllowDrop = true;
            this.Doodle2.BackColor = System.Drawing.Color.White;
            this.Doodle2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Doodle2.ForeColor = System.Drawing.Color.Cornsilk;
            this.Doodle2.Image = ((System.Drawing.Image)(resources.GetObject("Doodle2.Image")));
            this.Doodle2.Location = new System.Drawing.Point(33, 196);
            this.Doodle2.Name = "Doodle2";
            this.Doodle2.Size = new System.Drawing.Size(35, 36);
            this.Doodle2.TabIndex = 40;
            this.Doodle2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Doodle2_MouseDown);
            this.Doodle2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Doodle2_MouseMove);
            this.Doodle2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Doodle2_MouseUp);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe Script", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.Crimson;
            this.button2.Image = global::DoodleJump.Properties.Resources.Кнопка5;
            this.button2.Location = new System.Drawing.Point(123, 236);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 43);
            this.button2.TabIndex = 42;
            this.button2.TabStop = false;
            this.button2.Text = "Records...";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe Script", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.ForeColor = System.Drawing.Color.Crimson;
            this.button3.Image = global::DoodleJump.Properties.Resources.Кнопка4;
            this.button3.Location = new System.Drawing.Point(110, 157);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 49);
            this.button3.TabIndex = 43;
            this.button3.TabStop = false;
            this.button3.Text = "Play!";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(265, 337);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Doodle2);
            this.Controls.Add(this.label5);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DoodleJump!";
            this.Activated += new System.EventHandler(this.Menu_Activated);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Menu_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label Doodle2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}