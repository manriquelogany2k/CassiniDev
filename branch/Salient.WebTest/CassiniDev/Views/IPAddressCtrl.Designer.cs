namespace CassiniDev.Views
{
    partial class IPAddressCtrl
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
            this.CheckBoxIPV6 = new System.Windows.Forms.CheckBox();
            this.IPSpecificTextBox = new System.Windows.Forms.TextBox();
            this.RadioButtonIPSpecific = new System.Windows.Forms.RadioButton();
            this.RadioButtonIPAny = new System.Windows.Forms.RadioButton();
            this.RadioButtonIPLoopBack = new System.Windows.Forms.RadioButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // CheckBoxIPV6
            // 
            this.CheckBoxIPV6.AutoSize = true;
            this.CheckBoxIPV6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckBoxIPV6.Location = new System.Drawing.Point(3, 18);
            this.CheckBoxIPV6.Name = "CheckBoxIPV6";
            this.CheckBoxIPV6.Size = new System.Drawing.Size(54, 18);
            this.CheckBoxIPV6.TabIndex = 8;
            this.CheckBoxIPV6.Text = "IPv6";
            this.toolTip1.SetToolTip(this.CheckBoxIPV6, "Use IPv6 address.");
            this.CheckBoxIPV6.UseVisualStyleBackColor = true;
            // 
            // IPSpecificTextBox
            // 
            this.IPSpecificTextBox.Enabled = false;
            this.IPSpecificTextBox.Location = new System.Drawing.Point(168, 0);
            this.IPSpecificTextBox.Name = "IPSpecificTextBox";
            this.IPSpecificTextBox.Size = new System.Drawing.Size(83, 20);
            this.IPSpecificTextBox.TabIndex = 7;
            this.toolTip1.SetToolTip(this.IPSpecificTextBox, "Use specific address.");
            // 
            // RadioButtonIPSpecific
            // 
            this.RadioButtonIPSpecific.AutoSize = true;
            this.RadioButtonIPSpecific.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonIPSpecific.Location = new System.Drawing.Point(110, 1);
            this.RadioButtonIPSpecific.Name = "RadioButtonIPSpecific";
            this.RadioButtonIPSpecific.Size = new System.Drawing.Size(69, 18);
            this.RadioButtonIPSpecific.TabIndex = 5;
            this.RadioButtonIPSpecific.Text = "Specific";
            this.toolTip1.SetToolTip(this.RadioButtonIPSpecific, "Use specific address.");
            this.RadioButtonIPSpecific.UseVisualStyleBackColor = true;
            this.RadioButtonIPSpecific.CheckedChanged += new System.EventHandler(this.IPModeChange);
            // 
            // RadioButtonIPAny
            // 
            this.RadioButtonIPAny.AutoSize = true;
            this.RadioButtonIPAny.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonIPAny.Location = new System.Drawing.Point(71, 1);
            this.RadioButtonIPAny.Name = "RadioButtonIPAny";
            this.RadioButtonIPAny.Size = new System.Drawing.Size(49, 18);
            this.RadioButtonIPAny.TabIndex = 5;
            this.RadioButtonIPAny.Text = "Any";
            this.toolTip1.SetToolTip(this.RadioButtonIPAny, "Use any  address 0.0.0.0");
            this.RadioButtonIPAny.UseVisualStyleBackColor = true;
            this.RadioButtonIPAny.CheckedChanged += new System.EventHandler(this.IPModeChange);
            // 
            // RadioButtonIPLoopBack
            // 
            this.RadioButtonIPLoopBack.AutoSize = true;
            this.RadioButtonIPLoopBack.Checked = true;
            this.RadioButtonIPLoopBack.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonIPLoopBack.Location = new System.Drawing.Point(3, 1);
            this.RadioButtonIPLoopBack.Name = "RadioButtonIPLoopBack";
            this.RadioButtonIPLoopBack.Size = new System.Drawing.Size(79, 18);
            this.RadioButtonIPLoopBack.TabIndex = 5;
            this.RadioButtonIPLoopBack.TabStop = true;
            this.RadioButtonIPLoopBack.Text = "Loopback";
            this.toolTip1.SetToolTip(this.RadioButtonIPLoopBack, "Use loopback address 127.0.0.1");
            this.RadioButtonIPLoopBack.UseVisualStyleBackColor = true;
            this.RadioButtonIPLoopBack.CheckedChanged += new System.EventHandler(this.IPModeChange);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // IPAddressCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.IPSpecificTextBox);
            this.Controls.Add(this.RadioButtonIPSpecific);
            this.Controls.Add(this.CheckBoxIPV6);
            this.Controls.Add(this.RadioButtonIPAny);
            this.Controls.Add(this.RadioButtonIPLoopBack);
            this.Name = "IPAddressCtrl";
            this.Size = new System.Drawing.Size(273, 40);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CheckBoxIPV6;
        private System.Windows.Forms.TextBox IPSpecificTextBox;
        private System.Windows.Forms.RadioButton RadioButtonIPSpecific;
        private System.Windows.Forms.RadioButton RadioButtonIPAny;
        private System.Windows.Forms.RadioButton RadioButtonIPLoopBack;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
