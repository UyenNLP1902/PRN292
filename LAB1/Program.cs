using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LAB1
{
    /// <summary>
    /// The main Program class.
    /// Contains main methods.
    /// </summary>
    class Program
    {
        class m { public int x, y; }
        /// <summary>
        /// Performs functions.
        /// </summary>
        /// <param name="args">A string array contains command from command-line</param>
        public static void Main(string[] args)
        {
            m p1 = new m();
            p1.x = 100;
            p1.y = 100;
            m p2 = p1;
            p2.x = 900;
            Console.WriteLine("{0} {1}", p1.x, p2.x);
            Console.ReadLine();
            string info = CLIHelper.ShowCLI(args);
            if (info == "")
            {
                string schoolName = CLIHelper.SchoolName;
                int student = CLIHelper.NumberOfStudent;
                int room = CLIHelper.NumberOfRoom;
                School school = new School(schoolName);
                //string directoryPath = @"..\..\..\" + schoolName;
                string directoryPath = schoolName;
                Directory.CreateDirectory(directoryPath);

                if (student == 0 && room == 0)
                {
                    student = GenerateDataHelper.GenerateRandomStudentAmount();
                    room = GenerateDataHelper.GenerateRoomAmountByStudent(student);
                }
                else if (student == 0 && room > 0)
                {
                    student = GenerateDataHelper.GenerateStudentAmountByRoom(room);
                }
                else if (student > 0 && room == 0)
                {
                    room = GenerateDataHelper.GenerateRoomAmountByStudent(student);
                }

                DataList.CreateDataList(student, room);

                school.Level = DataList.LevelList.ToList();
                school.SaveLevel(directoryPath + "\\" + "Level.csv");

                school.Field = DataList.FieldList.ToList();
                school.SaveField(directoryPath + "\\" + "Field.csv");

                school.Room = DataList.RoomList.ToList();
                school.SaveRoom(directoryPath + "\\" + "Room.csv");

                school.Class = DataList.ClassList.ToList();
                school.SaveClass(directoryPath + "\\" + "Class.csv");

                school.Subject = DataList.SubjectList.ToList();
                school.SaveSubject(directoryPath + "\\" + "Subject.csv");

                school.Student = DataList.StudentList.ToList();
                school.SaveStudent(directoryPath + "\\" + "Student.csv");

                school.Teacher = DataList.TeacherList.ToList();
                school.SaveTeacher(directoryPath + "\\" + "Teacher.csv");

                school.Attendance = DataList.AttendanceList.ToList();
                school.SaveAttendance(directoryPath + "\\" + "Attendance.csv");

                school.Grade = DataList.GradeList.ToList();
                school.SaveGrade(directoryPath + "\\" + "Grade.csv");

                Console.WriteLine("Succesful: You have a new school database with " + student + " students and " + room + " rooms");

                school.SaveSchool(directoryPath + "\\" + schoolName + ".json");
            }
            else
            {
                Console.WriteLine(info);
            }
        }
    }
}
