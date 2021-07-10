using Newtonsoft.Json;
using SimpleSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSave
{
    public class SimpleSaveManager : MonoBehaviour
    {
        public SimpleSaveSettings settings;
        [ContextMenu("SaveAlltheData")]
        void SaveAlltheData()
        {
            SaveAlltheData(settings);
        }

        public static void SaveAlltheData(SimpleSaveSettings settings)
        {
            SSave.ReadFile(settings, out var data);

            foreach (var saveable in FindObjectsOfType<SaveGameObject>())
            {
                data[saveable.id] = saveable.GetAllComponents();
            }

            SSave.WriteFile(data, settings);
        }

        [ContextMenu("LoadAllData")]
        void LoadAllData()
        {
            LoadAllData(settings);
        }

        public static void LoadAllData(SimpleSaveSettings settings)
        {
            if (SSave.ReadFile(settings, out var data))
            {
                foreach (var saveable in FindObjectsOfType<SaveGameObject>())
                {
                    saveable.SetAllComponents(data[saveable.id]);
                }
            }
        }


        // Testing  

        //[JsonConverter(typeof(CustomDictionaryConverter<v2, string>))]
        Dictionary<Vector2Int, string> testy = new Dictionary<Vector2Int, string>();

        public Vector2Int v2Test;

        [SerializeField] SimpleSaveSettings saveSettings;

        [System.Serializable]
        public struct v2 
        { 
            public int x;
            public int y;

            public v2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [ContextMenu("TestSave")]
        public void TestSave()
        {
            SSave.Save("v2Test", v2Test, saveSettings);

            testy = new Dictionary<Vector2Int, string>();
            testy.Add(new Vector2Int(12, 34),"asd");
            SSave.Save("testy", testy, saveSettings);
        }

        [ContextMenu("TestLoad")]
        public void TestLoad()
        {
            v2Test = SSave.Load<Vector2Int>("v2Test", saveSettings);
            
            testy = SSave.Load<Dictionary<Vector2Int, string>>("testy", saveSettings);     
            foreach (var item in testy)
            {
                Debug.Log("dict:) " + item.Key.ToString() + " " + item.Value.ToString());
            } 
        }


        public v2 v2Test2;
        Dictionary<v2, string> testy2 = new Dictionary<v2, string>();


        [ContextMenu("TestSave2")]
        public void TestSave2()
        {
            //SSave.Save("v2Test", v2Test2, saveSettings);

            testy2 = new Dictionary<v2, string>();
            testy2.Add(new v2(12, 34), "asd");
            SSave.Save("testy2", testy2, saveSettings);
        }

        [ContextMenu("TestLoad2")]
        public void TestLoad2()
        {
            //v2Test2 = SSave.Load<v2>("v2Test", saveSettings);

            testy2 = SSave.Load<Dictionary<v2, string>>("testy2", saveSettings);
            foreach (var item in testy2)
            {
                Debug.Log("dict:) " + item.Key.ToString() + " " + item.Value.ToString());
            }
        }

    }

}
