using SimpleSave;
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

    }

}
