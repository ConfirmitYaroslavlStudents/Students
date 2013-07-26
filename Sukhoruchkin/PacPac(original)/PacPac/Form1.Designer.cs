namespace PacPac
{
    partial class Level_One
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Level_One));
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.Botgr = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.Topgr = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.Leftgr = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1,
            this.Botgr,
            this.Topgr,
            this.Leftgr});
            this.shapeContainer1.Size = new System.Drawing.Size(550, 501);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BorderColor = System.Drawing.Color.Aquamarine;
            this.rectangleShape1.Location = new System.Drawing.Point(500, 0);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(50, 500);
            // 
            // Botgr
            // 
            this.Botgr.BorderColor = System.Drawing.Color.Aquamarine;
            this.Botgr.Location = new System.Drawing.Point(0, 450);
            this.Botgr.Name = "Botgr";
            this.Botgr.Size = new System.Drawing.Size(550, 50);
            // 
            // Topgr
            // 
            this.Topgr.BackColor = System.Drawing.Color.Aquamarine;
            this.Topgr.BorderColor = System.Drawing.Color.Aquamarine;
            this.Topgr.Location = new System.Drawing.Point(0, 0);
            this.Topgr.Name = "Topgr";
            this.Topgr.Size = new System.Drawing.Size(550, 50);
            // 
            // Leftgr
            // 
            this.Leftgr.BackColor = System.Drawing.Color.Teal;
            this.Leftgr.BorderColor = System.Drawing.Color.Aquamarine;
            this.Leftgr.FillColor = System.Drawing.Color.Teal;
            this.Leftgr.FillGradientColor = System.Drawing.Color.Teal;
            this.Leftgr.Location = new System.Drawing.Point(0, 0);
            this.Leftgr.Name = "Leftgr";
            this.Leftgr.SelectionColor = System.Drawing.Color.Aquamarine;
            this.Leftgr.Size = new System.Drawing.Size(50, 500);
            // 
            // Level_One
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.BackgroundImage = global::PacPac.Properties.Resources._00029;
            this.ClientSize = new System.Drawing.Size(550, 501);
            this.Controls.Add(this.shapeContainer1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Level_One";
            this.Text = "Level_One";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Level_One_Paint_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Level_One_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape Leftgr;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape Botgr;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape Topgr;
    }
}

