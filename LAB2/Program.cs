using System;
using System.IO;

namespace LAB2
{
    /// <summary>
    /// The main Program class.
    /// Contains main methods.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Performs functions.
        /// </summary>
        /// <param name="args">A string array contains command from command-line</param>
        static void Main(string[] args)
        {
            string info = CLIHelper.ShowCLI(args);
            if (info == "")
            {
                string fileName = CLIHelper.FileName;
                string databaseName = CLIHelper.DatabaseName;
                bool check = School.CreateDatabase(databaseName);
                if (check)
                {
                    School.CreateTables(databaseName);

                    Console.WriteLine("Succesful: You've exported " + fileName + " to " + databaseName + " in SQLSERVER");
                }
            }
            else
            {
                Console.WriteLine(info);
            }

        }
    }
}
