using Microsoft.Extensions.Logging;
using NGEL.Data.Settings;
using NPOI.XWPF.Usermodel;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GMServer.Services
{
    public class CryptoService
    {
        private readonly ILogger<CryptoService> _logger;
        private readonly CommonSettings _commonSettings;
        public CryptoService(ILogger<CryptoService> logger, CommonSettings commonSettings)
        {
            _logger = logger;
            _commonSettings = commonSettings;
        }
        public string Encrypt(string plainText)
        {
            if (plainText == null || plainText.Length <= 0)
            {
                _logger.LogInformation(new ArgumentNullException("plainText is emtpy").Message);
                return plainText ?? "";
            }

            if (_commonSettings.cryptoKey == null || _commonSettings.cryptoKey.Length <= 0)
            {
                _logger.LogInformation(new ArgumentNullException("Key is emtpy").Message);
                return plainText ?? "";
            }

            if (_commonSettings.cryptoIV == null || _commonSettings.cryptoIV.Length <= 0)
            {
                _logger.LogInformation(new ArgumentNullException("IV is emtpy").Message);
                return plainText ?? "";
            }

            try
            {
                byte[] encrypted;
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(_commonSettings.cryptoKey);
                    aes.IV = Encoding.UTF8.GetBytes(_commonSettings.cryptoIV);

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
                //string cipherText = ConvertBinToHex(encrypted);
                string cipherText = Convert.ToBase64String(encrypted); ;
                return cipherText;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                _logger.LogInformation(e.StackTrace);
                return plainText;
            }
        }

        public string Decrypt(string cipherText)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                _logger.LogInformation(new ArgumentNullException("cipherText is empty").Message);
                return cipherText ?? "";
            }

            if (_commonSettings.cryptoKey == null || _commonSettings.cryptoKey.Length <= 0)
            {
                _logger.LogInformation(new ArgumentNullException("Key is emtpy").Message);
                return cipherText ?? "";
            }

            if (_commonSettings.cryptoIV == null || _commonSettings.cryptoIV.Length <= 0)
            {
                _logger.LogInformation(new ArgumentNullException("IV is emtpy").Message);
                return cipherText ?? "";
            }

            try
            {
                string plainText = string.Empty;
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(_commonSettings.cryptoKey);
                    aes.IV = Encoding.UTF8.GetBytes(_commonSettings.cryptoIV);

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    //byte[] encrypted = ConvertHexToBin(cipherText);
                    byte[] encrypted = Convert.FromBase64String(cipherText);
                    using (MemoryStream msDecrypt = new MemoryStream(encrypted))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                plainText = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                return plainText;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                _logger.LogInformation(e.StackTrace);
                return cipherText;
            }
        }

        public string ConvertBinToHex(byte[] binary)
        {
            string convertArrString = string.Empty;
            convertArrString = string.Concat(Array.ConvertAll(binary, byt => byt.ToString("X2")));
            return convertArrString;
        }

        public byte[] ConvertHexToBin(string hex)
        {
            byte[] binary = new byte[hex.Length / 2];
            for (int i = 0; i < binary.Length; i++)
            {
                binary[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return binary;
        }

    }
}
