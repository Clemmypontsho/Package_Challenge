using com.mobiquity.packer;
using System;

namespace com.mobiquity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Prompt the user to enter the absolute path to the file
            Console.WriteLine("Type the path to the input file:");
            string path = Console.ReadLine();

            try
            {
                // Invoke the Pack method from the Packer class library to process the file and get the results
                string response = Packer.Pack(path);

                // Display the results
                Console.WriteLine("Results:");
                Console.WriteLine(response);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                // Display any exception message that occurs during the processing
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}