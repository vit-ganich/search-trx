using System;

namespace SearchTRX
{
    class Program
    {
        static void Main(string[] args)
        {
            TrxFileSearch.TextInformation();

            while (true)
            {
                try
                {
                    TrxFileSearch.CreateAndWriteLogFile();
                    Console.Write("\nResulting txt-file was succesfully created. ");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\nPlease, try again...\n");
                }
            }
        }
    }
}
