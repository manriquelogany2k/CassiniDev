namespace CassiniDev.Views
{
    partial class PortCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LabelWaitForPort = new System.Windows.Forms.Label();
            this.RadioButtonPortFind = new System.Windows.Forms.RadioButton();
            this.RadioButtonPortSpecific = new System.Windows.Forms.RadioButton();
            this.PortSpecificNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PortRangeStartNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PortRangeEndNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.WaitNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PortSpecificNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PortRangeStartNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PortRangeEndNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaitNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelWaitForPort
            // 
            this.LabelWaitForPort.AutoSize = true;
            this.LabelWaitForPort.Location = new System.Drawing.Point(1, 26);
            this.LabelWaitForPort.Name = "LabelWaitForPort";
            this.LabelWaitForPort.Size = new System.Drawing.Size(32, 13);
            this.LabelWaitForPort.TabIndex = 18;
            this.LabelWaitForPort.Text = "Wait:";
            this.toolTip1.SetToolTip(this.LabelWaitForPort, "Length of time, in ms, to wait for selected port to become available");
            // 
            // RadioButtonPortFind
            // 
            this.RadioButtonPortFind.AutoSize = true;
            this.RadioButtonPortFind.Checked = true;
            this.RadioButtonPortFind.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonPortFind.Location = new System.Drawing.Point(134, 1);
            this.RadioButtonPortFind.Name = "RadioButtonPortFind";
            this.RadioButtonPortFind.Size = new System.Drawing.Size(63, 18);
            this.RadioButtonPortFind.TabIndex = 6;
            this.RadioButtonPortFind.TabStop = true;
            this.RadioButtonPortFind.Text = "Range";
            this.toolTip1.SetToolTip(this.RadioButtonPortFind, "Allocate first port available in this range.");
            this.RadioButtonPortFind.UseVisualStyleBackColor = true;
            this.RadioButtonPortFind.CheckedChanged += new System.EventHandler(this.PortModeChange);
            // 
            // RadioButtonPortSpecific
            // 
            this.RadioButtonPortSpecific.AutoSize = true;
            this.RadioButtonPortSpecific.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonPortSpecific.Location = new System.Drawing.Point(0, 1);
            this.RadioButtonPortSpecific.Name = "RadioButtonPortSpecific";
            this.RadioButtonPortSpecific.Size = new System.Drawing.Size(69, 18);
            this.RadioButtonPortSpecific.TabIndex = 6;
            this.RadioButtonPortSpecific.TabStop = true;
            this.RadioButtonPortSpecific.Text = "Specific";
            this.toolTip1.SetToolTip(this.RadioButtonPortSpecific, "Use specific port.");
            this.RadioButtonPortSpecific.UseVisualStyleBackColor = true;
            this.RadioButtonPortSpecific.CheckedChanged += new System.EventHandler(this.PortModeChange);
            // 
            // PortSpecificNumericUpDown
            // 
            this.PortSpecificNumericUpDown.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PortSpecificNumericUpDown.Location = new System.Drawing.Point(59, 0);
            this.PortSpecificNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.PortSpecificNumericUpDown.Name = "PortSpecificNumericUpDown";
            this.PortSpecificNumericUpDown.Size = new System.Drawing.Size(55, 20);
            this.PortSpecificNumericUpDown.TabIndex = 19;
            this.toolTip1.SetToolTip(this.PortSpecificNumericUpDown, "Use specific port.");
            this.PortSpecificNumericUpDown.Value = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            // 
            // PortRangeStartNumericUpDown
            // 
            this.PortRangeStartNumericUpDown.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PortRangeStartNumericUpDown.Location = new System.Drawing.Point(187, 0);
            this.PortRangeStartNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.PortRangeStartNumericUpDown.Name = "PortRangeStartNumericUpDown";
            this.PortRangeStartNumericUpDown.Size = new System.Drawing.Size(55, 20);
            this.PortRangeStartNumericUpDown.TabIndex = 20;
            this.toolTip1.SetToolTip(this.PortRangeStartNumericUpDown, "Port Range Start");
            this.PortRangeStartNumericUpDown.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // PortRangeEndNumericUpDown
            // 
            this.PortRangeEndNumericUpDown.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PortRangeEndNumericUpDown.Location = new System.Drawing.Point(248, 0);
            this.PortRangeEndNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.PortRangeEndNumericUpDown.Name = "PortRangeEndNumericUpDown";
            this.PortRangeEndNumericUpDown.Size = new System.Drawing.Size(55, 20);
            this.PortRangeEndNumericUpDown.TabIndex = 21;
            this.toolTip1.SetToolTip(this.PortRangeEndNumericUpDown, "Port Range End");
            this.PortRangeEndNumericUpDown.Value = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            // 
            // WaitNumericUpDown
            // 
            this.WaitNumericUpDown.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WaitNumericUpDown.Location = new System.Drawing.Point(33, 23);
            this.WaitNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.WaitNumericUpDown.Name = "WaitNumericUpDown";
            this.WaitNumericUpDown.Size = new System.Drawing.Size(81, 20);
            this.WaitNumericUpDown.TabIndex = 22;
            this.toolTip1.SetToolTip(this.WaitNumericUpDown, "Length of time, in ms, to wait for selected port to become available");
            this.WaitNumericUpDown.Value = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // PortCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WaitNumericUpDown);
            this.Controls.Add(this.LabelWaitForPort);
            this.Controls.Add(this.PortRangeEndNumericUpDown);
            this.Controls.Add(this.PortRangeStartNumericUpDown);
            this.Controls.Add(this.RadioButtonPortFind);
            this.Controls.Add(this.PortSpecificNumericUpDown);
            this.Controls.Add(this.RadioButtonPortSpecific);
            this.Name = "PortCtrl";
            this.Size = new System.Drawing.Size(329, 44);
            ((System.ComponentModel.ISupportInitialize)(this.PortSpecificNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PortRangeStartNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PortRangeEndNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaitNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelWaitForPort;
        private System.Windows.Forms.RadioButton RadioButtonPortFind;
        private System.Windows.Forms.RadioButton RadioButtonPortSpecific;
        private System.Windows.Forms.NumericUpDown PortSpecificNumericUpDown;
        private System.Windows.Forms.NumericUpDown PortRangeStartNumericUpDown;
        private System.Windows.Forms.NumericUpDown PortRangeEndNumericUpDown;
        private System.Windows.Forms.NumericUpDown WaitNumericUpDown;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
