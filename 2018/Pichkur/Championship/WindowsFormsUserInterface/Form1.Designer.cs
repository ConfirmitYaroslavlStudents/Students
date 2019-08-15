namespace WindowsFormsUserInterface
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartButton = new System.Windows.Forms.Button();
            this.MessagesBox = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TeamListBox = new System.Windows.Forms.RichTextBox();
            this.NextRoundButton = new System.Windows.Forms.Button();
            this.Single = new System.Windows.Forms.RadioButton();
            this.Double = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 71);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(112, 27);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // MessagesBox
            // 
            this.MessagesBox.Location = new System.Drawing.Point(201, 12);
            this.MessagesBox.Name = "MessagesBox";
            this.MessagesBox.ReadOnly = true;
            this.MessagesBox.Size = new System.Drawing.Size(188, 42);
            this.MessagesBox.TabIndex = 2;
            this.MessagesBox.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(202, 71);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(187, 20);
            this.textBox1.TabIndex = 3;
            // 
            // TeamListBox
            // 
            this.TeamListBox.Location = new System.Drawing.Point(442, 11);
            this.TeamListBox.Name = "TeamListBox";
            this.TeamListBox.ReadOnly = true;
            this.TeamListBox.Size = new System.Drawing.Size(112, 140);
            this.TeamListBox.TabIndex = 4;
            this.TeamListBox.Text = "";
            // 
            // NextRoundButton
            // 
            this.NextRoundButton.Location = new System.Drawing.Point(12, 104);
            this.NextRoundButton.Name = "NextRoundButton";
            this.NextRoundButton.Size = new System.Drawing.Size(112, 34);
            this.NextRoundButton.TabIndex = 5;
            this.NextRoundButton.Text = "NextRound";
            this.NextRoundButton.UseVisualStyleBackColor = true;
            this.NextRoundButton.Click += new System.EventHandler(this.NextRoundButton_Click);
            // 
            // Single
            // 
            this.Single.AutoSize = true;
            this.Single.Checked = true;
            this.Single.Location = new System.Drawing.Point(12, 12);
            this.Single.Name = "Single";
            this.Single.Size = new System.Drawing.Size(104, 17);
            this.Single.TabIndex = 6;
            this.Single.TabStop = true;
            this.Single.Text = "SingleElimination";
            this.Single.UseVisualStyleBackColor = true;
            // 
            // Double
            // 
            this.Double.AutoSize = true;
            this.Double.Location = new System.Drawing.Point(12, 37);
            this.Double.Name = "Double";
            this.Double.Size = new System.Drawing.Size(112, 17);
            this.Double.TabIndex = 7;
            this.Double.Text = "Double Elimination";
            this.Double.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Double);
            this.Controls.Add(this.Single);
            this.Controls.Add(this.NextRoundButton);
            this.Controls.Add(this.TeamListBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.MessagesBox);
            this.Controls.Add(this.StartButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.RichTextBox MessagesBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox TeamListBox;
        private System.Windows.Forms.Button NextRoundButton;
        private System.Windows.Forms.RadioButton Single;
        private System.Windows.Forms.RadioButton Double;
    }
}

