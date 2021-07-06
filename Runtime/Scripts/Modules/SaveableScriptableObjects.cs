using System.Collections.Generic;
using UnityEngine;

namespace SimpleSave
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SaveableScriptableObjects(TestOnly)")]
    public class SaveableScriptableObjects : ScriptableObject
    {
        static Dictionary<string, ScriptableObject> iDDictionary = null;

        public string GetIDFromObject()
        {
            return name;
        }

        public static List<string> ListOfIDs<T>(List<T> ids) where T : SaveableScriptableObjects
        {
            List<string> strings = new List<string>();
            foreach (var obj in ids)
            {
                strings.Add(obj.name);
            }
            return strings;
        }

        public static void GetObjectFromID<T>(string id, out T result) where T : SaveableScriptableObjects
        {
            result = GetObjectFromID<T>(id);
        }

        public static T GetObjectFromID<T>(string id) where T : SaveableScriptableObjects
        {
            
            
            if (iDDictionary == null || iDDictionary.Count == 0) SetupIDDictionary<T>();
            else
            {
                Debug.Log("ther is a dict for " + typeof(T).FullName +" with "+ iDDictionary.Count);
            }

            if (id == null || !iDDictionary.ContainsKey(id))
            {
                Debug.LogError("ID " + id + " not found in Database");
                return null;
            }

            return (T)iDDictionary[id];
        }

        public static void SetupIDDictionary<T>() where T : SaveableScriptableObjects
        {
            //Debug.Log("setting up dict for" + typeof(T).FullName);
            iDDictionary = new Dictionary<string, ScriptableObject>();
            var itemList = Resources.LoadAll<SaveableScriptableObjects>("");

            //Debug.Log("loaded all " + typeof(T).FullName +" "+ itemList.Length);

            foreach (ScriptableObject item in itemList)
            {
                //Debug.Log("add " + item.name);
                if (iDDictionary.ContainsKey(item.name))
                {
                    Debug.LogError(string.Format("Duplicate ID found!! ID {0} and {1}. " +
                        "Please Assain new ID to on of these Items ", iDDictionary[item.name], item));
                    continue;
                }
                iDDictionary[item.name] = item;
            }
        }

        public static List<T> GetObjectListFromIDs<T>(List<string> ids) where T : SaveableScriptableObjects
        {
            if (iDDictionary == null || iDDictionary.Count == 0) SetupIDDictionary<T>();

            List<T> list = new List<T>();
            foreach (var item in ids)
            {
                list.Add(GetObjectFromID<T>(item));
            }

            return list;
        }



    }

    public static class SimpleSaveExtensions
    {
        public static List<string> GetScriptableObjectIDs<T>(this List<T> objects) where T : SaveableScriptableObjects
        {
            return SaveableScriptableObjects.ListOfIDs<T>(objects);
        }

        public static List<T> GetScriptableObjectsFromID<T>(this List<string> names) where T : SaveableScriptableObjects
        {
            return SaveableScriptableObjects.GetObjectListFromIDs<T>(names);
        }

        public static List<T> GetScriptableObjectsFromID<T>(this SimpleSaveData simpleSaveData, string key) where T : SaveableScriptableObjects
        {
            List<string> data = simpleSaveData.Get<List<string>>(key);
            return SaveableScriptableObjects.GetObjectListFromIDs<T>(data);
        }

    }
}
