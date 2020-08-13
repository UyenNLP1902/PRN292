using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LAB1
{
    /// <summary>
    /// The Student class
    /// Contains create method and properties of Student.
    /// </summary>
    public class Student
    {
        /// <value>
        /// The id of student
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The name of student
        /// </value>
        public string Name { get; set; }

        /// <value>
        /// The birthday of student
        /// </value>
        public DateTime Birthday { get; set; }

        /// <value>
        /// The gender of student
        /// </value>
        public bool Gender { get; set; }

        /// <value>
        /// The class of student
        /// </value>
        public string Class { get; set; }

        /// <summary>
        /// An empty contrustor for student
        /// </summary>
        public Student() { }

        /// <summary>
        /// A constructor for student
        /// </summary>
        /// <param name="uuid">A string value</param>
        /// <param name="name">A string value</param>
        /// <param name="birthdate">A DateTime value</param>
        /// <param name="gender">A boolean value</param>
        /// <param name="classInfo">A string value</param>
        public Student(string uuid, string name, DateTime birthdate, bool gender, string classInfo)
        {
            UUID = uuid;
            Name = name;
            Birthday = birthdate;
            Gender = gender;
            Class = classInfo;
        }

        /*
        /// <summary>
        /// Creates a random list of students and returns the result
        /// </summary>
        /// <param name="numberOfStudent">An integer value</param>
        /// <returns>An array of students</returns>
        public static Student[] Create(int numberOfStudent)
        {
            List<Student> result = new List<Student>();

            Random rnd = new Random();

            List<string> listOfClasses = FileHelper.GetListOfClass();

            int currentTotal = 0;
            int quantity;
            for (int i = 0; i < listOfClasses.Count; i++)
            {
                if (i == listOfClasses.Count)
                {
                    quantity = numberOfStudent - currentTotal;
                }
                else
                {
                    quantity = rnd.Next(30, 51);
                }

                string[] classs = listOfClasses[i].Split(',');
                string classInfo = classs[0].Trim();
                string levelId = classs[1].Trim();
                string level = FileHelper.GetLevelByPrimaryKey(levelId).Split(',')[1];

                for (int j = 0; j < quantity; j++)
                {
                    //id
                    string uuid = Guid.NewGuid().ToString();

                    //gender
                    bool gender = rnd.Next(2) == 1;

                    //name
                    string fullName = GenerateDataHelper.GetRandomNameByGender(gender);

                    //birthdate
                    DateTime birthdate = GenerateDataHelper.GetRandomBirthdayByLevel(level);

                    result.Add(new Student(uuid, fullName, birthdate, gender, classInfo));
                    currentTotal++;
                }
            }
            return result.ToArray();
        }*/

        /// <summary>
        /// Creates a random list of students and returns the result
        /// </summary>
        /// <param name="numberOfStudent">An integer value</param>
        /// <returns>An array of students</returns>
        public static Student[] Create(int numberOfStudent)
        {
            List<Student> result = new List<Student>();

            Random rnd = new Random();

            Level[] listOfLevel = DataList.LevelList;
            int amountEachLevel = (int)Math.Ceiling((double)numberOfStudent / listOfLevel.Length) + 1;
            int levelIndex = 0;
            int count = 0;

            for (int i = 0; i < numberOfStudent; i++)
            {
                //id
                string uuid = Guid.NewGuid().ToString();

                //gender
                bool gender = rnd.Next(2) == 1;

                //name
                string fullName = GenerateDataHelper.GetRandomNameByGender(gender);

                //birthdate
                string level = listOfLevel[levelIndex].Name;
                DateTime birthdate = GenerateDataHelper.GetRandomBirthdayByLevel(level);

                result.Add(new Student(uuid, fullName, birthdate, gender, listOfLevel[levelIndex].UUID));
                if (count == amountEachLevel)
                {
                    levelIndex++;
                    amountEachLevel--;
                    count = 0;
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Search for Student object by UUID 
        /// </summary>
        /// <param name="uuid">A string value</param>
        /// <returns>A Student object if found, null if not.</returns>
        private static Student SearchStudent(string uuid)
        {
            Student[] students = DataList.StudentList;
            foreach (Student s in students)
            {
                if (s.UUID.Equals(uuid))
                {
                    return s;
                }
            }
            return null;
        }

        /// <summary>
        /// Updates the class student in, sort by the lowest to highest total points
        /// </summary>
        /// <param name="topGradeList">A Dictionary value</param>
        public static void UpdateStudentClass(Dictionary<string, int> topGradeList)
        {
            int numStudentEachClass = (int)Math.Ceiling((double)DataList.StudentList.Length / (double)DataList.RoomList.Length);

            List<Student> g10 = new List<Student>();
            List<Student> g11 = new List<Student>();
            List<Student> g12 = new List<Student>();

            foreach (KeyValuePair<string, int> x in topGradeList)
            {
                Student student = SearchStudent(x.Key);
                if (student.Class.Equals(DataList.FieldList[0].UUID))
                {
                    g10.Add(student);
                }
                else if (student.Class.Equals(DataList.FieldList[1].UUID))
                {
                    g11.Add(student);
                }
                else
                {
                    g12.Add(student);
                }
            }

            int count = 0;
            int index = 0;
            foreach (Student s in g10)
            {
                count++;
                s.Class = DataList.ClassList[index].UUID;

                if (count == numStudentEachClass)
                {
                    count = 0;
                    index++;
                }
            }

            count = 0;
            foreach (Student s in g11)
            {
                count++;
                s.Class = DataList.ClassList[index].UUID;

                if (count == numStudentEachClass)
                {
                    count = 0;
                    index++;
                }
            }

            count = 0;
            foreach (Student s in g12)
            {
                count++;
                s.Class = DataList.ClassList[index].UUID;

                if (count == numStudentEachClass)
                {
                    count = 0;
                    index++;
                }
            }
        }
    }
}
