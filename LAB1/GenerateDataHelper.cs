using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace LAB1
{
    /// <summary>
    /// The GenerateDataHelper class.
    /// Contains all methods which helps generate random data
    /// </summary>
    class GenerateDataHelper
    {
        /// <summary>
        /// Link to configure file
        /// </summary>
        private static readonly string CONFIG_FILE = @"Configure.json";

        /// <summary>
        /// Random object to random value later
        /// </summary>
        private static Random rnd = new Random();

        /// <summary>
        /// String of text read from Configure.json file
        /// </summary>
        private static string content = File.ReadAllText(CONFIG_FILE);

        /// <summary>
        /// Deserialize content to different objects
        /// </summary>
        private static Configure config = JsonSerializer.Deserialize<Configure>(content);

        /// <summary>
        /// Get a random date in a specific year
        /// </summary>
        /// <param name="year">An integer value</param>
        /// <returns>A random date in a specific year</returns>
        private static DateTime GetRandomDateInYear(int year)
        {
            DateTime start = new DateTime(year, 1, 1);
            DateTime end = new DateTime(year, 12, 31);
            int range = (end - start).Days;
            return start.AddDays(rnd.Next(range));
        }

        /// <summary>
        /// Get a random birthdate by student's level
        /// </summary>
        /// <param name="level">A string value</param>
        /// <returns>A random DateTime value</returns>
        /// <exception cref="System.FormatException">
        /// Thrown when the string is not numeric
        /// </exception>
        public static DateTime GetRandomBirthdayByLevel(string level)
        {
            try
            {
                int levelNumber = Int32.Parse(level);
                int currentYear = DateTime.Now.Year;
                int minAge = 15 + (levelNumber - 10) +1;
                int maxAge = 19 + (levelNumber - 10);
                int maxYear = currentYear - minAge;
                int minYear = currentYear - maxAge;

                int year = rnd.Next(minYear, maxYear + 1);
                return GetRandomDateInYear(year);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("GenerateDataHelper _ FormatException: " + ex.Message);
            }
            return new DateTime(1900, 1, 1);
        }

        /// <summary>
        /// Get a random name by gender
        /// </summary>
        /// <param name="gender">A boolean value</param>
        /// <returns>A random string of name from config file</returns>
        /// <exception cref="System.IO.FileNotFoundException">
        /// Thrown when the Json file is not exist.
        /// </exception>
        /// <exception cref="System.NullReferenceException">
        /// Thrown when the object in Json file is not exist or has no data.
        /// </exception>
        public static string GetRandomNameByGender(bool gender)
        {
            string fullName = null;
            try
            {
                NameConfig _ = config.NameConfig;
                int lastNameIndex = rnd.Next(_.last_name_set.Length);
                int firstNameIndex;
                string firstname;
                if (gender == true)
                {
                    firstNameIndex = rnd.Next(_.male_first_name_set.Length);
                    firstname = _.male_first_name_set[firstNameIndex];
                }
                else
                {
                    firstNameIndex = rnd.Next(_.fem_first_name_set.Length);
                    firstname = _.fem_first_name_set[firstNameIndex];
                }

                int middleNameIndex = rnd.Next(_.middle_name_set.Length);

                fullName = _.last_name_set[lastNameIndex] + " ";
                fullName += _.middle_name_set[middleNameIndex] + " ";
                fullName += firstname;

                fullName = fullName.Trim();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("GenerateDataHelper _ FileNotFoundException: " + ex.Message);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("GenerateDataHelper _ NullReferenceException: " + ex.Message);
            }
            return fullName;
        }

        /// <summary>
        /// Check if the number of student is in range
        /// </summary>
        /// <param name="number">A positive integer value</param>
        /// <returns>The number of student if in range, -1 if out of range</returns>
        public static int CheckStudentAmount(int number)
        {
            if (number >= 500 & number <= 3000)
            {
                return number;
            }
            return -1;
        }

        /// <summary>
        /// Check if the number of room is in range
        /// </summary>
        /// <param name="number">A positive integer value</param>
        /// <returns>The number of room if in range, -1 if out of range</returns>
        public static int CheckRoomAmount(int number)
        {
            if (number <= 100 & number >= 3)
            {
                return number;
            }
            return -1;
        }

        /// <summary>
        /// Check if each room has 30-50 students
        /// </summary>
        /// <param name="student">A positive integer value</param>
        /// <param name="room">A positive integer value</param>
        /// <returns>True if each room has 30-50 students, false if not</returns>
        public static bool CheckValidNumber(int student, int room)
        {
            double tmp = (double)student / (double)room;
            if (tmp >= 30 && tmp <= 50)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get number of room based on the number of student
        /// </summary>
        /// <param name="numberOfStudent">A positive integer value</param>
        /// <returns>The random number of room</returns>
        public static int GenerateRoomAmountByStudent(int numberOfStudent)
        {
            int maxRoom = (int)Math.Ceiling((double) numberOfStudent / 30);
            int minRoom = (int)Math.Ceiling((double) numberOfStudent / 50);
            return rnd.Next(minRoom, maxRoom + 1);
        }

        /// <summary>
        /// Get number of student based on the number of room
        /// </summary>
        /// <param name="numberOfRoom">A positive integer value</param>
        /// <returns>A random number of student</returns>
        public static int GenerateStudentAmountByRoom(int numberOfRoom)
        {
            int maxStudent = (int)Math.Ceiling((double)numberOfRoom * 50);
            int minStudent = (int)Math.Ceiling((double)numberOfRoom * 30);
            return rnd.Next(minStudent, maxStudent + 1);
        }

        /// <summary>
        /// Get random number of student in range 500 to 3000
        /// </summary>
        /// <returns>A random number of student</returns>
        public static int GenerateRandomStudentAmount()
        {
            return rnd.Next(500, 3000 + 1);
        }
    }
}