using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace LAB1
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
        /// <param name="uuid">A string value</param>
        /// <param name="level">A string value</param>
        /// <param name="field">A string value</param>
        public Subject(string uuid, string level, string field)
        {
            UUID = uuid;
            Level = level;
            Field = field;
        }
        /*
        /// <summary>
        /// Creates a random list of subjects and returns the result
        /// </summary>
        /// <returns>An array of subjects</returns>
        public static Subject[] Create()
        {
            List<string> listOfLevels = FileHelper.GetListOfLevel();
            List<string> listOfFields = FileHelper.GetListOfField();

            List<Subject> result = new List<Subject>();

            for (int i = 0; i < listOfLevels.Count; i++)
            {
                //int numberOfField = rnd.Next(8, 10);
                //for(int j = 0; j < numberOfField; j++)
                for (int j = 0; j < listOfFields.Count; j++)
                {
                    //id
                    string uuid = Guid.NewGuid().ToString();

                    //level
                    string line = listOfLevels[i];
                    string[] levels = line.Split(',');
                    string level = levels[0].Trim();

                    //field
                    string[] fields = listOfFields[j].Split(',');
                    string field = fields[0].Trim();

                    result.Add(new Subject(uuid, level, field));
                }
            }

            return result.ToArray();
        }*/

        /// <summary>
        /// Creates a random list of subjects and returns the result
        /// </summary>
        /// <returns>An array of subjects</returns>
        public static Subject[] Create()
        {
            Level[] listOfLevels = DataList.LevelList;
            Field[] listOfFields = DataList.FieldList;

            List<Subject> result = new List<Subject>();

            for (int i = 0; i < listOfLevels.Length; i++)
            {
                for (int j = 0; j < listOfFields.Length; j++)
                {
                    //id
                    string uuid = Guid.NewGuid().ToString();

                    //level
                    string level = listOfLevels[i].UUID;

                    //field
                    string field = listOfFields[j].UUID;

                    result.Add(new Subject(uuid, level, field));
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Search for Subject object by level and field
        /// </summary>
        /// <param name="level">A string value</param>
        /// <param name="field">A string value</param>
        /// <returns>A Subject object if found, null if not.</returns>
        public static Subject SearchSubject(string level, string field)
        {
            Subject result = null;

            foreach(Subject subject in DataList.SubjectList)
            {
                if (subject.Level.Equals(level) && subject.Field.Equals(field))
                {
                    result = subject;
                    break;
                }
            }

            return result;
        }
    }
}
