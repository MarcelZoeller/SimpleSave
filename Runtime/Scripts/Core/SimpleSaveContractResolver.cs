using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SimpleSave
{
    public class SimpleSaveContractResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);

            if (objectType == typeof(Vector4))
            {
                contract.Converter = new Vector4Converter();
            }

            if (objectType == typeof(Vector3))
            {
                contract.Converter = new Vector3Converter();
            }

            if (objectType == typeof(Vector2))
            {
                contract.Converter = new Vector2Converter();
            }

            if (objectType == typeof(Vector3Int))
            {
                contract.Converter = new Vector3IntConverter();
            }

            if (objectType == typeof(Vector2Int))
            {
                contract.Converter = new Vector2IntConverter();
            }

            if (objectType.IsSubclassOf(typeof(SaveableScriptableObjects)))
            {
                Debug.Log("subclass! " + objectType.FullName);
                contract.Converter = new SaveableScriptableObjectConverter();
            }

            if (objectType == typeof(SaveableScriptableObjects))
            {
                Debug.Log("soso " + objectType.FullName);
                contract.Converter = new SaveableScriptableObjectConverter();
            }


            if (objectType == typeof(Dictionary<SimpleSaveManager.v2, string>))
            {
                Debug.Log("aha!!!");
                contract.Converter = new CustomDictionaryConverter<SimpleSaveManager.v2, string>();
            }

            //if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            //{
            //    Type keyType = objectType.GetGenericArguments()[0];
            //    Type valueType = objectType.GetGenericArguments()[1];

            //    contract.Converter = new CustomDictionaryConverter<int, int>();
            //}

            if((objectType.IsGenericType && (objectType.GetGenericTypeDefinition() == typeof(IDictionary<,>) 
                || objectType.GetGenericTypeDefinition() == typeof(Dictionary<,>))) 
                && objectType != typeof(Dictionary<string,object>))
            {
                contract.Converter = new DictionaryJsonConverter();
            }

            


            if (objectType == typeof(Dictionary<SimpleSaveManager.v2, string>))
            {
                Debug.Log("aha!!!");
                contract.Converter = new CustomDictionaryConverter<SimpleSaveManager.v2, string>();
            }



            return contract;
        }
    }

    public class Vector4Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Vector4))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize(reader);
            var iv = JsonConvert.DeserializeObject<Vector4>(t.ToString());
            return iv;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Vector4 v = (Vector4)value;

            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(v.x);
            writer.WritePropertyName("y");
            writer.WriteValue(v.y);
            writer.WritePropertyName("z");
            writer.WriteValue(v.z);
            writer.WritePropertyName("w");
            writer.WriteValue(v.w);
            writer.WriteEndObject();
        }
    }

    public class Vector3IntConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Vector3Int))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize(reader);
            var iv = JsonConvert.DeserializeObject<Vector3Int>(t.ToString());
            return iv;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Vector3Int v = (Vector3Int)value;

            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(v.x);
            writer.WritePropertyName("y");
            writer.WriteValue(v.y);
            writer.WritePropertyName("z");
            writer.WriteValue(v.z);
            writer.WriteEndObject();
        }
    }

    public class Vector3Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Vector3))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize(reader);
            var iv = JsonConvert.DeserializeObject<Vector3>(t.ToString());
            return iv;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Vector3 v = (Vector3)value;

            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(v.x);
            writer.WritePropertyName("y");
            writer.WriteValue(v.y);
            writer.WritePropertyName("z");
            writer.WriteValue(v.z);
            writer.WriteEndObject();
        }
    }

    public class Vector2Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Vector2))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize(reader);
            var iv = JsonConvert.DeserializeObject<Vector2>(t.ToString());
            return iv;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Vector2 v = (Vector2)value;

            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(v.x);
            writer.WritePropertyName("y");
            writer.WriteValue(v.y);
            writer.WriteEndObject();
        }



    }

    public class Vector2IntConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Vector2Int))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize(reader);
            var iv = JsonConvert.DeserializeObject<Vector2Int>(t.ToString());
            return iv;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Vector2Int v = (Vector2Int)value;

            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(v.x);
            writer.WritePropertyName("y");
            writer.WriteValue(v.y);
            writer.WriteEndObject();
        }

    }











    //public class DeepDictionaryConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return (typeof(IDictionary).IsAssignableFrom(objectType) ||
    //                TypeImplementsGenericInterface(objectType, typeof(IDictionary<,>)));
    //    }

    //    private static bool TypeImplementsGenericInterface(Type concreteType, Type interfaceType)
    //    {
    //        return concreteType.GetInterfaces()
    //               .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType);
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        Type type = value.GetType();
    //        IEnumerable keys = (IEnumerable)type.GetProperty("Keys").GetValue(value, null);
    //        IEnumerable values = (IEnumerable)type.GetProperty("Values").GetValue(value, null);
    //        IEnumerator valueEnumerator = values.GetEnumerator();

    //        writer.WriteStartArray();
    //        foreach (object key in keys)
    //        {
    //            valueEnumerator.MoveNext();

    //            writer.WriteStartArray();
    //            serializer.Serialize(writer, key);
    //            serializer.Serialize(writer, valueEnumerator.Current);
    //            writer.WriteEndArray();
    //        }
    //        writer.WriteEndArray();
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}




    public class CustomDictionaryConverter<TKey, TValue> : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(Dictionary<TKey, TValue>);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => serializer.Serialize(writer, ((Dictionary<TKey, TValue>)value).ToList());

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            => serializer.Deserialize<KeyValuePair<TKey, TValue>[]>(reader).ToDictionary(kv => kv.Key, kv => kv.Value);
    }



    public class DictionaryJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dictionary = (IDictionary)value;

            writer.WriteStartArray();

            foreach (var key in dictionary.Keys)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("Key");

                serializer.Serialize(writer, key);

                writer.WritePropertyName("Value");

                serializer.Serialize(writer, dictionary[key]);

                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!CanConvert(objectType))
                throw new Exception(string.Format("This converter is not for {0}.", objectType));

            var keyType = objectType.GetGenericArguments()[0];
            var valueType = objectType.GetGenericArguments()[1];
            var dictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
            var result = (IDictionary)Activator.CreateInstance(dictionaryType);

            if (reader.TokenType == JsonToken.Null)
                return null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndArray)
                {
                    return result;
                }

                if (reader.TokenType == JsonToken.StartObject)
                {
                    AddObjectToDictionary(reader, result, serializer, keyType, valueType);
                }
            }

            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType && (objectType.GetGenericTypeDefinition() == typeof(IDictionary<,>) || objectType.GetGenericTypeDefinition() == typeof(Dictionary<,>));
        }

        private void AddObjectToDictionary(JsonReader reader, IDictionary result, JsonSerializer serializer, Type keyType, Type valueType)
        {
            object key = null;
            object value = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject && key != null)
                {
                    result.Add(key, value);
                    return;
                }

                var propertyName = reader.Value.ToString();
                if (propertyName == "Key")
                {
                    reader.Read();
                    key = serializer.Deserialize(reader, keyType);
                }
                else if (propertyName == "Value")
                {
                    reader.Read();
                    value = serializer.Deserialize(reader, valueType);
                }
            }
        }
    }


















    public class SaveableScriptableObjectConverter : JsonConverter //where T: SaveableScriptableObjects
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(SaveableScriptableObjects))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object t = serializer.Deserialize(reader);
            var customer2 = JsonConvert.DeserializeAnonymousType(t.ToString(), new { id = "" });
            SaveableScriptableObjects.GetObjectFromID(customer2.id, out SaveableScriptableObjects saveableScriptableObjects);

            return saveableScriptableObjects;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            SaveableScriptableObjects saveableScriptableObjects = (SaveableScriptableObjects)value;
            string id = saveableScriptableObjects.GetIDFromObject();
            writer.WriteStartObject();
            writer.WritePropertyName("id");
            writer.WriteValue(id);
            writer.WriteEndObject();
        }

        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //{
        //    Debug.Log(reader + "  Start Read");
        //    object t = serializer.Deserialize(reader);
        //    //var customer2 = JsonConvert.DeserializeAnonymousType(t.ToString(), new { id = "" });
        //    //SaveableScriptableObjects.GetObjectFromID(customer2.id, out SaveableScriptableObjects saveableScriptableObjects);

        //    Debug.Log(t.ToString());

        //    SaveableScriptableObjects.GetObjectFromID(t.ToString(), out SaveableScriptableObjects saveableScriptableObjects);

        //    //SaveableScriptableObjects saveableScriptableObjects = new SaveableScriptableObjects();

        //    return saveableScriptableObjects;
        //}





        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{
        //    SaveableScriptableObjects saveableScriptableObjects = (SaveableScriptableObjects)value;
        //    string id = saveableScriptableObjects.GetIDFromObject();

        //    writer.WriteValue(id);
        //    //writer.WriteStartObject();
        //    //writer.WritePropertyName("id");
        //    //writer.WriteValue(id);
        //    //writer.WriteEndObject();
        //}

    }
}
