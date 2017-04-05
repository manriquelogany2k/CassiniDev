using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CassiniDev.Views
{
    ///<summary>
    ///</summary>
    public partial class CassiniArgsCtrl : UserControl,IView
    {
        ///<summary>
        ///</summary>
        public CassiniArgsCtrl()
        {
            InitializeComponent();
        }


        public string VirtualPath
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        ///<summary>
        ///</summary>
        public string HostName
        {
            get { return TextBoxHostName.Text; }
            set { TextBoxHostName.Text = value; }
        }



        ///<summary>
        ///</summary>
        public bool AddHost
        {
            get { return CheckBoxAddHostEntry.Checked; }
            set { CheckBoxAddHostEntry.Checked = value; }
        }


        public IPresenter Presenter
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        ///<summary>
        ///</summary>
        public int TimeOut
        {
            get { return (int)IdleTimeOutNumericUpDown.Value; }
            set { IdleTimeOutNumericUpDown.Value = value; }
        }


        ///<summary>
        ///</summary>
        public string IPAddress
        {
            get { return ipAddressCtrl1.IPAddress; }
            set { ipAddressCtrl1.IPAddress = value; }
        }


        ///<summary>
        ///</summary>
        public bool IPv6
        {
            get { return ipAddressCtrl1.IPv6; }
            set { ipAddressCtrl1.IPv6 = value; }
        }

        ///<summary>
        ///</summary>
        public IPMode IPMode
        {
            get{return ipAddressCtrl1.IPMode;}
            set{ipAddressCtrl1.IPMode = value;}
        }

        public RunState RunState
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void ClearError()
        {
            throw new NotImplementedException();
        }

        public void SetError(ErrorField field, string value)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        /// 
        ///</summary>
        public void Start()
        {
            /*noop*/
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            /*noop*/
        }

        ///<summary>
        ///</summary>
        public ushort Port
        {
            get { return portCtrl1.Port; }
            set { portCtrl1.Port = value; }
        }


        ///<summary>
        ///</summary>
        public ushort PortRangeStart
        {
            get { return portCtrl1.PortRangeStart; }
            set { portCtrl1.PortRangeStart = value; }
        }


        ///<summary>
        ///</summary>
        public ushort PortRangeEnd
        {
            get { return portCtrl1.PortRangeEnd; }
            set { portCtrl1.PortRangeEnd = value; }
        }

        public string RootUrl
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        ///<summary>
        ///</summary>
        public PortMode PortMode
        {
            get { return portCtrl1.PortMode; }
            set { portCtrl1.PortMode = value; }
        }

        ///<summary>
        ///</summary>
        public int WaitForPort
        {
            get{return portCtrl1.WaitForPort;}
            set{portCtrl1.WaitForPort = value;}
        }

        public string ApplicationPath
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        private void HostNameChange(object sender, EventArgs e)
        {
            if (HostName == null || string.IsNullOrEmpty(HostName.Trim()))
            {
                AddHost = false;
                CheckBoxAddHostEntry.Enabled = false;
            }
            else
            {
                CheckBoxAddHostEntry.Enabled = true;
            }
        }
    }
}
