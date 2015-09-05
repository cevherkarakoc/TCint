using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCint
{
    class Program
    {
        

        static int Main(string[] args)
        {
            string source,target;
            if (args.Length == 0)
            {
                return 1;
            }
            else if (args.Length == 1)
            {
                source = args[0];
                target = args[0] + "(COPY)";
            }
            else if (args.Length == 2)
            {
                source = args[0];
                target = args[1];
            }
            else
                return 2;

            copyDir(source, target);

            return 0;
            
        }

        static void copyDir(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(file);
                if (fileName.Substring(fileName.Length - 3)=="png"){
                    File.Copy(file, Path.Combine(targetDir, fileName));
                }                
            }
                
        }
    }

}
