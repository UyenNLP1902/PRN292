using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace LAB1
{
    /// <summary>
    /// The Field class
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
        /// <param name="uuid">A string value</param>
        /// <param name="name">A string value</param>
        public Field(string uuid, string name)
        {
            UUID = uuid;
            Name = name;
        }

        /// <summary>
        /// Creates a list of fields and returns the result
        /// </summary>
        /// <returns>An array of fields</returns>
        /// <exception cref="System.IO.FileNotFoundException">
        /// Thrown when the Json file is not exist.
        /// </exception>
        /// <exception cref="System.NullReferenceException">
        /// Thrown when the object in Json file is not exist or has no data.
        /// </exception>
        public static Field[] Create()
        {
            try
            {
                List<Field> result = new List<Field>();
                string content = File.ReadAllText(@"Configure.json");
                Configure config = JsonSerializer.Deserialize<Configure>(content);
                FieldNameConfig _ = config.FieldNameConfig;

                int size = _.field_name_set.Length;

                for (int i = 0; i < size; i++)
                {
                    string name = _.field_name_set[i].ToString();
                    string uuid = Guid.NewGuid().ToString();
                    result.Add(new Field(uuid, name));
                }
                return result.ToArray();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERROR: Field _ FileNotFoundException: " + ex.Message);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("ERROR: Field _ NullReferenceException: " + ex.Message);
            }
            return null;
        }
    }
}
