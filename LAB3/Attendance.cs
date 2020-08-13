
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;


namespace LAB3
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

        public Attendance(string teacher, string @class, string subject)
        {
            UUID = Guid.NewGuid().ToString();
            Teacher = teacher;
            Class = @class;
            Subject = subject;
        }

        public static bool CreateAttendance(Attendance attendance)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    string sql = String.Format("INSERT INTO Attendance(UUID, Teacher, Class, Subject) " +
                                "VALUES('{0}','{1}','{2}','{3}')",
                                attendance.UUID, attendance.Teacher, attendance.Class, attendance.Subject);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    result = cmd.ExecuteNonQuery() > 0;
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
            return result;
        }
    }
}