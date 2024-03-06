using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Text;

namespace Logistics.Insfrastructure.Helpers.Security
{
    public class Security
    {
        protected Security() { }

        protected readonly static byte[] Key1 = { 36, 217, 35, 77, 44, 17, 39, 154, 114 };
        protected readonly static byte[] Key2 = { 41, 87, 30, 74, 100, 41, 78, 10, 2, 78, 96 };

        public static string EncryptString(string sToEncrypt)
        {
            if (sToEncrypt == string.Empty) return "";

            var CryptProvider = new RijndaelManaged();
            byte[] InputbyteArray = Encoding.UTF8.GetBytes(sToEncrypt);

            CryptProvider.Key = MD5.HashData(Key1);
            CryptProvider.IV = MD5.HashData(Key2);
            CryptProvider.Mode = CipherMode.ECB;

            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, CryptProvider.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(InputbyteArray, 0, InputbyteArray.Length);
            cryptoStream.FlushFinalBlock();

            var stringBuilder = new StringBuilder();
            byte[] b = memoryStream.ToArray();
            int I;
            for (I = 0; I <= Information.UBound(b); I++)
                stringBuilder.AppendFormat("{0:X2}", b[I]);

            return stringBuilder.ToString();
        }
        public static string DecryptString(string sToDecrypt)
        {
            if (Strings.Trim(sToDecrypt) == string.Empty) return string.Empty;

            var DES = new RijndaelManaged
            {
                Key = MD5.HashData(Key1),
                IV = MD5.HashData(Key2),
                Mode = CipherMode.ECB
            };

            byte[] inputByteArray = new byte[Convert.ToInt32(sToDecrypt.Length / (double)2 - 1 + 1)];
            int X;
            for (X = 0; X <= sToDecrypt.Length / (double)2 - 1; X++)
            {
                int IJ = Convert.ToInt32(sToDecrypt.Substring(X * 2, 2), 16);
                inputByteArray[X] = new byte();
                inputByteArray[X] = Convert.ToByte(IJ);
            }

            var memorySream = new MemoryStream();
            var cryptoStream = new CryptoStream(memorySream, DES.CreateDecryptor(), CryptoStreamMode.Write);

            cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
            cryptoStream.FlushFinalBlock();

            var stringBuilder = new StringBuilder();
            byte[] B = memorySream.ToArray();
            int I;
            for (I = 0; I <= Information.UBound(B); I++)
                stringBuilder.Append(Strings.Chr(B[I]));
            return stringBuilder.ToString();
        }
    }
}
