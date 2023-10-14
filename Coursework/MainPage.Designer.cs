namespace Coursework
{
    partial class MainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.colorButton = new System.Windows.Forms.Button();
            this.carButton = new System.Windows.Forms.Button();
            this.groupTables = new System.Windows.Forms.GroupBox();
            this.passportSeriesButton = new System.Windows.Forms.Button();
            this.ownerButton = new System.Windows.Forms.Button();
            this.markButton = new System.Windows.Forms.Button();
            this.groupForms = new System.Windows.Forms.GroupBox();
            this.form2Button = new System.Windows.Forms.Button();
            this.form1Button = new System.Windows.Forms.Button();
            this.groupTables.SuspendLayout();
            this.groupForms.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorButton
            // 
            this.colorButton.BackColor = System.Drawing.Color.White;
            this.colorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorButton.Location = new System.Drawing.Point(144, 31);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(94, 23);
            this.colorButton.TabIndex = 1;
            this.colorButton.Text = "Color";
            this.colorButton.UseVisualStyleBackColor = false;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // carButton
            // 
            this.carButton.BackColor = System.Drawing.Color.White;
            this.carButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.carButton.Location = new System.Drawing.Point(6, 31);
            this.carButton.Name = "carButton";
            this.carButton.Size = new System.Drawing.Size(94, 23);
            this.carButton.TabIndex = 0;
            this.carButton.Text = "Car";
            this.carButton.UseVisualStyleBackColor = false;
            this.carButton.Click += new System.EventHandler(this.carButton_Click);
            // 
            // groupTables
            // 
            this.groupTables.Controls.Add(this.passportSeriesButton);
            this.groupTables.Controls.Add(this.ownerButton);
            this.groupTables.Controls.Add(this.markButton);
            this.groupTables.Controls.Add(this.carButton);
            this.groupTables.Controls.Add(this.colorButton);
            this.groupTables.Location = new System.Drawing.Point(12, 12);
            this.groupTables.Name = "groupTables";
            this.groupTables.Size = new System.Drawing.Size(391, 152);
            this.groupTables.TabIndex = 2;
            this.groupTables.TabStop = false;
            this.groupTables.Text = "Таблицы";
            // 
            // passportSeriesButton
            // 
            this.passportSeriesButton.BackColor = System.Drawing.Color.White;
            this.passportSeriesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.passportSeriesButton.Location = new System.Drawing.Point(210, 90);
            this.passportSeriesButton.Name = "passportSeriesButton";
            this.passportSeriesButton.Size = new System.Drawing.Size(94, 23);
            this.passportSeriesButton.TabIndex = 4;
            this.passportSeriesButton.Text = "Passport_series";
            this.passportSeriesButton.UseVisualStyleBackColor = false;
            this.passportSeriesButton.Click += new System.EventHandler(this.passportSeriesButton_Click);
            // 
            // ownerButton
            // 
            this.ownerButton.BackColor = System.Drawing.Color.White;
            this.ownerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ownerButton.Location = new System.Drawing.Point(80, 90);
            this.ownerButton.Name = "ownerButton";
            this.ownerButton.Size = new System.Drawing.Size(94, 23);
            this.ownerButton.TabIndex = 3;
            this.ownerButton.Text = "Owner";
            this.ownerButton.UseVisualStyleBackColor = false;
            this.ownerButton.Click += new System.EventHandler(this.ownerButton_Click);
            // 
            // markButton
            // 
            this.markButton.BackColor = System.Drawing.Color.White;
            this.markButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.markButton.Location = new System.Drawing.Point(287, 31);
            this.markButton.Name = "markButton";
            this.markButton.Size = new System.Drawing.Size(94, 23);
            this.markButton.TabIndex = 2;
            this.markButton.Text = "Mark";
            this.markButton.UseVisualStyleBackColor = false;
            this.markButton.Click += new System.EventHandler(this.markButton_Click);
            // 
            // groupForms
            // 
            this.groupForms.Controls.Add(this.form2Button);
            this.groupForms.Controls.Add(this.form1Button);
            this.groupForms.Location = new System.Drawing.Point(12, 170);
            this.groupForms.Name = "groupForms";
            this.groupForms.Size = new System.Drawing.Size(391, 82);
            this.groupForms.TabIndex = 3;
            this.groupForms.TabStop = false;
            this.groupForms.Text = "Формы";
            // 
            // form2Button
            // 
            this.form2Button.BackColor = System.Drawing.Color.White;
            this.form2Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form2Button.Location = new System.Drawing.Point(210, 35);
            this.form2Button.Name = "form2Button";
            this.form2Button.Size = new System.Drawing.Size(94, 23);
            this.form2Button.TabIndex = 1;
            this.form2Button.Text = "Форма 2";
            this.form2Button.UseVisualStyleBackColor = false;
            this.form2Button.Click += new System.EventHandler(this.form2Button_Click);
            // 
            // form1Button
            // 
            this.form1Button.BackColor = System.Drawing.Color.White;
            this.form1Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form1Button.Location = new System.Drawing.Point(80, 35);
            this.form1Button.Name = "form1Button";
            this.form1Button.Size = new System.Drawing.Size(94, 23);
            this.form1Button.TabIndex = 0;
            this.form1Button.Text = "Форма 1";
            this.form1Button.UseVisualStyleBackColor = false;
            this.form1Button.Click += new System.EventHandler(this.form1Button_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(417, 273);
            this.Controls.Add(this.groupForms);
            this.Controls.Add(this.groupTables);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainPage";
            this.Text = "Главная страница";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainPage_FormClosed);
            this.groupTables.ResumeLayout(false);
            this.groupForms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Button carButton;
        private System.Windows.Forms.GroupBox groupTables;
        private System.Windows.Forms.Button passportSeriesButton;
        private System.Windows.Forms.Button ownerButton;
        private System.Windows.Forms.Button markButton;
        private System.Windows.Forms.GroupBox groupForms;
        private System.Windows.Forms.Button form2Button;
        private System.Windows.Forms.Button form1Button;
    }
}

