namespace StickHeroGame
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
            this.components = new System.ComponentModel.Container();
            this.StickHeroField = new System.Windows.Forms.PictureBox();
            this.StickTimer = new System.Windows.Forms.Timer(this.components);
            this.StickDropTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.StickHeroField)).BeginInit();
            this.SuspendLayout();
            // 
            // StickHeroField
            // 
            this.StickHeroField.Location = new System.Drawing.Point(600, 166);
            this.StickHeroField.Name = "StickHeroField";
            this.StickHeroField.Size = new System.Drawing.Size(58, 45);
            this.StickHeroField.TabIndex = 0;
            this.StickHeroField.TabStop = false;
            this.StickHeroField.Paint += new System.Windows.Forms.PaintEventHandler(this.StickHeroField_Paint);
            // 
            // StickTimer
            // 
            this.StickTimer.Tick += new System.EventHandler(this.StickTimer_Tick);
            // 
            // StickDropTimer
            // 
            this.StickDropTimer.Tick += new System.EventHandler(this.StickDropTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StickHeroField);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.StickHeroField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox StickHeroField;
        private System.Windows.Forms.Timer StickTimer;
        private System.Windows.Forms.Timer StickDropTimer;
    }
}

