using System;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace NazmProtection
{
	public class EncryptDecryptClass
	{
		string original = "secret message";
		byte[] encrypted;
		byte[] decrypted;

		public EncryptDecryptClass()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		//static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
		public static string EncryptStringToBytes(string plainText)
		{
			byte[] encryptedData;

			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
			{
				rsa.ImportParameters(rsa.ExportParameters(false));

				encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), false);
			}

			return Convert.ToBase64String(encryptedData);
		}

		static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
		{
			string decrypted;

			// Create an Aes object with the specified key and IV.
			using (Aes aes = Aes.Create())
			{
				aes.Key = key;
				aes.IV = iv;

				// Create a new MemoryStream object to contain the decrypted bytes.
				using (MemoryStream memoryStream = new MemoryStream(cipherText))
				{
					// Create a CryptoStream object to perform the decryption.
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
					{
						// Decrypt the ciphertext.
						using (StreamReader streamReader = new StreamReader(cryptoStream))
						{
							decrypted = streamReader.ReadToEnd();
						}
					}
				}
			}

			return decrypted;
		}

		/// <summary>
		/// Encrypting the string 
		/// </summary>
		/// <param name="textToEncrypt">Regular text to encrypt</param>
		/// <returns></returns>
		public static string Encrypt(string textToEncrypt)
		{
			//string encryptedText = String.Empty;
			//byte[] bytesKey = getEncryptionKey();
			//textToEncrypt = textToEncrypt.Trim();

			//TripleDESCryptoServiceProvider engine = new TripleDESCryptoServiceProvider();
			////TripleDES engine = TripleDES.Create();
			//MemoryStream ms = new MemoryStream();
			//CryptoStream crs = new CryptoStream(ms, engine.CreateEncryptor(bytesKey, bytesKey), CryptoStreamMode.Write);

			//StreamWriter writer = new StreamWriter(crs);
			//writer.Write(textToEncrypt);
			//writer.Flush();
			//crs.FlushFinalBlock();
			//writer.Flush();
			//encryptedText = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
			//return encryptedText;

			byte[] iv = new byte[16];
			byte[] array;

			using (Aes aes = Aes.Create())
			{
				//aes.Key = Encoding.UTF8.GetBytes(key);
				aes.Key = getEncryptionKey();
				aes.IV = iv;

				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
						{
							streamWriter.Write(textToEncrypt);
						}

						array = memoryStream.ToArray();
					}
				}
			}

			return Convert.ToBase64String(array);
		}

		public static byte[] Encrypt(byte[] BytesToEncrypt)
		{
			byte[]? encryptedByte = null;
			byte[] bytesKey = getEncryptionKey();

			//TripleDESCryptoServiceProvider engine = new TripleDESCryptoServiceProvider();
			TripleDES engine = TripleDES.Create();
			engine.Padding = PaddingMode.None;
			MemoryStream ms = new MemoryStream();
			CryptoStream crs = new CryptoStream(ms, engine.CreateEncryptor(bytesKey, bytesKey), CryptoStreamMode.Write);
			crs.Write(BytesToEncrypt, 0, BytesToEncrypt.Length);
			crs.FlushFinalBlock();
			encryptedByte = ms.ToArray();
			crs.Close();
			ms.Close();
			return encryptedByte;
		}

		/// <summary>
		/// Decrypting the string
		/// </summary>
		/// <param name="textToDecrypt">Encrypted text to decrypt </param>
		/// <returns></returns>
		public static string Decrypt(string TextToDecrypt)
		{
			string decryptedText = String.Empty;
			byte[] bytesKey = getEncryptionKey();
			TextToDecrypt = TextToDecrypt.Replace(' ', '+');

			//TripleDESCryptoServiceProvider engine = new TripleDESCryptoServiceProvider();
			TripleDES engine = TripleDES.Create();
			MemoryStream ms = new MemoryStream(Convert.FromBase64String(TextToDecrypt));
			CryptoStream crs = new CryptoStream(ms, engine.CreateDecryptor(bytesKey, bytesKey), CryptoStreamMode.Read);

			StreamReader reader = new StreamReader(crs);
			decryptedText = reader.ReadToEnd();
			return decryptedText;
		}

		public static byte[] Decrypt(byte[] BytesToDecrypt)
		{
			//BytesToDecrypt Size Must be divisible by 8
			byte[]? decryptedByte = null;
			byte[] bytesKey = getEncryptionKey();

			//TripleDESCryptoServiceProvider engine = new TripleDESCryptoServiceProvider();
			TripleDES engine = TripleDES.Create();
			engine.Padding = PaddingMode.None;
			MemoryStream ms = new MemoryStream();
			CryptoStream crs = new CryptoStream(ms, engine.CreateDecryptor(bytesKey, bytesKey), CryptoStreamMode.Write);
			crs.Write(BytesToDecrypt, 0, BytesToDecrypt.Length);
			crs.FlushFinalBlock();
			decryptedByte = ms.ToArray();
			crs.Close();
			ms.Close();
			return decryptedByte;
		}

		private static byte[] getEncryptionKey()
		{
			byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes("samani#!@$(*^inamas"); //16 Byte Key
			return bytes;
		}
	}
}
