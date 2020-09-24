using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDNS
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            args = new string[] { "C:\\Users\\ainai\\Desktop\\доки" };
#endif
            var path = args[0];
            var dir = GetAllDirectories(path, null);
            //var firstDir = new Dir(path, null);
        }
        public static Dir GetAllDirectories(string path = null, Dir parent = null)
        {
            var dir = new Dir(path, parent);
            var subDirs = Directory.GetDirectories(path);
            foreach (var subDir in subDirs)
                dir.SubDirs.Add(GetAllDirectories(subDir, dir));
            var files = Directory.GetFiles(path);
            foreach (var file in files)
                dir.Files.Add(new File(file, dir, null));
            return dir;

        }

        public static void PrintTreeOfDir(Dir root)
        {
            foreach(var dir in root.SubDirs)
            {
                PrintTreeOfDir(root);
            }
        }
    }
    public class Dir : Element
    {

        /// <param name="name">Path to directory including it's own name</param>
        /// <param name="parent">Parent directory of current directory</param>
        public Dir(string path, Dir parent) : base(path, parent) 
        {
            SubDirs = new List<Dir>();
            Files = new List<File>();
        }
        /// <summary>
        /// Directories contained in current directory
        /// </summary>
        public List<Dir> SubDirs { get; set; }
        /// <summary>
        /// Files contained in current directory
        /// </summary>
        public List<File> Files { get; set; }

    }

    public class File : Element
    {
        /// <summary>
        /// File's size (enabled by argument -s)
        /// </summary>
        public int? Size { get; }

        /// <summary>
        /// File's size in conveniently presented format
        /// </summary>
        public string ReadableSize { get; }

        /// <param name="name">Filename including its path</param>
        /// <param name="parent"></param>
        /// <param name="size"></param>
        public File(string name, Dir parent, int? size) : base(name, parent) { Size = size; }
    }
    public abstract class Element
    {
        /// <summary>
        /// Path to element including it's own name
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Element's name
        /// </summary>
        //хитрый прием, если элемента не будет, то возьмется сабстринг с 0 элемента
        //что является идеальным решением при его отсутствии
        public string Name => System.IO.Path.GetFileName(Path);

        /// <summary>
        /// Element's parent directory
        /// </summary>
        public Dir Parent { get; }

        /// <param name="path">Path to element including it's own name</param>
        /// <param name="parent">Element's parent directory</param>
        public Element(string path, Dir parent)
        {
            Path = path;
            Parent = parent;
        }

    }
}
