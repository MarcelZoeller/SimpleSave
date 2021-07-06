using SimpleSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSave
{
    public class SaveGameObject : MonoBehaviour
    {
        public string id;

        [ContextMenu("Generate new Id")]
        private void GenerateId()
        {
            id = gameObject.name + "_" + Guid.NewGuid().ToString();
        }

        [ContextMenu("Save")]
        public void Save()
        {
            SSave.Save(id, GetAllComponents());
        }

        [ContextMenu("Load")]
        public void Load()
        {
            SetAllComponents(SSave.Load<object>(id));
        }


        public object GetAllComponents()
        {
            var data = new Dictionary<string, object>();
            ISaveable[] saveables = GetComponents<ISaveable>();
            foreach (var saveable in saveables)
            {
                data[GetTypeName(saveable)] = saveable.Save();
            }
            return data;
        }

        public void SetAllComponents(object data)
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
}

