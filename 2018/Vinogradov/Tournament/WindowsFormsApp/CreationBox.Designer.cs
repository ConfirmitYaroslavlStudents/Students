namespace WindowsFormsApp
{
    partial class CreationBox
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
            this.checkBoxDoubleElimination = new System.Windows.Forms.CheckBox();
            this.numericNumberOfPlayers = new System.Windows.Forms.NumericUpDown();
            this.textBoxInputName = new System.Windows.Forms.TextBox();
            this.labelInputName = new System.Windows.Forms.Label();
            this.labelNumberOfPlayers = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.textBoxNames = new System.Windows.Forms.TextBox();
            this.buttonClearNames = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericNumberOfPlayers)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxDoubleElimination
            // 
            this.checkBoxDoubleElimination.AutoSize = true;
            this.checkBoxDoubleElimination.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxDoubleElimination.Location = new System.Drawing.Point(24, 30);
            this.checkBoxDoubleElimination.Name = "checkBoxDoubleElimination";
            this.checkBoxDoubleElimination.Size = new System.Drawing.Size(144, 21);
            this.checkBoxDoubleElimination.TabIndex = 0;
            this.checkBoxDoubleElimination.Text = "Double Elimination";
            this.checkBoxDoubleElimination.UseVisualStyleBackColor = true;
            // 
            // numericNumberOfPlayers
            // 
            this.numericNumberOfPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericNumberOfPlayers.Location = new System.Drawing.Point(24, 74);
            this.numericNumberOfPlayers.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericNumberOfPlayers.Name = "numericNumberOfPlayers";
            this.numericNumberOfPlayers.Size = new System.Drawing.Size(120, 23);
            this.numericNumberOfPlayers.TabIndex = 1;
            this.numericNumberOfPlayers.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericNumberOfPlayers.ValueChanged += new System.EventHandler(this.numericNumberOfPlayers_ValueChanged);
            // 
            // textBoxInputName
            // 
            this.textBoxInputName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxInputName.Location = new System.Drawing.Point(24, 120);
            this.textBoxInputName.MaxLength = 8;
            this.textBoxInputName.Name = "textBoxInputName";
            this.textBoxInputName.Size = new System.Drawing.Size(100, 23);
            this.textBoxInputName.TabIndex = 2;
            this.textBoxInputName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxInputName_KeyPress);
            // 
            // labelInputName
            // 
            this.labelInputName.AutoSize = true;
            this.labelInputName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInputName.Location = new System.Drawing.Point(21, 100);
            this.labelInputName.Name = "labelInputName";
            this.labelInputName.Size = new System.Drawing.Size(80, 17);
            this.labelInputName.TabIndex = 4;
            this.labelInputName.Text = "Input Name";
            // 
            // labelNumberOfPlayers
            // 
            this.labelNumberOfPlayers.AutoSize = true;
            this.labelNumberOfPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNumberOfPlayers.Location = new System.Drawing.Point(21, 54);
            this.labelNumberOfPlayers.Name = "labelNumberOfPlayers";
            this.labelNumberOfPlayers.Size = new System.Drawing.Size(174, 17);
            this.labelNumberOfPlayers.TabIndex = 4;
            this.labelNumberOfPlayers.Text = "Choose number of players";
            // 
            // buttonCreate
            // 
            this.buttonCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCreate.Location = new System.Drawing.Point(23, 160);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(145, 60);
            this.buttonCreate.TabIndex = 5;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // textBoxNames
            // 
            this.textBoxNames.Location = new System.Drawing.Point(210, 21);
            this.textBoxNames.Multiline = true;
            this.textBoxNames.Name = "textBoxNames";
            this.textBoxNames.ReadOnly = true;
            this.textBoxNames.Size = new System.Drawing.Size(98, 150);
            this.textBoxNames.TabIndex = 6;
            // 
            // buttonClearNames
            // 
            this.buttonClearNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClearNames.Location = new System.Drawing.Point(222, 177);
            this.buttonClearNames.Name = "buttonClearNames";
            this.buttonClearNames.Size = new System.Drawing.Size(73, 27);
            this.buttonClearNames.TabIndex = 7;
            this.buttonClearNames.Text = "Clear";
            this.buttonClearNames.UseVisualStyleBackColor = true;
            this.buttonClearNames.Click += new System.EventHandler(this.buttonClearNames_Click);
            // 
            // CreationBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 232);
            this.Controls.Add(this.buttonClearNames);
            this.Controls.Add(this.textBoxNames);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.labelNumberOfPlayers);
            this.Controls.Add(this.labelInputName);
            this.Controls.Add(this.textBoxInputName);
            this.Controls.Add(this.numericNumberOfPlayers);
            this.Controls.Add(this.checkBoxDoubleElimination);
            this.Name = "CreationBox";
            this.Text = "Creation Box";
            ((System.ComponentModel.ISupportInitialize)(this.numericNumberOfPlayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxDoubleElimination;
        private System.Windows.Forms.NumericUpDown numericNumberOfPlayers;
        private System.Windows.Forms.TextBox textBoxInputName;
        private System.Windows.Forms.Label labelInputName;
        private System.Windows.Forms.Label labelNumberOfPlayers;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.TextBox textBoxNames;
        private System.Windows.Forms.Button buttonClearNames;
    }
}