// Simple Save by Marcel Z�ller

using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Collections;

namespace SimpleSave
{
    public static class SSave
    {
        static JsonSerializerSettings defaultJsonSerializerSettings;

        static JsonSerializerSettings jsonSerializerSettings => new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            ContractResolver = new SimpleSaveContractResolver()
        };

        public static void Save<T>(string key, T value)
        {
            Save<T>(key, value, new SimpleSaveSettings());
        }
        public static void Save<T>(string key, T value, SimpleSaveSettings settings)
        {
            ReadFile(settings, out var data);

            data[key] = value;

            WriteFile(data, settings);
        }
        public static T Load<T>(string key)
        {
            return Load<T>(key, new SimpleSaveSettings());
        }
        public static T Load<T>(string key, SimpleSaveSettings settings)
        {
            if (ReadFile(settings, out var data))
            {
                return ConvertObject<T>(data[key]);
            }
            else
            {
                throw new Exception("File: " + settings.filename + " not found.");
            }
        }
        public static T Load<T>(string key, SimpleSaveSettings settings, T defaultValue)
        {
            if (ReadFile(settings, out var data))
            {
                return ConvertObject<T>(data[key]);
            }
            else
            {
                return defaultValue;
            }
        }


        public static List<T> LoadScriptableObject<T>(string key, SimpleSaveSettings settings) where T : SaveableScriptableObjects
        {
            List<string> data = SSave.Load<List<string>>(key, settings);
            return SaveableScriptableObjects.GetObjectListFromIDs<T>(data);
        }

        public static T ConvertObject<T>(object value)
        {
            if (typeof(T) == typeof(IDictionary))
            {
                Debug.Log("its a Dictionary" + value);
            }


            if (typeof(T) == typeof(string))
                return (T)value;
            
            
            if (typeof(T) == typeof(int))
                value = Convert.ToInt32(value);

            else if (typeof(T) == typeof(float))
                value = Convert.ToSingle(value);

            else if ((typeof(T) == typeof(Vector2))
            || (typeof(T) == typeof(Vector2Int))
            || (typeof(T) == typeof(Vector3))
            || (typeof(T) == typeof(Vector3Int))
            || (typeof(T) == typeof(Vector4))
            || (typeof(T) == typeof(SaveableScriptableObjects))
            ||  typeof(T).IsSubclassOf(typeof(SaveableScriptableObjects))
            )
            {
                if (value != null)
                {
                    //Debug.Log("value" + value);
                    return (T)JsonConvert.DeserializeObject<T>(value.ToString(), jsonSerializerSettings);
                }
            }

            



                else// Testing
            {
                //Debug.Log("type: " + typeof(T) + " " + typeof(T).Name);
                //value = JsonConvert.DeserializeObject<T>(value.ToString(), jsonSerializerSettings);
            }

            //Debug.Log(value);
            return (T)value;
        }




        //public override bool CanConvert(Type objectType)
        //{
        //    return (typeof(IDictionary).IsAssignableFrom(objectType) ||
        //            TypeImplementsGenericInterface(objectType, typeof(IDictionary<,>)));
        //}

        //private static bool TypeImplementsGenericInterface(Type concreteType, Type interfaceType)
        //{
        //    return concreteType.GetInterfaces()
        //           .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType);
        //}




        public static bool WriteFile(Dictionary<string, object> data, SimpleSaveSettings settings)
        {
            string json = JsonConvert.SerializeObject(data, jsonSerializerSettings);
            return SimpleSaveFileHandler.Write(settings, json);
        }
        public static bool ReadFile(SimpleSaveSettings settings, out Dictionary<string, object> data)
        {
            if (SimpleSaveFileHandler.Read(settings, out string json))
            {
                //TODO try
                data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, jsonSerializerSettings);
                return true;
            }
            else
            {
                data = new Dictionary<string, object>();
                return false;
            }
        }
    }
}