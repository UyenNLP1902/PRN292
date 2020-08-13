using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LAB3
{
    /// <summary>
    /// The Util class.
    /// Create properties of Configure and a filepath.
    /// </summary>
    public class Util
    {
        /// <summary>
        /// A Configure object
        /// </summary>
        public static Configure Config { get; set; }

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
                Configure config = JsonConvert.DeserializeObject<Configure>(content);

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

        public static string GetConnectionString()
        {
            string strConnection = String.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3}",
                Config.ServerName, Config.Database, Config.UserID, Config.Password); 
          //  string strConnection = String.Format("server={0}; database={1}; uid={2}; pwd={3}",
          //      Config.ServerName, Config.Database, Config.UserID, Config.Password);
            return strConnection;
        }


    }
}
