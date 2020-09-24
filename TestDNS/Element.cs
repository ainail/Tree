using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDNS
{
    public abstract class Element
    {
        /// <summary>
        /// Path to element including it's own name
        /// </summary>
        public string Path { get; }

        public int Depth => Parent?.Depth + 1 ?? 0;

        /// <summary>
        /// Element's name
        /// </summary>
        public string Name => System.IO.Path.GetFileName(Path);

        /// <summary>
        /// Element's parent directory
        /// </summary>
        public Dir Parent { get; }
        public DateTime? Time { get; }

        /// <param name="path">Path to element including it's own name</param>
        /// <param name="parent">Element's parent directory</param>
        /// <param name="time"></param>
        protected Element(string path, Dir parent, DateTime? time)
        {
            Path = path;
            Parent = parent;
            Time = time;
        }

    }
}
