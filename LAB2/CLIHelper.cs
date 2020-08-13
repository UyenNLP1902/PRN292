using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Text;

namespace LAB2
{
    /// <summary>
    /// The CLIHelper class.
    /// Contains all methods to show CLI in cmd
    /// </summary>
    class CLIHelper
    {
        /// <value>
        /// Gets and sets the file name
        /// </value>
        public static string FileName { get; set; }

        /// <value>
        /// Gets and sets the database name
        /// </value>
        public static string DatabaseName { get; set; }

        /// <value>
        /// Gets  the table name
        /// </value>
        public static string TableName { get; set; }

        /// <summary>
        /// Checks if the command line contains target value>
        /// </summary>
        /// <param name="args">An array of string</param>
        /// <param name="targetvalue">A string value</param>
        /// <returns>Return the index of target value if found, return -1 if not found.</returns>
        public static int CheckContain(string[] args, string targetvalue)
        {
            int result = -1;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals(targetvalue))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Show CLI message
        /// </summary>
        /// <param name="args">An array of strings</param>
        /// <returns>CLI message</returns>
        public static String ShowCLI(string[] args)
        {
            string helpString = @"  Help: 
./exportJson2Database -j <<json_file>> -d <<database_name>>: Save json file to a table in chosen database";

            if ((args.Length == 0) || (args.Length == 1 && args[0].Equals("-h")))
            {
                return helpString;
            }
            else
            {
                int indexJson = CheckContain(args, "-j");
                int indexDatabase = CheckContain(args, "-d");
                if (indexJson == -1)
                {
                    return "ERROR: Lack of json file name.";
                } 
                else if (!args[indexJson + 1].Contains(".json"))
                {
                    return "ERROR: Wrong json file name.";
                }
                else
                {
                    Util.Path = AppDomain.CurrentDomain.BaseDirectory + args[indexJson + 1];
                    
                    bool exist = Util.CheckJsonFile();
                    if (!exist)
                    {
                        return "ERROR: Cannot find json file.";
                    }
                }

                if (indexDatabase == -1)
                {

                    return "ERROR: Lack of database name.";
                }

                if ((indexJson + 1) == args.Length
                    || (indexDatabase + 1) == args.Length
                    || (indexJson + 1) == indexDatabase)
                {
                    return "ERROR: Your CLI format is not correct.";
                }

                string json = args[indexJson + 1];
                string db = args[indexDatabase + 1];

                if (!String.IsNullOrEmpty(json))
                {
                    FileName = json;
                }
                else
                {
                    return "ERROR: Your CLI format is not correct.";
                }

                if (!String.IsNullOrEmpty(db))
                {
                    DatabaseName = db;
                }
                else
                {
                    return "ERROR: Your CLI format is not correct.";
                }

                TableName = FileName.Replace(".json", "");

                return "";
            }
        }
    }
}
