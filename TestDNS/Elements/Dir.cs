using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDNS
{
    public class Dir : Element
    {
        /// <param name="path">Path to directory including it's own name</param>
        /// <param name="parent">Parent directory of current directory</param>
        /// <param name="time">Time of creation or last edit</param>
        public Dir(string path, Dir parent, DateTime? time)
            : base(path, parent, time)
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

        /// <summary>
        /// Method that recursive gets all directories
        /// </summary>
        /// <param name="path">path to dir</param>
        /// <param name="parent">parent dir, null if it's root</param>
        /// <param name="settings">settings such as sorting, size counting and so on</param>
        /// <returns></returns>
        public static Dir GetAllDirectories(string path, Dir parent = null, Settings settings = null)
        {
            DateTime? time = null;

            if (settings.NeedsCreationTime)
                time = Directory.GetCreationTime(path);
            else if (settings.NeedsEditTime)
                time = Directory.GetLastWriteTime(path);
            var dir = new Dir(path, parent, time);
            if (dir.Depth != settings?.Depth)
            {
                var subDirs = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                foreach (var subDir in subDirs)
                {
                    dir.SubDirs.Add(GetAllDirectories(subDir, dir, settings));
                }
                foreach (var file in files)
                {
                    DateTime? fileTime = null;
                    if (settings.NeedsCreationTime)
                        fileTime = System.IO.File.GetCreationTime(file);
                    else if (settings.NeedsEditTime)
                        fileTime = System.IO.File.GetLastWriteTime(file);
                    dir.Files.Add(new File(file, dir, fileTime,
                        settings != null && settings.CountSize ? new FileInfo(file)?.Length : null));
                }

            }
            dir = dir.SortDirElements(settings.SortType);
            return dir;

        }

        public static void PrintTreeOfDir(Dir root, Settings settings)
        {
            PrintWithTabs(root, settings);

            foreach (var dir in root.SubDirs)
            {
                PrintTreeOfDir(dir, settings);
            }

            foreach (var file in root.Files)
            {
                PrintWithTabs(file, settings);
            }

        }

        private static void PrintWithTabs(Element element, Settings settings)
        {
            String elementName;
            if (element is File file && settings.CountSize && settings.ShowSize)
            {
                elementName = settings.ShowReadableSize ? $"{file.Name} ({file.ReadableSize})" :
                    $"{file.Name} {file.Size}";
            }
            else
            {
                elementName = element.Name;
            }

            Console.WriteLine($"{string.Empty.PadLeft(element.Depth, '\t')}{elementName}");
        }
    }
}
