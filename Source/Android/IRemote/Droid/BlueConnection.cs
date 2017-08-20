using System;
using System.Threading.Tasks;
using IRemote.Droid;
using Xamarin.Forms;
using System.Collections.Generic;
using Android.Bluetooth;
using IRemote;
using System.Threading;

[assembly: Dependency(typeof(BlueConnection))]
namespace IRemote.Droid
{

	public class BlueConnection : IBlueConnection
	{
		private BluetoothAdapter adapter;
		private MakeToast toster;

		private BluetoothDevice device;
		private List<BluetoothDevice> bondedDevices;

		public void SendIR(IRSignal signal)
		{
			try
			{
				char[] charsToSend = ("S" + signal.ToString()).ToCharArray();
				byte[] toSend = new byte[charsToSend.Length];
				for (int i = 0; i < charsToSend.Length; i++)
				{
					toSend[i] = Convert.ToByte(charsToSend[i]);
				}
				socket.OutputStream.Write(toSend, 0, toSend.Length);
			}
			catch (Exception)
			{
				toster.ShowMessage("Failed to send, check your connction", true);
			}
		}

		public bool AnyBluetooth
		{
			get;
		}
		private BluetoothSocket socket;

		public BlueConnection()
		{
			toster = new MakeToast();
			adapter = BluetoothAdapter.DefaultAdapter;
			if (adapter == null)
			{
				toster.ShowMessage("You haven't bluetooth on your device", true);
				AnyBluetooth = false;
			}
			else
			{
				AnyBluetooth = true;
			}

		}

		public List<string> BoundedDevicesNames
		{
			get
			{
				List<string> devNames = new List<string>();
				bondedDevices = new List<BluetoothDevice>(adapter.BondedDevices);

				foreach (BluetoothDevice dev in bondedDevices)
				{
					devNames.Add(dev.Name);
				}
				return devNames;
			}
		}
		public bool IsBluetoothOn
		{
			get { return adapter.IsEnabled; }
		}
		public int DeviceToWorkSetByNumber
		{
			set { device = bondedDevices[value]; }
		}
		async public Task<bool> ConnectToSelectedDevice()
		{
			bool result = await Task.Run(() =>
			{
				try
				{
					if (socket != null)
					{
						socket.Close();
						socket = null;
					}
					adapter.StartDiscovery();
					device.SetPairingConfirmation(false);
					device.SetPairingConfirmation(true);
					device.CreateBond();
					adapter.CancelDiscovery();
					socket = device.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString(
						"00001101-0000-1000-8000-00805f9b34fb"));
					socket.Connect();

				}
				catch (Exception) { return false; }
				return true;
			});
			return result;
		}
		public void Disconnect()
		{
			if (socket != null)
			{
				socket.Close();
				socket = null;
			}
		}
		public bool IsConnected
		{
			get
			{
				if (socket != null && socket.IsConnected) { return true; }
				return false;
			}

		}
		public string ConnectedDeviceName
		{
			get
			{
				if (socket != null && socket.IsConnected) return $"Connected to {device.Name}";
				else return "Not connected";
			}
		}
		public async Task<IRSignal> ReciveIR()
		{

			IRSignal output = new IRSignal();
			output.Ok = false;


			try
			{
				socket.OutputStream.WriteByte(Convert.ToByte('R'));
				byte[] buffer = new byte[23];
				socket.InputStream.Flush();

				int readen = 0;
				while (readen < 23)
				{
					await socket.InputStream.ReadAsync(buffer, readen, 1);
					readen++;

				}
				char[] charedBuffer = new char[buffer.Length];
				for (int i = 0; i < buffer.Length; i++)
				{
					charedBuffer[i] = (char)buffer[i];
				}
				string recived = new string(charedBuffer);
				if (recived[0] == 'T')
				{
					(new MakeToast()).ShowMessage("Timeout", true);
				}
				string[] splited = recived.Split('@');
				output.Length = int.Parse(splited[0]);
				output.Code = uint.Parse(splited[1]);
				output.ArduinoIRemoteType = int.Parse(splited[2]);
				output.Adress = int.Parse(splited[3]);
				output.Ok = true;
			}
			catch (Exception a)
			{

			}
			socket.InputStream.Flush();
			return output;
		}
		public void CalancellReciving()
		{
			if (AnyBluetooth && IsBluetoothOn && IsConnected)
			{
				try
				{
					byte[] toSend = new byte[] { (byte)'C' };
					socket.OutputStream.Write(toSend, 0, 1);
				}
				catch
				{
					toster.ShowMessage("Please, restart the app", true);
				}
			}
		}
	}
}

