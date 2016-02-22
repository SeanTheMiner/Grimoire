using System;
using System.Collections.Generic;
using ParadoxNotion.Serialization.FullSerializer;


namespace ParadoxNotion.Serialization{

    ///Serializes/Deserializes to/from JSON with 'FullSerializer'
    public static class JSONSerializer {
        
        private static Dictionary<string, fsData> cache = new Dictionary<string, fsData>();


        private static fsSerializer serializer = new fsSerializer();
        private static bool init = false;

        ///Serialize to json
        public static string Serialize(Type type, object value, bool pretyJson = false, List<UnityEngine.Object> objectReferences = null) {

            if (!init){
                serializer.AddConverter(new fsUnityObjectConverter());
                init = true;
            }

            //set the objectReferences context
            if (objectReferences != null)
                serializer.Context.Set<List<UnityEngine.Object>>(objectReferences);

            //serialize the data
            fsData data;
            serializer.TrySerialize(type, value, out data).AssertSuccess();



            cache[fsJsonPrinter.CompressedJson(data)] = data;



            //print data to json
            if (pretyJson){
                return fsJsonPrinter.PrettyJson(data);
            }
            return fsJsonPrinter.CompressedJson(data);
        }

        ///Deserialize generic
        public static T Deserialize<T>(string serializedState, List<UnityEngine.Object> objectReferences = null){
            return (T)Deserialize(typeof(T), serializedState, objectReferences);
        }

        ///Deserialize from json
        public static object Deserialize(Type type, string serializedState, List<UnityEngine.Object> objectReferences = null) {

            if (!init){
                serializer.AddConverter(new fsUnityObjectConverter());
                init = true;
            }

            if (objectReferences != null){
                serializer.Context.Set<List<UnityEngine.Object>>(objectReferences);
            }

            fsData data = null;
            cache.TryGetValue(serializedState, out data);
            if (data == null){
                data = fsJsonParser.Parse(serializedState);
                cache[serializedState] = data;                
            }

/*
            //parse the JSON data
            var data = fsJsonParser.Parse(serializedState);
*/


            //deserialize the data
            object deserialized = null;
            serializer.TryDeserialize(data, type, ref deserialized).AssertSuccess();

            return deserialized;
        }
    }
}