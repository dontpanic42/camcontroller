namespace camcontrolui
{
    partial class PortSelect
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
            this.serialPortComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.updatePortsButton = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.baudRateNumeric = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.baudRateNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPortComboBox
            // 
            this.serialPortComboBox.FormattingEnabled = true;
            this.serialPortComboBox.Location = new System.Drawing.Point(48, 10);
            this.serialPortComboBox.Name = "serialPortComboBox";
            this.serialPortComboBox.Size = new System.Drawing.Size(121, 21);
            this.serialPortComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port:";
            // 
            // updatePortsButton
            // 
            this.updatePortsButton.Location = new System.Drawing.Point(183, 10);
            this.updatePortsButton.Name = "updatePortsButton";
            this.updatePortsButton.Size = new System.Drawing.Size(72, 21);
            this.updatePortsButton.TabIndex = 2;
            this.updatePortsButton.Text = "Update";
            this.updatePortsButton.UseVisualStyleBackColor = true;
            this.updatePortsButton.Click += new System.EventHandler(this.updatePortsButton_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(180, 81);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Baud:";
            // 
            // baudRateNumeric
            // 
            this.baudRateNumeric.Location = new System.Drawing.Point(49, 43);
            this.baudRateNumeric.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.baudRateNumeric.Name = "baudRateNumeric";
            this.baudRateNumeric.Size = new System.Drawing.Size(120, 20);
            this.baudRateNumeric.TabIndex = 6;
            this.baudRateNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.baudRateNumeric.Value = new decimal(new int[] {
            9600,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(243, 2);
            this.label3.TabIndex = 7;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 82);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // PortSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 109);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.baudRateNumeric);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.updatePortsButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serialPortComboBox);
            this.MaximizeBox = false;
            this.Name = "PortSelect";
            this.Text = "PortSelect";
            this.Load += new System.EventHandler(this.PortSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.baudRateNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox serialPortComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button updatePortsButton;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown baudRateNumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cancelButton;
    }
}