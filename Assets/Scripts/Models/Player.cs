using System.Collections.Generic;
using ZeroFormatter;

namespace Models
{
    [ZeroFormattable]
    public class Player
    {
        // Index is key of serialization
        [Index(0)]
        public virtual int Age { get; set; }

        [Index(1)]
        public virtual string FirstName { get; set; }

        [Index(2)]
        public virtual string LastName { get; set; }

        // When mark IgnoreFormatAttribute, out of the serialization target
        [IgnoreFormat]
        public string FullName
        {
            get { return FirstName + LastName; }
        }

        [Index(3)]
        public virtual IList<int> List { get; set; }
    }
}