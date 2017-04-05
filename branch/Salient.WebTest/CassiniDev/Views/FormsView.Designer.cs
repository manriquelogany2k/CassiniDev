namespace CassiniDev.Views
{
    partial class FormsView 
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormsView));
            this.LinkLabelRootUrl = new System.Windows.Forms.LinkLabel();
            this.TextBoxAppPath = new System.Windows.Forms.TextBox();
            this.TextBoxVPath = new System.Windows.Forms.TextBox();
            this.TextBoxHostName = new System.Windows.Forms.TextBox();
            this.LabelPhysicalPath = new System.Windows.Forms.Label();
            this.CheckBoxAddHostEntry = new System.Windows.Forms.CheckBox();
            this.LabelVPath = new System.Windows.Forms.Label();
            this.ButtonBrowsePhysicalPath = new System.Windows.Forms.Button();
            this.LabelHostName = new System.Windows.Forms.Label();
            this.GroupBoxPort = new System.Windows.Forms.GroupBox();
            this.TextBoxPortRangeEnd = new System.Windows.Forms.TextBox();
            this.TextBoxPortRangeStart = new System.Windows.Forms.TextBox();
            this.TextBoxPort = new System.Windows.Forms.TextBox();
            this.LabelPortRangeSeperator = new System.Windows.Forms.Label();
            this.RadioButtonPortFind = new System.Windows.Forms.RadioButton();
            this.RadioButtonPortSpecific = new System.Windows.Forms.RadioButton();
            this.GroupBoxIPAddress = new System.Windows.Forms.GroupBox();
            this.CheckBoxIPV6 = new System.Windows.Forms.CheckBox();
            this.TextBoxIPSpecific = new System.Windows.Forms.TextBox();
            this.RadioButtonIPSpecific = new System.Windows.Forms.RadioButton();
            this.RadioButtonIPAny = new System.Windows.Forms.RadioButton();
            this.RadioButtonIPLoopBack = new System.Windows.Forms.RadioButton();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.TextBoxWaitForPort = new System.Windows.Forms.TextBox();
            this.LabelWaitForPort = new System.Windows.Forms.Label();
            this.TextBoxIdleTimeOut = new System.Windows.Forms.TextBox();
            this.LabelIdleTimeOut = new System.Windows.Forms.Label();
            this.GroupBoxPort.SuspendLayout();
            this.GroupBoxIPAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // LinkLabelRootUrl
            // 
            this.LinkLabelRootUrl.AutoSize = true;
            this.LinkLabelRootUrl.Location = new System.Drawing.Point(9, 293);
            this.LinkLabelRootUrl.Name = "LinkLabelRootUrl";
            this.LinkLabelRootUrl.Size = new System.Drawing.Size(189, 13);
            this.LinkLabelRootUrl.TabIndex = 24;
            this.LinkLabelRootUrl.TabStop = true;
            this.LinkLabelRootUrl.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXX";
            this.LinkLabelRootUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelRootUrl_LinkClicked);
            // 
            // TextBoxAppPath
            // 
            this.TextBoxAppPath.Location = new System.Drawing.Point(12, 26);
            this.TextBoxAppPath.Name = "TextBoxAppPath";
            this.TextBoxAppPath.Size = new System.Drawing.Size(252, 20);
            this.TextBoxAppPath.TabIndex = 0;
            // 
            // TextBoxVPath
            // 
            this.TextBoxVPath.Location = new System.Drawing.Point(12, 65);
            this.TextBoxVPath.Name = "TextBoxVPath";
            this.TextBoxVPath.Size = new System.Drawing.Size(268, 20);
            this.TextBoxVPath.TabIndex = 2;
            this.TextBoxVPath.Text = "/";
            // 
            // TextBoxHostName
            // 
            this.TextBoxHostName.Location = new System.Drawing.Point(12, 104);
            this.TextBoxHostName.Name = "TextBoxHostName";
            this.TextBoxHostName.Size = new System.Drawing.Size(180, 20);
            this.TextBoxHostName.TabIndex = 3;
            this.TextBoxHostName.TextChanged += new System.EventHandler(this.TextBoxHostName_TextChanged);
            // 
            // LabelPhysicalPath
            // 
            this.LabelPhysicalPath.AutoSize = true;
            this.LabelPhysicalPath.Location = new System.Drawing.Point(12, 9);
            this.LabelPhysicalPath.Name = "LabelPhysicalPath";
            this.LabelPhysicalPath.Size = new System.Drawing.Size(71, 13);
            this.LabelPhysicalPath.TabIndex = 1;
            this.LabelPhysicalPath.Text = "Physical Path";
            // 
            // CheckBoxAddHostEntry
            // 
            this.CheckBoxAddHostEntry.AutoSize = true;
            this.CheckBoxAddHostEntry.Enabled = false;
            this.CheckBoxAddHostEntry.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckBoxAddHostEntry.Location = new System.Drawing.Point(198, 104);
            this.CheckBoxAddHostEntry.Name = "CheckBoxAddHostEntry";
            this.CheckBoxAddHostEntry.Size = new System.Drawing.Size(105, 18);
            this.CheckBoxAddHostEntry.TabIndex = 4;
            this.CheckBoxAddHostEntry.Text = "Add hosts entry";
            this.toolTip1.SetToolTip(this.CheckBoxAddHostEntry, "Host Name will be added to the hosts file while this server \r\nis running and remo" +
                    "ved when it is stopped.\r\nRequires elevated process.");
            this.CheckBoxAddHostEntry.UseVisualStyleBackColor = true;
            // 
            // LabelVPath
            // 
            this.LabelVPath.AutoSize = true;
            this.LabelVPath.Location = new System.Drawing.Point(12, 48);
            this.LabelVPath.Name = "LabelVPath";
            this.LabelVPath.Size = new System.Drawing.Size(61, 13);
            this.LabelVPath.TabIndex = 3;
            this.LabelVPath.Text = "Virtual Path";
            // 
            // ButtonBrowsePhysicalPath
            // 
            this.ButtonBrowsePhysicalPath.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonBrowsePhysicalPath.Location = new System.Drawing.Point(270, 24);
            this.ButtonBrowsePhysicalPath.Name = "ButtonBrowsePhysicalPath";
            this.ButtonBrowsePhysicalPath.Size = new System.Drawing.Size(27, 23);
            this.ButtonBrowsePhysicalPath.TabIndex = 1;
            this.ButtonBrowsePhysicalPath.Text = "...";
            this.ButtonBrowsePhysicalPath.UseVisualStyleBackColor = true;
            this.ButtonBrowsePhysicalPath.Click += new System.EventHandler(this.ButtonBrowsePhysicalPath_Click);
            // 
            // LabelHostName
            // 
            this.LabelHostName.AutoSize = true;
            this.LabelHostName.Location = new System.Drawing.Point(12, 88);
            this.LabelHostName.Name = "LabelHostName";
            this.LabelHostName.Size = new System.Drawing.Size(106, 13);
            this.LabelHostName.TabIndex = 5;
            this.LabelHostName.Text = "Host Name (optional)";
            // 
            // GroupBoxPort
            // 
            this.GroupBoxPort.Controls.Add(this.LabelWaitForPort);
            this.GroupBoxPort.Controls.Add(this.TextBoxWaitForPort);
            this.GroupBoxPort.Controls.Add(this.TextBoxPortRangeEnd);
            this.GroupBoxPort.Controls.Add(this.TextBoxPortRangeStart);
            this.GroupBoxPort.Controls.Add(this.TextBoxPort);
            this.GroupBoxPort.Controls.Add(this.LabelPortRangeSeperator);
            this.GroupBoxPort.Controls.Add(this.RadioButtonPortFind);
            this.GroupBoxPort.Controls.Add(this.RadioButtonPortSpecific);
            this.GroupBoxPort.Location = new System.Drawing.Point(12, 216);
            this.GroupBoxPort.Name = "GroupBoxPort";
            this.GroupBoxPort.Size = new System.Drawing.Size(291, 65);
            this.GroupBoxPort.TabIndex = 9;
            this.GroupBoxPort.TabStop = false;
            this.GroupBoxPort.Text = "Port";
            // 
            // TextBoxPortRangeEnd
            // 
            this.TextBoxPortRangeEnd.Location = new System.Drawing.Point(237, 12);
            this.TextBoxPortRangeEnd.Name = "TextBoxPortRangeEnd";
            this.TextBoxPortRangeEnd.Size = new System.Drawing.Size(37, 20);
            this.TextBoxPortRangeEnd.TabIndex = 15;
            this.TextBoxPortRangeEnd.Text = "65534";
            // 
            // TextBoxPortRangeStart
            // 
            this.TextBoxPortRangeStart.Location = new System.Drawing.Point(178, 12);
            this.TextBoxPortRangeStart.Name = "TextBoxPortRangeStart";
            this.TextBoxPortRangeStart.Size = new System.Drawing.Size(36, 20);
            this.TextBoxPortRangeStart.TabIndex = 14;
            this.TextBoxPortRangeStart.Text = "8080";
            // 
            // TextBoxPort
            // 
            this.TextBoxPort.Enabled = false;
            this.TextBoxPort.Location = new System.Drawing.Point(63, 12);
            this.TextBoxPort.Name = "TextBoxPort";
            this.TextBoxPort.Size = new System.Drawing.Size(43, 20);
            this.TextBoxPort.TabIndex = 7;
            this.TextBoxPort.Text = "8080";
            // 
            // LabelPortRangeSeperator
            // 
            this.LabelPortRangeSeperator.AutoSize = true;
            this.LabelPortRangeSeperator.Location = new System.Drawing.Point(214, 15);
            this.LabelPortRangeSeperator.Name = "LabelPortRangeSeperator";
            this.LabelPortRangeSeperator.Size = new System.Drawing.Size(22, 13);
            this.LabelPortRangeSeperator.TabIndex = 16;
            this.LabelPortRangeSeperator.Text = "<->";
            // 
            // RadioButtonPortFind
            // 
            this.RadioButtonPortFind.AutoSize = true;
            this.RadioButtonPortFind.Checked = true;
            this.RadioButtonPortFind.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonPortFind.Location = new System.Drawing.Point(126, 13);
            this.RadioButtonPortFind.Name = "RadioButtonPortFind";
            this.RadioButtonPortFind.Size = new System.Drawing.Size(63, 18);
            this.RadioButtonPortFind.TabIndex = 6;
            this.RadioButtonPortFind.TabStop = true;
            this.RadioButtonPortFind.Text = "Range";
            this.RadioButtonPortFind.UseVisualStyleBackColor = true;
            this.RadioButtonPortFind.CheckedChanged += new System.EventHandler(this.RadioButtonPortFind_CheckedChanged);
            // 
            // RadioButtonPortSpecific
            // 
            this.RadioButtonPortSpecific.AutoSize = true;
            this.RadioButtonPortSpecific.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonPortSpecific.Location = new System.Drawing.Point(6, 13);
            this.RadioButtonPortSpecific.Name = "RadioButtonPortSpecific";
            this.RadioButtonPortSpecific.Size = new System.Drawing.Size(69, 18);
            this.RadioButtonPortSpecific.TabIndex = 6;
            this.RadioButtonPortSpecific.TabStop = true;
            this.RadioButtonPortSpecific.Text = "Specific";
            this.RadioButtonPortSpecific.UseVisualStyleBackColor = true;
            this.RadioButtonPortSpecific.CheckedChanged += new System.EventHandler(this.RadioButtonPortSpecific_CheckedChanged);
            // 
            // GroupBoxIPAddress
            // 
            this.GroupBoxIPAddress.Controls.Add(this.CheckBoxIPV6);
            this.GroupBoxIPAddress.Controls.Add(this.TextBoxIPSpecific);
            this.GroupBoxIPAddress.Controls.Add(this.RadioButtonIPSpecific);
            this.GroupBoxIPAddress.Controls.Add(this.RadioButtonIPAny);
            this.GroupBoxIPAddress.Controls.Add(this.RadioButtonIPLoopBack);
            this.GroupBoxIPAddress.Location = new System.Drawing.Point(12, 157);
            this.GroupBoxIPAddress.Name = "GroupBoxIPAddress";
            this.GroupBoxIPAddress.Size = new System.Drawing.Size(291, 58);
            this.GroupBoxIPAddress.TabIndex = 8;
            this.GroupBoxIPAddress.TabStop = false;
            this.GroupBoxIPAddress.Text = "IP Address";
            // 
            // CheckBoxIPV6
            // 
            this.CheckBoxIPV6.AutoSize = true;
            this.CheckBoxIPV6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckBoxIPV6.Location = new System.Drawing.Point(6, 35);
            this.CheckBoxIPV6.Name = "CheckBoxIPV6";
            this.CheckBoxIPV6.Size = new System.Drawing.Size(77, 18);
            this.CheckBoxIPV6.TabIndex = 8;
            this.CheckBoxIPV6.Text = "Use IPV6";
            this.CheckBoxIPV6.UseVisualStyleBackColor = true;
            // 
            // TextBoxIPSpecific
            // 
            this.TextBoxIPSpecific.Enabled = false;
            this.TextBoxIPSpecific.Location = new System.Drawing.Point(191, 14);
            this.TextBoxIPSpecific.Name = "TextBoxIPSpecific";
            this.TextBoxIPSpecific.Size = new System.Drawing.Size(83, 20);
            this.TextBoxIPSpecific.TabIndex = 7;
            // 
            // RadioButtonIPSpecific
            // 
            this.RadioButtonIPSpecific.AutoSize = true;
            this.RadioButtonIPSpecific.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonIPSpecific.Location = new System.Drawing.Point(131, 15);
            this.RadioButtonIPSpecific.Name = "RadioButtonIPSpecific";
            this.RadioButtonIPSpecific.Size = new System.Drawing.Size(69, 18);
            this.RadioButtonIPSpecific.TabIndex = 5;
            this.RadioButtonIPSpecific.Text = "Specific";
            this.RadioButtonIPSpecific.UseVisualStyleBackColor = true;
            this.RadioButtonIPSpecific.CheckedChanged += new System.EventHandler(this.RadioButtonIPSpecific_CheckedChanged);
            // 
            // RadioButtonIPAny
            // 
            this.RadioButtonIPAny.AutoSize = true;
            this.RadioButtonIPAny.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonIPAny.Location = new System.Drawing.Point(83, 15);
            this.RadioButtonIPAny.Name = "RadioButtonIPAny";
            this.RadioButtonIPAny.Size = new System.Drawing.Size(49, 18);
            this.RadioButtonIPAny.TabIndex = 5;
            this.RadioButtonIPAny.Text = "Any";
            this.RadioButtonIPAny.UseVisualStyleBackColor = true;
            this.RadioButtonIPAny.CheckedChanged += new System.EventHandler(this.RadioButtonIPAny_CheckedChanged);
            // 
            // RadioButtonIPLoopBack
            // 
            this.RadioButtonIPLoopBack.AutoSize = true;
            this.RadioButtonIPLoopBack.Checked = true;
            this.RadioButtonIPLoopBack.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonIPLoopBack.Location = new System.Drawing.Point(6, 15);
            this.RadioButtonIPLoopBack.Name = "RadioButtonIPLoopBack";
            this.RadioButtonIPLoopBack.Size = new System.Drawing.Size(79, 18);
            this.RadioButtonIPLoopBack.TabIndex = 5;
            this.RadioButtonIPLoopBack.TabStop = true;
            this.RadioButtonIPLoopBack.Text = "Loopback";
            this.RadioButtonIPLoopBack.UseVisualStyleBackColor = true;
            this.RadioButtonIPLoopBack.CheckedChanged += new System.EventHandler(this.RadioButtonIPLoopBack_CheckedChanged);
            // 
            // ButtonStart
            // 
            this.ButtonStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonStart.Location = new System.Drawing.Point(143, 326);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(75, 23);
            this.ButtonStart.TabIndex = 22;
            this.ButtonStart.Text = "Start";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // ButtonStop
            // 
            this.ButtonStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonStop.Enabled = false;
            this.ButtonStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonStop.Location = new System.Drawing.Point(229, 326);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(75, 23);
            this.ButtonStop.TabIndex = 21;
            this.ButtonStop.Text = "Stop";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // TextBoxWaitForPort
            // 
            this.TextBoxWaitForPort.Location = new System.Drawing.Point(80, 39);
            this.TextBoxWaitForPort.Name = "TextBoxWaitForPort";
            this.TextBoxWaitForPort.Size = new System.Drawing.Size(71, 20);
            this.TextBoxWaitForPort.TabIndex = 17;
            // 
            // LabelWaitForPort
            // 
            this.LabelWaitForPort.AutoSize = true;
            this.LabelWaitForPort.Location = new System.Drawing.Point(6, 42);
            this.LabelWaitForPort.Name = "LabelWaitForPort";
            this.LabelWaitForPort.Size = new System.Drawing.Size(68, 13);
            this.LabelWaitForPort.TabIndex = 18;
            this.LabelWaitForPort.Text = "Wait for port:";
            // 
            // TextBoxIdleTimeOut
            // 
            this.TextBoxIdleTimeOut.Location = new System.Drawing.Point(92, 130);
            this.TextBoxIdleTimeOut.Name = "TextBoxIdleTimeOut";
            this.TextBoxIdleTimeOut.Size = new System.Drawing.Size(83, 20);
            this.TextBoxIdleTimeOut.TabIndex = 25;
            // 
            // LabelIdleTimeOut
            // 
            this.LabelIdleTimeOut.AutoSize = true;
            this.LabelIdleTimeOut.Location = new System.Drawing.Point(12, 133);
            this.LabelIdleTimeOut.Name = "LabelIdleTimeOut";
            this.LabelIdleTimeOut.Size = new System.Drawing.Size(73, 13);
            this.LabelIdleTimeOut.TabIndex = 26;
            this.LabelIdleTimeOut.Text = "Idle Time Out:";
            // 
            // FormsView
            // 
            this.AcceptButton = this.ButtonStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonStop;
            this.ClientSize = new System.Drawing.Size(312, 364);
            this.Controls.Add(this.LabelIdleTimeOut);
            this.Controls.Add(this.TextBoxIdleTimeOut);
            this.Controls.Add(this.TextBoxAppPath);
            this.Controls.Add(this.LinkLabelRootUrl);
            this.Controls.Add(this.TextBoxVPath);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(this.TextBoxHostName);
            this.Controls.Add(this.ButtonStop);
            this.Controls.Add(this.LabelPhysicalPath);
            this.Controls.Add(this.CheckBoxAddHostEntry);
            this.Controls.Add(this.LabelVPath);
            this.Controls.Add(this.GroupBoxIPAddress);
            this.Controls.Add(this.ButtonBrowsePhysicalPath);
            this.Controls.Add(this.GroupBoxPort);
            this.Controls.Add(this.LabelHostName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormsView";
            this.Text = "Cassini Developer Edition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.View_FormClosing);
            this.GroupBoxPort.ResumeLayout(false);
            this.GroupBoxPort.PerformLayout();
            this.GroupBoxIPAddress.ResumeLayout(false);
            this.GroupBoxIPAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel LinkLabelRootUrl;
        private System.Windows.Forms.TextBox TextBoxAppPath;
        private System.Windows.Forms.TextBox TextBoxVPath;
        private System.Windows.Forms.TextBox TextBoxHostName;
        private System.Windows.Forms.Label LabelPhysicalPath;
        private System.Windows.Forms.CheckBox CheckBoxAddHostEntry;
        private System.Windows.Forms.Label LabelVPath;
        private System.Windows.Forms.Button ButtonBrowsePhysicalPath;
        private System.Windows.Forms.Label LabelHostName;
        private System.Windows.Forms.GroupBox GroupBoxPort;
        private System.Windows.Forms.TextBox TextBoxPortRangeEnd;
        private System.Windows.Forms.TextBox TextBoxPortRangeStart;
        private System.Windows.Forms.TextBox TextBoxPort;
        private System.Windows.Forms.Label LabelPortRangeSeperator;
        private System.Windows.Forms.RadioButton RadioButtonPortFind;
        private System.Windows.Forms.RadioButton RadioButtonPortSpecific;
        private System.Windows.Forms.GroupBox GroupBoxIPAddress;
        private System.Windows.Forms.TextBox TextBoxIPSpecific;
        private System.Windows.Forms.RadioButton RadioButtonIPSpecific;
        private System.Windows.Forms.RadioButton RadioButtonIPAny;
        private System.Windows.Forms.RadioButton RadioButtonIPLoopBack;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.CheckBox CheckBoxIPV6;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label LabelWaitForPort;
        private System.Windows.Forms.TextBox TextBoxWaitForPort;
        private System.Windows.Forms.Label LabelIdleTimeOut;
        private System.Windows.Forms.TextBox TextBoxIdleTimeOut;
    }
}