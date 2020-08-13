using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LAB1
{
    /// <summary>
    /// The ClassInfo class.
    /// Contains create method and properties of ClassInfo. 
    /// </summary>
    public class Class
    {

        /// <value>
        /// The id of class
        /// </value>
        public string UUID { get; set; }

        /// <value>
        /// The level of class
        /// </value>
        public string Level { get; set; }

        /// <value>
        /// The room of class
        /// </value>
        public string Room { get; set; }

        /// <value>
        /// The name of class
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// An empty constructor for class
        /// </summary>
        public Class() { }

        /// <summary>
        /// A constructor for class
        /// </summary>
        /// <param name="uuid">A string value</param>
        /// <param name="level">A string value</param>
        /// <param name="room">A string value</param>
        /// <param name="name">A string value</param>
        public Class(string uuid, string level, string room, string name)
        {
            UUID = uuid;
            Level = level;
            Room = room;
            Name = name;
        }

        /// <summary>
        /// Creates a random list of classes and returns the result
        /// </summary>
        /// <param name="list">A dictionary value</param>
        /// <returns>An array of classes</returns>
        public static Class[] Create(Dictionary<string, string> list)
        {
            List<Class> result = new List<Class>();
            Level[] listOfLevel = DataList.LevelList;
            int amountEachLevel = (int)Math.Ceiling((double)list.Count / listOfLevel.Length);
            int count = 0;
            int levelIndex = 0;
            for (uint i = 0; i < list.Count; i++)
            {
                count++;
                //id
                string uuid = list.ElementAt((int)i).Value;

                //level
                string levelId = listOfLevel[levelIndex].UUID;

                //room
                string roomId = list.ElementAt((int)i).Key;

                //name
                string levelName = listOfLevel[levelIndex].Name;
                string name = levelName + "A" + count;

                result.Add(new Class(uuid, levelId, roomId, name));

                if (count == amountEachLevel)
                {
                    levelIndex++;
                    count = 0;
                }
            }

            return result.ToArray();
        }
    }
}