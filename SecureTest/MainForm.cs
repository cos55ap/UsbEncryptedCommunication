using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SecureRelay
{
    public partial class mainForm : Form
    {
        private Int16[] privateKey = { 255, 255, 255, 255, 255, 255, 255, 255 };

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            portComboBox.Items.Clear();
            foreach (string com in SerialPort.GetPortNames())
            {
                portComboBox.Items.Add(com);
            }

            this.privateKeyNumericUpDown1.Value = privateKey[0];
            this.privateKeyNumericUpDown2.Value = privateKey[1];
            this.privateKeyNumericUpDown3.Value = privateKey[2];
            this.privateKeyNumericUpDown4.Value = privateKey[3];
            this.privateKeyNumericUpDown5.Value = privateKey[4];
            this.privateKeyNumericUpDown6.Value = privateKey[5];
            this.privateKeyNumericUpDown7.Value = privateKey[6];
            this.privateKeyNumericUpDown8.Value = privateKey[7];
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            var port = this.portComboBox.Text;
            resultLabel.Text = string.Empty;

            using (var relayCtr = new RelayControl(port))
            {
                if (this.onRadioButton.Checked)
                {
                    resultLabel.Text = relayCtr.SetON(privateKey);
                }
                else if (this.offRadioButton.Checked)
                {
                    resultLabel.Text = relayCtr.SetOFF(privateKey);
                }
                else if (this.statusRadioButton.Checked)
                {
                    resultLabel.Text = relayCtr.GetStatus(privateKey);
                }
                else if (this.timeOutRadioButton.Checked)
                {
                    resultLabel.Text = relayCtr.SetTimer(privateKey, (byte)this.timeOutNumericUpDown.Value);
                }
                else if (this.privateKeyRadioButton.Checked)
                {
                    var result = MessageBox.Show("If operation succeed, the board will work only with new private key. Loosing private key the board will not be useful anymore." + 
                                                    Environment.NewLine + "Do you continue?", "Change private key is critical",  MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                    {
                        resultLabel.Text = "Canceled";
                        return;
                    }


                    var newKey = new List<Int16>();
                    newKey.Add((Int16)this.privateKeyNumericUpDown1.Value);
                    newKey.Add((Int16)this.privateKeyNumericUpDown2.Value);
                    newKey.Add((Int16)this.privateKeyNumericUpDown3.Value);
                    newKey.Add((Int16)this.privateKeyNumericUpDown4.Value);
                    newKey.Add((Int16)this.privateKeyNumericUpDown5.Value);
                    newKey.Add((Int16)this.privateKeyNumericUpDown6.Value);
                    newKey.Add((Int16)this.privateKeyNumericUpDown7.Value);
                    newKey.Add((Int16)this.privateKeyNumericUpDown8.Value);
                    resultLabel.Text = relayCtr.ChangePrivateKey(privateKey, newKey.ToArray());
                    if (resultLabel.Text.StartsWith("Success"))
                    {
                        privateKey[0] = (Int16)this.privateKeyNumericUpDown1.Value;
                        privateKey[1] = (Int16)this.privateKeyNumericUpDown2.Value;
                        privateKey[2] = (Int16)this.privateKeyNumericUpDown3.Value;
                        privateKey[3] = (Int16)this.privateKeyNumericUpDown4.Value;
                        privateKey[4] = (Int16)this.privateKeyNumericUpDown5.Value;
                        privateKey[5] = (Int16)this.privateKeyNumericUpDown6.Value;
                        privateKey[6] = (Int16)this.privateKeyNumericUpDown7.Value;
                        privateKey[7] = (Int16)this.privateKeyNumericUpDown8.Value;
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        private void portComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void timeOutRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.SetNumericTimeoutEnabled(this.timeOutRadioButton.Name);
            this.SetPrivateKeyEnabled(this.timeOutRadioButton.Name);
        }

        private void statusRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.SetNumericTimeoutEnabled(this.statusRadioButton.Name);
            this.SetPrivateKeyEnabled(this.statusRadioButton.Name);
        }

        private void offRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.SetNumericTimeoutEnabled(this.offRadioButton.Name);
            this.SetPrivateKeyEnabled(this.offRadioButton.Name);
        }

        private void onRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.SetNumericTimeoutEnabled(this.onRadioButton.Name);
            this.SetPrivateKeyEnabled(this.onRadioButton.Name);
        }

        private void privateKeyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.SetNumericTimeoutEnabled(this.privateKeyRadioButton.Name);
            this.SetPrivateKeyEnabled(this.privateKeyRadioButton.Name);
        }

        #region  Helpers
        private void SetNumericTimeoutEnabled(string controlName)
        {
            this.timeOutNumericUpDown.Enabled = controlName == this.timeOutRadioButton.Name;
        }

        private void SetPrivateKeyEnabled(string controlName)
        {
            var enabled = controlName == this.privateKeyRadioButton.Name;
            this.privateKeyNumericUpDown1.Enabled = enabled;
            this.privateKeyNumericUpDown2.Enabled = enabled;
            this.privateKeyNumericUpDown3.Enabled = enabled;
            this.privateKeyNumericUpDown4.Enabled = enabled;
            this.privateKeyNumericUpDown5.Enabled = enabled;
            this.privateKeyNumericUpDown6.Enabled = enabled;
            this.privateKeyNumericUpDown7.Enabled = enabled;
            this.privateKeyNumericUpDown8.Enabled = enabled;
        }
        #endregion
    }
}
