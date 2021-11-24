using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecureRelay
{
    public class RelayControl : IDisposable
    {
        private readonly ManualResetEvent receiveHandler = new ManualResetEvent(false);
        private SerialPort serialPort;
        private readonly object sharedMonitor = new object();
        private int receiveTimeOut = 2000;

        public RelayControl(string portName, int receiveTimeOut = 2000)
        {
            this.serialPort = new SerialPort(portName);
            this.receiveTimeOut = receiveTimeOut;

            this.serialPort.DataReceived += this.SerialPort_DataReceived;
        }

        private string receivedData;
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = sender as SerialPort;
            var buffer = new List<int>();

            Thread.CurrentThread.Join(50); //Critical! serve some delay to fill up the buffer, do not think to remove this
            try
            {
                do
                {
                    buffer.Add(sp.ReadByte());
                } while (sp.IsOpen && sp.BytesToRead != 0);
            }
            catch (Exception)
            {
            }

            receivedData = new string(buffer.Select(i => (char)i).ToArray());
            this.receiveHandler.Set();
        }

        protected void OpenPort()
        {
            if (serialPort.IsOpen) return;
            serialPort.Open();
        }

        public string SetON(Int16[] privateKey)
        {
            this.OpenPort();
            this.receivedData = null;
            this.serialPort.Write(new byte[] { 0xFF, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, 9);
            this.receiveHandler.Reset();
            this.receiveHandler.WaitOne(this.receiveTimeOut);
            var charData = receivedData?.ToCharArray();
            this.receivedData = null;
            var result = this.ConfirmReplay(charData, privateKey);

            return RelayControl.InterpretResult(result, 1);
        }

        public string SetOFF(Int16[] privateKey)
        {
            this.OpenPort();
            this.serialPort.Write(new byte[] { 0xFF, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, 9);
            this.receiveHandler.Reset();
            this.receiveHandler.WaitOne(this.receiveTimeOut);
            var charData = receivedData?.ToCharArray();
            this.receivedData = null;
            var result = this.ConfirmReplay(charData, privateKey);
            return RelayControl.InterpretResult(result, 0);
        }

        public string GetStatus(Int16[] privateKey)
        {
            this.OpenPort();
            this.receivedData = null;
            this.serialPort.Write(new byte[] { 0xFF, 0x01, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, 9);
            this.receiveHandler.Reset();
            this.receiveHandler.WaitOne(this.receiveTimeOut);
            var charData = receivedData?.ToCharArray();
            this.receivedData = null;
            var result = this.ConfirmReplay(charData, privateKey);

            return RelayControl.InterpretResult(result, 3);
        }

        public string SetTimer(Int16[] privateKey, byte seconds)
        {
            this.OpenPort();
            this.serialPort.Write(new byte[] { 0xFF, 0xE0, seconds, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, 9);
            this.receiveHandler.Reset();
            this.receiveHandler.WaitOne(this.receiveTimeOut);
            var charData = receivedData?.ToCharArray();
            this.receivedData = null;
            var result = this.ConfirmReplay(charData, privateKey);
            return RelayControl.InterpretResult(result, (int)seconds);
        }

        public string ChangePrivateKey(Int16[] privateKey, Int16[] newPrivateKey)
        {
            this.OpenPort();
            var buffer = newPrivateKey.Select(i => (char)i).Select(c => Convert.ToByte(c));
            this.serialPort.Write(new byte[] { 0xFD }.Concat(buffer).ToArray(), 0, 9);
            this.receiveHandler.Reset();
            this.receiveHandler.WaitOne(this.receiveTimeOut * 3);
            var charData = receivedData?.ToCharArray();
            this.receivedData = null;
            var result = this.ConfirmReplay(charData, privateKey);
            return RelayControl.InterpretResult(result, 0);
        }

        #region Helpers
        private string ConfirmReplay(char[] charData, Int16[] privateKey)
        {
            if (charData == null) return null;

            var startIndex = Array.IndexOf(charData.Reverse().ToArray(), (char)255);
            startIndex = charData.Length - startIndex;
            if (startIndex < 0 || charData.Length < (startIndex + 8)) return null;

            var randomNbr = new List<Int16>();
            for (int i = startIndex; i < startIndex + 8; i++)
            {
                randomNbr.Add((Int16)charData[i]);
            }
            var secureObj = new SerialSecure(privateKey);
            var encriptNbr = secureObj.SetupEncryptedRandomNbr(randomNbr.ToArray());

            var buffer = encriptNbr.Select(i => (char)i).Select(c => Convert.ToByte(c));

            this.serialPort.Write(new byte[] { 0xFE }.Concat(buffer).ToArray(), 0, 9);

            this.receiveHandler.Reset();
            this.receiveHandler.WaitOne(2000);
            var data = receivedData;
            this.receivedData = null;

            return data;
        }

        private static string InterpretResult(string result, int command)
        {
            if (result == null) return "Err: Unknown";

            var data = result.ToCharArray();

            if (data.Length > 2)
            {
                if (data.Length > 0 && data[0] == 253)
                {
                    return "Success: new private key";
                }

                var relayCode = (Int16)data[1];
                var replayCode = (Int16)data[2];
                if (relayCode == 224)
                {
                    if (replayCode == command) return string.Format("Success: T: {0}", replayCode);
                }
                else if (command == 0 || command == 1)
                {
                    if (replayCode == command) return string.Format("Success: R: {0} S:{1}", relayCode, replayCode);
                }
                else if (replayCode == 98)
                {
                    return "Err: timeout";
                }
                else if (replayCode == 99)
                {
                    return "Err: encryption";
                }
                else
                {
                    return string.Format("Info: R: {0} S: {1}", relayCode, replayCode);
                }
            }

            return "Err: Unknown";
        }
        #endregion

        public void Dispose()
        {
            if (serialPort.IsOpen) this.serialPort.Close();
            serialPort.Dispose();
            this.serialPort = null;
        }
    }
}
