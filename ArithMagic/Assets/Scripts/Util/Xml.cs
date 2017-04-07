using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Util {
    /// <summary>
    /// Saves an serializable object to a xml file or load a xml file into an object.
    /// </summary>
    public static class Xml {

        /// <summary>
        /// Saves an object to a xml file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <param name="obj">The object.</param>
        public static void SaveXml<T>(string path, T obj, bool append = false) {
            try {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlWriterSettings xws = new XmlWriterSettings();
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                xws.CloseOutput = true;
                using (StreamWriter stream = new StreamWriter(path, append, encoding)) {
                    xs.Serialize(stream, obj);
                    stream.Close();
                }
            } catch (IOException e) {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// Loads the specified file to a object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static T LoadXml<T>(string path) {
            try {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlReaderSettings xrs = new XmlReaderSettings();
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                xrs.CloseInput = true;
                using (StreamReader stream = new StreamReader(path, encoding)) {
                    T val = (T)xs.Deserialize(stream);
                    stream.Close();
                    return val;
                }
            } catch (IOException e) {
                Debug.LogException(e);
                return default(T);
            } catch (InvalidCastException e) {
                Debug.LogException(e);
                return default(T);
            }
        }
    }
}
