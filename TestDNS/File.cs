using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDNS
{
    public class File : Element
    {
        /// <summary>
        /// File's size (enabled by argument -s)
        /// </summary>
        public long? Size { get; }

        /// <summary>
        /// File's size in conveniently presented format (enabled by argument -h)
        /// </summary>
        public string ReadableSize => Size?.BytesToString();

        /// <param name="name">Filename including its path</param>
        /// <param name="parent"></param>
        /// <param name="time"></param>
        /// <param name="size"></param>
        public File(string name, Dir parent, DateTime? time, long? size)
        : base(name, parent, time) { Size = size; }
    }
}
