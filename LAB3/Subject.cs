using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace LAB3
{
    /// <summary>
    /// The Subject class
    /// Contains create method and properties of Subject. 
    /// </summary>
    public class Subject
    {
        /// <value>
        /// The id the subject
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The level the subject
        /// </value>
        public string Level { get; set; }

        /// <value>
        /// The field the subject
        /// </value>
        public string Field { get; set; }

        /// <summary>
        /// An empty constructor for subject
        /// </summary>
        public Subject() { }

        /// <summary>
        /// A constructor for subject
        /// </summary>
        /// <param name="id">A string value</param>
        /// <param name="level">A string value</param>
        /// <param name="field">A string value</param>
        public Subject(string id, string level, string field)
        {
            UUID = id;
            Level = level;
            Field = field;
        }

        public Subject(string level, string field)
        {
            UUID = Guid.NewGuid().ToString();
            Level = level;
            Field = field;
        }

        public static bool CreateSubject(Subject subject)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    string sql = string.Format("INSERT INTO Subject(UUID, Level, Field) " +
                        "VALUES('{0}','{1}','{2}')",
                        subject.UUID, subject.Level, subject.Field);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: Subject _ SqlException " + ex.Message);
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
