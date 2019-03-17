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
            int drop1Delay = decimal.ToInt32(drop1DelayNumeric.Value);
            int drop1Duration = decimal.ToInt32(drop1DurationNumeric.Value);
            int drop2Delay = decimal.ToInt32(drop2DelayNumeric.Value);
            int drop2Duration = decimal.ToInt32(drop2DurationNumeric.Value);
            int shutterDelay = decimal.ToInt32(shutterDelayNumeric.Value);
            int shutterDuration = decimal.ToInt32(shutterDurationNumeric.Value);
            int flashDelay = decimal.ToInt32(flashDelayNumeric.Value);
            int flashDuration = decimal.ToInt32(flashDurationNumeric.Value);

            PlanEntry[] plan =
            {
                new PlanEntry(drop1Delay, drop1Duration, "dd0", "du0"),
                new PlanEntry(drop2Delay, drop2Duration, "dd1", "du1"),
                new PlanEntry(shutterDelay, shutterDuration, "sd0", "su0"),
                new PlanEntry(flashDelay, flashDuration, "fd0", "fu0"),
            };

            button1.Enabled = false;

            try {
                int seriesSteps = 1;
                int seriesOffset = 0;
                int seriesDelay = 0;

                if (enableFlashSeriesCheckbox.Checked)
                {
                    seriesSteps = decimal.ToInt32(flashSeriesNumExecNumeric.Value);
                    seriesOffset = decimal.ToInt32(flashSeriesOffsetNumeric.Value);
                    seriesDelay = decimal.ToInt32(flashSeriesDelayNumeric.Value);
                }

                PlanSeries series = new PlanSeries(seriesSteps, seriesOffset, "fd0");

                while(series.HasNext()) { 

                    ExecutePlan(plan);

                    plan = series.UpdatePlan(plan);

                    Wait(seriesDelay);
                }

            }
            finally
            {
                button1.Enabled = true;
            }
        }

        private void ExecutePlan(PlanEntry[] plan)
        {
            foreach(PlanEntry e in plan)
            {
                portSelector.SelectedPort.Write(e.delayCommand + e.delay + "&");
                portSelector.SelectedPort.Write(e.durationCommand + e.duration + "&");

                Console.Out.WriteLine(e.delayCommand + e.delay + "&");
            }

            Console.Out.WriteLine("---------");
            portSelector.SelectedPort.Write("exe&");
        }

        private void MainControl_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
    }
}
