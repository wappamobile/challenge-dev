using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Wappa.Middleware.Miscellaneous
{
    public static class FolderFinder
    {
        public static string FetchRoot()
        {
            //return "D:\\home\\site\\wwwroot\\";

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (path == null)
            {
                throw new Exception("Wappa.Middleware.Domain not found");
            }

            var dirInfo = new DirectoryInfo(path);

            var teste = dirInfo;

            while (!Contains(dirInfo.FullName, "Wappa.Middleware.sln"))
            {
                if (dirInfo.Parent == null)
                {
                    throw new Exception("Root not found :" + teste.FullName);
                }

                dirInfo = dirInfo.Parent;
            }

            var hostPath = Path.Combine(dirInfo.FullName, "src\\Wappa.Middleware.Host");

            if (Directory.Exists(hostPath))
            {
                return hostPath;
            }

            throw new Exception("Root not found.");
        }

        private static bool Contains(string dirname, string filename)
        {
            return Directory.GetFiles(dirname).Any(filePath => string.Equals(Path.GetFileName(filePath), filename));
        }
    }
}
