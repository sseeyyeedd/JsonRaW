using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
namespace JsonRaW
{
    public class JsonRaW<T>
    {
        /// <summary>
        /// Reads all json text from a json file and Deserialize it in type IEnumerable<T>/>
        /// </summary>
        /// <param name="jsonFilePath">Json file path</param>
        /// <returns>Returns IEnumerable of type T read from a json file</returns>
        public static IEnumerable<T> jsonReader(string jsonFilePath)
        {
            IEnumerable<T> jsonObject = null;
            string json = File.ReadAllText(jsonFilePath);
            jsonObject =JsonConvert.DeserializeObject<List<T>>(json);
            return jsonObject ?? new List<T>();
        }
        /// <summary>
        /// Reads all json text from a web address and Deserialize it in type IEnumerable<T>
        /// </summary>
        /// <param name="jsonWebAddress">Json web address</param>
        /// <returns>Returns IEnumerable of type T read from a web address</returns>
        public static IEnumerable<T> JsonReader(string jsonWebAddress)
        {
            IEnumerable<T> jsonObject = null;
            HttpClient client = new HttpClient();
            var response=client.GetAsync(jsonWebAddress);
            jsonObject = JsonConvert.DeserializeObject(response.Result.Content.ToString()) as IEnumerable<T>;
            return jsonObject ?? new List<T>();
        }
        /// <summary>
        /// Writes an object from type IEnumerable<T> in a file in path "JsonFilePath" 
        /// </summary>
        /// <param name="convertibleObject">an object from type IEnumerable<T></param>
        /// <param name="jsonFilePath">Json file path</param>
        public static void jsonWriter(IEnumerable<T> convertibleObject, string jsonFilePath)
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
