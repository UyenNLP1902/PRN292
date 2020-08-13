using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace LAB3
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

        public Teacher(string name, bool gender, string field)
        {
            UUID = Guid.NewGuid().ToString();
            Name = name;
            Gender = gender;
            Field = field;
        }

        public static bool CreateTeacher(Teacher teacher)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    string sql = string.Format("INSERT INTO Teacher(UUID, Name, Gender, Field) " +
                        "VALUES('{0}','{1}','{2}','{3}')",
                        teacher.UUID, teacher.Name, teacher.Gender, teacher.Field);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    result = cmd.ExecuteNonQuery() > 0;
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
            return result;
        }
    }
}
