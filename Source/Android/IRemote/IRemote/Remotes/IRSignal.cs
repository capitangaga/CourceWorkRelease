using System;
namespace IRemote
{
	public class IRSignal
	{
		public IRSignal() : this(0, 32, 3, 0) { }

		public IRSignal(uint code, int type) : this(code, 32, type, 0) { }

		public IRSignal(uint code, int len, int type) : this(code, len, type, 0) { }

		public IRSignal(uint code, int len, int type, int adress)
		{
			Code = code;
			Length = len;
			ArduinoIRemoteType = type;
			Adress = adress;
		}

		public uint Code { get; set; }

		public int Length { get; set; }

		public int Adress { get; set; }

		public int ArduinoIRemoteType { get; set; }

		public override string ToString()
		{
			return $"{Length.ToString("D3")}@{Code.ToString("D10")}" +
				$"@{ArduinoIRemoteType.ToString("D2")}@{Adress.ToString("D5")}";
		}
		public bool Ok { get; set; }
	}
}
