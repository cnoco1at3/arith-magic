using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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
    public static void Save<T> (string path, T obj) {
        try {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            XmlWriterSettings xws = new XmlWriterSettings();
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            xws.CloseOutput = true;
            using (StreamWriter stream = new StreamWriter(path, false, encoding)) {
                xs.Serialize(stream, obj);
                stream.Close();
            }
        }
        catch (IOException e) {
            Debug.LogException(e);
        }
    }

    /// <summary>
    /// Loads the specified file to a object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    public static T Load<T> (string path) {
        try {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            using (StreamReader stream = new StreamReader(path, encoding)) {
                T val = (T)xs.Deserialize(stream);
                stream.Close();
                return val;
            }
        }
        catch (IOException e) {
            Debug.LogException(e);
            return default(T);
        }
        catch (InvalidCastException e) {
            Debug.LogException(e);
            return default(T);
        }
    }
}
