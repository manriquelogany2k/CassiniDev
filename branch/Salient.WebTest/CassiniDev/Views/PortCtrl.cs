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
    public partial class PortCtrl : UserControl
    {
        ///<summary>
        ///</summary>
        public PortCtrl()
        {
            InitializeComponent();
        }

        ///<summary>
        ///</summary>
        public ushort Port
        {
            get { return (ushort) PortSpecificNumericUpDown.Value; }
            set { PortSpecificNumericUpDown.Value = value; }
        }


        ///<summary>
        ///</summary>
        public ushort PortRangeStart
        {
            get { return (ushort) PortRangeStartNumericUpDown.Value; }
            set { PortRangeStartNumericUpDown.Value = value; }
        }


        ///<summary>
        ///</summary>
        public ushort PortRangeEnd
        {
            get { return (ushort) PortRangeEndNumericUpDown.Value; }
            set { PortRangeEndNumericUpDown.Value = value; }
        }


        ///<summary>
        ///</summary>
        public PortMode PortMode
        {
            get { return RadioButtonPortSpecific.Checked ? PortMode.Specific : PortMode.FirstAvailable; }
            set
            {
                switch (value)
                {
                    case PortMode.FirstAvailable:
                        RadioButtonPortFind.Checked = true;
                        break;
                    case PortMode.Specific:
                        RadioButtonPortSpecific.Checked = true;
                        break;
                }
            }
        }

        ///<summary>
        ///</summary>
        public int WaitForPort
        {
            get
            {
                return (int) WaitNumericUpDown.Value ;
            }   
            set
            {
                WaitNumericUpDown.Value = value;
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
                case ErrorField.Port:
                    errorProvider1.SetError(PortSpecificNumericUpDown, message);
                    break;
                case ErrorField.PortRange:
                    errorProvider1.SetError(PortRangeEndNumericUpDown, message);
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

        private void PortModeChange(object sender, EventArgs e)
        {
            if (RadioButtonPortSpecific.Checked)
            {
                PortRangeEndNumericUpDown.Enabled = false;
                PortRangeStartNumericUpDown.Enabled = false;
                PortSpecificNumericUpDown.Enabled = true;
            }
            else
            {
                PortRangeEndNumericUpDown.Enabled = true;
                PortRangeStartNumericUpDown.Enabled = true;
                PortSpecificNumericUpDown.Enabled = false;
            }
        }
    }
}