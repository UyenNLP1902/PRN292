using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;


namespace LAB3
{
    /// <summary>
    /// The Room class
    /// Contains create method and properties of Room. 
    /// </summary>
    public class Room
    {
        /// <value>
        /// The id of room
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The class that use this room
        /// </value>
        public string Class { get; set; }

        /// <value>
        /// The room number
        /// </value>
        public int No { get; set; }

        /// <summary>
        /// An empty constructor for room
        /// </summary>
        public Room() { }

        /// <summary>
        /// A constructor for room
        /// </summary>
        /// <param name="id">A string value</param>
        /// <param name="classInfo">A string value</param>
        /// <param name="no">A positive integer value></param>
        public Room(string id, string classInfo, int no)
        {
            UUID = id;
            Class = classInfo;
            No = no;
        }

        public Room(string @class, int no)
        {
            UUID = Guid.NewGuid().ToString();
            Class = @class;
            No = no;
        }

        public static bool CreateRoom(Room room)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    string sql = String.Format("INSERT INTO Room(UUID, Class, No) " +
                        "VALUES('{0}','{1}','{2}')", 
                        room.UUID, room.Class, room.No);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: Room _ SqlException " + ex.Message);
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
