using System.Collections.Generic;

namespace TOGE
{
    public class Location
    {
        public Location()
        {
            Items = new Items();
        }

        /// <summary>
        /// The 'Name' should finish the sentence "You are ..."
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description displayed when looking from an adjacent location.
        /// Should finish the sentence "To the (NSEW direction) you see ..."
        /// </summary>
        public string DistanceDescription { get; set; }

        /// <summary>
        /// The description printed when a user types LOOK.
        /// </summary>
        public string LookDescription { get; set; }

        public Location North { get; set; }
        public Location South { get; set; }
        public Location East { get; set; }
        public Location West { get; set; }

        public string NorthIsBlockedText { get; set; }
        public string SouthIsBlockedText { get; set; }
        public string EastIsBlockedText { get; set; }
        public string WestIsBlockedText { get; set; }

        public Items Items { get; set; }
    }
}
