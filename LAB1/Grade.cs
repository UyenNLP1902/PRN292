using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAB1
{
    /// <summary>
    /// The Grade class.
    /// Contains create method and properties of Grade. 
    /// </summary>
    class Grade
    {
        /// <value>
        /// The id of grade
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The subject of grade
        /// </value>
        public string Subject { get; set; }

        /// <value>
        /// The student of grade
        /// </value>
        public string Student { get; set; }

        /// <value>
        /// The grade
        /// </value>
        public int Point { get; set; }

        /// <summary>
        /// An empty constructor for grade
        /// </summary>
        public Grade() { }

        /// <summary>
        /// A constructor for grade
        /// </summary>
        /// <param name="uuid">A string value</param>
        /// <param name="subject">A string value</param>
        /// <param name="student">A string value</param>
        /// <param name="point">A positive integer value</param>
        public Grade(string uuid, string subject, string student, int point)
        {
            UUID = uuid;
            Subject = subject;
            Student = student;
            Point = point;
        }

        /// <summary>
        /// Creates a random list of grade of each student and returns the result
        /// </summary>
        /// <returns>An array of grade</returns>
        public static Grade[] Create()
        {
            Random rnd = new Random();
            List<Grade> result = new List<Grade>();

            Student[] listOfStudents = DataList.StudentList;

            foreach (Student student in listOfStudents)
            {
                //student id
                string studentId = student.UUID;

                Subject[] subjectList = DataList.GetSubjectByLevel(student.Class);
                foreach (Subject subject in subjectList)
                {
                    //uuid
                    string uuid = Guid.NewGuid().ToString();

                    //subject id
                    string subjectId = subject.UUID;

                    //random point
                    int point = rnd.Next(101);

                    result.Add(new Grade(uuid, subjectId, studentId, point));
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Sort students by grade
        /// </summary>
        /// <returns>A Dictionary(HashMap) of students and their total points</returns>
        public static Dictionary<string, int> GetTotalPointEachStudent()
        {
            Dictionary<string, int> result = null;
            Grade[] list = DataList.GradeList;
            foreach (Grade grade in list)
            {
                if (result == null)
                {
                    result = new Dictionary<string, int>();
                }

                string key = SearchForGrade(result, grade.Student);
                if (key == null)
                {
                    result.Add(grade.Student, grade.Point);
                }
                else
                {
                    result[key] += 1;
                }
            }
            result = result.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return result;
        }

        /// <summary>
        /// Get Grade object by student UUID
        /// </summary>
        /// <param name="dict">A Dictionary value</param>
        /// <param name="key">A string value</param>
        /// <returns></returns>
        private static string SearchForGrade(Dictionary<string, int> dict, string key)
        {
            foreach (string x in dict.Keys)
            {
                if (x.Equals(key))
                {
                    return x;
                }
            }
            return null;
        }
    }
}
