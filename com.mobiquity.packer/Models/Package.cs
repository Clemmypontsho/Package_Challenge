using System;

namespace com.mobiquity.packer.Models
{
   // Class to represent the package
    
    public class Package
    {
        public int WeightLimit { get; set; }
        public PackageItem[] PackageItems { get; set; }
    }
}
