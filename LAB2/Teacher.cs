using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace LAB2
{
    /// <summary>
    /// The Teacher class
    /// Contains create method and properties of Teacher. 
    /// </summary>
    public class Teacher
    {
        /// <value>
        /// The id of teacher
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The name of teacher
        /// </value>
        public string Name { get; set; }

        /// <value>
        /// The gender of teacher
        /// </value>
        public bool Gender { get; set; }

        /// <value>
        /// The field of teacher
        /// </value>
        public string Field { get; set; }

        /// <summary>
        /// An empty constructor for teacher
        /// </summary>
        public Teacher() { }

        /// <summary>
        /// A constructor for teacher
        /// </summary>
        /// <param name="id">A string value</param>
        /// <param name="name">A string value</param>
        /// <param name="gender">A boolean value></param>
        /// <param name="field">A string value</param>
        public Teacher(string id, string name, bool gender, string field)
        {
            UUID = id;
            Name = name;
            Gender = gender;
            Field = field;
        }

        /// <summary>
        /// Gets teacher list
        /// </summary>
        /// <returns>An array of teachers</returns>
        public static Teacher[] GetTeacherList()
        {
            return Util.Config.Teacher;
        }

        /// <summary>
        /// Create Teacher table in database
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <exception cref="System.Data.SqlClient.SqlException">
        /// Thrown when the sql string is wrong
        /// </exception>
        public static void CreateTeacherTable(string databaseName)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(School.GetConnectionString(databaseName));
                if (con != null)
                {
                    con.Open();
                    string sql = string.Format("CREATE TABLE Teacher(" +
                        "UUID VARCHAR(50) PRIMARY KEY, " +
                        "Name VARCHAR(50)," +
                        "Gender BIT," +
                        "Field VARCHAR(50) REFERENCES Field(UUID))");
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    Teacher[] teacherList = GetTeacherList();
                    foreach (Teacher teacher in teacherList)
                    {
                        sql = String.Format("INSERT INTO Teacher(UUID, Name, Gender, Field) " +
                        "VALUES('{0}','{1}','{2}','{3}')",
                        teacher.UUID, teacher.Name, teacher.Gender, teacher.Field);
                        cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: Teacher _ SqlException " + ex.Message);
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
