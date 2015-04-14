﻿using System;
using System.Collections.Generic;

namespace aes
{

	// Big integer di C# apa?  di java tuh math.BigInteger
	
	public class AesEncrypt
	{
		internal static sbyte[] initKey = new sbyte[] {(sbyte) 0x0F, (sbyte) 0x47, (sbyte) 0x0C, unchecked((sbyte) 0xAF), (sbyte) 0x15, unchecked((sbyte) 0xD9), unchecked((sbyte) 0xB7), (sbyte) 0x7F, (sbyte) 0x71, unchecked((sbyte) 0xE8), unchecked((sbyte) 0xAD), (sbyte) 0x67, unchecked((sbyte) 0xC9), (sbyte) 0x59, unchecked((sbyte) 0xD6), unchecked((sbyte) 0x98)};

		internal static List<sbyte[]> keyList = new List<sbyte[]>();

		internal static sbyte[] nounce = new sbyte[] {(sbyte) 0x00, (sbyte) 0x11, (sbyte) 0x22, (sbyte) 0x33, (sbyte) 0x44, (sbyte) 0x55, (sbyte) 0x66, (sbyte) 0x77, unchecked((sbyte) 0x88), unchecked((sbyte) 0x99), unchecked((sbyte) 0xAA), unchecked((sbyte) 0xBB), unchecked((sbyte) 0xCC), unchecked((sbyte) 0xDD), unchecked((sbyte) 0xEE), unchecked((sbyte) 0xFF)};

		internal static readonly char[] s_box = new char[] {0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76, 0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0, 0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15, 0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75, 0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84, 0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF, 0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8, 0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2, 0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73, 0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB, 0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79, 0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08, 0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A, 0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E, 0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF, 0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16};

		internal static readonly char[] rcon = new char[] {0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d};

		internal static sbyte[] KeyExpansion(sbyte[] key, int round)
		{
			sbyte[] temp = new sbyte[16];
			sbyte[] x = new sbyte[4];
			sbyte[] y = new sbyte[4];
			sbyte[] z = new sbyte[4];

			int i;
			int j = 0;
			for (i = 3; i < 16; i += 4)
			{
				x[j++] = key[(i + 4) % 16];
	//            System.out.println(Integer.toString(x[j-1] & 0xFF, 16));
			}
			for (i = 0; i < 4; i++)
			{
				y[i] = (sbyte) s_box[x[i] & 0xFF];
	//            System.out.println(Integer.toString(y[i] & 0xFF, 16));
			}
			for (i = 0; i < 4; i++)
			{
				if (i == 0)
				{
					z[i] = (sbyte)(y[i] ^ (sbyte) rcon[round - 1]);
				}
				else
				{
					z[i] = y[i];
				}
	//            System.out.println(Integer.toString(z[i] & 0xFF, 16));
			}
			j = 0;
			for (i = 0; i < 13; i += 4)
			{
				temp[i] = (sbyte)(key[i] ^ z[j++]);
	//            System.out.println(Integer.toString(temp[i] & 0xFF, 16));
			}
			for (i = 1; i < 14; i += 4)
			{
				temp[i] = (sbyte)(temp[i - 1] ^ key[i]);
	//            System.out.println(Integer.toString(temp[i] & 0xFF, 16));
			}
			for (i = 2; i < 15; i += 4)
			{
				temp[i] = (sbyte)(temp[i - 1] ^ key[i]);
	//            System.out.print(Integer.toString(temp[i] & 0xFF, 16) + " ");
	//            System.out.println(Integer.toString((byte)(key[i]) & 0xFF, 16));
			}
			for (i = 3; i < 16; i += 4)
			{
				temp[i] = (sbyte)(temp[i - 1] ^ key[i]);
	//            System.out.println(Integer.toString(temp[i] & 0xFF, 16));
			}
			return temp;
		}

		internal static sbyte[][] AddRoundKey(sbyte[][] state, sbyte[] expKey)
		{
			int i;
			int j;
			for (i = 0; i < 4; i++)
			{
				for (j = 0; j < 4; j++)
				{
					state[j][i] ^= expKey[i * 4 + j];
				}
			}
			return state;
		}

