
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;


namespace LAB2
{
    /// <summary>
    /// The ClassInfo class.
    /// Contains create method and properties of ClassInfo. 
    /// </summary>
    public class Class
    {
        /// <value>
        /// The id of class
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The level of class
        /// </value>
        public string Level { get; set; }

        /// <value>
        /// The room of class
        /// </value>
        public string Room { get; set; }

        /// <value>
        /// The name of class
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// An empty constructor for class
        /// </summary>
        public Class() { }

        /// <summary>
        /// A constructor for class
        /// </summary>
        /// <param name="id">A string value</param>
        /// <param name="level">A string value</param>
        /// <param name="room">A string value</param>
        /// <param name="name">A string value</param>
        public Class(string id, string level, string room, string name)
        {
            UUID = id;
            Level = level;
            Room = room;
            Name = name;
        }

        /// <summary>
        /// Gets class list
        /// </summary>
        /// <returns>An array of classes</returns>
        public static Class[] GetClassList()
        {
            return Util.Config.Class;
        }

        /// <summary>
        /// Create Class table in database
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <exception cref="System.Data.SqlClient.SqlException">
        /// Thrown when the sql string is wrong
        /// </exception>
        public static void CreateClassTable(string databaseName)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(School.GetConnectionString(databaseName));
                if (con != null)
                {
                    con.Open();
                    string sql = @"CREATE TABLE Class(
                        UUID VARCHAR(50) PRIMARY KEY,
                        Level VARCHAR(50) REFERENCES Level(UUID), 
                        Room VARCHAR(50) REFERENCES Room(UUID), 
                        Name VARCHAR(50)
                    )";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();

                    Class[] classList = GetClassList();
                    foreach (Class classs in classList)
                    {
                        sql = String.Format("INSERT INTO Class(UUID, Level, Room, Name) " +
                            "VALUES('{0}','{1}','{2}','{3}')", 
                            classs.UUID, classs.Level, classs.Room, classs.Name);
                        cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: Class _ SqlException " + ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
    }
}