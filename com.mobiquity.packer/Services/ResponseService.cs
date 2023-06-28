using com.mobiquity.packer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{
    public class ResponseService : IResponseService
    {
        public string PrintResponse(List<Package> packages)
        {
            var result = "";

            foreach (var package in packages)
            {
                if (package.PackageItems.Length > 0)
                {
                    // Concatenate the indices of package items separated by commas
                    result += string.Join(",", package.PackageItems.Select(x => x.Index.ToString()));
                }
                else
                {
                    // If no package items, represent it with a hyphen
                    result += "-";
                }

                // Add a new line after each package
                result += Environment.NewLine;
            }

            return result;
        }
    }
}