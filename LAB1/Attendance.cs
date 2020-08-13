using System;
using System.Collections.Generic;

namespace LAB1
{
    /// <summary>
    /// The Attendance class
    /// Contains create method and properties of Attendance. 
    /// </summary>
    public class Attendance
    {
        /// <value>
        /// The id of attendance
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The teacher id
        /// </value>
        public string Teacher { get; set; }

        /// <value>
        /// The class id which the teacher teaches
        /// </value>
        public string Class { get; set; }

        /// <value>
        /// The subject id which the teacher teaches
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// An empty constructor for attendance
        /// </summary>
        public Attendance() { }

        /// <summary>
        /// A constructor for attendance
        /// </summary>
        /// <param name="uuid">A string value</param>
        /// <param name="teacher">A string value</param>
        /// <param name="classInfo">A string value</param>
        /// <param name="subject">A string value</param>
        public Attendance(string uuid, string teacher, string classInfo, string subject)
        {
            UUID = uuid;
            Teacher = teacher;
            Class = classInfo;
            Subject = subject;
        }
        /*
        /// <summary>
        /// Creates a list of data teachers teach which classes and which subjects; and return the result
        /// </summary>
        /// <returns>An array of Attendance</returns>
        public static Attendance[] Create()
        {
            Random rnd = new Random();
            List<string> listOfTeacher = FileHelper.GetListOfTeacher();
            List<Attendance> list = new List<Attendance>();
            int index;

            for (int i = 0; i < listOfTeacher.Count; i++)
            {
                //teacher
                string[] teacherInfo = listOfTeacher[i].Split(',');
                string teacher = teacherInfo[0].Trim();
                string field = teacherInfo[3].Trim();
                int numberOfClass = rnd.Next(4, 10);

                for (int j = 0; j < numberOfClass; j++)
                {
                    //subject
                    List<string> listOfSubject = FileHelper.GetListOfSubjectByField(field);
                    index = rnd.Next(listOfSubject.Count);
                    string[] subjectInfo = listOfSubject[index].Split(',');
                    string subject = subjectInfo[0];
                    string level = subjectInfo[1];

                    //class
                    List<string> listOfClass = FileHelper.GetListOfClassByLevel(level);
                    index = rnd.Next(listOfClass.Count);
                    string[] classes = listOfClass[index].Split(',');
                    string classInfo = classes[0];

                    list.Add(new Attendance(teacher, classInfo, subject));
                }
            }
            return list.ToArray();
        }*/

        /// <summary>
        /// Creates a list of data teachers teach which classes and which subjects; and return the result
        /// Creates a list of classes the teacher teaches
        /// </summary>
        /// <param name="teacherId">A string value</param>
        /// <param name="fieldId">A string value</param>
        /// <returns></returns>
        public static int Create(string teacherId, string fieldId)
        {
            Random rnd = new Random();
            Class[] listOfClasses = DataList.ClassList;

            //teacherId

            //class
            int number = rnd.Next(4, 11);
            int count = 0;
            foreach (Class classs in listOfClasses)
            {
                string subjectId = LAB1.Subject.SearchSubject(classs.Level, fieldId).UUID;
                bool check = DataList.IsClassHaveAttendSubject(classs.UUID, subjectId);
                if (!check)
                {
                    count++;
                    string classId = classs.UUID;

                    //uuid
                    string uuid = Guid.NewGuid().ToString();

                    if (DataList.AttendanceList == null)
                    {
                        DataList.AttendanceList = new List<Attendance>();
                    }

                    DataList.AttendanceList.Add(new Attendance(uuid, teacherId, classId, subjectId));

                    if (count >= number)
                    {
                        break;
                    }
                }
            }
            return count;
        }

    }
}