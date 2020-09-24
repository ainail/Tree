using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDNS
{
    public class Settings
    {
        public int? Depth { get; }
        public bool CountSize { get; }
        public bool ShowSize { get; }
        public bool ShowReadableSize { get; }
        public bool NeedsCreationTime => SortType == SortType.ByCreationDate
            || SortType == SortType.ByCreationDateDesc;
        public bool NeedsEditTime => SortType == SortType.ByEditDate ||
               SortType == SortType.ByEditDateDesc;
        public SortType SortType { get; }

        public Settings(int? depth, bool showSize, bool showReadableSize, SortType sortType)
        {
            Depth = depth;

            ShowSize = showSize;
            ShowReadableSize = showReadableSize;
            SortType = sortType;
            CountSize = showSize || showReadableSize ||
                sortType == SortType.BySize || sortType == SortType.ByEditDateDesc;
        }
    }
}
