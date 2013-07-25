namespace DoodleJump
{
    partial class Lose
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lose));
            this.PlayAgainButton = new System.Windows.Forms.Button();
            this.MenuButton = new System.Windows.Forms.Button();
            this.LoseDoodle = new System.Windows.Forms.Label();
            this.LooseLabel = new System.Windows.Forms.Label();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PlayAgainButton
            // 
            this.PlayAgainButton.BackColor = System.Drawing.Color.Transparent;
            this.PlayAgainButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.PlayAgainButton.FlatAppearance.BorderSize = 0;
            this.PlayAgainButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.PlayAgainButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.PlayAgainButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayAgainButton.Font = new System.Drawing.Font("Segoe Script", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PlayAgainButton.ForeColor = System.Drawing.Color.Crimson;
            this.PlayAgainButton.Image = ((System.Drawing.Image)(resources.GetObject("PlayAgainButton.Image")));
            this.PlayAgainButton.Location = new System.Drawing.Point(99, 215);
            this.PlayAgainButton.Name = "PlayAgainButton";
            this.PlayAgainButton.Size = new System.Drawing.Size(135, 41);
            this.PlayAgainButton.TabIndex = 0;
            this.PlayAgainButton.TabStop = false;
            this.PlayAgainButton.Text = "play again";
            this.PlayAgainButton.UseVisualStyleBackColor = false;
            this.PlayAgainButton.Click += new System.EventHandler(this.PlayAgainButton_Click);
            // 
            // MenuButton
            // 
            this.MenuButton.BackColor = System.Drawing.Color.Transparent;
            this.MenuButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.MenuButton.FlatAppearance.BorderSize = 0;
            this.MenuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.MenuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.MenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MenuButton.Font = new System.Drawing.Font("Segoe Script", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MenuButton.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.MenuButton.Image = ((System.Drawing.Image)(resources.GetObject("MenuButton.Image")));
            this.MenuButton.Location = new System.Drawing.Point(148, 262);
            this.MenuButton.Name = "MenuButton";
            this.MenuButton.Size = new System.Drawing.Size(98, 42);
            this.MenuButton.TabIndex = 1;
            this.MenuButton.TabStop = false;
            this.MenuButton.Text = "menu";
            this.MenuButton.UseVisualStyleBackColor = false;
            this.MenuButton.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // LoseDoodle
            // 
            this.LoseDoodle.BackColor = System.Drawing.Color.White;
            this.LoseDoodle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoseDoodle.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.LoseDoodle.Image = ((System.Drawing.Image)(resources.GetObject("LoseDoodle.Image")));
            this.LoseDoodle.Location = new System.Drawing.Point(21, 198);
            this.LoseDoodle.Name = "LoseDoodle";
            this.LoseDoodle.Size = new System.Drawing.Size(35, 36);
            this.LoseDoodle.TabIndex = 41;
            // 
            // LooseLabel
            // 
            this.LooseLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.LooseLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LooseLabel.Location = new System.Drawing.Point(21, 294);
            this.LooseLabel.Name = "LooseLabel";
            this.LooseLabel.Size = new System.Drawing.Size(45, 10);
            this.LooseLabel.TabIndex = 42;
            // 
            // NameBox
            // 
            this.NameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NameBox.Font = new System.Drawing.Font("Segoe Print", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameBox.Location = new System.Drawing.Point(134, 137);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(100, 23);
            this.NameBox.TabIndex = 43;
            this.NameBox.Text = "Игрок";
            this.NameBox.Visible = false;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.BackColor = System.Drawing.Color.Transparent;
            this.NameLabel.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameLabel.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.NameLabel.Location = new System.Drawing.Point(133, 137);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(47, 17);
            this.NameLabel.TabIndex = 44;
            this.NameLabel.Text = "name";
            this.NameLabel.Click += new System.EventHandler(this.NameLabel_Click);
            // 
            // Lose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(265, 337);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.LooseLabel);
            this.Controls.Add(this.LoseDoodle);
            this.Controls.Add(this.MenuButton);
            this.Controls.Add(this.PlayAgainButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Lose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game over!";
            this.Activated += new System.EventHandler(this.Lose_Activated);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Lose_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PlayAgainButton;
        private System.Windows.Forms.Button MenuButton;
        public System.Windows.Forms.Label LoseDoodle;
        private System.Windows.Forms.Label LooseLabel;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label NameLabel;
    }
}