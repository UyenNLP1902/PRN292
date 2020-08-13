using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace LAB2
{
    /// <summary>
    /// The School class.
    /// Contains methods to save file to database.
    /// </summary>
    class School
    {
        /// <summary>
        /// Gets connection string
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <returns>A connection string</returns>
        public static string GetConnectionString(string databaseName)
        {
            //string strConnection = "Data Source=localhost,1433;Initial Catalog=" + databaseName + ";User ID=sa;Password=123";
            string strConnection = "Data Source=SE140355; Initial Catalog=" + databaseName + "; User ID=sa; Password=SE140355";
            return strConnection;
        }

        /// <summary>
        /// Creates a new database in SQL Server
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <returns>Whether create success of fail</returns>
        public static bool CreateDatabase(string databaseName)
        {
            bool result = false;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(GetConnectionString("master"));
                if (con != null)
                {
                    con.Open();
                    string queryString = string.Format("DROP DATABASE IF EXISTS {0}; CREATE DATABASE {0};", databaseName);
                    
                    SqlCommand cmd = new SqlCommand(queryString, con);
                    cmd.ExecuteNonQuery();
                    result = true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: School _ SqlException " + ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Create tables in database
        /// </summary>
        /// <param name="databaseName">A string value</param>
        public static void CreateTables(string databaseName)
        {
            Level.CreateLevelTable(databaseName);
            Field.CreateFieldTable(databaseName);
            Room.CreateRoomTable(databaseName);
            Class.CreateClassTable(databaseName);
            Subject.CreateSubjectTable(databaseName);
            Student.CreateStudentTable(databaseName);
            Teacher.CreateTeacherTable(databaseName);
            Attendance.CreateAttendanceTable(databaseName);
            Grade.CreateClassTable(databaseName);
        }
    }
}
