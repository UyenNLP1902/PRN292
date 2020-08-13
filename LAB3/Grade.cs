using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LAB3
{
    /// <summary>
    /// The Grade class.
    /// Contains create method and properties of Grade. 
    /// </summary>
    public class Grade
    {
        /// <value>
        /// The id of grade
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The subject of grade
        /// </value>
        public string Subject { get; set; }

        /// <value>
        /// The student of grade
        /// </value>
        public string Student { get; set; }

        /// <value>
        /// The grade
        /// </value>
        public int Point { get; set; }

        /// <summary>
        /// An empty constructor for grade
        /// </summary>
        public Grade() { }

        /// <summary>
        /// A constructor for grade
        /// </summary>
        /// <param name="uuid">A string value</param>
        /// <param name="subject">A string value</param>
        /// <param name="student">A string value</param>
        /// <param name="point">A positive integer value</param>
        public Grade(string uuid, string subject, string student, int point)
        {
            UUID = uuid;
            Subject = subject;
            Student = student;
            Point = point;
        }

        public Grade(string subject, string student, int point)
        {
            UUID = Guid.NewGuid().ToString();
            Subject = subject;
            Student = student;
            Point = point;
        }

        public static bool CreateGrade(Grade grade)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    string sql = String.Format("INSERT INTO Grade(UUID, Subject, Student, Point) " +
                                    "VALUES('{0}','{1}','{2}','{3}')",
                                    grade.UUID, grade.Subject, grade.Student, grade.Point);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: Grade _ SqlException " + ex.Message);
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
