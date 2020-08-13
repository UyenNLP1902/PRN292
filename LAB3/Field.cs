
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace LAB3
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

        public Field(string name)
        {
            UUID = Guid.NewGuid().ToString();
            Name = name;
        }

        public static bool CreateField(Field field)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    string sql = String.Format("INSERT INTO Field(UUID,Name) " +
                        "VALUES('{0}','{1}')", field.UUID, field.Name);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    result = cmd.ExecuteNonQuery() > 0;

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
            return result;
        }
    }
}