using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LAB2
{
    /// <summary>
    /// The Configure class.
    /// Create properties for other config array in json file
    /// </summary>
    public class Configure
    {
        /// <summary>
        /// An empty constructor for Configure
        /// </summary>
        public Configure() { }

        /// <summary>
        /// An array of Student
        /// </summary>
        public Student[] Student { get; set; }

        /// <summary>
        /// An array of Teacher
        /// </summary>
        public Teacher[] Teacher { get; set; }

        /// <summary>
        /// An array of Level
        /// </summary>
        public Level[] Level { get; set; }

        /// <summary>
        /// An array of Field
        /// </summary>
        public Field[] Field { get; set; }

        /// <summary>
        /// An array of Room
        /// </summary>
        public Room[] Room { get; set; }

        /// <summary>
        /// An array of Class
        /// </summary>
        public Class[] Class { get; set; }

        /// <summary>
        /// An array of Subject
        /// </summary>
        public Subject[] Subject { get; set; }

        /// <summary>
        /// An array of Attendance
        /// </summary>
        public Attendance[] Attendance { get; set; }

        /// <summary>
        /// An array of GradeClass
        /// </summary>
        public Grade[] Grade { get; set; }
    }
}