		internal static sbyte[][] Subtitution(sbyte[][] state)
		{
			int i;
			int j;
			for (i = 0; i < 4; i++)
			{
				for (j = 0; j < 4; j++)
				{
					state[i][j] = (sbyte) s_box[state[i][j] & 0xFF];
				}
			}
			return state;
		}

		internal static sbyte[][] ShiftRow(sbyte[][] state)
		{
			sbyte[] temp = new sbyte[4];
			int i;
			int j;
			for (i = 1; i < 4; i++)
			{
				for (j = 0; j < 4; j++)
				{
					temp[j] = state[i][(j + i) % 4];
				}
				for (j = 0; j < 4; j++)
				{
					state[i][j] = temp[j];
				}
			}
			return state;
		}

		internal static sbyte GFMultiplication(sbyte a, sbyte b)
		{
			sbyte result = 0;
			sbyte temp;
			while (a != 0)
			{
				if ((a & 1) != 0)
				{
					result = (sbyte)(result ^ b);
				}
				temp = unchecked((sbyte)(b & 0x80));
				b = (sbyte)(b << 1);
				if (temp != 0)
				{
					b = (sbyte)(b ^ 0x1B);
				}
				a = (sbyte)((a & 0xFF) >> 1);
			}
			return result;
		}

		internal static sbyte[][] MixColumn(sbyte[][] state)
		{
			int[] temp = new int[4];
			sbyte a = (sbyte)(0x03);
			sbyte b = (sbyte)(0x01);
			sbyte c = (sbyte)(0x01);
			sbyte d = (sbyte)(0x02);

			int i;
			int j;
			for (i = 0; i < 4; i++)
			{
				temp[0] = GFMultiplication(d, state[0][i]) ^ GFMultiplication(a, state[1][i]) ^ GFMultiplication(b, state[2][i]) ^ GFMultiplication(c, state[3][i]);
				temp[1] = GFMultiplication(c, state[0][i]) ^ GFMultiplication(d, state[1][i]) ^ GFMultiplication(a, state[2][i]) ^ GFMultiplication(b, state[3][i]);
				temp[2] = GFMultiplication(b, state[0][i]) ^ GFMultiplication(c, state[1][i]) ^ GFMultiplication(d, state[2][i]) ^ GFMultiplication(a, state[3][i]);
				temp[3] = GFMultiplication(a, state[0][i]) ^ GFMultiplication(b, state[1][i]) ^ GFMultiplication(c, state[2][i]) ^ GFMultiplication(d, state[3][i]);
				for (j = 0; j < 4; j++)
				{
					state[j][i] = (sbyte)(temp[j]);
				}
			}
			return state;
		}

		internal static sbyte[][] ArrayToMatrix(sbyte[] array)
		{
		//dari sbyte[][] matrix = new sbyte[4][4];\ pake RectangularArrays.cs helper class reproduces the rectangular array initialization that is automatic in Java:
		
			sbyte[][] matrix = RectangularArrays.ReturnRectangularSbyteArray(4, 4);
			int i;
			int j;
			for (i = 0; i < 4; i++)
			{
				for (j = 0; j < 4; j++)
				{
					matrix[j][i] = array[i * 4 + j];
				}
			}
			return matrix;
		}

		internal static sbyte[] MatrixToArray(sbyte[][] matrix)
		{
			sbyte[] array = new sbyte[16];
			int i;
			int j;
			for (i = 0; i < 4; i++)
			{
				for (j = 0; j < 4; j++)
				{
					array[i * 4 + j] = matrix[j][i];
				}
			}
			return array;
		}

		internal static string StringToHexString(string @string)
		{
			//return String.format("%x", new BigInteger(1, string.getBytes()));
			sbyte[] temp = @string.GetBytes();
			return ByteArrayToHexString(temp);
		}

// throws di C# make apa ->static String HexStringToString(String string) throws java.io.UnsupportedEncodingException
		internal static string HexStringToString(string @string)
		{
			@string = @string.replaceAll("00", "");
			sbyte[] temp = HexStringToByteArray(@string);
			return StringHelperClass.NewString(temp, "UTF-8");
		}

