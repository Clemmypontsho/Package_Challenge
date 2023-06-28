using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services;
using System.Collections.Generic;

namespace com.mobiquity.packer
{// Provides a method to optimize packaging and return the output.

    public static class Packer
    {
        private static readonly IFileService fileService = new FileService();
        private static readonly IPackageService packageService = new PackageService();
        private static readonly IWrappingService wrappingService = new WrappingService();
        private static readonly IResponseService responseService = new ResponseService();

        
        // Optimizes the packaging based on the input file and returns the optimized package list as a string.
        
        // <param name="filePath">The path of the input file.</param>
        // <returns>A string representation of the optimized package list.</returns>
        // <exception cref="APIException">Thrown when the input file does not exist.</exception>
        public static string Pack(string filePath)
        {
            // Check if the input file exists
            if (!fileService.IsExist(filePath))
            {
                throw new APIException($"Error: The file at {filePath} does not exist!");
            }

            // Read all lines from the input file
            string[] allLines = fileService.ReadFile(filePath);

            // Create a list of packages from the input lines
            List<Package> packageList = packageService.CreatePackageList(allLines);

            // Optimize the package list
            List<Package> optimizedPackageList = wrappingService.Wrap(packageList);

            // Generate the string representation of the optimized package list
            return responseService.PrintResponse(optimizedPackageList);
        }
    }
}