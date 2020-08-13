using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;


namespace LAB3
{
    /// <summary>
    /// The Student class
    /// Contains create method and properties of Student.
    /// </summary>
    public class Student
    {
        /// <value>
        /// The id of student
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The name of student
        /// </value>
        public string Name { get; set; }

        /// <value>
        /// The birthday of student
        /// </value>
        public DateTime Birthday { get; set; }

        /// <value>
        /// The gender of student
        /// </value>
        public bool Gender { get; set; }

        /// <value>
        /// The class of student
        /// </value>
        public string Class { get; set; }

        /// <summary>
        /// An empty contrustor for student
        /// </summary>
        public Student() { }

        /// <summary>
        /// A constructor for student
        /// </summary>
        /// <param name="id">A string value</param>
        /// <param name="name">A string value</param>
        /// <param name="birthdate">A DateTime value></param>
        /// <param name="gender">A boolean value></param>
        /// <param name="classInfo">A string value</param>
        public Student(string id, string name, DateTime birthdate, bool gender, string classInfo)
        {
            UUID = id;
            Name = name;
            Birthday = birthdate;
            Gender = gender;
            Class = classInfo;
        }

        public Student(string name, DateTime birthdate, bool gender, string classInfo)
        {
            UUID = Guid.NewGuid().ToString();
            Name = name;
            Birthday = birthdate;
            Gender = gender;
            Class = classInfo;
        }

        public static bool CreateStudent(Student student)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    string sql = String.Format("INSERT INTO Student(UUID, Name, Birthday, Gender, Class) " +
                        "VALUES('{0}','{1}','{2}','{3}','{4}')",
                        student.UUID, student.Name, student.Birthday, student.Gender, student.Class);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: Student _ SqlException " + ex.Message);
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
