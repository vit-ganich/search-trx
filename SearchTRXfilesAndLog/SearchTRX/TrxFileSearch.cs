using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SearchTRX
{
    class TrxFileSearch
    {
        /// <summary>
        /// Method returns a list of TRX-files from the specified folder (and subfolders).
        /// </summary>
        /// <param name="testResultsDir">Path to the folder with TRX-files</param>
        /// <returns>list of the TRX-files</returns>
        public static IEnumerable TrxSearch(string testResultsDir)
        {
            var initialDirState = new List<string>();

            try
            {
                initialDirState.AddRange(Directory.GetFiles(
                    testResultsDir, "*.trx", SearchOption.AllDirectories));
            }
            catch (Exception) { throw; }

            return initialDirState;
        }

        /// <summary>
        /// Method a full path to the files for search folder from the user input.
        /// </summary>
        /// <returns>String path to folder</returns>
        public static string GetPathToFilesFolder()
        {
            string filesDir = null;

            try
            {
                Console.WriteLine("Enter the full path to the TRX-files folder (example: C:\\TestResults)...\n");
                Console.Write("TRX-files folder: ");

                filesDir = Console.ReadLine();

                if (!Directory.Exists(filesDir))
                {
                    throw new DirectoryNotFoundException();
                }
            }
            catch (Exception) { throw; }

            return filesDir;
        }

        /// <summary>
        /// Method creates a default folder for log file with search results.
        /// </summary>
        /// <returns>String path to folder with log file</returns>
        public static string CreateLogFolder()
        {
            string pathToLogFolder = "C:\\SearchTRXdefaultFolder";

            try
            {
                if (!Directory.Exists(pathToLogFolder))
                {
                    var myDir = Directory.CreateDirectory(pathToLogFolder);
                    Console.WriteLine(String.Format("\nLog file folder: {0} was succesfully created...\n", pathToLogFolder));
                }
            }
            catch (Exception) { throw; }

            return pathToLogFolder;
            
        }

        /// <summary>
        /// Method gets the folder with files to search, than provides a search for TRX-fles there.
        /// Finally writes the search results to the log file
        /// </summary>
        public static void CreateAndWriteLogFile()
        {
            try
            {
                var filesForSearchDir = GetPathToFilesFolder();
                var searchResults = TrxSearch(filesForSearchDir);
                var pathToLogFolder = CreateLogFolder();
                string datetime = DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss");
                string logFilePath = String.Format("{0}\\{1}-CI-log.txt", pathToLogFolder, datetime);

                if (!File.Exists(logFilePath))
                {
                    var myFile = File.Create(logFilePath);
                    myFile.Close();
                }

                using (StreamWriter outputFile = new StreamWriter(logFilePath))
                {
                    foreach (string file in searchResults)
                        outputFile.WriteLine(file);
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Merely provides the text information about this "program"
        /// </summary>
        public static void TextInformation()
        {
            Console.WriteLine("This simple program providing a search for the *.trx files in a specified directory.");
            Console.WriteLine("Resulting txt-file location: C:\\SearchTRXdefaultFolder");
            Console.WriteLine("------------------------------------------------------------------------------------\n");
        }
    }
}