using System;
using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    /// <summary>
    /// Класс для шифрования.
    /// </summary>
    public class Encrypt
    {
        /// <summary>
        /// Возвращает захэшированную указанным алгоритмом шифрования строку.
        /// </summary>
        /// <param name="input">Входная строка.</param>
        /// <param name="algorithm">Алгоритм шифрования.</param>
        /// <returns></returns>
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        /// <summary>
        /// Возвращает зашифрованную по SHA512 алгоритму строку.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetSHA512(string data) => ComputeHash(data, new SHA512CryptoServiceProvider());
    }
}
