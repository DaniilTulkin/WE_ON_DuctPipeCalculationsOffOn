﻿namespace DuctPipeCalculationsOffOn
{
    using System;
    using System.IO;
    using DuctPipeCalculationsOffOn.Services;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class Json
    {
        public static string applicationPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string jsonFolderPath = $"{applicationPath}\\Nika_RD_Data\\Nika_RD_Json\\DuctPipeCalculationsOffOn";
        public static string revitFileName;
        public static string jsonFilePath;

        static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            CheckAdditionalContent = true,
            Formatting = Formatting.Indented
        };

        public static void WriteJson(object information, string RevitFilename)
        {
            revitFileName = RevitFilename;
            jsonFilePath = $"{applicationPath}\\Nika_RD_Data\\Nika_RD_Json\\DuctPipeCalculationsOffOn\\{revitFileName}.json";

            if (!Directory.Exists(jsonFolderPath))
            {
                Directory.CreateDirectory(jsonFolderPath);
            }
            using (StreamWriter writer = File.CreateText(jsonFilePath))
            {
                string output = JsonConvert.SerializeObject(information);
                writer.Write(output);
            }
        }

        public static T Deserialize<T>(string json) where T : new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Failed to deserialize json string into a valid object___{ex.Message}");
            }
        }

        public static T ReadJson<T>(string RevitFilename) where T : new()
        {
            revitFileName = RevitFilename;
            jsonFilePath = $"{applicationPath}\\Nika_RD_Data\\Nika_RD_Json\\DuctPipeCalculationsOffOn\\{revitFileName}.json";

            T result = default(T);
            try
            {
                if (File.Exists(jsonFilePath))
                {
                    using (var reader = File.OpenText(jsonFilePath))
                    {
                        string fileText = reader.ReadToEnd();
                        var jsonModel = Deserialize<T>(fileText);
                        if (jsonModel != null)
                        {
                            result = jsonModel;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }
    }
}