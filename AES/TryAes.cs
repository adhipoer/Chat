using System;

namespace aes
{

	public class TryAes
	{
		//throws di java kalo ke C# jadi apa? -> public static void main(String[] args) throws java.io.UnsupportedEncodingException

		public static void Main(string[] args)
		{

			string input = "Dalam kriptografi, Advanced Encryption Standard (AES) merupakan standar enkripsi dengan kunci-simetris yang diadopsi oleh pemerintah Amerika Serikat. Standar ini terdiri atas 3 blok cipher, yaitu AES-128, AES-192 and AES-256, yang diadopsi dari koleksi yang lebih besar yang awalnya diterbitkan sebagai Rijndael. Masing-masing cipher memiliki ukuran 128-bit, dengan ukuran kunci masing-masing 128, 192, dan 256 bit. AES telah dianalisis secara luas dan sekarang digunakan di seluruh dunia, seperti halnya dengan pendahulunya, Data Encryption Standard (DES).";
					//"Wewwwwwww...... wkwkwkwk :v";

			Console.WriteLine("AES");

			Console.WriteLine(input);
			Console.WriteLine(AesEncrypt.StringToHexString(input));
			string temp = AesEncrypt.Encrypt(input, AesEncrypt.initKey, 10);
			Console.WriteLine(temp);


			Console.WriteLine(temp);
			string temp2 = AesDecrypt.Decrypt(temp, AesDecrypt.initKey, 10);
			Console.WriteLine(temp2);
			Console.WriteLine(AesDecrypt.HexStringToString(temp2));


			Console.WriteLine("");
			Console.WriteLine("AES OFB");

			Console.WriteLine(input);
			Console.WriteLine(AesEncryptOfbMode.StringToHexString(input));
			string temp3 = AesEncryptOfbMode.Encrypt(input, AesEncryptOfbMode.initKey, 10);
			Console.WriteLine(temp3);


			Console.WriteLine(temp3);
			string temp4 = AesDecryptOfbMode.Decrypt(temp3, AesDecryptOfbMode.initKey, 10);
			Console.WriteLine(temp4);
			Console.WriteLine(AesDecryptOfbMode.HexStringToString(temp4));



		}
	}

}