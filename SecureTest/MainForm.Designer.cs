namespace SecureRelay
{
    partial class mainForm
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
            this.sendButton = new System.Windows.Forms.Button();
            this.onRadioButton = new System.Windows.Forms.RadioButton();
            this.offRadioButton = new System.Windows.Forms.RadioButton();
            this.portLabel = new System.Windows.Forms.Label();
            this.portComboBox = new System.Windows.Forms.ComboBox();
            this.resultLabel = new System.Windows.Forms.Label();
            this.statusRadioButton = new System.Windows.Forms.RadioButton();
            this.timeOutRadioButton = new System.Windows.Forms.RadioButton();
            this.timeOutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.privateKeyRadioButton = new System.Windows.Forms.RadioButton();
            this.privateKeyNumericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.privateKeyNumericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.privateKeyNumericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.privateKeyNumericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.privateKeyNumericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.privateKeyNumericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.privateKeyNumericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.privateKeyNumericUpDown8 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.timeOutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown8)).BeginInit();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(337, 5);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(107, 36);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // onRadioButton
            // 
            this.onRadioButton.AutoSize = true;
            this.onRadioButton.Checked = true;
            this.onRadioButton.Location = new System.Drawing.Point(12, 38);
            this.onRadioButton.Name = "onRadioButton";
            this.onRadioButton.Size = new System.Drawing.Size(88, 17);
            this.onRadioButton.TabIndex = 1;
            this.onRadioButton.TabStop = true;
            this.onRadioButton.Text = "Set Relay On";
            this.onRadioButton.UseVisualStyleBackColor = true;
            this.onRadioButton.CheckedChanged += new System.EventHandler(this.onRadioButton_CheckedChanged);
            // 
            // offRadioButton
            // 
            this.offRadioButton.AutoSize = true;
            this.offRadioButton.Location = new System.Drawing.Point(127, 38);
            this.offRadioButton.Name = "offRadioButton";
            this.offRadioButton.Size = new System.Drawing.Size(88, 17);
            this.offRadioButton.TabIndex = 2;
            this.offRadioButton.Text = "Set Relay Off";
            this.offRadioButton.UseVisualStyleBackColor = true;
            this.offRadioButton.CheckedChanged += new System.EventHandler(this.offRadioButton_CheckedChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(13, 5);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(29, 13);
            this.portLabel.TabIndex = 40;
            this.portLabel.Text = "Port:";
            // 
            // portComboBox
            // 
            this.portComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureRelay.Properties.Settings.Default, "portSetting", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(68, 2);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(104, 21);
            this.portComboBox.TabIndex = 39;
            this.portComboBox.Text = global::SecureRelay.Properties.Settings.Default.portSetting;
            this.portComboBox.SelectedIndexChanged += new System.EventHandler(this.portComboBox_SelectedIndexChanged);
            // 
            // resultLabel
            // 
            this.resultLabel.Location = new System.Drawing.Point(260, 61);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(184, 17);
            this.resultLabel.TabIndex = 41;
            this.resultLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // statusRadioButton
            // 
            this.statusRadioButton.AutoSize = true;
            this.statusRadioButton.Location = new System.Drawing.Point(12, 61);
            this.statusRadioButton.Name = "statusRadioButton";
            this.statusRadioButton.Size = new System.Drawing.Size(105, 17);
            this.statusRadioButton.TabIndex = 42;
            this.statusRadioButton.Text = "Get Relay Status";
            this.statusRadioButton.UseVisualStyleBackColor = true;
            this.statusRadioButton.CheckedChanged += new System.EventHandler(this.statusRadioButton_CheckedChanged);
            // 
            // timeOutRadioButton
            // 
            this.timeOutRadioButton.AutoSize = true;
            this.timeOutRadioButton.Location = new System.Drawing.Point(12, 92);
            this.timeOutRadioButton.Name = "timeOutRadioButton";
            this.timeOutRadioButton.Size = new System.Drawing.Size(68, 17);
            this.timeOutRadioButton.TabIndex = 43;
            this.timeOutRadioButton.Text = "Time Out";
            this.timeOutRadioButton.UseVisualStyleBackColor = true;
            this.timeOutRadioButton.CheckedChanged += new System.EventHandler(this.timeOutRadioButton_CheckedChanged);
            // 
            // timeOutNumericUpDown
            // 
            this.timeOutNumericUpDown.Enabled = false;
            this.timeOutNumericUpDown.Location = new System.Drawing.Point(85, 89);
            this.timeOutNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.timeOutNumericUpDown.Name = "timeOutNumericUpDown";
            this.timeOutNumericUpDown.Size = new System.Drawing.Size(46, 20);
            this.timeOutNumericUpDown.TabIndex = 44;
            // 
            // privateKeyRadioButton
            // 
            this.privateKeyRadioButton.AutoSize = true;
            this.privateKeyRadioButton.Location = new System.Drawing.Point(12, 115);
            this.privateKeyRadioButton.Name = "privateKeyRadioButton";
            this.privateKeyRadioButton.Size = new System.Drawing.Size(119, 17);
            this.privateKeyRadioButton.TabIndex = 45;
            this.privateKeyRadioButton.Text = "Change Private Key";
            this.privateKeyRadioButton.UseVisualStyleBackColor = true;
            this.privateKeyRadioButton.CheckedChanged += new System.EventHandler(this.privateKeyRadioButton_CheckedChanged);
            // 
            // privateKeyNumericUpDown1
            // 
            this.privateKeyNumericUpDown1.Enabled = false;
            this.privateKeyNumericUpDown1.Location = new System.Drawing.Point(34, 138);
            this.privateKeyNumericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.privateKeyNumericUpDown1.Name = "privateKeyNumericUpDown1";
            this.privateKeyNumericUpDown1.Size = new System.Drawing.Size(46, 20);
            this.privateKeyNumericUpDown1.TabIndex = 46;
            this.privateKeyNumericUpDown1.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // privateKeyNumericUpDown2
            // 
            this.privateKeyNumericUpDown2.Enabled = false;
            this.privateKeyNumericUpDown2.Location = new System.Drawing.Point(86, 138);
            this.privateKeyNumericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.privateKeyNumericUpDown2.Name = "privateKeyNumericUpDown2";
            this.privateKeyNumericUpDown2.Size = new System.Drawing.Size(46, 20);
            this.privateKeyNumericUpDown2.TabIndex = 47;
            this.privateKeyNumericUpDown2.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // privateKeyNumericUpDown3
            // 
            this.privateKeyNumericUpDown3.Enabled = false;
            this.privateKeyNumericUpDown3.Location = new System.Drawing.Point(138, 138);
            this.privateKeyNumericUpDown3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.privateKeyNumericUpDown3.Name = "privateKeyNumericUpDown3";
            this.privateKeyNumericUpDown3.Size = new System.Drawing.Size(46, 20);
            this.privateKeyNumericUpDown3.TabIndex = 48;
            this.privateKeyNumericUpDown3.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // privateKeyNumericUpDown4
            // 
            this.privateKeyNumericUpDown4.Enabled = false;
            this.privateKeyNumericUpDown4.Location = new System.Drawing.Point(190, 138);
            this.privateKeyNumericUpDown4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.privateKeyNumericUpDown4.Name = "privateKeyNumericUpDown4";
            this.privateKeyNumericUpDown4.Size = new System.Drawing.Size(46, 20);
            this.privateKeyNumericUpDown4.TabIndex = 49;
            this.privateKeyNumericUpDown4.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // privateKeyNumericUpDown5
            // 
            this.privateKeyNumericUpDown5.Enabled = false;
            this.privateKeyNumericUpDown5.Location = new System.Drawing.Point(242, 137);
            this.privateKeyNumericUpDown5.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.privateKeyNumericUpDown5.Name = "privateKeyNumericUpDown5";
            this.privateKeyNumericUpDown5.Size = new System.Drawing.Size(46, 20);
            this.privateKeyNumericUpDown5.TabIndex = 50;
            this.privateKeyNumericUpDown5.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // privateKeyNumericUpDown6
            // 
            this.privateKeyNumericUpDown6.Enabled = false;
            this.privateKeyNumericUpDown6.Location = new System.Drawing.Point(294, 138);
            this.privateKeyNumericUpDown6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.privateKeyNumericUpDown6.Name = "privateKeyNumericUpDown6";
            this.privateKeyNumericUpDown6.Size = new System.Drawing.Size(46, 20);
            this.privateKeyNumericUpDown6.TabIndex = 51;
            this.privateKeyNumericUpDown6.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // privateKeyNumericUpDown7
            // 
            this.privateKeyNumericUpDown7.Enabled = false;
            this.privateKeyNumericUpDown7.Location = new System.Drawing.Point(346, 138);
            this.privateKeyNumericUpDown7.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.privateKeyNumericUpDown7.Name = "privateKeyNumericUpDown7";
            this.privateKeyNumericUpDown7.Size = new System.Drawing.Size(46, 20);
            this.privateKeyNumericUpDown7.TabIndex = 52;
            this.privateKeyNumericUpDown7.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // privateKeyNumericUpDown8
            // 
            this.privateKeyNumericUpDown8.Enabled = false;
            this.privateKeyNumericUpDown8.Location = new System.Drawing.Point(398, 137);
            this.privateKeyNumericUpDown8.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.privateKeyNumericUpDown8.Name = "privateKeyNumericUpDown8";
            this.privateKeyNumericUpDown8.Size = new System.Drawing.Size(46, 20);
            this.privateKeyNumericUpDown8.TabIndex = 53;
            this.privateKeyNumericUpDown8.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 170);
            this.Controls.Add(this.privateKeyNumericUpDown8);
            this.Controls.Add(this.privateKeyNumericUpDown7);
            this.Controls.Add(this.privateKeyNumericUpDown6);
            this.Controls.Add(this.privateKeyNumericUpDown5);
            this.Controls.Add(this.privateKeyNumericUpDown4);
            this.Controls.Add(this.privateKeyNumericUpDown3);
            this.Controls.Add(this.privateKeyNumericUpDown2);
            this.Controls.Add(this.privateKeyNumericUpDown1);
            this.Controls.Add(this.privateKeyRadioButton);
            this.Controls.Add(this.timeOutNumericUpDown);
            this.Controls.Add(this.timeOutRadioButton);
            this.Controls.Add(this.statusRadioButton);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.portComboBox);
            this.Controls.Add(this.offRadioButton);
            this.Controls.Add(this.onRadioButton);
            this.Controls.Add(this.sendButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "mainForm";
            this.Text = "Relay control";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeOutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.privateKeyNumericUpDown8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.RadioButton onRadioButton;
        private System.Windows.Forms.RadioButton offRadioButton;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.ComboBox portComboBox;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.RadioButton statusRadioButton;
        private System.Windows.Forms.RadioButton timeOutRadioButton;
        private System.Windows.Forms.NumericUpDown timeOutNumericUpDown;
        private System.Windows.Forms.RadioButton privateKeyRadioButton;
        private System.Windows.Forms.NumericUpDown privateKeyNumericUpDown1;
        private System.Windows.Forms.NumericUpDown privateKeyNumericUpDown2;
        private System.Windows.Forms.NumericUpDown privateKeyNumericUpDown3;
        private System.Windows.Forms.NumericUpDown privateKeyNumericUpDown4;
        private System.Windows.Forms.NumericUpDown privateKeyNumericUpDown5;
        private System.Windows.Forms.NumericUpDown privateKeyNumericUpDown6;
        private System.Windows.Forms.NumericUpDown privateKeyNumericUpDown7;
        private System.Windows.Forms.NumericUpDown privateKeyNumericUpDown8;
    }
}

