namespace mapDesigner
{
    partial class MapDesigner
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
            this.numberOfLinesLabel = new System.Windows.Forms.Label();
            this.numberOfColumnsLabel = new System.Windows.Forms.Label();
            this.numberOfColumns = new System.Windows.Forms.TextBox();
            this.numberOfLines = new System.Windows.Forms.TextBox();
            this.OK = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // numberOfLinesLabel
            // 
            this.numberOfLinesLabel.AutoSize = true;
            this.numberOfLinesLabel.Location = new System.Drawing.Point(12, 9);
            this.numberOfLinesLabel.Name = "numberOfLinesLabel";
            this.numberOfLinesLabel.Size = new System.Drawing.Size(81, 13);
            this.numberOfLinesLabel.TabIndex = 0;
            this.numberOfLinesLabel.Text = "number of lines:";
            // 
            // numberOfColumnsLabel
            // 
            this.numberOfColumnsLabel.AutoSize = true;
            this.numberOfColumnsLabel.Location = new System.Drawing.Point(12, 34);
            this.numberOfColumnsLabel.Name = "numberOfColumnsLabel";
            this.numberOfColumnsLabel.Size = new System.Drawing.Size(99, 13);
            this.numberOfColumnsLabel.TabIndex = 1;
            this.numberOfColumnsLabel.Text = "number of columns:";
            // 
            // numberOfColumns
            // 
            this.numberOfColumns.Location = new System.Drawing.Point(155, 34);
            this.numberOfColumns.Name = "numberOfColumns";
            this.numberOfColumns.Size = new System.Drawing.Size(100, 20);
            this.numberOfColumns.TabIndex = 2;
            this.numberOfColumns.Validating += new System.ComponentModel.CancelEventHandler(this.numberOfColumns_Validating);
            this.numberOfColumns.Validated += new System.EventHandler(this.numberOfColumns_Validated);
            // 
            // numberOfLines
            // 
            this.numberOfLines.Location = new System.Drawing.Point(155, 8);
            this.numberOfLines.Name = "numberOfLines";
            this.numberOfLines.Size = new System.Drawing.Size(100, 20);
            this.numberOfLines.TabIndex = 1;
            this.numberOfLines.Validating += new System.ComponentModel.CancelEventHandler(this.numberOfLines_Validating);
            this.numberOfLines.Validated += new System.EventHandler(this.numberOfLines_Validated);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(88, 71);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 20);
            this.OK.TabIndex = 3;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // MapDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 103);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.numberOfLines);
            this.Controls.Add(this.numberOfColumns);
            this.Controls.Add(this.numberOfColumnsLabel);
            this.Controls.Add(this.numberOfLinesLabel);
            this.Name = "MapDesigner";
            this.Text = "mapDesigner";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label numberOfLinesLabel;
        private System.Windows.Forms.Label numberOfColumnsLabel;
        private System.Windows.Forms.TextBox numberOfColumns;
        private System.Windows.Forms.TextBox numberOfLines;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}

