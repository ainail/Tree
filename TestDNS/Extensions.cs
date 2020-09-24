using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDNS
{
    public static class Extensions
    {
        public static string BytesToString(this long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return Math.Sign(byteCount) * num + suf[place];
        }

        public static Dir SortDirElements(this Dir dir, SortType sortType)
        {
            if (sortType == SortType.Alphabetical)
            {
                dir.SubDirs = dir.SubDirs.OrderBy(_ => _.Name).ToList();
                dir.Files = dir.Files.OrderBy(_ => _.Name).ToList();
            }
            else if (sortType == SortType.AlphabeticalDesc)
            {
                dir.SubDirs = dir.SubDirs.OrderByDescending(_ => _.Name).ToList();
                dir.Files = dir.Files.OrderByDescending(_ => _.Name).ToList();
            }
            else if (sortType == SortType.ByCreationDate || sortType == SortType.ByEditDate)
            {
                dir.SubDirs = dir.SubDirs.OrderBy(_ => _.Time).ToList();
                dir.Files = dir.Files.OrderBy(_ => _.Time).ToList();
            }
            else if (sortType == SortType.ByCreationDateDesc || sortType == SortType.ByEditDateDesc)
            {
                dir.SubDirs = dir.SubDirs.OrderByDescending(_ => _.Time).ToList();
                dir.Files = dir.Files.OrderByDescending(_ => _.Time).ToList();
            }
            else if (sortType == SortType.BySize)
            {
                dir.Files = dir.Files.OrderBy(_ => _.Size.Value).ToList();
            }
            else if (sortType == SortType.BySizeDesc)
            {
                dir.Files = dir.Files.OrderByDescending(_ => _.Size.Value).ToList();
            }
            return dir;
        }
    }
}
