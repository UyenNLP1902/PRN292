using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LAB1
{
    class DataList
    {
        /// <value>
        /// An array of Level
        /// </value>
        public static Level[] LevelList { get; set; }

        /// <value>
        /// An array of Field
        /// </value>
        public static Field[] FieldList { get; set; }

        /// <value>
        /// An array of Room
        /// </value>
        public static Room[] RoomList { get; set; }

        /// <value>
        /// An array of Class
        /// </value>
        public static Class[] ClassList { get; set; }

        /// <value>
        /// An array of Subject
        /// </value>
        public static Subject[] SubjectList { get; set; }

        /// <value>
        /// An array of Student
        /// </value>
        public static Student[] StudentList { get; set; }

        /// <value>
        /// An array of Teacher
        /// </value>
        public static Teacher[] TeacherList { get; set; }

        /// <value>
        /// A list of Attendance
        /// </value>
        public static List<Attendance> AttendanceList { get; set; }

        /// <value>
        /// An array of Grade
        /// </value>
        public static Grade[] GradeList { get; set; }

        /// <summary>
        /// Create data lists
        /// </summary>
        /// <param name="student">A number of student</param>
        /// <param name="room">A number of room</param>
        public static void CreateDataList(int student, int room)
        {
            LevelList = Level.Create();
            FieldList = Field.Create();
            Dictionary<string, string> uuids = CreateUUIDForRoomAndClass(room);
            RoomList = Room.Create(uuids);
            ClassList = Class.Create(uuids);
            SubjectList = Subject.Create();
            StudentList = Student.Create(student);
            TeacherList = Teacher.Create();
            GradeList = Grade.Create();
            Dictionary<string, int> topGrade = Grade.GetTotalPointEachStudent();
            Student.UpdateStudentClass(topGrade);
        }

        /// <summary>
        /// Create uuid for both Room and Class
        /// </summary>
        /// <param name="size">A positive integer number</param>
        /// <returns>A dictionary which roomId is key and classId is value</returns>
        public static Dictionary<string, string> CreateUUIDForRoomAndClass(int size)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            for (int i = 0; i < size; i++)
            {
                string room = Guid.NewGuid().ToString();
                string classinfo = Guid.NewGuid().ToString();
                result.Add(room, classinfo);
            }
            return result;
        }


        /// <summary>
        /// Get subjects by level
        /// </summary>
        /// <param name="level">A string value</param>
        /// <returns>An array of subject</returns>
        public static Subject[] GetSubjectByLevel(string level)
        {
            List<Subject> result = null;
            if (SubjectList != null)
            {
                foreach (Subject subject in SubjectList)
                {
                    if (subject.Level.Equals(level))
                    {
                        if (result == null)
                        {
                            result = new List<Subject>();
                        }
                        result.Add(subject);
                    }
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Get subjects by level
        /// </summary>
        /// <param name="level">A string value</param>
        /// <returns>An array of subject</returns>
        public static Class[] GetClassByLevel(string level)
        {
            List<Class> result = null;
            if (ClassList != null)
            {
                foreach (Class classs in ClassList)
                {
                    if (classs.Level.Equals(level))
                    {
                        if (result == null)
                        {
                            result = new List<Class>();
                        }

                        result.Add(classs);
                    }
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Check if a class have learn a subject
        /// </summary>
        /// <param name="classId">A string value</param>
        /// <param name="subjectId">A string value</param>
        /// <returns>True if already learn, false if not.</returns>
        public static bool IsClassHaveAttendSubject(string classId, string subjectId)
        {
            bool result = false;
            if (AttendanceList != null)
            {
                foreach (Attendance attendance in AttendanceList)
                {
                    if (attendance.Subject.Equals(subjectId) && attendance.Class.Equals(classId))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