		internal static sbyte[] HexStringToByteArray(string hexString)
		{
			int len = hexString.Length;
	//        System.out.println(len);
			sbyte[] data = new sbyte[len / 2];
			int i;
			for (i = 0; i < len; i += 2)
			{
				data[i / 2] = (sbyte)((char.digit(hexString[i], 16) << 4) + char.digit(hexString[i + 1], 16));
			}
			return data;
		}

		internal static string ByteArrayToHexString(sbyte[] hexArray)
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
	//            System.out.println(hexString);
			}
			return hexString;
		}

		public static string Encrypt(string cipher, sbyte[] key, int c)
		{
			int i;
			keyList.Add(key);
			for (i = 0; i < 10; i++)
			{
				keyList.Add(KeyExpansion(keyList[i], i + 2));
	//            System.out.println(ByteArrayToHexString(keyList.get(i+1)));
			}

			string temp = StringToHexString(cipher);
			string cipherText;
			string finalCipherText = "";
			sbyte[][] state;
			sbyte[] plain;
			int j;
			while (temp.Length % 32 != 0)
			{
				temp += "00";
			}

			for (i = 0; i < temp.Length; i += 32)
			{
	//            System.out.println(ByteArrayToHexString(nounce));
				state = ArrayToMatrix(HexStringToByteArray(temp.Substring(i, 32)));
				for (j = 0; j < c; j++)
				{
					if (j == 0)
					{
						state = AddRoundKey(state, keyList[j]);
					}
					else if (j == c - 1)
					{
						state = Subtitution(state);
						state = ShiftRow(state);
						state = AddRoundKey(state, keyList[j]);
					}
					else
					{
						state = Subtitution(state);
						state = ShiftRow(state);
						state = MixColumn(state);
						state = AddRoundKey(state, keyList[j]);
					}
				}
	//            nounce = MatrixToArray(state);

	//            plain = HexStringToByteArray(temp.substring(i, i+32));
	//            state = AddRoundKey(state, plain);

				cipherText = ByteArrayToHexString(MatrixToArray(state));
				finalCipherText += cipherText;
			}

			return finalCipherText;
		}

		public static void Set(sbyte[] key, sbyte[] iv)
		{
			initKey = key;
			nounce = iv;
		}

	//    public static void main(String[] args) {
	////        System.out.println(0x8C);
	////        System.out.println(Integer.toBinaryString(0x8C));
	////        System.out.println((int)s_box[0xF0 & 0xFF]);
	////        System.out.println(Integer.toBinaryString(0xAAAA));
	////        System.out.println(ByteArrayToHexString(key));
	////        key = KeyExpansion(key, 2);
	////        System.out.println(ByteArrayToHexString(key));
	////        key = KeyExpansion(key, 3);
	////        System.out.println(ByteArrayToHexString(key));
	////        key = KeyExpansion(key, 4);
	////        System.out.println(ByteArrayToHexString(key));
	////        key = KeyExpansion(key, 5);
	////        System.out.println(ByteArrayToHexString(key));
	////        key = KeyExpansion(key, 6);
	////        System.out.println(ByteArrayToHexString(key));
	////        key = KeyExpansion(key, 7);
	////        System.out.println(ByteArrayToHexString(key));
	////        key = KeyExpansion(key, 8);
	////        System.out.println(ByteArrayToHexString(key));
	////        key = KeyExpansion(key, 9);
	////        System.out.println(ByteArrayToHexString(key));
	////        key = KeyExpansion(key, 10);
	////        System.out.println(ByteArrayToHexString(key));
	//        System.out.println(StringToHexString("Wewwwwwww...... wkwkwkwk :v"));
	//        System.out.println(Encrypt("Wewwwwwww...... wkwkwkwk :v", initKey, 10));
	////        byte[] a = new byte[3];
	////        a[0]=(byte)0x99;
	////        a[1]=(byte)0x22;
	////        a[2]=GFMultiplication((byte) ((int)0x99), (byte) ((int)0x22));
	////        System.out.println(ByteArrayToHexString(a));
	//    }


	}

}