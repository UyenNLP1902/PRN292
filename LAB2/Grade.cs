using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LAB2
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


        /// <summary>
        /// Gets grade list
        /// </summary>
        /// <returns>An array of grades</returns>
        public static Grade[] GetGradeList()
        {
            return Util.Config.Grade;
        }

        /// <summary>
        /// Create Grade table in database
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
                    string sql = string.Format("CREATE TABLE Grade(" +
                        "UUID VARCHAR(50) PRIMARY KEY, " +
                        "Subject VARCHAR(50) REFERENCES Subject(UUID), " +
                        "Student VARCHAR(50) REFERENCES Student(UUID), " +
                        "Point INTEGER)");
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    Grade[] GradeList = GetGradeList();
                    foreach (Grade grade in GradeList)
                    {
                        bool exist = CheckIfExist(databaseName, grade);
                        if (!exist)
                        {
                            sql = String.Format("INSERT INTO Grade(UUID, Subject, Student, Point) " +
                                    "VALUES('{0}','{1}','{2}','{3}')",
                                    grade.UUID, grade.Subject, grade.Student, grade.Point);
                            cmd = new SqlCommand(sql, con);
                            cmd.ExecuteNonQuery();
                        }

                    }
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
        }

        /// <summary>
        /// Check if a record is exist or not
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <param name="grade">An GradeClass object</param>
        /// <returns>true if exist, false if not</returns>
        private static bool CheckIfExist(string databaseName, Grade grade)
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
                            "FROM Grade " +
                            "WHERE Subject = '{0}' AND " +
                            "Student = '{1}'",
                            grade.Subject, grade.Student);
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
