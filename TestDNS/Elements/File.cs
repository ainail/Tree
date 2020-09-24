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
        /// <param name="parent">file's parent directory</param>
        /// <param name="time">time of creation or last edit</param>
        /// <param name="size">size of file in bytes</param>
        public File(string name, Dir parent, DateTime? time, long? size)
        : base(name, parent, time) { Size = size; }
    }
}
