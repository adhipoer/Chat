using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApps
{
    static class ctr
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

        public static void cobaCoba()
        {
            String baru = ByteArrayToHexString(dafuq);
            char[] baru2 = baru.ToCharArray();
            int len = baru2.Length;
            for (int l = 0; l < 100; l++)
            {
                char[] baru4 = baru2;
                int x = 0, y = len - 1;
                Console.WriteLine(len);
                do
                {
                    x = Counter(baru2[y], out baru4[y]);
                    Console.WriteLine("x:{0} y:{1}", baru2[y], baru4[y]);
                    y--;
                } while (x == 1 && y != -1);

                String baru3 = new String(baru2);
                String baru5 = new String(baru4);
                Console.WriteLine(baru3);
                Console.WriteLine(baru5);
                Console.WriteLine(len);
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
        }
    }
}
