namespace MineSweeper
{
    partial class MainMineSweeperForm
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
			this.newGameButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// newGameButton
			// 
			this.newGameButton.Location = new System.Drawing.Point(12, 12);
			this.newGameButton.Name = "newGameButton";
			this.newGameButton.Size = new System.Drawing.Size(272, 23);
			this.newGameButton.TabIndex = 0;
			this.newGameButton.Text = "New Game";
			this.newGameButton.UseVisualStyleBackColor = true;
			this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
			// 
			// MainMineSweeperForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(296, 307);
			this.Controls.Add(this.newGameButton);
			this.Name = "MainMineSweeperForm";
			this.Text = "Mine Sweeper";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainMineSweeperForm_Paint);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newGameButton;
    }
}

