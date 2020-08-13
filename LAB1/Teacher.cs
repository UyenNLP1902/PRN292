using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace LAB1
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
        /// <param name="uuid">A string value</param>
        /// <param name="name">A string value</param>
        /// <param name="gender">A boolean value</param>
        /// <param name="field">A string value</param>
        public Teacher(string uuid, string name, bool gender, string field)
        {
            UUID = uuid;
            Name = name;
            Gender = gender;
            Field = field;
        }

        /*
        /// <summary>
        /// Creates a random list of teachers and returns the result
        /// </summary>
        /// <param name="numberOfTeacher">An integer value</param>
        /// <returns>An array of teachers</returns>
        public static Teacher[] Create(int numberOfTeacher)
        {
            List<Teacher> result = new List<Teacher>();

            List<string> listOfField = FileHelper.GetListOfField();
            Random rnd = new Random();

            for (uint i = 0; i < numberOfTeacher; i++)
            {
                //id
                String uuid = Guid.NewGuid().ToString();

                //gender
                bool gender = rnd.Next(2) == 1;

                //name
                string fullName = GenerateDataHelper.GetRandomNameByGender(gender);

                //Field
                int index = rnd.Next(listOfField.Count);
                string[] fields = listOfField[index].Split(',');
                string field = fields[0].Trim();

                result.Add(new Teacher(uuid, fullName, gender, field));
            }
            return result.ToArray();
        }*/

        /// <summary>
        /// Creates a random list of teachers and returns the result
        /// </summary>
        /// <returns>An array of teachers</returns>
        public static Teacher[] Create()
        {
            List<Teacher> result = new List<Teacher>();

            Field[] listOfField = DataList.FieldList;
            Random rnd = new Random();

            while(true)
            {
                //id
                string uuid = Guid.NewGuid().ToString();

                //gender
                bool gender = rnd.Next(2) == 1;

                //name
                string fullName = GenerateDataHelper.GetRandomNameByGender(gender);

                //field
                int index = rnd.Next(listOfField.Length);
                string field = listOfField[index].UUID;

                Teacher x = new Teacher(uuid, fullName, gender, field);
                result.Add(x);
                int num = Attendance.Create(uuid, field);
                if (num == 0)
                {
                    result.Remove(x);
                    break;
                }
            }
            return result.ToArray();
        }
    }
}
