using System;

namespace aes
{

	public class RandomNumberGenerator
	{
		public static sbyte[] Random(int x)
		{
			sbyte[] b = new sbyte[16];
			(new Random()).NextBytes(b);
			return b;
		}

		public static string StringToHexString(string @string)
		{
			return string.Format("{0:x}", new System.Numerics.BigInteger(1, @string.GetBytes()));
		}

		public static sbyte[] HexStringToByteArray(string hexString)
		{
			int len = hexString.Length;
			sbyte[] data = new sbyte[len / 2];
			int i;
			for (i = 0; i < len; i += 2)
			{
				data[i / 2] = (sbyte)((char.digit(hexString[i], 16) << 4) + char.digit(hexString[i + 1], 16));
			}
			return data;
		}

		public static string ByteArrayToHexString(sbyte[] hexArray)
		{
			string hexString = "";
			foreach (sbyte hex in hexArray)
			{
				string temp = Convert.ToString(hex & 0xFF, 16);
				if (temp.Length == 1)
				{
					temp = "0" + temp;
				}
				hexString += temp;
			}
			return hexString;
		}
	}

}