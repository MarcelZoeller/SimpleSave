using SimpleSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class SaveGameObject : MonoBehaviour
    {
        public string id;

        public object SaveManager { get; private set; }

        public void SetID (string ID)
        {
            id = ID;
        }

        [ContextMenu("Generate new Id")]
        private void GenerateId()
        {
            id = gameObject.name + "_" + Guid.NewGuid().ToString();
        }



        public object CaptureAllComponents()
        {
            var data = new Dictionary<string, object>();
            ISaveable[] saveables = GetComponents<ISaveable>();
            foreach (var saveable in saveables)
            {
                data[GetTypeName(saveable)] = saveable.Save();
            }
            return data;
        }

        public void RestoreAllComponents(object data)
        {
            var stateDictionary = (Dictionary<string, object>)data;
            foreach (var saveable in GetComponents<ISaveable>())
            {
                if (stateDictionary.TryGetValue(GetTypeName(saveable), out object value))
                {
                    saveable.Load(value);
                }
            }
        }

        string GetTypeName(ISaveable iSaveable) => iSaveable.GetType().ToString();

        

    }


