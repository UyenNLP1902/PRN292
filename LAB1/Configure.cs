using System;
using System.Collections.Generic;
using System.Text;

namespace LAB1
{
    /// <summary>
    /// The Configure class
    /// Create properties for other config array in Configure.json file
    /// </summary>
    public class Configure
    {
        /// <summary>
        /// A property for NameConfig class
        /// </summary>
        public NameConfig NameConfig
        { get; set; }

        /// <summary>
        /// A property for LevelNameConfig class
        /// </summary>
        public LevelNameConfig LevelNameConfig
        { get; set; }

        /// <summary>
        /// A property for FieldNameConfig class
        /// </summary>
        public FieldNameConfig FieldNameConfig
        { get; set; }
    }

    /// <summary>
    /// A NameConfig class
    /// Creates properties for NameConfig array in Configure.json file
    /// </summary>
    public class NameConfig
    {
        /// <summary>
        /// Properties of last_name_set in NameConfig
        /// </summary>
        public string[] last_name_set { get; set; }

        /// <summary>
        /// Properties of middle_name_set in NameConfig
        /// </summary>
        public string[] middle_name_set { get; set; }

        /// <summary>
        /// Properties of fem_first_name_set in NameConfig
        /// </summary>
        public string[] fem_first_name_set { get; set; }

        /// <summary>
        /// Properties of male_first_name_set in NameConfig
        /// </summary>
        public string[] male_first_name_set { get; set; }
    }

    /// <summary>
    /// A LevelNameConfig class
    /// Creates properties for LevelNameConfig array in Configure.json file
    /// </summary>
    public class LevelNameConfig
    {
        /// <summary>
        /// Properties of level_name_set in LevelNameConfig
        /// </summary>
        public string[] level_name_set { get; set; }
    }

    /// <summary>
    /// A FieldNameConfig class
    /// Creates properties for FieldNameConfig array in Configure.json file
    /// </summary>
    public class FieldNameConfig
    {
        /// <summary>
        /// Properties of field_name_set in FieldNameConfig
        /// </summary>
        public string[] field_name_set { get; set; }
    }

}
