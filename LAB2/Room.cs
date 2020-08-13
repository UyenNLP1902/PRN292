using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;


namespace LAB2
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
        public Room(string id,  string classInfo, int no)
        {
            UUID = id;
            Class = classInfo;
            No = no;
        }

        /// <summary>
        /// Gets room list
        /// </summary>
        /// <returns>An array of rooms</returns>
        public static Room[] GetRoomList()
        {
            return Util.Config.Room;
        }

        /// <summary>
        /// Create Room table in database
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <exception cref="System.Data.SqlClient.SqlException">
        /// Thrown when the sql string is wrong
        /// </exception>
        public static void CreateRoomTable(string databaseName)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(School.GetConnectionString(databaseName));
                if (con != null)
                {
                    con.Open();
                    string sql = string.Format("CREATE TABLE Room(" +
                        "UUID VARCHAR(50) PRIMARY KEY, " +
                        "Class VARCHAR(50)," +
                        "No INTEGER)");
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    Room[] roomList = GetRoomList();
                    foreach (Room room in roomList)
                    {
                        sql = String.Format("INSERT INTO Room(UUID, Class, No) " +
                        "VALUES('{0}','{1}','{2}')", room.UUID, room.Class, room.No);
                        cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
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
        }
    }
}
