using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace SimpleSave
{
    public class SimpleSaveFileHandler
    {
        public static bool Write(SimpleSaveSettings settings, string data)
        {
            try
            {
                List<Stream> streams = new List<Stream>();

                FileStream fileStream = new FileStream(GetFullPath(settings), FileMode.Create);
                streams.Add(fileStream);

                if (settings.compress)
                {
                    GZipStream gZipStream = new GZipStream(streams[streams.Count - 1], CompressionMode.Compress);
                    streams.Add(gZipStream);
                }

                if (settings.encrypt)
                {
                    CryptoStream cryptoStream = new CryptoStream(streams[streams.Count - 1], GetAESEncryptor(settings.password), CryptoStreamMode.Write);
                    streams.Add(cryptoStream);
                }

                StreamWriter streamWriter = new StreamWriter(streams[streams.Count - 1]);
                streamWriter.Write(data);
                streamWriter.Close();

                foreach (var stream in streams)
                {
                    stream.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.Log($"Failed to write to {GetFullPath(settings)} with exception {e}");
                return false;
            }
        }

        public static bool Read(SimpleSaveSettings settings, out string data)
        {
            try
            {
                List<Stream> streams = new List<Stream>();

                FileStream fileStream = new FileStream(GetFullPath(settings), FileMode.Open);
                streams.Add(fileStream);

                if (settings.compress)
                {
                    GZipStream gZipStream = new GZipStream(streams[streams.Count - 1], CompressionMode.Decompress);
                    streams.Add(gZipStream);
                }

                if (settings.encrypt)
                {
                    CryptoStream cryptoStream = new CryptoStream(streams[streams.Count - 1], GetAESDecryptor(settings.password), CryptoStreamMode.Read);
                    streams.Add(cryptoStream);
                }

                StreamReader streamReader = new StreamReader(streams[streams.Count - 1]);

                data = streamReader.ReadToEnd();

                streamReader.Close();

                foreach (var stream in streams)
                {
                    stream.Close();
                }

                return true;
            }
            catch (Exception exception)
            {
                if (exception is FileNotFoundException)
                    Debug.Log($"No File Found. Create new.");
                else
                    Debug.Log($"Failed to read from {GetFullPath(settings)} with exception {exception}");
                data = "";
                return false;
            }
        }


        public static string GetFullPath(SimpleSaveSettings settings)
        {
            return Path.Combine(SimpleSaveSettings.GetPath(settings.path), settings.filename);
        }

        public static bool DoesFileExists(SimpleSaveSettings settings)
        {
            return File.Exists(GetFullPath(settings));
            
        }

        public static ICryptoTransform GetAESEncryptor(string password)
        {
            Aes aes = Aes.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] aesKey = SHA256Managed.Create().ComputeHash(passwordBytes);
            byte[] aesIV = MD5.Create().ComputeHash(passwordBytes);

            return aes.CreateEncryptor(aesKey, aesIV);
        }

        public static ICryptoTransform GetAESDecryptor(string password)
        {
            Aes aes = Aes.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] aesKey = SHA256Managed.Create().ComputeHash(passwordBytes);
            byte[] aesIV = MD5.Create().ComputeHash(passwordBytes);

            return aes.CreateDecryptor(aesKey, aesIV);
        }


    }

}
