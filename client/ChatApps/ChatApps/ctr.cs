using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApps
{
    public static class ctr
    {
        static byte[] dafuq = {
            (byte) 0x0f, (byte) 0x47, (byte) 0x0c, (byte) 0xaf,
            (byte) 0x15, (byte) 0xd9, (byte) 0xb7, (byte) 0x7f,
            (byte) 0x71, (byte) 0xe8, (byte) 0xad, (byte) 0x67,
            (byte) 0xc9, (byte) 0x59, (byte) 0xd6, (byte) 0x9c
        };

        static int Counter(char ab, out char abc)
        {
            Console.WriteLine(ab);
            int x = 0;
            abc = new char();
            if (ab < '9' && ab >= '0')
                abc = (char)(ab + 1);
            else if (ab == '9')
                abc = 'a';
            else if (ab < 'f' && ab >= 'a')
                abc += (char)(ab + 1);
            else if (ab == 'f')
            {
                abc = '0';
                x = 1;
            }
            Console.WriteLine(Convert.ToString(dafuq));
            return x;
        }

        static byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        static String ByteArrayToHexString(byte[] hexArray)
        {
            String hexString = "";
            foreach (byte hex in hexArray)
            {
                String temp = Convert.ToString(hex & 0xFF, 16);
                if (temp.Length == 1)
                    temp = "0" + temp;
                hexString += temp;
            }
            return hexString;
        }
        
        public static void cobaCoba(byte[] dafuc)
        {
            String baru = ByteArrayToHexString(dafuc);
            char[] baru2 = baru.ToCharArray();

            for(int x=0; x<10; x++)
            {
                int len = baru.Length;
                int y = 0, z = len - 1;
                
                char[] baru4 = baru2;

                do
                {
                    x = Counter(baru2[z], out baru4[z]);
                    Console.WriteLine(baru4[y]);
                    y--;
                }while (y == 1 && y != 1);

            }
        }
    }
}

//y = len - 1;
//do
//{
//    x = Counter(baru4[y], out baru2[y]);
//    Console.WriteLine("x:{0} y:{1}", baru4[y], baru2[y]);
//    y--;
//} while (x == 1 && y != -1);
//baru3 = new String(baru4);
//baru = new String(baru2);
//Console.WriteLine(baru);
//Console.WriteLine(baru3);