﻿using System.IO;
//using System.Runtime.Serialization.Json;
using SimpleJSON;
using Radix.Logging;
using System.Runtime.Serialization;

namespace Radix.Json
{
    public class JsonUtility
    {
        public static void SaveToFile<T, O>(O pObject, string pPath) where T:JsonParser<O>, new()
        {
            var parser = new T();
            string json = parser.Parse(pObject);

            //save (to put in a service or somewhere else
#if !UNITY_WSA && !UNITY_WP8 && !UNITY_WP8_1
            DeleteFileIfExist(pPath);

            StreamWriter writer = new StreamWriter(pPath);
            writer.WriteLine(json);
            writer.Close();
#endif
        }

        private static void DeleteFileIfExist(string aPath)
        {
#if !UNITY_WSA && !UNITY_WP8 && !UNITY_WP8_1
            if (File.Exists(aPath))
            {
                File.Delete(aPath);
            }
#endif
        }
    }
}
