
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;


namespace LAB3
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

        public Class(string level, string room, string name)
        {
            UUID = Guid.NewGuid().ToString();
            Level = level;
            Room = room;
            Name = name;
        }

        public Class(string room, string name)
        {
            UUID = Guid.NewGuid().ToString();
            Room = room;
            Name = name;
        }

        public static bool CreateClass(Class @class)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    string sql;
                    if (@class.Level == null)
                    {
                        sql = string.Format("INSERT INTO Class(UUID, Room, Name) " +
                            "VALUES('{0}','{1}','{2}')",
                            @class.UUID, @class.Room, @class.Name);
                    }
                    else
                    {
                        sql = string.Format("INSERT INTO Class(UUID, Level, Room, Name) " +
                            "VALUES('{0}','{1}','{2}','{3}')",
                            @class.UUID, @class.Level, @class.Room, @class.Name);
                    }
                    SqlCommand cmd = new SqlCommand(sql, con);
                    result = cmd.ExecuteNonQuery() > 0;
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
            return result;
        }
    }
}