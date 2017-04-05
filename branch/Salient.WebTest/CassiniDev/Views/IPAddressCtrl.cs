// /* **********************************************************************************
//  *
//  * Copyright (c) Sky Sanders. All rights reserved.
//  * 
//  * This source code is subject to terms and conditions of the Microsoft Public
//  * License (Ms-PL). A copy of the license can be found in the license.htm file
//  * included in this distribution.
//  *
//  * You must not remove this notice, or any other, from this software.
//  *
//  * **********************************************************************************/
using System;
using System.Windows.Forms;

namespace CassiniDev.Views
{
    ///<summary>
    ///</summary>
    public partial class IPAddressCtrl : UserControl
    {
        private string _ipAddress;

        ///<summary>
        ///</summary>
        public IPAddressCtrl()
        {
            InitializeComponent();
        }

        ///<summary>
        ///</summary>
        public string IPAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }


        ///<summary>
        ///</summary>
        public bool IPv6
        {
            get { return CheckBoxIPV6.Checked; }
            set { CheckBoxIPV6.Checked = value; }
        }

        ///<summary>
        ///</summary>
        public IPMode IPMode
        {
            get
            {
                if (RadioButtonIPLoopBack.Checked)
                {
                    return IPMode.Loopback;
                }
                if (RadioButtonIPSpecific.Checked)
                {
                    return IPMode.Specific;
                }
                if (RadioButtonIPAny.Checked)
                {
                    return IPMode.Any;
                }
                throw new Exception("Invalid control state: IPMode");
            }
            set
            {
                switch (value)
                {
                    case IPMode.Any:
                        RadioButtonIPAny.Checked = true;
                        break;
                    case IPMode.Loopback:
                        RadioButtonIPLoopBack.Checked = true;
                        break;
                    case IPMode.Specific:
                        RadioButtonIPLoopBack.Checked = true;
                        break;
                    default:
                        throw new Exception("Invalid control state: IPMode");
                }
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="field"></param>
        ///<param name="message"></param>
        public void SetError(ErrorField field, string message)
        {
            switch (field)
            {
                case ErrorField.IPAddress:
                    errorProvider1.SetError(IPSpecificTextBox, message);
                    break;
            }
        }

        ///<summary>
        /// Clear validation errors
        ///</summary>
        public void ClearError()
        {
            errorProvider1.Clear();
        }


        private void IPModeChange(object sender, EventArgs e)
        {
            if (RadioButtonIPLoopBack.Checked)
            {
                IPSpecificTextBox.Enabled = false;
                CheckBoxIPV6.Enabled = true;
            }
            if (RadioButtonIPSpecific.Checked)
            {
                IPSpecificTextBox.Enabled = true;
                CheckBoxIPV6.Enabled = false;
                CheckBoxIPV6.Checked = false;
            }
            if (RadioButtonIPAny.Checked)
            {
                IPSpecificTextBox.Enabled = false;
                CheckBoxIPV6.Enabled = true;
            }
        }
    }
}