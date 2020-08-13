using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LAB3
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

        public string ServerName { get; set; }

        public string UserID { get; set; }

        public string Password { get; set; }

        public string Database { get; set; }

    }
}