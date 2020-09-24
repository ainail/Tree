using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDNS
{
    public class Settings
    {
        /// <summary>
        /// Indicates how deep the search will be
        /// </summary>
        public int? Depth { get; set; }

        /// <summary>
        /// Serves to determine whether the size counting will be done or not
        /// </summary>
        public bool CountSize => ShowSize || ShowReadableSize ||
                SortType == SortType.BySize || SortType == SortType.ByEditDateDesc;

        /// <summary>
        /// Show size or not
        /// </summary>
        public bool ShowSize { get; set; }

        /// <summary>
        /// Show size in human readable format
        /// </summary>
        public bool ShowReadableSize { get; set; }

        /// <summary>
        /// Serves to determine wherer the time of creation will be needed or not
        /// </summary>
        public bool NeedsCreationTime => SortType == SortType.ByCreationDate
            || SortType == SortType.ByCreationDateDesc;

        /// <summary>
        /// Serves to determine wherer the time of last edit will be needed or not
        /// </summary>
        public bool NeedsEditTime => SortType == SortType.ByEditDate ||
               SortType == SortType.ByEditDateDesc;

        /// <summary>
        /// Stores sort type
        /// </summary>
        public SortType SortType { get; set; }
        public static Settings GetSettingsFromConsoleInput(string[] args)
        {
            var help = args.Intersect(new string[] { "--help", "-?" }).Any();
            if (help)
                return null;
            var settings = new Settings();
            var depthIndex = Array.IndexOf(args, args.FirstOrDefault(_ => _ == "-d" || _ == "--depth"));
            var sizeIndex = Array.IndexOf(args, args.FirstOrDefault(_ => _ == "-s" || _ == "--size"));
            var humanIndex = Array.IndexOf(args, args.FirstOrDefault(_ => _ == "-h" || _ == "--human-readable"));

            var orderIndex = Array.IndexOf(args, args.FirstOrDefault(_ => _ == "-o" || _ == "--order"));
            if (depthIndex != -1 && args.Length > depthIndex + 1 && int.TryParse(args[depthIndex + 1], out var dIndex))
            {
                settings.Depth = dIndex;
            }
            if (sizeIndex != -1)
            {
                settings.ShowSize = true;
            }
            if (humanIndex != -1)
            {
                settings.ShowSize = true;
                settings.ShowReadableSize = true;
            }
            if (orderIndex != -1 && args.Length > orderIndex + 1 && int.TryParse(args[depthIndex + 1], out var oIndex))
            {
                switch (args[oIndex])
                {
                    case "al":
                        settings.SortType = SortType.Alphabetical;
                        break;
                    case "ald":
                        settings.SortType = SortType.AlphabeticalDesc;
                        break;
                    case "sz":
                        settings.SortType = SortType.BySize;
                        break;
                    case "szd":
                        settings.SortType = SortType.BySizeDesc;
                        break;
                    case "cr":
                        settings.SortType = SortType.ByCreationDate;
                        break;
                    case "crd":
                        settings.SortType = SortType.ByCreationDateDesc;
                        break;
                    case "ed":
                        settings.SortType = SortType.ByEditDate;
                        break;
                    case "edd":
                        settings.SortType = SortType.ByEditDateDesc;
                        break;
                    default:
                        settings.SortType = SortType.None;
                        break;
                }

            }
            return settings;
        }
    }
}
