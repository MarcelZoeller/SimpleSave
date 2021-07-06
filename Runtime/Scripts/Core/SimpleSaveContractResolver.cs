using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
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
