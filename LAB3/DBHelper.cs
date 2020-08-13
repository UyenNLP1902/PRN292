using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB3
{
    public class DBHelper
    {

        public static bool IsDatabaseExist(string databaseName)
        {
            bool result = false;
            string sql = String.Format("SELECT * FROM master.dbo.sysdatabases where name = '{0}'", databaseName);
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                result = reader.HasRows;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error _ DBHelper: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public static DataTable GetData(string tblName)
        {
            string sql = "SELECT * FROM " + tblName;
            DataTable table = new DataTable();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(table);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error _ DBHelper: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return table;
        }


        public static string[] GetTableName()
        {
            List<string> result = new List<string>();

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT name FROM sys.Tables where name != 'sysdiagrams'", con);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    result.Add(reader["name"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error _ DBHelper: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return result.ToArray();
        }

        public static bool DeleteData(string table, string key)
        {
            bool result = false;

            DeleteDependentTable(table, key);
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    string sql = String.Format("DELETE FROM {0} WHERE UUID = '{1}'",
                                table, key);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: DBHelper _ SqlException " + ex.Message);
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

        private static void DeleteDependentTable(string column, object data)
        {
            string[] tables = GetTableName();

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    foreach (string table in tables)
                    {
                        if (CheckColumnExist(column, table))
                        {
                            string sql = string.Format(@"DELETE FROM {0} WHERE {1} = '{2}'", 
                                table, column, data.ToString());

                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: DBHelper _ SqlException " + ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private static bool CheckColumnExist(string column, string table)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();
                    string sql = String.Format(@"SELECT * FROM sys.columns WHERE name = '{0}' AND object_id = OBJECT_ID('{1}')",
                        column, table);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: DBHelper _ SqlException " + ex.Message);
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

        public static bool UpdateData(string table, string key, DataColumnCollection columns, DataRow row)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Util.GetConnectionString());
                if (con != null)
                {
                    con.Open();

                    string sql = GetUpdateString(table, key, columns, row);

                    SqlCommand cmd = new SqlCommand(sql, con);
                    for (int i = 1; i < columns.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + columns[i].ColumnName, row[i]);
                    }
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR: DBHelper _ SqlException " + ex.Message);
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

        private static string GetUpdateString(string table, string key, DataColumnCollection columns, DataRow row)
        {
            string sql = string.Format("UPDATE {0} SET ", table);

            for (int i = 1; i < columns.Count; i++)
            {
                sql += string.Format("{0} = @{0}, ", columns[i].ColumnName);
            }
            sql = sql.Substring(0, sql.Length - 2);
            sql += " WHERE UUID = '" + key + "'";

            return sql;
        }
    }
}
