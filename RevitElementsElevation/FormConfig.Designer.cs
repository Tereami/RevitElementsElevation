namespace RevitElementsElevation
{
    partial class FormConfig
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
            this.btnOk = new System.Windows.Forms.Button();
            this.txtBoxNamePrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxLevelElevation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxElevationFromLevel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBoxWallAndColumns = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTopElevParamName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBottomElevParamName = new System.Windows.Forms.TextBox();
            this.checkBoxElevIsCurrency = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(121, 357);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtBoxNamePrefix
            // 
            this.txtBoxNamePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxNamePrefix.Location = new System.Drawing.Point(15, 40);
            this.txtBoxNamePrefix.Name = "txtBoxNamePrefix";
            this.txtBoxNamePrefix.Size = new System.Drawing.Size(262, 20);
            this.txtBoxNamePrefix.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label4.Location = new System.Drawing.Point(12, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Общий параметр высоты уровня";
            // 
            // txtBoxLevelElevation
            // 
            this.txtBoxLevelElevation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxLevelElevation.Location = new System.Drawing.Point(15, 91);
            this.txtBoxLevelElevation.Name = "txtBoxLevelElevation";
            this.txtBoxLevelElevation.Size = new System.Drawing.Size(262, 20);
            this.txtBoxLevelElevation.TabIndex = 3;
            this.txtBoxLevelElevation.Text = "Рзм.ВысотаБазовогоУровня";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label5.Location = new System.Drawing.Point(12, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(202, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Общий параметр смещения от уровня";
            // 
            // txtBoxElevationFromLevel
            // 
            this.txtBoxElevationFromLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxElevationFromLevel.Location = new System.Drawing.Point(15, 140);
            this.txtBoxElevationFromLevel.Name = "txtBoxElevationFromLevel";
            this.txtBoxElevationFromLevel.Size = new System.Drawing.Size(262, 20);
            this.txtBoxElevationFromLevel.TabIndex = 3;
            this.txtBoxElevationFromLevel.Text = "Рзм.СмещениеОтУровня";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Префикс имени семейства (точка с запятой для нескольких значений)";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(202, 357);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // checkBoxWallAndColumns
            // 
            this.checkBoxWallAndColumns.AutoSize = true;
            this.checkBoxWallAndColumns.Location = new System.Drawing.Point(15, 187);
            this.checkBoxWallAndColumns.Name = "checkBoxWallAndColumns";
            this.checkBoxWallAndColumns.Size = new System.Drawing.Size(176, 17);
            this.checkBoxWallAndColumns.TabIndex = 4;
            this.checkBoxWallAndColumns.Text = "Обработать стены и колонны";
            this.checkBoxWallAndColumns.UseVisualStyleBackColor = true;
            this.checkBoxWallAndColumns.CheckedChanged += new System.EventHandler(this.checkBoxWallAndColumns_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 210);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Параметр отметки верха:";
            // 
            // textBoxTopElevParamName
            // 
            this.textBoxTopElevParamName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTopElevParamName.Location = new System.Drawing.Point(15, 228);
            this.textBoxTopElevParamName.Name = "textBoxTopElevParamName";
            this.textBoxTopElevParamName.Size = new System.Drawing.Size(262, 20);
            this.textBoxTopElevParamName.TabIndex = 6;
            this.textBoxTopElevParamName.Text = "Рзм.ОтметкаВерха";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 256);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Параметр отметки низа:";
            // 
            // textBoxBottomElevParamName
            // 
            this.textBoxBottomElevParamName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBottomElevParamName.Location = new System.Drawing.Point(15, 274);
            this.textBoxBottomElevParamName.Name = "textBoxBottomElevParamName";
            this.textBoxBottomElevParamName.Size = new System.Drawing.Size(262, 20);
            this.textBoxBottomElevParamName.TabIndex = 6;
            this.textBoxBottomElevParamName.Text = "Рзм.ОтметкаНиза";
            // 
            // checkBoxElevIsCurrency
            // 
            this.checkBoxElevIsCurrency.AutoSize = true;
            this.checkBoxElevIsCurrency.Location = new System.Drawing.Point(15, 300);
            this.checkBoxElevIsCurrency.Name = "checkBoxElevIsCurrency";
            this.checkBoxElevIsCurrency.Size = new System.Drawing.Size(210, 17);
            this.checkBoxElevIsCurrency.TabIndex = 7;
            this.checkBoxElevIsCurrency.Text = "Использовать \"Денежную единицу\"";
            this.checkBoxElevIsCurrency.UseVisualStyleBackColor = true;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 392);
            this.Controls.Add(this.checkBoxElevIsCurrency);
            this.Controls.Add(this.textBoxBottomElevParamName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxTopElevParamName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxWallAndColumns);
            this.Controls.Add(this.txtBoxElevationFromLevel);
            this.Controls.Add(this.txtBoxLevelElevation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBoxNamePrefix);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtBoxNamePrefix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxLevelElevation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxElevationFromLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;


        #endregion

        private System.Windows.Forms.CheckBox checkBoxWallAndColumns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTopElevParamName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxBottomElevParamName;
        private System.Windows.Forms.CheckBox checkBoxElevIsCurrency;
    }
}