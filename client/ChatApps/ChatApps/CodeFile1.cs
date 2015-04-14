using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    static class Program
    {
        static byte[] initKey = {
            (byte) 0x0F, (byte) 0x47, (byte) 0x0C, (byte) 0xAF,
            (byte) 0x15, (byte) 0xD9, (byte) 0xB7, (byte) 0x7F,
            (byte) 0x71, (byte) 0xE8, (byte) 0xAD, (byte) 0x67,
            (byte) 0xC9, (byte) 0x59, (byte) 0xD6, (byte) 0x98
        };

        //static BitArray[] keyList = new BitArray[17];
        static ArrayList keyList = new ArrayList();

        static byte[] nounce = {
            (byte) 0x00, (byte) 0x11, (byte) 0x22, (byte) 0x33,
            (byte) 0x44, (byte) 0x55, (byte) 0x66, (byte) 0x77,
            (byte) 0x88, (byte) 0x99, (byte) 0xAA, (byte) 0xBB,
            (byte) 0xCC, (byte) 0xDD, (byte) 0xEE, (byte) 0xFF
        };

        //sbox
        static int[] sbox = {
            0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76,
            0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0,
            0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15,
            0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75,
            0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84,
            0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF,
            0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8,
            0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2,
            0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73,
            0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB,
            0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79,
            0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08,
            0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A,
            0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E,
            0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF,
            0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16                
        };

        static int[] rcon = {
            0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 
            0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 
            0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 
            0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 
            0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 
            0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 
            0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 
            0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 
            0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 
            0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 
            0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 
            0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 
            0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 
            0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 
            0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 
            0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d
        };

        static void keyExp (byte[] key, int round)
        {
            BitArray temp = new BitArray(16);
            BitArray x = new BitArray(4);
            BitArray y = new BitArray(4);
            BitArray z = new BitArray(4);

            BitArray keys = new BitArray(key);

            int i;
            int j = 0;
            for(i=3; i<16; i+=4) {
                x[j++] = keys[(i + 4) % 16];
            }
            for(i=0; i<4; i++) {
                y[i] = (byte)sbox[x[i] & 0xFF];
            }
            for(i=0; i<4; i++) {
                if(i == 0)
                    z[i] = (Convert.ToByte(y[i]) ^ Convert.ToByte(rcon[round - 1]));
                else
                    z[i] = y[i];
            }
            j = 0;
            for(i=0; i<13; i+=4) {
                temp[i] = (byte) (key[i] ^ z[j++]);
            }
            for(i=1; i<14; i+=4) {
                temp[i] = (byte) (temp[i-1] ^ key[i]);
            }
            for(i=2; i<15; i+=4) {
                temp[i] = (byte) (temp[i-1] ^ key[i]);
            }
            for(i=3; i<16; i+=4) {
                temp[i] = (byte) (temp[i-1] ^ key[i]);
            }
            return temp;
        }

        /*static void GenerateKey()
        {
            var bits = new BitArray(initKey);
            BitArray bits2 = new BitArray(56);
            BitArray c = new BitArray(28);
            BitArray d = new BitArray(28);

            BitArray tempC = new BitArray(28);
            BitArray tempD = new BitArray(28);

            int i;
            for (i = 0; i < 56; i++)
            {
                bits2[i] = bits[pc1[i] - 1];
            }

            //CC[0] = new BitArray(28);
            //DD[0] = new BitArray(28);

            for (i = 0; i < 28; i++)
            {
                c[i] = bits2[i];
                d[i] = bits2[i + 28];
                //CC[0][i] = bits2[i];
                //DD[0][i] = bits2[i + 28];
            }
            tempC = c;
            tempD = d;

            //Console.Write(BitArrayToHexStr(c));
            //Console.Write(" ");
            //Console.WriteLine(BitArrayToHexStr(d));

            for (i = 0; i < 16; i++)
            {
                //CC[i + 1] = new BitArray(28);
                //DD[i + 1] = new BitArray(28);

                int j;
                for (j = 0; j < 28; j++)
                {
                    c[j] = tempC[(j + iter[i]) % 28];
                    d[j] = tempD[(j + iter[i]) % 28];
                    //CC[i + 1][j] = tempC[(j + iter[i]) % 28];
                    //DD[i + 1][j] = tempD[(j + iter[i]) % 28];
                }
                tempC = c;
                tempD = d;

                KK[i + 1] = new BitArray(48);

                for (j = 0; j < 48; j++)
                {
                    if (pc2[j] < 29)
                    {
                        KK[i + 1][j] = c[pc2[j] - 1];
                    }
                    else
                    {
                        KK[i + 1][j] = d[pc2[j] - 29];
                    }
                }

                //Console.Write(BitArrayToHexStr(c));
                //Console.Write(" ");
                //Console.WriteLine(BitArrayToHexStr(d));
            }

        }

        static void Process64bit(byte[] data)
        {
            BitArray temp = new BitArray(data);
            BitArray datas = new BitArray(data);
            int i;
            for (i = 0; i < 64; i++)
            {
                datas[i] = temp[ip[i] - 1];
            }
            Console.WriteLine(BitArrayToHexStr(datas));

            BitArray[] L = new BitArray[17];
            BitArray[] R = new BitArray[17];

            L[0] = new BitArray(32);
            R[0] = new BitArray(32);

            for (i = 0; i < 32; i++)
            {
                L[0][i] = datas[i];
                R[0][i] = datas[i + 32];
            }

            for (i = 0; i < 1; i++)
            {
                int j;
                BitArray E = new BitArray(48);
                for (j = 0; j < 48; j++)
                {
                    E[j] = R[i][exp[j] - 1];
                }
                Console.WriteLine(BitArrayToHexStr(E));

                for (j = 0; j < 48; j++)
                {
                    E[j] ^= KK[i + 1][j];
                }
                Console.WriteLine(BitArrayToHexStr(E));

                BitArray[] B = new BitArray[8];
                for (j = 0; j < 8; j++)
                {
                    B[j] = new BitArray(6);
                }

                for (j = 0; j < 8; j++)
                {
                    int k;
                    for (k = 0; k < 6; k++)
                    {
                        B[j][k] = E[k + j * 6];
                    }

                }

                BitArray[] BB = new BitArray[8];
                for (j = 0; j < 8; j++)
                {
                    BB[j] = new BitArray(4);
                }

                for (j = 0; j < 8; j++)
                {
                    int x = 0;
                    int y = 0;
                    if (B[j][0] == true)
                        x += 2;
                    if (B[j][5] == true)
                        x += 1;
                    int k;
                    for (k = 1; k < 5; k++)
                    {
                        if (B[j][k] == true)
                            y += (int)Math.Pow(2, 4 - k);
                    }
                    byte[] tempt = BitConverter.GetBytes(sbox[j][y + x * 16]);
                    BitArray temptt = new BitArray(tempt);
                    BitArray temptb = new BitArray(4);
                    for (k = 0; k < 4; k++)
                    {
                        temptb[k] = temptt[k];
                    }
                    BB[j] = new BitArray(temptb);

                    Console.Write(j + " ");
                    //Console.Write(sbox[j][y + x * 16] + " ");
                    //Console.Write(BB[j].Length);
                    Console.WriteLine(BitArrayToHexStr(BB[j]));
                }
            }
        }

        static byte[] ToByteArray(this BitArray bits)
        {
            int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }

            return bytes;
        }

        static String BitArrayToStr(BitArray ba)
        {
            byte[] strArr = new byte[ba.Length / 8];

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            for (int i = 0; i < ba.Length / 8; i++)
            {
                for (int index = i * 8, m = 1; index < i * 8 + 8; index++, m *= 2)
                {
                    strArr[i] += ba.Get(index) ? (byte)m : (byte)0;
                }
            }

            return encoding.GetString(strArr);
        }

        static String BitArrayToHexStr(BitArray ba)
        {
            String hex = "";
            int len = ba.Length;
            int i;
            int temp;
            for (i = 0; i < len; i += 4)
            {
                temp = 0;
                if (ba[i] == true)
                {
                    temp += 1;
                }
                if (ba[i + 1] == true)
                {
                    temp += 2;
                }
                if (ba[i + 2] == true)
                {
                    temp += 4;
                }
                if (ba[i + 3] == true)
                {
                    temp += 8;
                }

                if (temp < 10)
                {
                    hex += temp.ToString();
                }
                else
                {
                    if (temp == 10)
                        hex += "A";
                    else if (temp == 11)
                        hex += "B";
                    else if (temp == 12)
                        hex += "C";
                    else if (temp == 13)
                        hex += "D";
                    else if (temp == 14)
                        hex += "E";
                    else
                        hex += "F";
                }
            }

            return hex;
        }

        /*static void Main(string[] args)
        {
            Console.WriteLine(BitArrayToHexStr(new BitArray(initKey)));
            GenerateKey();
            int i;
            for (i = 1; i < 17; i++)
            {
                Console.WriteLine(BitArrayToHexStr(KK[i]));
            }
            Process64bit(moc);
            //string s = "Hello World";
            //byte[] bytes = Encoding.ASCII.GetBytes(s);
            //BitArray b = new BitArray(bytes);
            //string s2 = BitArrayToStr(b);
            //Console.WriteLine(s2);

            //Console.WriteLine(Math.Pow(3, 2));
            Console.ReadKey();
        }*/
    }
}