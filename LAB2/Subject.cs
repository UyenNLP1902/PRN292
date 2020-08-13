using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace LAB2
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

        /// <summary>
        /// Gets subject list
        /// </summary>
        /// <returns>An array of subjects</returns>
        public static Subject[] GetSubjectList()
        {
            return Util.Config.Subject;
        }

        /// <summary>
        /// Create Subject table in database
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <exception cref="System.Data.SqlClient.SqlException">
        /// Thrown when the sql string is wrong
        /// </exception>
        public static void CreateSubjectTable(string databaseName)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(School.GetConnectionString(databaseName));
                if (con != null)
                {
                    con.Open();
                    string sql = string.Format("CREATE TABLE Subject(" +
                        "UUID VARCHAR(50) PRIMARY KEY, " +
                        "Level VARCHAR(50) REFERENCES Level(UUID)," +
                        "Field VARCHAR(50) REFERENCES Field(UUID))");
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    Subject[] subjectList = GetSubjectList();
                    foreach (Subject subject in subjectList)
                    {
                        bool exist = CheckIfExist(databaseName, subject);
                        if (!exist)
                        {
                            sql = String.Format("INSERT INTO Subject(UUID, Level, Field) " +
                                                    "VALUES('{0}','{1}','{2}')", subject.UUID, subject.Level, subject.Field);
                            cmd = new SqlCommand(sql, con);
                            cmd.ExecuteNonQuery();
                        }
                    }
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
        }

        /// <summary>
        /// Check if a record is exist or not
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <param name="subject">An Subject object</param>
        /// <returns>true if exist, false if not</returns>
        private static bool CheckIfExist(string databaseName, Subject subject)
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
                            "FROM Subject " +
                            "WHERE Level = '{0}' AND " +
                            "Field = '{1}'", subject.Level, subject.Field);
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
