using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace LAB1
{
    /// <summary>
    /// The School class.
    /// Contains methods to save file.
    /// </summary>
    class School
    {
        /// <summary>
        /// The name of the school
        /// </summary>
        private string _school_name;

        /// <summary>
        /// An empty constructor for school
        /// </summary>
        public School() { }

        /// <summary>
        /// A constructor for school
        /// </summary>
        /// <param name="schoolName">A string value</param>
        public School(string schoolName)
        {
            _school_name = schoolName;
        }

        /// <value>
        /// A list of students
        /// </value>
        public List<Student> Student { get; set; }

        /// <value>
        /// A list of teachers
        /// </value>
        public List<Teacher> Teacher { get; set; }

        /// <value>
        /// A list of levels
        /// </value>
        public List<Level> Level { get; set; }

        /// <value>
        /// A list of fields
        /// </value>
        public List<Field> Field { get; set; }

        /// <value>
        /// A list of rooms
        /// </value>
        public List<Room> Room { get; set; }

        /// <value>
        /// A list of classes
        /// </value>
        public List<Class> Class { get; set; }

        /// <value>
        /// A list of subjects
        /// </value>
        public List<Subject> Subject { get; set; }

        /// <value>
        /// A list of attendances
        /// </value>
        public List<Attendance> Attendance { get; set; }

        /// <value>
        /// A list of grades
        /// </value>
        public List<Grade> Grade { get; set; }

        /// <summary>
        /// Save list of student to .csv file
        /// </summary>
        /// <param name="filename">A string value</param>
        public void SaveStudent(string filename)
        {
            string content = "UUID,Name,Birthday,Gender,Class\n";
            foreach (Student student in Student)
            {
                content += student.UUID + ",";
                content += student.Name + ",";
                content += student.Birthday.ToString("dd/MM/yyyy") + ",";
                content += student.Gender + ",";
                content += student.Class + "\n";
            }
            File.WriteAllText(filename, content);
        }

        /// <summary>
        /// Save list of teacher to .csv file
        /// </summary>
        /// <param name="filename"></param>
        public void SaveTeacher(string filename)
        {
            string content = "UUID,Name,Gender,Field\n";
            foreach (Teacher teacher in Teacher)
            {
                content += teacher.UUID + ",";
                content += teacher.Name + ",";
                content += teacher.Gender + ",";
                content += teacher.Field + "\n";
            }
            File.WriteAllText(filename, content);
        }

        /// <summary>
        /// Save list of level to .csv file
        /// </summary>
        /// <param name="filename"></param>
        public void SaveLevel(string filename)
        {
            string content = "UUID,Name\n";
            foreach (Level level in Level)
            {
                content += level.UUID + ",";
                content += level.Name + "\n";
            }
            File.WriteAllText(filename, content);
        }

        /// <summary>
        /// Save list of field to .csv file
        /// </summary>
        /// <param name="filename"></param>
        public void SaveField(string filename)
        {
            string content = "UUID,Name\n";
            foreach (Field field in Field)
            {
                content += field.UUID + ",";
                content += field.Name + "\n";
            }
            File.WriteAllText(filename, content);
        }

        /// <summary>
        /// Save list of room to .csv file
        /// </summary>
        /// <param name="filename"></param>
        public void SaveRoom(string filename)
        {
            string content = "UUID,Class,No\n";
            foreach (Room room in Room)
            {
                content += room.UUID + ",";
                content += room.Class + ",";
                content += room.No + "\n";
            }
            File.WriteAllText(filename, content);
        }

        /// <summary>
        /// Save list of class to .csv file
        /// </summary>
        /// <param name="filename"></param>
        public void SaveClass(string filename)
        {
            string content = "UUID,Level,Room,Name\n";
            foreach (Class classInfo in Class)
            {
                content += classInfo.UUID + ",";
                content += classInfo.Level + ",";
                content += classInfo.Room + ",";
                content += classInfo.Name + "\n";
            }
            File.WriteAllText(filename, content);
        }

        /// <summary>
        /// Save list of subject to .csv file
        /// </summary>
        /// <param name="filename"></param>
        public void SaveSubject(string filename)
        {
            string content = "UUID,Level,Field\n";
            foreach (Subject subject in Subject)
            {
                content += subject.UUID + ",";
                content += subject.Level + ",";
                content += subject.Field + "\n";
            }
            File.WriteAllText(filename, content);
        }

        /// <summary>
        /// Save list of attendance to .csv file
        /// </summary>
        /// <param name="filename"></param>
        public void SaveAttendance(string filename)
        {
            string content = "UUID,Teacher,Class,Subject\n";
            foreach (Attendance attendance in Attendance)
            {
                content += attendance.UUID + ",";
                content += attendance.Teacher + ",";
                content += attendance.Class + ",";
                content += attendance.Subject + "\n";
            }
            File.WriteAllText(filename, content);
        }

        /// <summary>
        /// Save list of grade to .csv file
        /// </summary>
        /// <param name="filename"></param>
        public void SaveGrade(string filename)
        {
            string content = "UUID,Subject,Student,Point\n";
            foreach (Grade grade in Grade)
            {
                content += grade.UUID + ",";
                content += grade.Subject + ",";
                content += grade.Student + ",";
                content += grade.Point + "\n";
            }
            File.WriteAllText(filename, content);
        }

        /// <summary>
        /// Save school.json in database
        /// </summary>
        /// <param name="filename"></param>
        public void SaveSchool(string filename)
        {
            string content = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filename, content);
        }
    }
}
