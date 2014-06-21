using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Mobile.Service;

namespace CounterWtf.Service.DataObjects
{
    /// <summary>
    /// Represents a project that has WTF counters against it.
    /// </summary>
    public class Project : EntityData
    {
        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description for the project.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the user that created the project i
        /// </summary>
        public string UserId { get; set; }
    }
}