﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Diagnostics;

namespace AeroGear
{
    /// <summary>
    /// Provides JSON serialization/deserialization functionality.
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Serializes object to JSON string representation
        /// </summary>
        /// <param name="obj">object to serialize</param>
        /// <returns>JSON representation of the object. Returns 'null' string for null passed as argument</returns>
        public static string Serialize(object obj)
        {
            if (obj == null)
            {
                return "null";
            }

            DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());

            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);

            ms.Position = 0;

            string json = String.Empty;

            using (StreamReader sr = new StreamReader(ms))
            {
                json = sr.ReadToEnd();
            }

            ms.Close();

            return json;

        }

        /// <summary>
        /// Parses json string to object instance
        /// </summary>
        /// <typeparam name="T">type of the object</typeparam>
        /// <param name="json">json string representation of the object</param>
        /// <returns>Deserialized object instance</returns>
        public static T Deserialize<T>(string json)
        {
            DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(T));
            object result = null;
            try
            {
                using (MemoryStream mem = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    result = deserializer.ReadObject(mem);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("Failed to deserialize " + typeof(T) + " with JSON value :: " + json);
            }

            return (T)result;

        }
    }
}