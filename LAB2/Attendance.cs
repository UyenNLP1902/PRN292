
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;


namespace LAB2
{
    /// <summary>
    /// The Attendance class.
    /// Contains create method and properties of Attendance. 
    /// </summary>
    public class Attendance
    {
        /// <value>
        /// The id of attendance
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The teacher id
        /// </value>
        public string Teacher { get; set; }

        /// <value>
        /// The class id which the teacher teaches
        /// </value>
        public string Class { get; set; }

        /// <value>
        /// The subject id which the teacher teaches
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// An empty constructor for attendance
        /// </summary>
        public Attendance() { }

        /// <summary>
        /// A constructor for attendance
        /// </summary>
        /// <param name="uuid">A string value</param>
        /// <param name="teacher">A string value</param>
        /// <param name="classInfo">A string value</param>
        /// <param name="subject">A string value</param>
        public Attendance(string uuid, string teacher, string classInfo, string subject)
        {
            UUID = uuid;
            Teacher = teacher;
            Class = classInfo;
            Subject = subject;
        }

        /// <summary>
        /// Gets attendance list
        /// </summary>
        /// <returns>An array of attendances</returns>
        public static Attendance[] GetAttendanceList()
        {
            return Util.Config.Attendance;
        }

        /// <summary>
        /// Create Attendance table in database
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <exception cref="System.Data.SqlClient.SqlException">
        /// Thrown when the sql string is wrong
        /// </exception>
        public static void CreateAttendanceTable(string databaseName)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(School.GetConnectionString(databaseName));
                if (con != null)
                {
                    con.Open();
                    string sql = string.Format("CREATE TABLE Attendance(" +
                        "UUID VARCHAR(50) PRIMARY KEY," +
                        "Teacher VARCHAR(50) REFERENCES Teacher(UUID), " +
                        "Class VARCHAR(50) REFERENCES Class(UUID), " +
                        "Subject VARCHAR(50) REFERENCES Subject(UUID))");
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    Attendance[] attendanceList = GetAttendanceList();
                    foreach (Attendance attendance in attendanceList)
                    {
                        bool exist = CheckIfExist(databaseName, attendance);
                        if (!exist)
                        {
                            sql = String.Format("INSERT INTO Attendance(UUID, Teacher, Class, Subject) " +
                                "VALUES('{0}','{1}','{2}','{3}')",
                                attendance.UUID, attendance.Teacher, attendance.Class, attendance.Subject);
                            cmd = new SqlCommand(sql, con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: Attendance _ SqlException " + ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        /// <summary>
        /// Check if a record is exist or not
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <param name="attendance">An Attendance object</param>
        /// <returns>true if exist, false if not</returns>
        private static bool CheckIfExist(string databaseName, Attendance attendance)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(School.GetConnectionString(databaseName));
                if (con != null)
                {
                    con.Open();
                    string sql = String.Format("SELECT UUID " +
                            "FROM Attendance " +
                            "WHERE Teacher = '{0}' AND " +
                            "Class = '{1}' AND " +
                            "Subject = '{2}'", attendance.Teacher, attendance.Class, attendance.Subject);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = true;
                    }
                }
            }
            finally
            {
                con.Close();
            }

            return result;
        }
    }
}