namespace WindowsFormsApp
{
    partial class MainMenu
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
            this.labelWelcome = new System.Windows.Forms.Label();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.buttonLoadGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWelcome.Location = new System.Drawing.Point(13, 13);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(361, 20);
            this.labelWelcome.TabIndex = 0;
            this.labelWelcome.Text = "Welcome to the Tournament Grid Generator";
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNewGame.Location = new System.Drawing.Point(40, 45);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(120, 60);
            this.buttonNewGame.TabIndex = 1;
            this.buttonNewGame.Text = "New Game";
            this.buttonNewGame.UseVisualStyleBackColor = true;
            this.buttonNewGame.Click += new System.EventHandler(this.buttonNewGame_Click);
            // 
            // buttonLoadGame
            // 
            this.buttonLoadGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLoadGame.Location = new System.Drawing.Point(220, 45);
            this.buttonLoadGame.Name = "buttonLoadGame";
            this.buttonLoadGame.Size = new System.Drawing.Size(120, 60);
            this.buttonLoadGame.TabIndex = 1;
            this.buttonLoadGame.TabStop = false;
            this.buttonLoadGame.Text = "Load Game";
            this.buttonLoadGame.UseVisualStyleBackColor = true;
            this.buttonLoadGame.Click += new System.EventHandler(this.buttonLoadGame_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 121);
            this.Controls.Add(this.buttonLoadGame);
            this.Controls.Add(this.buttonNewGame);
            this.Controls.Add(this.labelWelcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.Button buttonLoadGame;
    }
}

