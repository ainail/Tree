using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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
            if (args.Length == 0 || !Directory.Exists(args[0]))
            {
                HelpAd();
                return;
            }
            
            var path = args[0];
            var settings = Settings.GetSettingsFromConsoleInput(args);
            if (settings == null)
            {
                HelpAd();
                return;
            }
            var tree = Dir.GetAllDirectories(path, null, settings);
            Dir.PrintTreeOfDir(tree, settings);
        }
        static void HelpAd()
        {
            Console.WriteLine("To build the tree, enter the path to directory as first argument");
            Console.WriteLine("To show the weight of files in bytes enter the argument -s  or --size");
            Console.WriteLine("If you want to see file sizes in more readable format, use the argument -h or --human-readable");
            Console.WriteLine("To limit the depth use the argument -d or --depth and add value after it");
            Console.WriteLine("To order elements in tree, use the argument -o or -order with the following keywords:");
            Console.WriteLine("\"al\" to sort alphabetically, and \"ald\" for reverse sorting");
            Console.WriteLine("\"sz\" to sort by size, and \"szd\" for reverse sorting");
            Console.WriteLine("\"cr to sort by creation date, and \"crd\" for reverse sorting");
            Console.WriteLine("\"ed to sort by edit date, and \"edd\" for reverse sorting");
        }

        
    }
}