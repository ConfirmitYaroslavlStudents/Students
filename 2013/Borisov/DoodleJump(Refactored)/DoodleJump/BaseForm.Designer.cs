namespace DoodleJump
{
    partial class BaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.DoodleBase = new System.Windows.Forms.Label();
            this.BaseLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DoodleBase
            // 
            this.DoodleBase.Image = ((System.Drawing.Image)(resources.GetObject("DoodleBase.Image")));
            this.DoodleBase.Location = new System.Drawing.Point(33, 170);
            this.DoodleBase.Name = "DoodleBase";
            this.DoodleBase.Size = new System.Drawing.Size(35, 36);
            this.DoodleBase.TabIndex = 0;
            this.DoodleBase.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DoodleBase_MouseDown);
            this.DoodleBase.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DoodleBase_MouseMove);
            this.DoodleBase.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DoodleBase_MouseUp);
            // 
            // BaseLabel
            // 
            this.BaseLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.BaseLabel.Location = new System.Drawing.Point(28, 294);
            this.BaseLabel.Name = "BaseLabel";
            this.BaseLabel.Size = new System.Drawing.Size(45, 10);
            this.BaseLabel.TabIndex = 1;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(265, 337);
            this.Controls.Add(this.BaseLabel);
            this.Controls.Add(this.DoodleBase);
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BaseForm";
            this.Activated += new System.EventHandler(this.BaseForm_Activated);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BaseForm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label BaseLabel;
        protected System.Windows.Forms.Label DoodleBase;
    }
}