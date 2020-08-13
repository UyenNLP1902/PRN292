using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace LAB2
{
    /// <summary>
    /// The Util class.
    /// Create properties of Configure and a filepath.
    /// </summary>
    class Util
    {
        /// <summary>
        /// A Configure object
        /// </summary>
        public static Configure Config { get; set; }

        /// <summary>
        /// A filepath
        /// </summary>
        public static string Path { get; set; }

        /// <summary>
        /// Find and deserialize json file
        /// </summary>
        /// <param name="path">A string value</param>
        /// <returns>true if json file is found and deserialized, false if not</returns>
        /// <exception cref="System.IO.FileNotFoundException">
        /// Thrown when the json file cannot be found
        /// </exception>
        public static bool CheckJsonFile()
        {
            bool check = false;
            try
            {
                string content = File.ReadAllText(Path);
                Configure config = JsonSerializer.Deserialize<Configure>(content);

                if (config != null)
                {
                    Config = config;
                    check = true;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERROR: Util _ FileNotFoundException " + ex.Message);
            }
            return check;
        }
    }
}
