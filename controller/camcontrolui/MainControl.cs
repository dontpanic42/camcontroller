using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace camcontrolui
{
    public partial class MainControl : Form
    {
        protected PortSelect portSelector;

        public MainControl()
        {
            InitializeComponent();
        }

        private void MainControl_Load(object sender, EventArgs e)
        {
            portSelector = new PortSelect();
            portSelector.ShowDialog();

            if(portSelector.DialogResult != DialogResult.OK)
            {
                Close();
                return;
            }

            Text = "CameraControl (" + portSelector.SelectedPortName + "@" + portSelector.SelectedBaudRate.ToString() + ")";

            portSelector.SelectedPort.DataReceived += SelectedPort_DataReceived;
        }

        private void SelectedPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (portSelector != null)
            {
                string textIn = portSelector.SelectedPort.ReadExisting();
                serialMonitorBox.Invoke((MethodInvoker)delegate
                {
                   serialMonitorBox.AppendText(textIn);
                });
            }
        }

        private void MainControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (portSelector != null && portSelector.SelectedPort != null && portSelector.SelectedPort.IsOpen)
            {
                portSelector.SelectedPort.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExecutePlan();
        }

        private void ExecutePlan()
        {
            int drop1Delay = decimal.ToInt32(drop1DelayNumeric.Value);
            int drop1Duration = decimal.ToInt32(drop1DurationNumeric.Value);
            int drop2Delay = decimal.ToInt32(drop2DelayNumeric.Value);
            int drop2Duration = decimal.ToInt32(drop2DurationNumeric.Value);
            int shutterDelay = decimal.ToInt32(shutterDelayNumeric.Value);
            int shutterDuration = decimal.ToInt32(shutterDurationNumeric.Value);
            int flashDelay = decimal.ToInt32(flashDelayNumeric.Value);
            int flashDuration = decimal.ToInt32(flashDurationNumeric.Value);
            
            portSelector.SelectedPort.Write("dd0" + drop1Delay + "&");
            portSelector.SelectedPort.Write("du0" + drop1Duration + "&");
            portSelector.SelectedPort.Write("dd1" + drop2Delay + "&");
            portSelector.SelectedPort.Write("du1" + drop2Duration + "&");
            portSelector.SelectedPort.Write("sd0" + shutterDelay + "&");
            portSelector.SelectedPort.Write("su0" + shutterDuration + "&");
            portSelector.SelectedPort.Write("fd0" + flashDelay + "&");
            portSelector.SelectedPort.Write("fu0" + flashDuration + "&");
            portSelector.SelectedPort.Write("exe&");
        }

        private void MainControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == ' ')
            {
                ExecutePlan();
            }
        }
    }
}
