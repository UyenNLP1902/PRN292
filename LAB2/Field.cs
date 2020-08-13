
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace LAB2
{
    /// <summary>
    /// The Field class.
    /// Contains create method and properties of Field. 
    /// </summary>
    public class Field
    {
        /// <value>
        /// The id of field
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The name of field
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// An empty constructor for field
        /// </summary>
        public Field() { }

        /// <summary>
        /// A constructor for field
        /// </summary>
        /// <param name="id">A string value</param>
        /// <param name="name">A string value</param>
        public Field(string id, string name)
        {
            UUID = id;
            Name = name;
        }

        /// <summary>
        /// Gets field list
        /// </summary>
        /// <returns>An array of fields</returns>
        public static Field[] GetFieldList()
        {
            return Util.Config.Field;
        }

        /// <summary>
        /// Create Field table in database
        /// </summary>
        /// <param name="databaseName">A string value</param>
        /// <exception cref="System.Data.SqlClient.SqlException">
        /// Thrown when the sql string is wrong
        /// </exception>
        public static void CreateFieldTable(string databaseName)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(School.GetConnectionString(databaseName));
                if (con != null)
                {
                    con.Open();
                    string sql = string.Format("CREATE TABLE Field(" +
                        "UUID VARCHAR(50) PRIMARY KEY, " +
                        "Name VARCHAR(50))");
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    Field[] fieldList = GetFieldList();
                    foreach (Field field in fieldList)
                    {
                        sql = String.Format("INSERT INTO Field(UUID,Name) " +
                        "VALUES('{0}','{1}')", field.UUID, field.Name);
                        cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: Field _ SqlException " + ex.Message);
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