using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JsonRaW.netStandard
{

    public class JsonRaW<T>
    {
        /// <summary>
        /// Reads all json text from a json file and Deserialize it in type List<T>/>
        /// </summary>
        /// <param name="jsonFilePath">Json file path</param>
        /// <returns>Returns List of type T read from a json file</returns>
        public static List<T> jsonReader(string jsonFilePath)
        {
            List<T> jsonObject = null;
            string json = File.ReadAllText(jsonFilePath);
            jsonObject = JsonConvert.DeserializeObject<List<T>>(json);
            return jsonObject ?? new List<T>();
        }
        /// <summary>
        /// Writes an object from type List<T> in a file in path "JsonFilePath" 
        /// </summary>
        /// <param name="convertibleObject">an object from type List<T></param>
        /// <param name="jsonFilePath">Json file path</param>
        public static void jsonWriter(List<T> convertibleObject, string jsonFilePath)
        {
            JsonFile.checkDirectory(jsonFilePath);
            string convertedJson;
            convertedJson = JsonConvert.SerializeObject(convertibleObject);
            File.WriteAllText(jsonFilePath, convertedJson);
        }

    }
    public class JsonFile
    {
        /// <summary>
        /// Checks JsonFilePath and if this file not exist, create it. 
        ///JsonRaW.JsonReader() and JsonRaW.JsonWriter() contains this methods
        /// </summary>
        /// <param name="jsonFilePath">Json file path</param>
        public static void checkFile(string jsonFilePath)
        {
            checkDirectory(jsonFilePath);
            if (File.Exists(jsonFilePath) == false)
            {
                File.WriteAllText(jsonFilePath, null);
            }
        }
        internal static void checkDirectory(string jsonFilePath)
        {
            if (Directory.Exists(Path.GetDirectoryName(jsonFilePath)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath));
            }
        }
    }
}

