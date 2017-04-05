namespace CassiniDev.Views
{
    partial class CassiniArgsCtrl
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
            this.LabelIdleTimeOut = new System.Windows.Forms.Label();
            this.TextBoxVPath = new System.Windows.Forms.TextBox();
            this.TextBoxHostName = new System.Windows.Forms.TextBox();
            this.CheckBoxAddHostEntry = new System.Windows.Forms.CheckBox();
            this.LabelVPath = new System.Windows.Forms.Label();
            this.LabelHostName = new System.Windows.Forms.Label();
            this.LabelPort = new System.Windows.Forms.Label();
            this.LabelIPAddress = new System.Windows.Forms.Label();
            this.IdleTimeOutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ipAddressCtrl1 = new CassiniDev.Views.IPAddressCtrl();
            this.portCtrl1 = new CassiniDev.Views.PortCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.IdleTimeOutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelIdleTimeOut
            // 
            this.LabelIdleTimeOut.AutoSize = true;
            this.LabelIdleTimeOut.Location = new System.Drawing.Point(3, 86);
            this.LabelIdleTimeOut.Name = "LabelIdleTimeOut";
            this.LabelIdleTimeOut.Size = new System.Drawing.Size(78, 13);
            this.LabelIdleTimeOut.TabIndex = 41;
            this.LabelIdleTimeOut.Text = "Idle Shutdown:";
            this.toolTip1.SetToolTip(this.LabelIdleTimeOut, "Length of time, in ms, to sit idle before shutting down the server.");
            // 
            // TextBoxVPath
            // 
            this.TextBoxVPath.Location = new System.Drawing.Point(3, 22);
            this.TextBoxVPath.Name = "TextBoxVPath";
            this.TextBoxVPath.Size = new System.Drawing.Size(242, 20);
            this.TextBoxVPath.TabIndex = 30;
            this.TextBoxVPath.Text = "/";
            // 
            // TextBoxHostName
            // 
            this.TextBoxHostName.Location = new System.Drawing.Point(3, 61);
            this.TextBoxHostName.Name = "TextBoxHostName";
            this.TextBoxHostName.Size = new System.Drawing.Size(242, 20);
            this.TextBoxHostName.TabIndex = 31;
            this.TextBoxHostName.TextChanged += new System.EventHandler(this.HostNameChange);
            // 
            // CheckBoxAddHostEntry
            // 
            this.CheckBoxAddHostEntry.AutoSize = true;
            this.CheckBoxAddHostEntry.Enabled = false;
            this.CheckBoxAddHostEntry.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckBoxAddHostEntry.Location = new System.Drawing.Point(251, 61);
            this.CheckBoxAddHostEntry.Name = "CheckBoxAddHostEntry";
            this.CheckBoxAddHostEntry.Size = new System.Drawing.Size(51, 18);
            this.CheckBoxAddHostEntry.TabIndex = 33;
            this.CheckBoxAddHostEntry.Text = "Add";
            this.CheckBoxAddHostEntry.UseVisualStyleBackColor = true;
            // 
            // LabelVPath
            // 
            this.LabelVPath.AutoSize = true;
            this.LabelVPath.Location = new System.Drawing.Point(3, 5);
            this.LabelVPath.Name = "LabelVPath";
            this.LabelVPath.Size = new System.Drawing.Size(61, 13);
            this.LabelVPath.TabIndex = 32;
            this.LabelVPath.Text = "Virtual Path";
            // 
            // LabelHostName
            // 
            this.LabelHostName.AutoSize = true;
            this.LabelHostName.Location = new System.Drawing.Point(3, 45);
            this.LabelHostName.Name = "LabelHostName";
            this.LabelHostName.Size = new System.Drawing.Size(106, 13);
            this.LabelHostName.TabIndex = 34;
            this.LabelHostName.Text = "Host Name (optional)";
            // 
            // LabelPort
            // 
            this.LabelPort.AutoSize = true;
            this.LabelPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPort.Location = new System.Drawing.Point(3, 162);
            this.LabelPort.Name = "LabelPort";
            this.LabelPort.Size = new System.Drawing.Size(30, 13);
            this.LabelPort.TabIndex = 42;
            this.LabelPort.Text = "Port";
            // 
            // LabelIPAddress
            // 
            this.LabelIPAddress.AutoSize = true;
            this.LabelIPAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIPAddress.Location = new System.Drawing.Point(3, 107);
            this.LabelIPAddress.Name = "LabelIPAddress";
            this.LabelIPAddress.Size = new System.Drawing.Size(68, 13);
            this.LabelIPAddress.TabIndex = 44;
            this.LabelIPAddress.Text = "IP Address";
            // 
            // IdleTimeOutNumericUpDown
            // 
            this.IdleTimeOutNumericUpDown.Location = new System.Drawing.Point(87, 84);
            this.IdleTimeOutNumericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.IdleTimeOutNumericUpDown.Name = "IdleTimeOutNumericUpDown";
            this.IdleTimeOutNumericUpDown.Size = new System.Drawing.Size(95, 20);
            this.IdleTimeOutNumericUpDown.TabIndex = 46;
            this.toolTip1.SetToolTip(this.IdleTimeOutNumericUpDown, "Length of time, in ms, to sit idle before shutting down the server.");
            this.IdleTimeOutNumericUpDown.Value = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ipAddressCtrl1
            // 
            this.ipAddressCtrl1.IPAddress = null;
            this.ipAddressCtrl1.IPMode = CassiniDev.IPMode.Loopback;
            this.ipAddressCtrl1.IPv6 = false;
            this.ipAddressCtrl1.Location = new System.Drawing.Point(6, 123);
            this.ipAddressCtrl1.Name = "ipAddressCtrl1";
            this.ipAddressCtrl1.Size = new System.Drawing.Size(325, 41);
            this.ipAddressCtrl1.TabIndex = 45;
            // 
            // portCtrl1
            // 
            this.portCtrl1.Location = new System.Drawing.Point(3, 178);
            this.portCtrl1.Name = "portCtrl1";
            this.portCtrl1.Port = ((ushort)(65535));
            this.portCtrl1.PortMode = CassiniDev.PortMode.FirstAvailable;
            this.portCtrl1.PortRangeEnd = ((ushort)(65535));
            this.portCtrl1.PortRangeStart = ((ushort)(8080));
            this.portCtrl1.Size = new System.Drawing.Size(328, 45);
            this.portCtrl1.TabIndex = 43;
            // 
            // CassiniArgsCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextBoxVPath);
            this.Controls.Add(this.TextBoxHostName);
            this.Controls.Add(this.LabelHostName);
            this.Controls.Add(this.IdleTimeOutNumericUpDown);
            this.Controls.Add(this.ipAddressCtrl1);
            this.Controls.Add(this.LabelIPAddress);
            this.Controls.Add(this.portCtrl1);
            this.Controls.Add(this.LabelPort);
            this.Controls.Add(this.LabelIdleTimeOut);
            this.Controls.Add(this.CheckBoxAddHostEntry);
            this.Controls.Add(this.LabelVPath);
            this.Name = "CassiniArgsCtrl";
            this.Size = new System.Drawing.Size(334, 224);
            ((System.ComponentModel.ISupportInitialize)(this.IdleTimeOutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelIdleTimeOut;
        private System.Windows.Forms.TextBox TextBoxVPath;
        private System.Windows.Forms.TextBox TextBoxHostName;
        private System.Windows.Forms.CheckBox CheckBoxAddHostEntry;
        private System.Windows.Forms.Label LabelVPath;
        private System.Windows.Forms.Label LabelHostName;
        private System.Windows.Forms.Label LabelPort;
        private PortCtrl portCtrl1;
        private System.Windows.Forms.Label LabelIPAddress;
        private IPAddressCtrl ipAddressCtrl1;
        private System.Windows.Forms.NumericUpDown IdleTimeOutNumericUpDown;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
