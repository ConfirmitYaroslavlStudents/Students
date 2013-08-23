namespace TimeLocker
{
    partial class MainLockerForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainLockerForm));
			this.RemaingTimeDisplay = new System.Windows.Forms.Label();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.SuspendLayout();
			// 
			// RemaingTimeDisplay
			// 
			this.RemaingTimeDisplay.AutoSize = true;
			this.RemaingTimeDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.RemaingTimeDisplay.ForeColor = System.Drawing.Color.Red;
			this.RemaingTimeDisplay.Location = new System.Drawing.Point(12, 20);
			this.RemaingTimeDisplay.Name = "RemaingTimeDisplay";
			this.RemaingTimeDisplay.Size = new System.Drawing.Size(0, 91);
			this.RemaingTimeDisplay.TabIndex = 0;
			// 
			// trayIcon
			// 
			this.trayIcon.Text = "TimeLocker";
			this.trayIcon.Visible = true;
			this.trayIcon.Click += new System.EventHandler(this.TrayIconClick);
			// 
			// MainLockerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(380, 138);
			this.Controls.Add(this.RemaingTimeDisplay);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainLockerForm";
			this.ShowInTaskbar = false;
			this.Text = "Remaining time at current day";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainLockerFormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label RemaingTimeDisplay;
        private System.Windows.Forms.NotifyIcon trayIcon;
    }
}

