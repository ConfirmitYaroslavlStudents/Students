namespace mapDesigner
{
    partial class NewLevelDesigner
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
            this.wall = new System.Windows.Forms.RadioButton();
            this.pacMan = new System.Windows.Forms.RadioButton();
            this.enemy = new System.Windows.Forms.RadioButton();
            this.clear = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // wall
            // 
            this.wall.AutoSize = true;
            this.wall.Location = new System.Drawing.Point(12, 218);
            this.wall.Name = "wall";
            this.wall.Size = new System.Drawing.Size(46, 17);
            this.wall.TabIndex = 1;
            this.wall.TabStop = true;
            this.wall.Text = "Wall";
            this.wall.UseVisualStyleBackColor = true;
            // 
            // pacMan
            // 
            this.pacMan.AutoSize = true;
            this.pacMan.Location = new System.Drawing.Point(64, 218);
            this.pacMan.Name = "pacMan";
            this.pacMan.Size = new System.Drawing.Size(65, 17);
            this.pacMan.TabIndex = 2;
            this.pacMan.TabStop = true;
            this.pacMan.Text = "PacMan";
            this.pacMan.UseVisualStyleBackColor = true;
            // 
            // enemy
            // 
            this.enemy.AutoSize = true;
            this.enemy.Location = new System.Drawing.Point(135, 218);
            this.enemy.Name = "enemy";
            this.enemy.Size = new System.Drawing.Size(57, 17);
            this.enemy.TabIndex = 3;
            this.enemy.TabStop = true;
            this.enemy.Text = "Enemy";
            this.enemy.UseVisualStyleBackColor = true;
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(103, 241);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 23);
            this.clear.TabIndex = 5;
            this.clear.Text = "Clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(197, 241);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(75, 23);
            this.exit.TabIndex = 6;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(12, 241);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 4;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // NewLevelDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.save);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.enemy);
            this.Controls.Add(this.pacMan);
            this.Controls.Add(this.wall);
            this.DoubleBuffered = true;
            this.Name = "NewLevelDesigner";
            this.Text = "newLevelDesigner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewLevelDesigner_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.NewLevelDesigner_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NewLevelDesigner_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton wall;
        private System.Windows.Forms.RadioButton pacMan;
        private System.Windows.Forms.RadioButton enemy;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button save;

    }
}