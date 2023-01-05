using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class FindTransactionsFileNames
    {
        public static List<string> Execute()
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(desktop,"Transactions");
            var directory = new DirectoryInfo(path);
            List<string> names =  directory.GetFiles()
                                            .ToList()
                                            .Select(c => c.Name)
                                            .ToList();
            return names;
        }
    }
}