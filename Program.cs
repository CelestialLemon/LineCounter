using System;
using System.IO;
using System.Linq;

namespace LineCounter
{
    class Program
    {
        static int someVar = 0;
        static void Main(string[] args)
        {
            string path;
            Console.Write("Enter the path of folder : ");
            path = Console.ReadLine();

            int lengthOfRootPath = path.Length;

            Console.Write("Number of folders to ignore : ");
            int numOfIgnoredFolders;
            numOfIgnoredFolders = int.Parse(Console.ReadLine());

            string[] ignoredFolderPaths = new string[numOfIgnoredFolders];
            for(int i=0; i<numOfIgnoredFolders; i++)
            {
                ignoredFolderPaths[i] = Console.ReadLine();
            }

            Console.WriteLine("How many types of files you want to count?");
            int numOfTypesOfFiles;
            numOfTypesOfFiles = int.Parse(Console.ReadLine());
             
            string[] fileTypes = new string[numOfTypesOfFiles];
            for(int i=0; i<numOfTypesOfFiles; i++)
            {
                fileTypes[i] = "*" + Console.ReadLine();
            }

            
            
            GetTotalNumOfLinesFromFolder(path, ignoredFolderPaths, lengthOfRootPath, fileTypes);
            Console.WriteLine("Total num of lines in the project are {0}", someVar);
        }

        

        static int GetTotalNumOfLinesFromFolder(string path, string[] ignoredFolderPaths, int lengthOfRootPath, string[] fileTypes)
        {

            for(int i=0; i<ignoredFolderPaths.Length; i++)
            {
                if(path == ignoredFolderPaths[i])
                {
                    return 0;
                }
            }

            Console.Write("\n\n\n");
            int totalNumOfLines = 0;
            string[] files = GetFileNames(path, fileTypes);
            for(int i=0; i<files.Length; i++)
            {
                int numOfLines = NumOfLinesInFile(files[i]);
                Console.WriteLine("Number of lines for file {0} is {1}", files[i].Substring(lengthOfRootPath), numOfLines);
                totalNumOfLines += numOfLines;
            }

            Console.WriteLine("Num of lines in folder {0} is {1}", path.Substring(lengthOfRootPath), totalNumOfLines);

            string[] subFolders = GetFolderNames(path);
            for(int i=0; i<subFolders.Length; i++)
            {
                GetTotalNumOfLinesFromFolder(subFolders[i], ignoredFolderPaths, lengthOfRootPath, fileTypes);
            }
            someVar += totalNumOfLines;

            return totalNumOfLines;        
        }

        static string[] GetFileNames(string path, string[] fileTypes)
        {
            
            string[] filePaths = fileTypes.SelectMany(f => Directory
            .GetFiles(path, f))
            .ToArray();

            return filePaths;
            
        }

        static string[] GetFolderNames(string path)
        {
            string[] folderNames = Directory.GetDirectories(path);
            return folderNames;
        }
        static int NumOfLinesInFile (string path)
        {
            int numOfLines = 0;
            try
            {
                StreamReader sr = new StreamReader(path);
                string line;
                while(!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    numOfLines++;
                }

                
            }
            catch(Exception e)
            {
                Console.WriteLine("Error " + e.Message);
            }

            return numOfLines;
        }
    }
}

