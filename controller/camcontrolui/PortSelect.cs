using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace camcontrolui
{
    public partial class PortSelect : Form
    {

        public string SelectedPortName { get; set; }
        public int SelectedBaudRate { get; set; }
        public SerialPort SelectedPort { get; set; }


        public PortSelect()
        {
            InitializeComponent();
        }

        private void PortSelect_Load(object sender, EventArgs e)
        {
            UpdatePortList();
        }

        private void UpdatePortList()
        {
            serialPortComboBox.Items.Clear();

            string[] ports = SerialPort.GetPortNames();
            foreach(string port in ports) {
                serialPortComboBox.Items.Add(port);
            }

            if(ports.Length > 0)
            {
                serialPortComboBox.Select(0, 1);
            }
        }

        private void updatePortsButton_Click(object sender, EventArgs e)
        {
            UpdatePortList();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (serialPortComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a port.", "No port selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectedPortName = serialPortComboBox.SelectedItem.ToString();
            SelectedBaudRate = decimal.ToInt32(baudRateNumeric.Value);
            SelectedPort = new SerialPort(SelectedPortName, SelectedBaudRate, Parity.None, 8, StopBits.One);

            try
            {
                SelectedPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while opening: " + ex.Message, "Error while opening port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!SelectedPort.IsOpen)
            {
                MessageBox.Show("Please select another port.", "Opening port failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
