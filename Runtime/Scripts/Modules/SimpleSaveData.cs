using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSave
{
    public class SimpleSaveData
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        string key = "key";
        public SimpleSaveSettings settings = new SimpleSaveSettings();
        public void Add<T>(string key, T value)
        {
            data[key] = value;
        }

        public bool TryToGet<T>(string key, out object value)
        {
            if (data.TryGetValue(key, out value))
            {
                value = SSave.ConvertObject<T>(value);
                return true;
            }
            return false;
        }


        public void Get<T>(string key, out T value)
        {
            if (data.TryGetValue(key, out object valueA))
            {
                value = SSave.ConvertObject<T>(valueA);
                return;
            }
            throw new Exception("key: " + key + " not found.");

        }

        public T Get<T>(string key)
        {
            if (data.TryGetValue(key, out object value))
            {
                return SSave.ConvertObject<T>(value);
            }
            throw new Exception("key: " + key + " not found.");
        }

        public void WriteToDisk()
        {
            Debug.Log(settings.filename);
            SSave.Save(key, data, settings);
        }
        public void ReadFromDisk()
        {
            data = SSave.Load<Dictionary<string, object>>(key, settings);
        }

        public Dictionary<string, object> GetData()
        {
            return data;
        }

        public void SetData(object data)
        {
            this.data = (Dictionary<string, object>)data;
        }

        public static SimpleSaveData GetFromDisk(string key, SimpleSaveSettings settings)
        {
            var ssd = new SimpleSaveData(key, settings);
            ssd.ReadFromDisk();
            return ssd;

        }


        public SimpleSaveData() { }
        public SimpleSaveData(string key)
        {
            this.key = key;
        }
        public SimpleSaveData(string key, bool readFromDisk)
        {
            this.key = key;
            if (readFromDisk) ReadFromDisk();
        }
        public SimpleSaveData(string key, SimpleSaveSettings settings)
        {
            this.key = key;
            this.settings = settings;
        }
        public SimpleSaveData(object data)
        {
            this.data = (Dictionary<string, object>)data;
        }
        public SimpleSaveData(Dictionary<string, object> data)
        {
            this.data = data;
        }

    }
}
