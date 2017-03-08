using System.Collections;
using UnityEngine;
using System.IO;

namespace Util {
    public static class CsvParser {

        public static void SaveCsv<T>(string path, T obj) {
            // TODO: 
            // We need to define a interface for T (which is a placeholder for all acceptable data type)
            // T would have a public method that convert itself into csv file format
            // This method then should write the result to a csv file
        }

        public static T LoadCsv<T>(string path) {
            using (var fs = File.OpenRead(@path))
            using (StreamReader reader = new StreamReader(fs)) {
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');
                }

                // Input csv file is defined as:
                // <Robot name>
                // <Problem 1>; <Answer 1>; <Problem 2>; <Answer 2> ...
                // Ex:
                // Robot 1
                // 1+2; 3; 2+1; 3;

                return default(T);
            }
        }
    }
}
