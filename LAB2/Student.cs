using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;


namespace LAB2
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

        /// <summary>
        /// Gets student list
        /// </summary>
        /// <returns>An array of students</returns>
        public static Student[] GetStudentList()
        {
            return Util.Config.Student;
        }

        /// <summary>
        /// Create Student table in database
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <exception cref="System.Data.SqlClient.SqlException">
        /// Thrown when the sql string is wrong
        /// </exception>
        public static void CreateStudentTable(string databaseName)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(School.GetConnectionString(databaseName));
                if (con != null)
                {
                    con.Open();
                    string sql = string.Format("CREATE TABLE Student(" +
                        "UUID VARCHAR(50) PRIMARY KEY, " +
                        "Name VARCHAR(50)," +
                        "Birthday Datetime," +
                        "Gender BIT," +
                        "Class VARCHAR(50) REFERENCES Class(UUID))");
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    Student[] studentList = GetStudentList();
                    foreach (Student student in studentList)
                    {
                        sql = String.Format("INSERT INTO Student(UUID, Name, Birthday, Gender, Class) " +
                        "VALUES('{0}','{1}','{2}','{3}','{4}')", 
                        student.UUID, student.Name, student.Birthday, student.Gender, student.Class);
                        cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
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
        }
    }
}
