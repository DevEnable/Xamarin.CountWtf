using Microsoft.WindowsAzure.Mobile.Service;

namespace CounterWtf.Service.DataObjects
{
    /// <summary>
    /// Represents a single WTF.
    /// </summary>
    /// <remarks>
    /// Could be extended to include more information such as camera shots capturing the WTF.
    /// Just using the <see cref="EntityData"/> fields to track the date & time of the WTF.
    /// </remarks>
    public class Wtf : EntityData
    {
        /// <summary>
        /// Gets or sets the project that the Wtf occurred in.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the user that registered the WTF.
        /// </summary>
        public string UserId { get; set; }
    }
}