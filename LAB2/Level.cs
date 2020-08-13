
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace LAB2
{
    /// <summary>
    /// The Level class
    /// Contains create method and properties of Level. 
    /// </summary>
    public class Level
    {
        /// <value>
        /// The id of level
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The name of level
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// An empty constructor for level
        /// </summary>
        public Level() { }

        /// <summary>
        /// A constructor for level
        /// </summary>
        /// <param name="id">A string value</param>
        /// <param name="name">A string value</param>
        public Level(string id, string name)
        {
            UUID = id;
            Name = name;
        }

        /// <summary>
        /// Gets level list
        /// </summary>
        /// <returns>An array of levels</returns>
        public static Level[] GetLevelList()
        {
            return Util.Config.Level;
        }

        /// <summary>
        /// Create Level table in database
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <exception cref="System.Data.SqlClient.SqlException">
        /// Thrown when the sql string is wrong
        /// </exception>
        public static void CreateLevelTable(string databaseName)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(School.GetConnectionString(databaseName));
                if (con != null)
                {
                    con.Open();
                    string sql = string.Format("CREATE TABLE Level(" +
                        "UUID VARCHAR(50) PRIMARY KEY, " +
                        "Name VARCHAR(50))");
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    Level[] levelList = GetLevelList();
                    foreach (Level level in levelList)
                    {
                        sql = String.Format("INSERT INTO Level(UUID,Name) " +
                        "VALUES('{0}','{1}')", level.UUID, level.Name);
                        cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: Level _ SqlException " + ex.Message);
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

