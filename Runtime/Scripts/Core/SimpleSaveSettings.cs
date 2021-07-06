using System;
using UnityEngine;

namespace SimpleSave
{
    [Serializable]
    public class SimpleSaveSettings
    {
        public string filename;
        public Path path;
        public bool compress;
        public bool encrypt;
        public string password;

        public SimpleSaveSettings(string filename)
        {
            this.filename = filename;
            path = Path.persistentDataPath;
            compress = false;
            encrypt = false;
        }

        public SimpleSaveSettings()
        {
            filename = "default.json";
            path = Path.persistentDataPath;
            compress = false;
            encrypt = false;
            password = "allyourbasebelongtous";
        }

        public enum Path
        {
            persistentDataPath,
            dataPath
        }

        public static string GetPath(Path path)
        {
            switch (path)
            {
                case Path.persistentDataPath:
                    return Application.persistentDataPath;
                case Path.dataPath:
                    return Application.dataPath;
                default:
                    return Application.persistentDataPath;
            }
        }
    }
}
