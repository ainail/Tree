using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDNS
{
    public enum SortType
    {
        BySize,
        ByCreationDate,
        ByEditDate,
        Alphabetical,
        BySizeDesc,
        ByCreationDateDesc,
        ByEditDateDesc,
        AlphabeticalDesc,
        None
    }
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            args = new string[] { "C:\\Users\\ainai\\Desktop\\доки" };
            //args = new string[] { "C:\\Users\\it015\\Desktop\\Cashier" };
#endif
            var path = args[0];

            var settings = new Settings(null, true, true, SortType.ByEditDate);
            var tree = Dir.GetAllDirectories(path, null, settings);
            Dir.PrintTreeOfDir(tree, settings);
            //var firstDir = new Dir(path, null);
        }
        

       
    }  
}