
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace LAB3
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
        /// A constructor for level
        /// </summary>
        /// <param name="name">A string value</param>
        public Level(string name)
        {
            UUID = Guid.NewGuid().ToString();
            Name = name;
        }

        public static bool CreateLevel(Level level)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    string sql = String.Format("INSERT INTO Level(UUID,Name) " +
                        "VALUES('{0}','{1}')", level.UUID, level.Name);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    result = cmd.ExecuteNonQuery() > 0;

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
            return result;
        }
    }
}

